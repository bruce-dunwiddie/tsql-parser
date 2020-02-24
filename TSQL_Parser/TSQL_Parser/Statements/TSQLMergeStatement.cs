using System.Collections.Generic;
using TSQL.Clauses;

namespace TSQL.Statements
{
	public class TSQLMergeStatement : TSQLStatement
	{
		internal TSQLMergeStatement()
		{

		}

#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Merge;
			}
		}

#pragma warning restore 1591

		public TSQLMergeClause Merge { get; internal set; }

		public TSQLIntoClause Into { get; internal set; }

		public TSQLUsingClause Using { get; internal set; }

		public TSQLOnClause On { get; internal set; }

		public List<TSQLWhenClause> When { get; } = new List<TSQLWhenClause>();

		public TSQLOutputClause Output { get; internal set; }
	}
}