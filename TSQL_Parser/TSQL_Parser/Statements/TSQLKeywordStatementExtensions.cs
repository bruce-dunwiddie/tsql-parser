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
				TSQLKeywords.MERGE,
				TSQLKeywords.DECLARE,
				TSQLKeywords.SET,
				TSQLKeywords.CREATE,
				TSQLKeywords.ALTER,
				TSQLKeywords.EXECUTE,
				TSQLKeywords.IF,
				TSQLKeywords.WHILE,
				TSQLKeywords.BEGIN,
				TSQLKeywords.GO,
				TSQLKeywords.DROP,
				TSQLKeywords.TRUNCATE,
				TSQLKeywords.ENABLE,
				TSQLKeywords.DISABLE,
				TSQLKeywords.BULK,
				TSQLKeywords.GRANT,
				TSQLKeywords.DENY,
				TSQLKeywords.REVOKE,
				TSQLKeywords.ADD,
				TSQLKeywords.COMMIT,
				TSQLKeywords.ROLLBACK,
				TSQLKeywords.DUMP,
				TSQLKeywords.BACKUP,
				TSQLKeywords.RESTORE,
				TSQLKeywords.LOAD,
				TSQLKeywords.CHECKPOINT,
				TSQLKeywords.BREAK,
				TSQLKeywords.CONTINUE,
				TSQLKeywords.GOTO,
				TSQLKeywords.PRINT,
				TSQLKeywords.FETCH,
				TSQLKeywords.OPEN,
				TSQLKeywords.CLOSE,
				TSQLKeywords.DEALLOCATE,
				TSQLKeywords.DBCC,
				TSQLKeywords.KILL,
				TSQLKeywords.MOVE,
				TSQLKeywords.GET,
				TSQLKeywords.RECEIVE,
				TSQLKeywords.SEND,
				TSQLKeywords.WAITFOR,
				TSQLKeywords.READTEXT,
				TSQLKeywords.UPDATETEXT,
				TSQLKeywords.WRITETEXT,
				TSQLKeywords.USE,
				TSQLKeywords.SHUTDOWN,
				TSQLKeywords.RETURN,
				TSQLKeywords.REVERT,
				// END isn't exactly a statement start, but it acts like one
				TSQLKeywords.END,
				TSQLKeywords.ELSE
			};

		public static bool IsStatementStart(this TSQLKeywords keyword)
		{
			var result = keyword.In(StatementStarts);
			return result;
		}
	}
}
