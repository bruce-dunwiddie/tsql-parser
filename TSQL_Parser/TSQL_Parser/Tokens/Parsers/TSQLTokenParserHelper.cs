using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Elements;
using TSQL.Expressions;
using TSQL.Expressions.Parsers;
using TSQL.Statements;

namespace TSQL.Tokens.Parsers
{
	internal static class TSQLTokenParserHelper
	{
		/// <summary>
		///		This reads recursively through parenthesis and returns when it hits
		///		one of the stop words outside of any nested parenthesis.
		/// </summary>
		public static void ReadUntilStop(
			ITSQLTokenizer tokenizer,
			TSQLElement element,
			List<TSQLFutureKeywords> futureKeywords,
			List<TSQLKeywords> keywords,
			bool lookForStatementStarts)
		{
			int nestedLevel = 0;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				(
					nestedLevel > 0 ||
					(
						tokenizer.Current.Type != TSQLTokenType.Keyword &&
						!futureKeywords.Any(fk => tokenizer.Current.IsFutureKeyword(fk))
					) ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!keywords.Any(k => tokenizer.Current.AsKeyword.Keyword == k) &&
						!(
							lookForStatementStarts &&
							tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
						)
					)
				))
			{
				RecurseParens(
					tokenizer,
					element,
					ref nestedLevel);
			}
		}

		public static void RecurseParens(
			ITSQLTokenizer tokenizer,
			TSQLElement element,
			ref int nestedLevel)
		{
			if (tokenizer.Current.Type == TSQLTokenType.Character)
			{
				element.Tokens.Add(tokenizer.Current);

                TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

				if (character == TSQLCharacters.OpenParentheses)
				{
					nestedLevel++;
				}
				else if (character == TSQLCharacters.CloseParentheses)
				{
					nestedLevel--;
				}
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.CASE))
			{
				// have to handle CASE special, so that we always match up
				// 1 and only 1 END

				TSQLCaseExpression caseExp = new TSQLCaseExpressionParser().Parse(tokenizer);

				element.Tokens.AddRange(caseExp.Tokens);

				if (tokenizer.Current != null)
				{
					element.Tokens.Add(tokenizer.Current);
				}
			}
			else
			{
				element.Tokens.Add(tokenizer.Current);
			}
		}

		public static void ReadCommentsAndWhitespace(
			ITSQLTokenizer tokenizer,
			TSQLElement element)
		{
			ReadCommentsAndWhitespace(
				tokenizer,
				element.Tokens);
		}

		public static void ReadCommentsAndWhitespace(
			ITSQLTokenizer tokenizer,
			List<TSQLToken> savedTokens)
		{
			while (tokenizer.Current.IsWhitespace() ||
				tokenizer.Current.IsComment())
			{
				savedTokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}
		}

		public static void ReadThroughAnyCommentsOrWhitespace(
			ITSQLTokenizer tokenizer,
			List<TSQLToken> savedTokens)
		{
			while (
				tokenizer.MoveNext() &&
				(
					tokenizer.Current.IsWhitespace() ||
					tokenizer.Current.IsComment())
				)
			{
				savedTokens.Add(tokenizer.Current);
			}
		}
	}
}
