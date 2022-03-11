using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Clauses
{
	public class TSQLIntersectClause : TSQLSetOperatorClause
	{
		internal TSQLIntersectClause()
		{

		}

		public override TSQLSetOperatorType Type
		{
			get
			{
				return TSQLSetOperatorType.Intersect;
			}
		}
	}
}
