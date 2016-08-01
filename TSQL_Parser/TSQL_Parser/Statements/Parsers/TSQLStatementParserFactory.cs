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
		public ITSQLStatementParser Create(TSQLToken token)
		{
			if (token.Type == TSQLTokenType.Keyword)
			{
				TSQLKeywords keyword = token.AsKeyword.Keyword;
				
				if (keyword == TSQLKeywords.SELECT)
				{
					return new TSQLSelectStatementParser();
				}
				else if (keyword == TSQLKeywords.INSERT)
				{
					return new TSQLInsertStatementParser();
				}
				else if (keyword == TSQLKeywords.UPDATE)
				{
					return new TSQLUpdateStatementParser();
				}
				else if (keyword == TSQLKeywords.DELETE)
				{
					return new TSQLDeleteStatementParser();
				}
				else if (keyword == TSQLKeywords.MERGE)
				{
					return new TSQLMergeStatementParser();
				}
				else if (keyword == TSQLKeywords.WITH)
				{
					return new TSQLWithAggregateStatementParser();
				}
				else
				{
					return new TSQLUnknownStatementParser();
				}
			}
			else
			{
				return new TSQLExecuteStatementParser();
			}
		}
	}
}
