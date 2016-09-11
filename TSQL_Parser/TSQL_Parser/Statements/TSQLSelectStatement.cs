using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses;

namespace TSQL.Statements
{
	public class TSQLSelectStatement : TSQLStatement
	{
		public TSQLSelectStatement()
		{

		}

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Select;
			}
		}

		public TSQLSelectClause Select { get; set; }

		public TSQLIntoClause Into { get; set; }

		public TSQLFromClause From { get; set; }

		public TSQLWhereClause Where { get; set; }

		public TSQLGroupByClause GroupBy { get; set; }

		public TSQLHavingClause Having { get; set; }

		public TSQLOrderByClause OrderBy { get; set; }
	}
}
