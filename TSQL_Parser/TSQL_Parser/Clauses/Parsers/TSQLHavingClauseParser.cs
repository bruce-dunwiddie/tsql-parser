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

			TSQLSubqueryHelper.ReadUntilStop(
				tokenizer,
				having,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.ORDER,
					TSQLKeywords.UNION,
					TSQLKeywords.EXCEPT,
					TSQLKeywords.INTERSECT,
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				true);

			return having;
		}
	}
}
