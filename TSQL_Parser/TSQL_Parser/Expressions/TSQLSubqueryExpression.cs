using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;

namespace TSQL.Expressions
{
	public class TSQLSubqueryExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Subquery;
			}
		}

		public TSQLSelectStatement Select { get; internal set; }
	}
}
