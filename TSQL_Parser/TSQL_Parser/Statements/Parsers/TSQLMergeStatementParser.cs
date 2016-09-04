using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements.Parsers
{
	public class TSQLMergeStatementParser : ITSQLStatementParser
	{
		TSQLMergeStatement Parse(TSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLStatement ITSQLStatementParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
