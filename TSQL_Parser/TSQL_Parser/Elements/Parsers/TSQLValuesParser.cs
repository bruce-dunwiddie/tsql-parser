using System;
using System.Collections.Generic;

using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Elements.Parsers
{
	internal class TSQLValuesParser
	{
		public TSQLValues Parse(ITSQLTokenizer tokenizer)
		{
			TSQLValues values = new TSQLValues();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.VALUES))
			{
				throw new InvalidOperationException("VALUES expected.");
			}

			values.Tokens.Add(tokenizer.Current);

			TSQLSubqueryHelper.ReadUntilStop(
				tokenizer,
				values,
				// stop words come from usage in MERGE
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.ON,
					TSQLKeywords.WHEN
				},
				// INSERT INTO ... VALUES ... SELECT
				lookForStatementStarts: true);

			return values;
		}
	}
}