using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Elements;

namespace TSQL.Expressions
{
	public partial class TSQLArgumentList : TSQLElement, IList<TSQLExpression>
	{
		private List<TSQLExpression> arguments = null;

		public TSQLArgumentList(List<TSQLExpression> arguments)
		{
			this.arguments = arguments;
		}

		public TSQLExpression this[int index]
		{
			get
			{
				return arguments[index];
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public int Count
		{
			get
			{
				return arguments.Count;
			}
		}
	}
}
