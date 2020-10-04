using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLExpressionFactory
	{
		public TSQLExpression Parse(ITSQLTokenizer tokenizer)
		{
			if (tokenizer.Current.IsKeyword(TSQLKeywords.CASE))
			{
				return new TSQLCaseExpressionParser().Parse(tokenizer);
			}
			else
			{
				return null;
			}
		}
	}
}
