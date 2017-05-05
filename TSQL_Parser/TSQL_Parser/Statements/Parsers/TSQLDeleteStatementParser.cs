using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLDeleteStatementParser : ITSQLStatementParser
	{
		public TSQLDeleteStatement Parse(IEnumerator<TSQLToken> tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
