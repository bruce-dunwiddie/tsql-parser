using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Elements;

namespace TSQL.Expressions
{
	public abstract class TSQLExpression : TSQLElement
	{
		public abstract TSQLExpressionType Type
		{
			get;
		}
	}
}
