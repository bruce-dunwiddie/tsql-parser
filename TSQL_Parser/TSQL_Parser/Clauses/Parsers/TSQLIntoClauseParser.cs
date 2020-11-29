using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLIntoClauseParser
	{
		public TSQLIntoClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLIntoClause into = new TSQLIntoClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				throw new InvalidOperationException("INTO expected.");
			}

			into.Tokens.Add(tokenizer.Current);

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
					tokenizer.Current.Type == TSQLTokenType.Identifier ||
					tokenizer.Current.IsCharacter(TSQLCharacters.Period) ||
					tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses) ||
					tokenizer.Current.Type == TSQLTokenType.Whitespace ||
					tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
					tokenizer.Current.Type == TSQLTokenType.MultilineComment
				))
			{
				TSQLTokenParserHelper.RecurseParens(
					tokenizer,
					into,
					ref nestedLevel);
			}

			return into;
		}
	}
}
