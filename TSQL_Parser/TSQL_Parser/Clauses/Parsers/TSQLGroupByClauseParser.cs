using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLGroupByClauseParser
	{
		public TSQLGroupByClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLGroupByClause groupBy = new TSQLGroupByClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.GROUP))
			{
				throw new InvalidOperationException("GROUP expected.");
			}

			groupBy.Tokens.Add(tokenizer.Current);

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
							TSQLKeywords.HAVING,
							TSQLKeywords.ORDER,
							TSQLKeywords.FOR,
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					groupBy,
					ref nestedLevel);
			}

			return groupBy;
		}
	}
}
