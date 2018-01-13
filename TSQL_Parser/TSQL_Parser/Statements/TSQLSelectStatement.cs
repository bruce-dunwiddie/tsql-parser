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

		public TSQLSelectClause Select { get; set; }

		public TSQLIntoClause Into { get; set; }

		public TSQLFromClause From { get; set; }

		public TSQLWhereClause Where { get; set; }

		public TSQLGroupByClause GroupBy { get; set; }

		public TSQLHavingClause Having { get; set; }

		public TSQLOrderByClause OrderBy { get; set; }

		public TSQLOptionClause Option { get; set; }
	}
}
