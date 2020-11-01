using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	public class TSQLFunctionExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Function;
			}
		}

		public string Name { get; internal set; }

		public TSQLArgumentList Arguments { get; internal set; }
	}
}
