using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;

namespace TSQL.Statements
{
	internal class TSQLWithStatement : TSQLStatement
	{
		internal TSQLWithStatement()
		{
			
		}

#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.With;
			}
		}

#pragma warning restore 1591

		public List<TSQLCommonTableExpressionClause> CommonTableExpressions { get; set; }
			= new List<TSQLCommonTableExpressionClause>();
	}
}
