using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	// represents only the CTE's portion of a larger SELECT/INSERT/UPDATE/DELETE statement
	internal class TSQLWithStatementParser : ITSQLStatementParser
	{
		public TSQLWithStatement Parse(ITSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
