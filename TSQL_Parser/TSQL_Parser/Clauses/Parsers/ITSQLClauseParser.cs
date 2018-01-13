using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal interface ITSQLClauseParser
	{
		TSQLClause Parse(ITSQLTokenizer tokenizer);
    }
}
