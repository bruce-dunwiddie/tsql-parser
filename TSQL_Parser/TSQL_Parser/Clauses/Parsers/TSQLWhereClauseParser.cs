using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLWhereClauseParser
	{
		public TSQLWhereClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLWhereClause where = new TSQLWhereClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.WHERE))
			{
				throw new InvalidOperationException("WHERE expected.");
			}

			where.Tokens.Add(tokenizer.Current);

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
							TSQLKeywords.GROUP,
							TSQLKeywords.HAVING,
							TSQLKeywords.UNION,
							TSQLKeywords.EXCEPT,
							TSQLKeywords.INTERSECT,
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
					where,
					ref nestedLevel);
			}

			return where;
		}
	}
}
