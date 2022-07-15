using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLOperationExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Operation;
			}
		}

		public TSQLExpression LeftSide { get; internal set; }

		public TSQLExpression RightSide { get; internal set; }

		public TSQLOperator Operator { get; internal set; }
	}
}
