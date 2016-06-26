using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements.Builders
{
	public class TSQLSelectStatementBuilder : ITSQLStatementBuilder
	{
		public TSQLStatement Build(TSQLTokenizer tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			return select;
		}
	}
}
