using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Expressions;

namespace TSQL.Elements
{
	public class TSQLSelectColumn : TSQLElement
	{
		public string Alias
		{
			get
			{
				return null;
			}
		}

		public TSQLExpression Expression
		{
			get
			{
				return null;
			}
		}
	}
}
