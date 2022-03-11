using System;
using System.Collections.Generic;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOnClauseParser
	{
		public TSQLOnClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOnClause on = new TSQLOnClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.ON))
			{
				throw new InvalidOperationException("ON expected.");
			}

			on.Tokens.Add(tokenizer.Current);

			// TODO: tighten logic to handle tables named OUTPUT, but still handle ON usage in MERGE

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				on,
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT,
					TSQLFutureKeywords.USING
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.INNER,
					TSQLKeywords.OUTER,
					TSQLKeywords.JOIN,
					TSQLKeywords.WHEN
				},
				lookForStatementStarts: true);

			return on;
		}
	}
}