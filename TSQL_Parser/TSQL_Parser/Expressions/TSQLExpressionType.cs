using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Expressions
{
	public enum TSQLExpressionType
	{
		Case,
		Column,
		Function,
		Subquery,
		Variable,
		Multicolumn,
		Operator,
		Grouped,
		Constant
	}
}
