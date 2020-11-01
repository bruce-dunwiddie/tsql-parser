using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Elements;

namespace TSQL.Clauses
{
	public class TSQLSelectClause : TSQLClause
	{
		internal TSQLSelectClause()
		{

		}

		public List<TSQLSelectColumn> Columns { get; } = new List<TSQLSelectColumn>();
	}
}
