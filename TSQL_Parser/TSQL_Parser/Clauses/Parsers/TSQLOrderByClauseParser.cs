using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOrderByClauseParser
	{
		public TSQLOrderByClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOrderByClause orderBy = new TSQLOrderByClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.ORDER))
			{
				throw new InvalidOperationException("ORDER expected.");
			}

			orderBy.Tokens.Add(tokenizer.Current);

			// subqueries
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
							TSQLKeywords.FOR,
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					orderBy,
					ref nestedLevel);
			}

			return orderBy;
		}
	}
}
