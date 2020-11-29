using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLUpdateClauseParser
	{
		public TSQLUpdateClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLUpdateClause update = new TSQLUpdateClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.UPDATE))
			{
				throw new InvalidOperationException("UPDATE expected.");
			}

			update.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				update,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.SET
				},
				lookForStatementStarts: false);

			return update;
		}
	}
}
