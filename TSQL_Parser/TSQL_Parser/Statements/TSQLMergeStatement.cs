using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
