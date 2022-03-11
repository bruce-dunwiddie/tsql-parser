using System;
using System.Collections.Generic;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLDeleteClauseParser
	{
		public TSQLDeleteClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLDeleteClause delete = new TSQLDeleteClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.DELETE))
			{
				throw new InvalidOperationException("DELETE expected.");
			}

			delete.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				delete,
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.FROM,
					TSQLKeywords.WHERE,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: true);

			return delete;
		}
	}
}
