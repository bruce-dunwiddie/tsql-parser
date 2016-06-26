using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements
{
	public abstract class TSQLStatement
	{
		public abstract TSQLStatementType Type
		{
			get;
		}
	}
}
