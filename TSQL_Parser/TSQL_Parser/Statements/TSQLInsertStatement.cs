using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements
{
	public class TSQLInsertStatement : TSQLStatement
	{
		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Insert;
			}
		}
	}
}
