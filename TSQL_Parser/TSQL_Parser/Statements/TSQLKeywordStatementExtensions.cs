using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Statements
{
	internal static class TSQLKeywordStatementExtensions
	{
		// purposely leaving out WITH, because it doesn't always mean the start
		// of a statement, e.g. table hints

		private static readonly TSQLKeywords[] StatementStarts =
			new TSQLKeywords[]
			{
				TSQLKeywords.SELECT,
				TSQLKeywords.INSERT,
				TSQLKeywords.UPDATE,
				TSQLKeywords.DELETE,
				TSQLKeywords.CREATE,
				TSQLKeywords.ALTER,
				TSQLKeywords.EXECUTE,
				TSQLKeywords.IF,
				TSQLKeywords.WHILE,
				TSQLKeywords.BEGIN
			};

		public static bool IsStatementStart(this TSQLKeywords keyword)
		{
			return keyword.In(StatementStarts);
		}
	}
}
