using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLUpdateClauseParser
	{
		public TSQLUpdateClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLUpdateClause update = new TSQLUpdateClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.UPDATE))
			{
				throw new InvalidOperationException("UPDATE expected.");
			}

			update.Tokens.Add(tokenizer.Current);

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
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.SET
						)
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					update,
					ref nestedLevel);
			}

			return update;
		}
	}
}
