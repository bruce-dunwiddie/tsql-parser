using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLFromClauseParser
	{
		public TSQLFromClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLFromClause from = new TSQLFromClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.FROM))
			{
				throw new InvalidOperationException("FROM expected.");
			}

			from.Tokens.Add(tokenizer.Current);

			// derived tables
			// TVF

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				from,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.WHERE,
					TSQLKeywords.GROUP,
					TSQLKeywords.HAVING,
					TSQLKeywords.ORDER,
					TSQLKeywords.UNION,
					TSQLKeywords.EXCEPT,
					TSQLKeywords.INTERSECT,
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: true);

			return from;
		}
	}
}
