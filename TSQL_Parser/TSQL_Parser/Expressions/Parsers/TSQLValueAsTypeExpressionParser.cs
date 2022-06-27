using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Expressions.Parsers
{
	/// <summary>
	///		Used to parse the unique argument format of the CAST function.
	/// </summary>
	internal class TSQLValueAsTypeExpressionParser
	{
		public TSQLArgumentList Parse(ITSQLTokenizer tokenizer)
		{
			TSQLValueAsTypeExpression argument = new TSQLValueAsTypeExpression();

			// need to do this before starting the argument loop
			// so we can handle an empty argument list of just whitespace
			// and comments
			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				argument);

			TSQLExpression expression = new TSQLValueExpressionParser().Parse(tokenizer);

			argument.Expression = expression;

			argument.Tokens.AddRange(expression.Tokens);

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				argument);

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.AS))
			{
				throw new InvalidOperationException("AS expected.");
			}

			argument.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
				tokenizer,
				argument.Tokens);

			List<TSQLToken> dataTypeTokens = new List<TSQLToken>();

			dataTypeTokens.Add(tokenizer.Current);

			int nestedLevel = 0;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				))
			{
				if (tokenizer.Current.Type == TSQLTokenType.Character)
				{
					dataTypeTokens.Add(tokenizer.Current);

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
				else
				{
					dataTypeTokens.Add(tokenizer.Current);
				}
			}

			argument.DataType = String.Join("", dataTypeTokens.Select(t => t.Text)).TrimEnd();
			argument.Tokens.AddRange(dataTypeTokens);

			TSQLArgumentList argList = new TSQLArgumentList(
				new List<TSQLExpression> { argument });

			argList.Tokens.AddRange(argument.Tokens);

			return argList;
		}
	}
}
