using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLHavingClauseParser
	{
		public TSQLHavingClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLHavingClause having = new TSQLHavingClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.HAVING))
			{
				throw new InvalidOperationException("HAVING expected.");
			}

			having.Tokens.Add(tokenizer.Current);

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
							TSQLKeywords.ORDER,
							TSQLKeywords.UNION,
							TSQLKeywords.EXCEPT,
							TSQLKeywords.INTERSECT,
							TSQLKeywords.FOR,
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					having,
					ref nestedLevel);
			}

			return having;
		}
	}
}
