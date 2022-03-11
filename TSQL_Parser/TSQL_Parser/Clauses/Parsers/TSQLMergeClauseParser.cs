using System;
using System.Collections.Generic;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLMergeClauseParser
	{
		public TSQLMergeClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLMergeClause merge = new TSQLMergeClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.MERGE))
			{
				throw new InvalidOperationException("MERGE expected.");
			}

			merge.Tokens.Add(tokenizer.Current);

			// can contain TOP()

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				merge,
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT,
					TSQLFutureKeywords.USING
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.INTO,
					TSQLKeywords.AS,
					TSQLKeywords.ON,
					TSQLKeywords.WHEN
				},
				lookForStatementStarts: true);

			return merge;
		}
	}
}