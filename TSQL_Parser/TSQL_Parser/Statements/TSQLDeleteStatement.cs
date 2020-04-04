using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;

namespace TSQL.Statements
{
	public class TSQLDeleteStatement : TSQLStatement
	{
		internal TSQLDeleteStatement()
		{

		}

#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Delete;
			}
		}

#pragma warning restore 1591

		public TSQLWithClause With { get; internal set; }

		public TSQLDeleteClause Delete { get; internal set; }

		public TSQLOutputClause Output { get; internal set; }

		public TSQLFromClause From { get; internal set; }

		public TSQLWhereClause Where { get; internal set; }

		public TSQLOptionClause Option { get; internal set; }
	}
}
