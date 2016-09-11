using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements.Parsers
{
	// represents only the CTE's portion of a larger SELECT/INSERT/UPDATE/DELETE statement
	public class TSQLWithStatementParser : ITSQLStatementParser
	{
		public TSQLWithStatement Parse(TSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
