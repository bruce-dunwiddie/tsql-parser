using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	public interface ITSQLClauseParser
	{
		TSQLClause Parse(IEnumerator<TSQLToken> tokenizer);
    }
}
