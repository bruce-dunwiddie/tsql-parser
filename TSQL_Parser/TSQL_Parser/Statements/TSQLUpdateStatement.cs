using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements
{
	public class TSQLUpdateStatement : TSQLStatement
	{
#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Update;
			}
		}

#pragma warning restore 1591
	}
}
