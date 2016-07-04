using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Clauses.Parsers
{
	public class TSQLWhereClauseParser
	{
		public TSQLWhereClause Build(TSQLTokenizer tokenizer)
		{
			TSQLWhereClause where = new TSQLWhereClause();

			return where;
		}
	}
}
