using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLConstantExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Constant;
			}
		}

		public TSQLLiteral Literal { get; internal set; }
	}
}
