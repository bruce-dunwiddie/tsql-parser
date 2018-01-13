using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Clauses
{
	public class TSQLCommonTableExpressionClause : TSQLClause
	{
		internal TSQLCommonTableExpressionClause()
		{

		}

		public string Name { get; set; }
	}
}
