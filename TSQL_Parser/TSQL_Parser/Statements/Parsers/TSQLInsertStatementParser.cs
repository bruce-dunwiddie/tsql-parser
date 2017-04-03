using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	public class TSQLInsertStatementParser : ITSQLStatementParser
	{
		public TSQLInsertStatement Parse(IEnumerator<TSQLToken> tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
