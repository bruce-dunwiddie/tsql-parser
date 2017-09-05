using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLWithClauseParser : ITSQLClauseParser
	{
		public TSQLWithClause Parse(ITSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
