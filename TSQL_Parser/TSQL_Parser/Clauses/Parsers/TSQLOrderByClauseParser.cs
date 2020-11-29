using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

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

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				orderBy,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: true);

			return orderBy;
		}
	}
}
