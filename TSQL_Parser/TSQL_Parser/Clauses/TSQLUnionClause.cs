using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Clauses
{
	public class TSQLUnionClause : TSQLSetOperatorClause
	{
		internal TSQLUnionClause()
		{

		}

		public override TSQLSetOperatorType Type
		{
			get
			{
				return TSQLSetOperatorType.Union;
			}
		}
	}
}
