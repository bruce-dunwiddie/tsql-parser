using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Builders
{
	public class TSQLStatementBuilderFactory
	{
		public ITSQLStatementBuilder Create(TSQLKeywords keyword)
		{
			if (keyword == TSQLKeywords.SELECT)
			{
				return new TSQLSelectStatementBuilder();
			}
			else
			{
				return null;
			}
		}
	}
}
