using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	public class TSQLStatementParserFactory
	{
		public ITSQLStatementParser Create(TSQLKeywords keyword)
		{
			if (keyword == TSQLKeywords.SELECT)
			{
				return new TSQLSelectStatementParser();
			}
			else
			{
				return new TSQLUnknownStatementParser();
			}
		}
	}
}
