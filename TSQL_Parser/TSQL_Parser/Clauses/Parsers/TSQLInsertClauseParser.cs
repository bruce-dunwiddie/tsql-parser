using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLInsertClauseParser
	{
		public TSQLInsertClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLInsertClause insert = new TSQLInsertClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.INSERT))
			{
				throw new InvalidOperationException("INSERT expected.");
			}

			insert.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				insert,
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.SELECT,
					TSQLKeywords.EXECUTE,
					TSQLKeywords.VALUES,
					TSQLKeywords.DEFAULT
				},
				lookForStatementStarts: false);

			return insert;
		}
	}
}
