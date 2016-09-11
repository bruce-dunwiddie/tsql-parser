using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	public class TSQLUpdateStatementParser : ITSQLStatementParser
	{
		TSQLUpdateStatement Parse(TSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
