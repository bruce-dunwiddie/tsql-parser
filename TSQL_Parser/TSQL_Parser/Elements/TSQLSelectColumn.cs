using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Expressions;
using TSQL.Tokens;

namespace TSQL.Elements
{
	public class TSQLSelectColumn : TSQLElement
	{
		public TSQLIdentifier ColumnAlias { get; internal set; }

		public TSQLExpression Expression { get; internal set; }
	}
}
