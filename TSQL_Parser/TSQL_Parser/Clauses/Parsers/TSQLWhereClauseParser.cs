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

			TSQLSubqueryHelper.ReadUntilStop(
				tokenizer,
				where,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.GROUP,
					TSQLKeywords.HAVING,
					TSQLKeywords.UNION,
					TSQLKeywords.EXCEPT,
					TSQLKeywords.INTERSECT,
					TSQLKeywords.ORDER,
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				true);

			return where;
		}
	}
}
