using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;

namespace TSQL.Statements
{
	public class TSQLSelectStatement : TSQLStatement
	{
		internal TSQLSelectStatement()
		{

		}

#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Select;
			}
		}

#pragma warning restore 1591

		public TSQLWithClause With { get; internal set; }

		public TSQLSelectClause Select { get; internal set; }

		public TSQLIntoClause Into { get; internal set; }

		public TSQLFromClause From { get; internal set; }

		public TSQLWhereClause Where { get; internal set; }

		public TSQLGroupByClause GroupBy { get; internal set; }

		public TSQLHavingClause Having { get; internal set; }

		public TSQLOrderByClause OrderBy { get; internal set; }

		public TSQLForClause For { get; internal set; }

		public TSQLOptionClause Option { get; internal set; }

		// UNION [ALL], EXCEPT, INTERSECT
		public List<TSQLSetOperatorClause> SetOperators { get; } = new List<TSQLSetOperatorClause>();
	}
}
