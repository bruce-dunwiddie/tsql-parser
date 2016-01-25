using System;
using System.Collections.Generic;

namespace TSQL
{
	public class TSQLKeywords
	{
		private static Dictionary<string, TSQLKeywords> keywordLookup =
			new Dictionary<string, TSQLKeywords>(StringComparer.InvariantCultureIgnoreCase);

		public static TSQLKeywords None = new TSQLKeywords("");

		#region commands

		public static TSQLKeywords ALTER = new TSQLKeywords("ALTER");
		public static TSQLKeywords CREATE = new TSQLKeywords("CREATE");
		public static TSQLKeywords DROP = new TSQLKeywords("DROP");
		public static TSQLKeywords SELECT = new TSQLKeywords("SELECT");
		public static TSQLKeywords INSERT = new TSQLKeywords("INSERT");
		public static TSQLKeywords UPDATE = new TSQLKeywords("UPDATE");
		public static TSQLKeywords DELETE = new TSQLKeywords("DELETE");
		public static TSQLKeywords MERGE = new TSQLKeywords("MERGE");
		public static TSQLKeywords TRUNCATE = new TSQLKeywords("TRUNCATE");
		public static TSQLKeywords DISABLE = new TSQLKeywords("DISABLE");
		public static TSQLKeywords ENABLE = new TSQLKeywords("ENABLE");
		public static TSQLKeywords EXECUTE = new TSQLKeywords("EXECUTE");
		// BULK {INSERT}
		public static TSQLKeywords BULK = new TSQLKeywords("BULK");
		public static TSQLKeywords GRANT = new TSQLKeywords("GRANT");
		public static TSQLKeywords DENY = new TSQLKeywords("DENY");
		public static TSQLKeywords REVOKE = new TSQLKeywords("REVOKE");
		// GO is not a Transact-SQL statement; it is a command recognized by the sqlcmd and osql utilities and SQL Server Management Studio Code editor.
		public static TSQLKeywords GO = new TSQLKeywords("GO");
		// ADD .. {SIGNATURE}
		public static TSQLKeywords ADD = new TSQLKeywords("ADD");
		// BEGIN ... END, but watch out for BEGIN (TRAN)SACTION
		public static TSQLKeywords BEGIN = new TSQLKeywords("BEGIN");
		public static TSQLKeywords COMMIT = new TSQLKeywords("COMMIT");
		public static TSQLKeywords ROLLBACK = new TSQLKeywords("ROLLBACK");
		public static TSQLKeywords DUMP = new TSQLKeywords("DUMP");
		public static TSQLKeywords BACKUP = new TSQLKeywords("BACKUP");
		public static TSQLKeywords RESTORE = new TSQLKeywords("RESTORE");
		public static TSQLKeywords LOAD = new TSQLKeywords("LOAD");
		public static TSQLKeywords CHECKPOINT = new TSQLKeywords("CHECKPOINT");
		public static TSQLKeywords WHILE = new TSQLKeywords("WHILE");
		public static TSQLKeywords IF = new TSQLKeywords("IF");
		public static TSQLKeywords BREAK = new TSQLKeywords("BREAK");
		public static TSQLKeywords CONTINUE = new TSQLKeywords("CONTINUE");
		public static TSQLKeywords GOTO = new TSQLKeywords("GOTO");
		public static TSQLKeywords SET = new TSQLKeywords("SET");
		public static TSQLKeywords DECLARE = new TSQLKeywords("DECLARE");
		public static TSQLKeywords PRINT = new TSQLKeywords("PRINT");
		public static TSQLKeywords FETCH = new TSQLKeywords("FETCH");
		public static TSQLKeywords OPEN = new TSQLKeywords("OPEN");
		public static TSQLKeywords CLOSE = new TSQLKeywords("CLOSE");
		public static TSQLKeywords DEALLOCATE = new TSQLKeywords("DEALLOCATE");
		// CTE
		public static TSQLKeywords WITH = new TSQLKeywords("WITH");
		public static TSQLKeywords DBCC = new TSQLKeywords("DBCC");
		public static TSQLKeywords KILL = new TSQLKeywords("KILL");
		// MOVE {CONVERSATION}
		public static TSQLKeywords MOVE = new TSQLKeywords("MOVE");
		// GET {CONVERSATION}
		public static TSQLKeywords GET = new TSQLKeywords("GET");
		public static TSQLKeywords RECEIVE = new TSQLKeywords("RECEIVE");
		public static TSQLKeywords SEND = new TSQLKeywords("SEND");
		public static TSQLKeywords WAITFOR = new TSQLKeywords("WAITFOR");
		public static TSQLKeywords READTEXT = new TSQLKeywords("READTEXT");
		public static TSQLKeywords UPDATETEXT = new TSQLKeywords("UPDATETEXT");
		public static TSQLKeywords WRITETEXT = new TSQLKeywords("WRITETEXT");
		public static TSQLKeywords USE = new TSQLKeywords("USE");
		public static TSQLKeywords SHUTDOWN = new TSQLKeywords("SHUTDOWN");
		public static TSQLKeywords RETURN = new TSQLKeywords("RETURN");
		public static TSQLKeywords REVERT = new TSQLKeywords("REVERT");

		#endregion

		// {stored procedure}

		#region other

		public static TSQLKeywords ALL = new TSQLKeywords("ALL");
		public static TSQLKeywords AND = new TSQLKeywords("AND");
		public static TSQLKeywords ANY = new TSQLKeywords("ANY");
		public static TSQLKeywords AS = new TSQLKeywords("AS");
		public static TSQLKeywords ASC = new TSQLKeywords("ASC");
		public static TSQLKeywords AUTHORIZATION = new TSQLKeywords("AUTHORIZATION");
		public static TSQLKeywords BETWEEN = new TSQLKeywords("BETWEEN");
		public static TSQLKeywords BROWSE = new TSQLKeywords("BROWSE");
		public static TSQLKeywords BY = new TSQLKeywords("BY");
		public static TSQLKeywords CASCADE = new TSQLKeywords("CASCADE");
		public static TSQLKeywords CASE = new TSQLKeywords("CASE");
		public static TSQLKeywords CHECK = new TSQLKeywords("CHECK");
		public static TSQLKeywords CLUSTERED = new TSQLKeywords("CLUSTERED");
		public static TSQLKeywords COLLATE = new TSQLKeywords("COLLATE");
		public static TSQLKeywords COLUMN = new TSQLKeywords("COLUMN");
		public static TSQLKeywords COMMITTED = new TSQLKeywords("COMMITTED");
		public static TSQLKeywords COMPUTE = new TSQLKeywords("COMPUTE");
		public static TSQLKeywords CONSTRAINT = new TSQLKeywords("CONSTRAINT");
		public static TSQLKeywords CROSS = new TSQLKeywords("CROSS");
		public static TSQLKeywords CURRENT = new TSQLKeywords("CURRENT");
		public static TSQLKeywords CURSOR = new TSQLKeywords("CURSOR");
		public static TSQLKeywords DATABASE = new TSQLKeywords("DATABASE");
		public static TSQLKeywords DEFAULT = new TSQLKeywords("DEFAULT");
		public static TSQLKeywords DESC = new TSQLKeywords("DESC");
		public static TSQLKeywords DISK = new TSQLKeywords("DISK");
		public static TSQLKeywords DISTINCT = new TSQLKeywords("DISTINCT");
		public static TSQLKeywords DISTRIBUTED = new TSQLKeywords("DISTRIBUTED");
		public static TSQLKeywords ELSE = new TSQLKeywords("ELSE");
		public static TSQLKeywords END = new TSQLKeywords("END");
		public static TSQLKeywords ESCAPE = new TSQLKeywords("ESCAPE");
		public static TSQLKeywords EXCEPT = new TSQLKeywords("EXCEPT");
		public static TSQLKeywords EXISTS = new TSQLKeywords("EXISTS");
		public static TSQLKeywords EXTERNAL = new TSQLKeywords("EXTERNAL");
		public static TSQLKeywords FILE = new TSQLKeywords("FILE");
		public static TSQLKeywords FILLFACTOR = new TSQLKeywords("FILLFACTOR");
		public static TSQLKeywords FOR = new TSQLKeywords("FOR");
		public static TSQLKeywords FOREIGN = new TSQLKeywords("FOREIGN");
		public static TSQLKeywords FROM = new TSQLKeywords("FROM");
		public static TSQLKeywords FULL = new TSQLKeywords("FULL");
		public static TSQLKeywords FUNCTION = new TSQLKeywords("FUNCTION");
		public static TSQLKeywords GROUP = new TSQLKeywords("GROUP");
		public static TSQLKeywords HAVING = new TSQLKeywords("HAVING");
		public static TSQLKeywords HOLDLOCK = new TSQLKeywords("HOLDLOCK");
		public static TSQLKeywords IDENTITY = new TSQLKeywords("IDENTITY");
		public static TSQLKeywords IDENTITY_INSERT = new TSQLKeywords("IDENTITY_INSERT");
		public static TSQLKeywords IDENTITYCOL = new TSQLKeywords("IDENTITYCOL");
		public static TSQLKeywords IN = new TSQLKeywords("IN");
		public static TSQLKeywords INDEX = new TSQLKeywords("INDEX");
		public static TSQLKeywords INNER = new TSQLKeywords("INNER");
		public static TSQLKeywords INTERSECT = new TSQLKeywords("INTERSECT");
		public static TSQLKeywords INTO = new TSQLKeywords("INTO");
		public static TSQLKeywords IS = new TSQLKeywords("IS");
		public static TSQLKeywords JOIN = new TSQLKeywords("JOIN");
		public static TSQLKeywords KEY = new TSQLKeywords("KEY");
		public static TSQLKeywords LEFT = new TSQLKeywords("LEFT");
		public static TSQLKeywords LIKE = new TSQLKeywords("LIKE");
		public static TSQLKeywords LINENO = new TSQLKeywords("LINENO");
		public static TSQLKeywords NOCHECK = new TSQLKeywords("NOCHECK");
		public static TSQLKeywords NONCLUSTERED = new TSQLKeywords("NONCLUSTERED");
		public static TSQLKeywords NOT = new TSQLKeywords("NOT");
		public static TSQLKeywords NULL = new TSQLKeywords("NULL");
		public static TSQLKeywords OFF = new TSQLKeywords("OFF");
		public static TSQLKeywords OFFSETS = new TSQLKeywords("OFFSETS");
		public static TSQLKeywords ON = new TSQLKeywords("ON");
		public static TSQLKeywords OPTION = new TSQLKeywords("OPTION");
		public static TSQLKeywords OR = new TSQLKeywords("OR");
		public static TSQLKeywords ORDER = new TSQLKeywords("ORDER");
		public static TSQLKeywords OUTER = new TSQLKeywords("OUTER");
		public static TSQLKeywords OVER = new TSQLKeywords("OVER");
		public static TSQLKeywords PERCENT = new TSQLKeywords("PERCENT");
		public static TSQLKeywords PIVOT = new TSQLKeywords("PIVOT");
		public static TSQLKeywords PLAN = new TSQLKeywords("PLAN");
		public static TSQLKeywords PRIMARY = new TSQLKeywords("PRIMARY");
		public static TSQLKeywords PROCEDURE = new TSQLKeywords("PROCEDURE");
		public static TSQLKeywords READ = new TSQLKeywords("READ");
		public static TSQLKeywords REPEATABLE = new TSQLKeywords("REPEATABLE");
		public static TSQLKeywords RECONFIGURE = new TSQLKeywords("RECONFIGURE");
		public static TSQLKeywords REFERENCES = new TSQLKeywords("REFERENCES");
		public static TSQLKeywords REPLICATION = new TSQLKeywords("REPLICATION");
		public static TSQLKeywords RETURNS = new TSQLKeywords("RETURNS");
		public static TSQLKeywords RIGHT = new TSQLKeywords("RIGHT");
		public static TSQLKeywords ROWCOUNT = new TSQLKeywords("ROWCOUNT");
		public static TSQLKeywords ROWGUIDCOL = new TSQLKeywords("ROWGUIDCOL");
		public static TSQLKeywords RULE = new TSQLKeywords("RULE");
		public static TSQLKeywords SCHEMA = new TSQLKeywords("SCHEMA");
		public static TSQLKeywords SOME = new TSQLKeywords("SOME");
		public static TSQLKeywords STATISTICS = new TSQLKeywords("STATISTICS");
		public static TSQLKeywords TABLE = new TSQLKeywords("TABLE");
		public static TSQLKeywords TABLESAMPLE = new TSQLKeywords("TABLESAMPLE");
		public static TSQLKeywords TEXTSIZE = new TSQLKeywords("TEXTSIZE");
		public static TSQLKeywords THEN = new TSQLKeywords("THEN");
		public static TSQLKeywords TOP = new TSQLKeywords("TOP");
		public static TSQLKeywords TRANSACTION = new TSQLKeywords("TRANSACTION");
		public static TSQLKeywords TRIGGER = new TSQLKeywords("TRIGGER");
		public static TSQLKeywords UNION = new TSQLKeywords("UNION");
		public static TSQLKeywords UNIQUE = new TSQLKeywords("UNIQUE");
		public static TSQLKeywords UNPIVOT = new TSQLKeywords("UNPIVOT");
		public static TSQLKeywords VALUES = new TSQLKeywords("VALUES");
		public static TSQLKeywords VARYING = new TSQLKeywords("VARYING");
		public static TSQLKeywords VIEW = new TSQLKeywords("VIEW");
		public static TSQLKeywords WHEN = new TSQLKeywords("WHEN");
		public static TSQLKeywords WHERE = new TSQLKeywords("WHERE");
		// WITHIN {GROUP}
		public static TSQLKeywords WITHIN = new TSQLKeywords("WITHIN");

		#endregion

		// system stored procs

		// built in functions

		// datatypes

		// operators

		// characters? comma, semicolon, etc

		private string Keyword;

		private TSQLKeywords(
			string keyword)
		{
			Keyword = keyword;
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				keywordLookup[keyword] = this;

				if (keyword.Equals("EXECUTE", StringComparison.InvariantCultureIgnoreCase))
				{
					keywordLookup["EXEC"] = this;
				}
				else if (keyword.Equals("TRANSACTION", StringComparison.InvariantCultureIgnoreCase))
				{
					keywordLookup["TRAN"] = this;
				}
				else if (keyword.Equals("PROCEDURE", StringComparison.InvariantCultureIgnoreCase))
				{
					keywordLookup["PROC"] = this;
				}
			}
		}

		public static TSQLKeywords Parse(
			string token)
		{
			if (keywordLookup.ContainsKey(token))
			{
				return keywordLookup[token];
			}
			else
			{
				return TSQLKeywords.None;
			}
		}

		public static bool IsKeyword(
			string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
			{
				return keywordLookup.ContainsKey(token);
			}
			else
			{
				return false;
			}
		}

		public static bool operator ==(
			TSQLKeywords a,
			TSQLKeywords b)
		{
			return
				(object)a != null &&
				a.Equals(b);
		}

		public static bool operator !=(
			TSQLKeywords a,
			TSQLKeywords b)
		{
			return
				(object)a == null ||
				!a.Equals(b);
		}

		private bool Equals(TSQLKeywords obj)
		{
			return
				(
					ReferenceEquals(this, obj)
				) ||
				(
					(object)obj != null &&
					Keyword == obj.Keyword
				);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLKeywords);
		}

		public override int GetHashCode()
		{
			return Keyword.GetHashCode();
		}
	}
}
