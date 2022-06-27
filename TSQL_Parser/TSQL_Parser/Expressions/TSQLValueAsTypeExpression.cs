using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLValueAsTypeExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.ValueAsType;
			}
		}

		public TSQLExpression Expression { get; internal set; }

		public string DataType { get; internal set; }
	}
}
