using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Elements;

namespace TSQL.Clauses
{
	public class TSQLWhereClause : TSQLClause
	{
		internal TSQLWhereClause()
		{

		}

		public List<TSQLPredicate> Predicates
		{
			get
			{
				return null;
			}
		}
	}
}
