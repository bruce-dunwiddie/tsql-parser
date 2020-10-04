using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	public class TSQLColumnExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Column;
			}
		}

		public string TableAlias
		{
			get
			{
				return null;
			}
		}
	}
}
