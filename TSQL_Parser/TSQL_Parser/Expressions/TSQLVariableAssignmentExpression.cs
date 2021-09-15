using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLVariableAssignmentExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.VariableAssignment;
			}
		}

		public TSQLVariable Variable { get; internal set; }

		public TSQLOperator Operator { get; internal set; }

		public TSQLExpression ValueExpression { get; internal set; }
	}
}
