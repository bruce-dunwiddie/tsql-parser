using System;
using System.Collections.Generic;
using System.Linq;

namespace TSQL
{
	public struct TSQLKeywords
	{
		private static Dictionary<string, TSQLKeywords> keywordLookup =
			new Dictionary<string, TSQLKeywords>(StringComparer.InvariantCultureIgnoreCase);

		public static readonly TSQLKeywords None = new TSQLKeywords("");

#pragma warning disable 1591

		#region commands

		public static readonly TSQLKeywords ALTER = new TSQLKeywords("ALTER");
		public static readonly TSQLKeywords CREATE = new TSQLKeywords("CREATE");
		public static readonly TSQLKeywords DROP = new TSQLKeywords("DROP");
		public static readonly TSQLKeywords SELECT = new TSQLKeywords("SELECT");
		public static readonly TSQLKeywords INSERT = new TSQLKeywords("INSERT");
		public static readonly TSQLKeywords UPDATE = new TSQLKeywords("UPDATE");
		public static readonly TSQLKeywords DELETE = new TSQLKeywords("DELETE");
		public static readonly TSQLKeywords MERGE = new TSQLKeywords("MERGE");
		public static readonly TSQLKeywords TRUNCATE = new TSQLKeywords("TRUNCATE");
		public static readonly TSQLKeywords DISABLE = new TSQLKeywords("DISABLE");
		public static readonly TSQLKeywords ENABLE = new TSQLKeywords("ENABLE");
		public static readonly TSQLKeywords EXECUTE = new TSQLKeywords("EXECUTE");
		// BULK {INSERT}
		public static readonly TSQLKeywords BULK = new TSQLKeywords("BULK");
		public static readonly TSQLKeywords GRANT = new TSQLKeywords("GRANT");
		public static readonly TSQLKeywords DENY = new TSQLKeywords("DENY");
		public static readonly TSQLKeywords REVOKE = new TSQLKeywords("REVOKE");
		// GO is not a Transact-SQL statement; it is a command recognized by the sqlcmd and osql utilities and SQL Server Management Studio Code editor.
		public static readonly TSQLKeywords GO = new TSQLKeywords("GO");
		// ADD .. {SIGNATURE}
		public static readonly TSQLKeywords ADD = new TSQLKeywords("ADD");
		// BEGIN ... END, but watch out for BEGIN (TRAN)SACTION
		public static readonly TSQLKeywords BEGIN = new TSQLKeywords("BEGIN");
		public static readonly TSQLKeywords COMMIT = new TSQLKeywords("COMMIT");
		public static readonly TSQLKeywords ROLLBACK = new TSQLKeywords("ROLLBACK");
		public static readonly TSQLKeywords DUMP = new TSQLKeywords("DUMP");
		public static readonly TSQLKeywords BACKUP = new TSQLKeywords("BACKUP");
		public static readonly TSQLKeywords RESTORE = new TSQLKeywords("RESTORE");
		public static readonly TSQLKeywords LOAD = new TSQLKeywords("LOAD");
		public static readonly TSQLKeywords CHECKPOINT = new TSQLKeywords("CHECKPOINT");
		public static readonly TSQLKeywords WHILE = new TSQLKeywords("WHILE");
		public static readonly TSQLKeywords IF = new TSQLKeywords("IF");
		public static readonly TSQLKeywords BREAK = new TSQLKeywords("BREAK");
		public static readonly TSQLKeywords CONTINUE = new TSQLKeywords("CONTINUE");
		public static readonly TSQLKeywords GOTO = new TSQLKeywords("GOTO");
		public static readonly TSQLKeywords SET = new TSQLKeywords("SET");
		public static readonly TSQLKeywords DECLARE = new TSQLKeywords("DECLARE");
		public static readonly TSQLKeywords PRINT = new TSQLKeywords("PRINT");
		public static readonly TSQLKeywords FETCH = new TSQLKeywords("FETCH");
		public static readonly TSQLKeywords OPEN = new TSQLKeywords("OPEN");
		public static readonly TSQLKeywords CLOSE = new TSQLKeywords("CLOSE");
		public static readonly TSQLKeywords DEALLOCATE = new TSQLKeywords("DEALLOCATE");
		// CTE
		public static readonly TSQLKeywords WITH = new TSQLKeywords("WITH");
		public static readonly TSQLKeywords DBCC = new TSQLKeywords("DBCC");
		public static readonly TSQLKeywords KILL = new TSQLKeywords("KILL");
		// MOVE {CONVERSATION}
		public static readonly TSQLKeywords MOVE = new TSQLKeywords("MOVE");
		// GET {CONVERSATION}
		public static readonly TSQLKeywords GET = new TSQLKeywords("GET");
		public static readonly TSQLKeywords RECEIVE = new TSQLKeywords("RECEIVE");
		public static readonly TSQLKeywords SEND = new TSQLKeywords("SEND");
		public static readonly TSQLKeywords WAITFOR = new TSQLKeywords("WAITFOR");
		public static readonly TSQLKeywords READTEXT = new TSQLKeywords("READTEXT");
		public static readonly TSQLKeywords UPDATETEXT = new TSQLKeywords("UPDATETEXT");
		public static readonly TSQLKeywords WRITETEXT = new TSQLKeywords("WRITETEXT");
		public static readonly TSQLKeywords USE = new TSQLKeywords("USE");
		public static readonly TSQLKeywords SHUTDOWN = new TSQLKeywords("SHUTDOWN");
		public static readonly TSQLKeywords RETURN = new TSQLKeywords("RETURN");
		public static readonly TSQLKeywords REVERT = new TSQLKeywords("REVERT");

		#endregion

		#region other

		public static readonly TSQLKeywords ALL = new TSQLKeywords("ALL");
		public static readonly TSQLKeywords AND = new TSQLKeywords("AND");
		public static readonly TSQLKeywords ANY = new TSQLKeywords("ANY");
		public static readonly TSQLKeywords AS = new TSQLKeywords("AS");
		public static readonly TSQLKeywords ASC = new TSQLKeywords("ASC");
		public static readonly TSQLKeywords AUTHORIZATION = new TSQLKeywords("AUTHORIZATION");
		public static readonly TSQLKeywords BETWEEN = new TSQLKeywords("BETWEEN");
		public static readonly TSQLKeywords BROWSE = new TSQLKeywords("BROWSE");
		public static readonly TSQLKeywords BY = new TSQLKeywords("BY");
		public static readonly TSQLKeywords CASCADE = new TSQLKeywords("CASCADE");
		public static readonly TSQLKeywords CASE = new TSQLKeywords("CASE");
		public static readonly TSQLKeywords CHECK = new TSQLKeywords("CHECK");
		public static readonly TSQLKeywords CLUSTERED = new TSQLKeywords("CLUSTERED");
		public static readonly TSQLKeywords COLLATE = new TSQLKeywords("COLLATE");
		public static readonly TSQLKeywords COLUMN = new TSQLKeywords("COLUMN");
		public static readonly TSQLKeywords COMMITTED = new TSQLKeywords("COMMITTED");
		public static readonly TSQLKeywords COMPUTE = new TSQLKeywords("COMPUTE");
		public static readonly TSQLKeywords CONSTRAINT = new TSQLKeywords("CONSTRAINT");
		public static readonly TSQLKeywords CROSS = new TSQLKeywords("CROSS");
		public static readonly TSQLKeywords CURRENT = new TSQLKeywords("CURRENT");
		public static readonly TSQLKeywords CURSOR = new TSQLKeywords("CURSOR");
		public static readonly TSQLKeywords DATABASE = new TSQLKeywords("DATABASE");
		public static readonly TSQLKeywords DEFAULT = new TSQLKeywords("DEFAULT");
		public static readonly TSQLKeywords DESC = new TSQLKeywords("DESC");
		public static readonly TSQLKeywords DISK = new TSQLKeywords("DISK");
		public static readonly TSQLKeywords DISTINCT = new TSQLKeywords("DISTINCT");
		public static readonly TSQLKeywords DISTRIBUTED = new TSQLKeywords("DISTRIBUTED");
		// DOUBLE PRECISION
		public static readonly TSQLKeywords DOUBLE = new TSQLKeywords("DOUBLE");
		public static readonly TSQLKeywords ELSE = new TSQLKeywords("ELSE");
		public static readonly TSQLKeywords END = new TSQLKeywords("END");
		// public static readonly TSQLKeywords ERRLVL = new TSQLKeywords("ERRLVL");
		public static readonly TSQLKeywords ESCAPE = new TSQLKeywords("ESCAPE");
		public static readonly TSQLKeywords EXCEPT = new TSQLKeywords("EXCEPT");
		public static readonly TSQLKeywords EXISTS = new TSQLKeywords("EXISTS");
		// public static readonly TSQLKeywords EXIT = new TSQLKeywords("EXIT");
		public static readonly TSQLKeywords EXTERNAL = new TSQLKeywords("EXTERNAL");
		public static readonly TSQLKeywords FILE = new TSQLKeywords("FILE");
		public static readonly TSQLKeywords FILLFACTOR = new TSQLKeywords("FILLFACTOR");
		public static readonly TSQLKeywords FOR = new TSQLKeywords("FOR");
		public static readonly TSQLKeywords FOREIGN = new TSQLKeywords("FOREIGN");
		public static readonly TSQLKeywords FROM = new TSQLKeywords("FROM");
		public static readonly TSQLKeywords FULL = new TSQLKeywords("FULL");
		public static readonly TSQLKeywords FUNCTION = new TSQLKeywords("FUNCTION");
		public static readonly TSQLKeywords GROUP = new TSQLKeywords("GROUP");
		public static readonly TSQLKeywords HAVING = new TSQLKeywords("HAVING");
		public static readonly TSQLKeywords HOLDLOCK = new TSQLKeywords("HOLDLOCK");
		public static readonly TSQLKeywords IDENTITY = new TSQLKeywords("IDENTITY");
		public static readonly TSQLKeywords IDENTITY_INSERT = new TSQLKeywords("IDENTITY_INSERT");
		public static readonly TSQLKeywords IDENTITYCOL = new TSQLKeywords("IDENTITYCOL");
		public static readonly TSQLKeywords IN = new TSQLKeywords("IN");
		public static readonly TSQLKeywords INDEX = new TSQLKeywords("INDEX");
		public static readonly TSQLKeywords INNER = new TSQLKeywords("INNER");
		public static readonly TSQLKeywords INTERSECT = new TSQLKeywords("INTERSECT");
		public static readonly TSQLKeywords INTO = new TSQLKeywords("INTO");
		public static readonly TSQLKeywords IS = new TSQLKeywords("IS");
		public static readonly TSQLKeywords JOIN = new TSQLKeywords("JOIN");
		public static readonly TSQLKeywords KEY = new TSQLKeywords("KEY");
		public static readonly TSQLKeywords LEFT = new TSQLKeywords("LEFT");
		public static readonly TSQLKeywords LIKE = new TSQLKeywords("LIKE");
		public static readonly TSQLKeywords LINENO = new TSQLKeywords("LINENO");
		// public static readonly TSQLKeywords NATIONAL = new TSQLKeywords("NATIONAL");
		public static readonly TSQLKeywords NOCHECK = new TSQLKeywords("NOCHECK");
		public static readonly TSQLKeywords NONCLUSTERED = new TSQLKeywords("NONCLUSTERED");
		public static readonly TSQLKeywords NOT = new TSQLKeywords("NOT");
		public static readonly TSQLKeywords NULL = new TSQLKeywords("NULL");
		// public static readonly TSQLKeywords OF = new TSQLKeywords("OF");
		public static readonly TSQLKeywords OFF = new TSQLKeywords("OFF");
		public static readonly TSQLKeywords OFFSETS = new TSQLKeywords("OFFSETS");
		public static readonly TSQLKeywords ON = new TSQLKeywords("ON");
		public static readonly TSQLKeywords OPTION = new TSQLKeywords("OPTION");
		public static readonly TSQLKeywords OR = new TSQLKeywords("OR");
		public static readonly TSQLKeywords ORDER = new TSQLKeywords("ORDER");
		public static readonly TSQLKeywords OUTER = new TSQLKeywords("OUTER");
		public static readonly TSQLKeywords OVER = new TSQLKeywords("OVER");
		public static readonly TSQLKeywords PERCENT = new TSQLKeywords("PERCENT");
		public static readonly TSQLKeywords PIVOT = new TSQLKeywords("PIVOT");
		public static readonly TSQLKeywords PLAN = new TSQLKeywords("PLAN");
		// DOUBLE PRECISION
		public static readonly TSQLKeywords PRECISION = new TSQLKeywords("PRECISION");
		public static readonly TSQLKeywords PRIMARY = new TSQLKeywords("PRIMARY");
		public static readonly TSQLKeywords PROCEDURE = new TSQLKeywords("PROCEDURE");
		public static readonly TSQLKeywords PUBLIC = new TSQLKeywords("PUBLIC");
		public static readonly TSQLKeywords READ = new TSQLKeywords("READ");
		public static readonly TSQLKeywords REPEATABLE = new TSQLKeywords("REPEATABLE");
		public static readonly TSQLKeywords RECONFIGURE = new TSQLKeywords("RECONFIGURE");
		public static readonly TSQLKeywords REFERENCES = new TSQLKeywords("REFERENCES");
		public static readonly TSQLKeywords REPLICATION = new TSQLKeywords("REPLICATION");
		// public static readonly TSQLKeywords RESTRICT = new TSQLKeywords("RESTRICT");
		public static readonly TSQLKeywords RETURNS = new TSQLKeywords("RETURNS");
		public static readonly TSQLKeywords RIGHT = new TSQLKeywords("RIGHT");
		public static readonly TSQLKeywords ROWCOUNT = new TSQLKeywords("ROWCOUNT");
		public static readonly TSQLKeywords ROWGUIDCOL = new TSQLKeywords("ROWGUIDCOL");
		public static readonly TSQLKeywords RULE = new TSQLKeywords("RULE");
		// SAVE TRANSACTION
		public static readonly TSQLKeywords SAVE = new TSQLKeywords("SAVE");
		public static readonly TSQLKeywords SCHEMA = new TSQLKeywords("SCHEMA");
		// public static readonly TSQLKeywords SECURITYAUDIT = new TSQLKeywords("SECURITYAUDIT");
		public static readonly TSQLKeywords SETUSER = new TSQLKeywords("SETUSER");
		public static readonly TSQLKeywords SOME = new TSQLKeywords("SOME");
		public static readonly TSQLKeywords STATISTICS = new TSQLKeywords("STATISTICS");
		public static readonly TSQLKeywords TABLE = new TSQLKeywords("TABLE");
		public static readonly TSQLKeywords TABLESAMPLE = new TSQLKeywords("TABLESAMPLE");
		public static readonly TSQLKeywords TEXTSIZE = new TSQLKeywords("TEXTSIZE");
		public static readonly TSQLKeywords THEN = new TSQLKeywords("THEN");
		// public static readonly TSQLKeywords TO = new TSQLKeywords("TO");
		public static readonly TSQLKeywords TOP = new TSQLKeywords("TOP");
		public static readonly TSQLKeywords TRANSACTION = new TSQLKeywords("TRANSACTION");
		public static readonly TSQLKeywords TRIGGER = new TSQLKeywords("TRIGGER");
		// public static readonly TSQLKeywords TSEQUAL = new TSQLKeywords("TSEQUAL");
		public static readonly TSQLKeywords UNION = new TSQLKeywords("UNION");
		public static readonly TSQLKeywords UNIQUE = new TSQLKeywords("UNIQUE");
		public static readonly TSQLKeywords UNPIVOT = new TSQLKeywords("UNPIVOT");
		// public static readonly TSQLKeywords USER = new TSQLKeywords("USER");
		public static readonly TSQLKeywords VALUES = new TSQLKeywords("VALUES");
		public static readonly TSQLKeywords VARYING = new TSQLKeywords("VARYING");
		public static readonly TSQLKeywords VIEW = new TSQLKeywords("VIEW");
		public static readonly TSQLKeywords WHEN = new TSQLKeywords("WHEN");
		public static readonly TSQLKeywords WHERE = new TSQLKeywords("WHERE");
		// WITHIN {GROUP}
		public static readonly TSQLKeywords WITHIN = new TSQLKeywords("WITHIN");

#pragma warning restore 1591

		#endregion

		private readonly string Keyword;

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

		public bool In(params TSQLKeywords[] keywords)
		{
			return
				keywords != null &&
				keywords.Contains(this);
		}

#pragma warning disable 1591

		public static bool operator ==(
			TSQLKeywords a,
			TSQLKeywords b)
		{
			if (Object.ReferenceEquals(a, null))
			{
				if (Object.ReferenceEquals(b, null))
				{
					// null == null = true.
					return true;
				}

				// Only the left side is null.
				return false;
			}

			// Equals handles case of null on right side.
			return a.Equals(b);
		}

		public static bool operator !=(
			TSQLKeywords a,
			TSQLKeywords b)
		{
			return !(a == b);
		}

		private bool Equals(TSQLKeywords obj)
		{
			// If parameter is null, return false.
			if (Object.ReferenceEquals(obj, null))
			{
				return false;
			}

			// Optimization for a common success case.
			if (Object.ReferenceEquals(this, obj))
			{
				return true;
			}

			// If run-time types are not exactly the same, return false.
			if (this.GetType() != obj.GetType())
				return false;

			// Return true if the fields match.
			// Note that the base class is not invoked because it is
			// System.Object, which defines Equals as reference equality.
			return Keyword == obj.Keyword;
		}

		public override bool Equals(object obj)
		{
			if (obj is TSQLKeywords)
			{
				return Equals((TSQLKeywords)obj);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return Keyword.GetHashCode();
		}

#pragma warning restore 1591
	}
}
