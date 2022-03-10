using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Clauses
{
	public class TSQLExceptClause : TSQLSetOperatorClause
	{
		internal TSQLExceptClause()
		{

		}

		public override TSQLSetOperatorType Type
		{
			get
			{
				return TSQLSetOperatorType.Except;
			}
		}
	}
}
