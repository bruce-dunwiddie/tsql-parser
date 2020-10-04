using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Elements;

namespace TSQL.Statements
{
	public class TSQLInsertStatement : TSQLStatement
	{
		internal TSQLInsertStatement()
		{

		}

#pragma warning disable 1591

		public override TSQLStatementType Type
		{
			get
			{
				return TSQLStatementType.Insert;
			}
		}

#pragma warning restore 1591

		public TSQLWithClause With { get; internal set; }

		public TSQLInsertClause Insert { get; internal set; }

		public TSQLOutputClause Output { get; internal set; }

		public TSQLValues Values { get; internal set; }

		public TSQLSelectStatement Select { get; internal set; }

		public TSQLExecuteStatement Execute { get; internal set; }

		public TSQLDefaultValues Default { get; internal set; }
	}
}
