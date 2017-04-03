using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	public interface ITSQLStatementParser
	{
		TSQLStatement Parse(IEnumerator<TSQLToken> tokenizer);
	}
}
