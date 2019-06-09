using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLStatementParserFactory
	{
		public ITSQLStatementParser Create(TSQLToken token)
		{
			if (token.Type == TSQLTokenType.Keyword)
			{
				TSQLKeywords keyword = token.AsKeyword.Keyword;
				
				if (keyword == TSQLKeywords.SELECT)
				{
					return new TSQLSelectStatementParser();
				}
				else
				{
					return new TSQLUnknownStatementParser();
				}
			}
			else
			{
				return new TSQLUnknownStatementParser();

				// TODO: check for an EXEC without the keyword
			}
		}
	}
}
