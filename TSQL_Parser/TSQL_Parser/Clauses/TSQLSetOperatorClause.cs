using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;

namespace TSQL.Clauses
{
	public abstract class TSQLSetOperatorClause : TSQLClause
	{
		public abstract TSQLSetOperatorType Type { get; }

		public TSQLSelectStatement Select { get; internal set; }
	}
}
