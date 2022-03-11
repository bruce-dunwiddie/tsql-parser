using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	public class TSQLCaseExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Case;
			}
		}
	}
}
