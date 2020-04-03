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

			TSQLSubqueryHelper.ReadUntilStop(
				tokenizer,
				groupBy,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.HAVING,
					TSQLKeywords.UNION,
					TSQLKeywords.EXCEPT,
					TSQLKeywords.INTERSECT,
					TSQLKeywords.ORDER,
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: true);

			return groupBy;
		}
	}
}
