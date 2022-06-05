using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL
{
	public struct TSQLIdentifiers
	{
		private static Dictionary<string, TSQLIdentifiers> identifierLookup =
			new Dictionary<string, TSQLIdentifiers>(StringComparer.OrdinalIgnoreCase);

		public static readonly TSQLIdentifiers None = new TSQLIdentifiers("");

#pragma warning disable 1591

		public static readonly TSQLIdentifiers COALESCE = new TSQLIdentifiers("COALESCE");
		public static readonly TSQLIdentifiers CONTAINS = new TSQLIdentifiers("CONTAINS");
		public static readonly TSQLIdentifiers CONTAINSTABLE = new TSQLIdentifiers("CONTAINSTABLE");
		public static readonly TSQLIdentifiers CONVERT = new TSQLIdentifiers("CONVERT");
		public static readonly TSQLIdentifiers CURRENT_DATE = new TSQLIdentifiers("CURRENT_DATE");
		public static readonly TSQLIdentifiers CURRENT_TIME = new TSQLIdentifiers("CURRENT_TIME");
		public static readonly TSQLIdentifiers CURRENT_TIMESTAMP = new TSQLIdentifiers("CURRENT_TIMESTAMP");
		public static readonly TSQLIdentifiers CURRENT_USER = new TSQLIdentifiers("CURRENT_USER");
		public static readonly TSQLIdentifiers FREETEXT = new TSQLIdentifiers("FREETEXT");
		public static readonly TSQLIdentifiers FREETEXTTABLE = new TSQLIdentifiers("FREETEXTTABLE");
		public static readonly TSQLIdentifiers NULLIF = new TSQLIdentifiers("NULLIF");
		public static readonly TSQLIdentifiers OPENDATASOURCE = new TSQLIdentifiers("OPENDATASOURCE");
		public static readonly TSQLIdentifiers OPENQUERY = new TSQLIdentifiers("OPENQUERY");
		public static readonly TSQLIdentifiers OPENROWSET = new TSQLIdentifiers("OPENROWSET");
		public static readonly TSQLIdentifiers OPENXML = new TSQLIdentifiers("OPENXML");
		public static readonly TSQLIdentifiers RAISERROR = new TSQLIdentifiers("RAISERROR");
		public static readonly TSQLIdentifiers SEMANTICKEYPHRASETABLE = new TSQLIdentifiers("SEMANTICKEYPHRASETABLE");
		public static readonly TSQLIdentifiers SEMANTICSIMILARITYDETAILSTABLE = new TSQLIdentifiers("SEMANTICSIMILARITYDETAILSTABLE");
		public static readonly TSQLIdentifiers SEMANTICSIMILARITYTABLE = new TSQLIdentifiers("SEMANTICSIMILARITYTABLE");
		public static readonly TSQLIdentifiers SESSION_USER = new TSQLIdentifiers("SESSION_USER");
		public static readonly TSQLIdentifiers SYSTEM_USER = new TSQLIdentifiers("SYSTEM_USER");
		public static readonly TSQLIdentifiers TRY_CONVERT = new TSQLIdentifiers("TRY_CONVERT");
		public static readonly TSQLIdentifiers BIT_LENGTH = new TSQLIdentifiers("BIT_LENGTH");
		public static readonly TSQLIdentifiers CONCAT = new TSQLIdentifiers("CONCAT");
		public static readonly TSQLIdentifiers OCTET_LENGTH = new TSQLIdentifiers("OCTET_LENGTH");
		public static readonly TSQLIdentifiers TRUNCATE = new TSQLIdentifiers("TRUNCATE");
		public static readonly TSQLIdentifiers CURDATE = new TSQLIdentifiers("CURDATE");
		public static readonly TSQLIdentifiers CURTIME = new TSQLIdentifiers("CURTIME");
		public static readonly TSQLIdentifiers DAYNAME = new TSQLIdentifiers("DAYNAME");
		public static readonly TSQLIdentifiers DAYOFMONTH = new TSQLIdentifiers("DAYOFMONTH");
		public static readonly TSQLIdentifiers DAYOFWEEK = new TSQLIdentifiers("DAYOFWEEK");
		public static readonly TSQLIdentifiers HOUR = new TSQLIdentifiers("HOUR");
		public static readonly TSQLIdentifiers MINUTE = new TSQLIdentifiers("MINUTE");
		public static readonly TSQLIdentifiers SECOND = new TSQLIdentifiers("SECOND");
		public static readonly TSQLIdentifiers MONTHNAME = new TSQLIdentifiers("MONTHNAME");
		public static readonly TSQLIdentifiers QUARTER = new TSQLIdentifiers("QUARTER");
		public static readonly TSQLIdentifiers WEEK = new TSQLIdentifiers("WEEK");
		public static readonly TSQLIdentifiers APPROX_COUNT_DISTINCT = new TSQLIdentifiers("APPROX_COUNT_DISTINCT");
		public static readonly TSQLIdentifiers AVG = new TSQLIdentifiers("AVG");
		public static readonly TSQLIdentifiers CHECKSUM_AGG = new TSQLIdentifiers("CHECKSUM_AGG");
		public static readonly TSQLIdentifiers COUNT = new TSQLIdentifiers("COUNT");
		public static readonly TSQLIdentifiers COUNT_BIG = new TSQLIdentifiers("COUNT_BIG");
		public static readonly TSQLIdentifiers GROUPING = new TSQLIdentifiers("GROUPING");
		public static readonly TSQLIdentifiers GROUPING_ID = new TSQLIdentifiers("GROUPING_ID");
		public static readonly TSQLIdentifiers MAX = new TSQLIdentifiers("MAX");
		public static readonly TSQLIdentifiers MIN = new TSQLIdentifiers("MIN");
		public static readonly TSQLIdentifiers STDEV = new TSQLIdentifiers("STDEV");
		public static readonly TSQLIdentifiers STDEVP = new TSQLIdentifiers("STDEVP");
		public static readonly TSQLIdentifiers SUM = new TSQLIdentifiers("SUM");
		public static readonly TSQLIdentifiers VAR = new TSQLIdentifiers("VAR");
		public static readonly TSQLIdentifiers VARP = new TSQLIdentifiers("VARP");
		public static readonly TSQLIdentifiers CUME_DIST = new TSQLIdentifiers("CUME_DIST");
		public static readonly TSQLIdentifiers FIRST_VALUE = new TSQLIdentifiers("FIRST_VALUE");
		public static readonly TSQLIdentifiers LAG = new TSQLIdentifiers("LAG");
		public static readonly TSQLIdentifiers LAST_VALUE = new TSQLIdentifiers("LAST_VALUE");
		public static readonly TSQLIdentifiers LEAD = new TSQLIdentifiers("LEAD");
		public static readonly TSQLIdentifiers PERCENT_RANK = new TSQLIdentifiers("PERCENT_RANK");
		public static readonly TSQLIdentifiers PERCENTILE_CONT = new TSQLIdentifiers("PERCENTILE_CONT");
		public static readonly TSQLIdentifiers PERCENTILE_DISC = new TSQLIdentifiers("PERCENTILE_DISC");
		public static readonly TSQLIdentifiers CAST = new TSQLIdentifiers("CAST");
		public static readonly TSQLIdentifiers PARSE = new TSQLIdentifiers("PARSE");
		public static readonly TSQLIdentifiers TRY_CAST = new TSQLIdentifiers("TRY_CAST");
		public static readonly TSQLIdentifiers TRY_PARSE = new TSQLIdentifiers("TRY_PARSE");
		public static readonly TSQLIdentifiers ENCRYPTBYKEY = new TSQLIdentifiers("ENCRYPTBYKEY");
		public static readonly TSQLIdentifiers DECRYPTBYKEY = new TSQLIdentifiers("DECRYPTBYKEY");
		public static readonly TSQLIdentifiers ENCRYPTBYPASSPHRASE = new TSQLIdentifiers("ENCRYPTBYPASSPHRASE");
		public static readonly TSQLIdentifiers DECRYPTBYPASSPHRASE = new TSQLIdentifiers("DECRYPTBYPASSPHRASE");
		public static readonly TSQLIdentifiers KEY_ID = new TSQLIdentifiers("KEY_ID");
		public static readonly TSQLIdentifiers KEY_GUID = new TSQLIdentifiers("KEY_GUID");
		public static readonly TSQLIdentifiers DECRYPTBYKEYAUTOASYMKEY = new TSQLIdentifiers("DECRYPTBYKEYAUTOASYMKEY");
		public static readonly TSQLIdentifiers KEY_NAME = new TSQLIdentifiers("KEY_NAME");
		public static readonly TSQLIdentifiers SYMKEYPROPERTY = new TSQLIdentifiers("SYMKEYPROPERTY");
		public static readonly TSQLIdentifiers ENCRYPTBYASYMKEY = new TSQLIdentifiers("ENCRYPTBYASYMKEY");
		public static readonly TSQLIdentifiers DECRYPTBYASYMKEY = new TSQLIdentifiers("DECRYPTBYASYMKEY");
		public static readonly TSQLIdentifiers ENCRYPTBYCERT = new TSQLIdentifiers("ENCRYPTBYCERT");
		public static readonly TSQLIdentifiers DECRYPTBYCERT = new TSQLIdentifiers("DECRYPTBYCERT");
		public static readonly TSQLIdentifiers ASYMKEYPROPERTY = new TSQLIdentifiers("ASYMKEYPROPERTY");
		public static readonly TSQLIdentifiers ASYMKEY_ID = new TSQLIdentifiers("ASYMKEY_ID");
		public static readonly TSQLIdentifiers SIGNBYASYMKEY = new TSQLIdentifiers("SIGNBYASYMKEY");
		public static readonly TSQLIdentifiers VERIFYSIGNEDBYASMKEY = new TSQLIdentifiers("VERIFYSIGNEDBYASMKEY");
		public static readonly TSQLIdentifiers SIGNBYCERT = new TSQLIdentifiers("SIGNBYCERT");
		public static readonly TSQLIdentifiers VERIGYSIGNEDBYCERT = new TSQLIdentifiers("VERIGYSIGNEDBYCERT");
		public static readonly TSQLIdentifiers IS_OBJECTSIGNED = new TSQLIdentifiers("IS_OBJECTSIGNED");
		public static readonly TSQLIdentifiers DECRYPTBYKEYAUTOCERT = new TSQLIdentifiers("DECRYPTBYKEYAUTOCERT");
		public static readonly TSQLIdentifiers HASHBYTES = new TSQLIdentifiers("HASHBYTES");
		public static readonly TSQLIdentifiers CERTENCODED = new TSQLIdentifiers("CERTENCODED");
		public static readonly TSQLIdentifiers CERTPRIVATEKEY = new TSQLIdentifiers("CERTPRIVATEKEY");
		public static readonly TSQLIdentifiers CURSOR_STATUS = new TSQLIdentifiers("CURSOR_STATUS");
		public static readonly TSQLIdentifiers DATALENGTH = new TSQLIdentifiers("DATALENGTH");
		public static readonly TSQLIdentifiers IDENT_SEED = new TSQLIdentifiers("IDENT_SEED");
		public static readonly TSQLIdentifiers IDENT_CURRENT = new TSQLIdentifiers("IDENT_CURRENT");
		public static readonly TSQLIdentifiers IDENTITY = new TSQLIdentifiers("IDENTITY");
		public static readonly TSQLIdentifiers IDENT_INCR = new TSQLIdentifiers("IDENT_INCR");
		public static readonly TSQLIdentifiers SQL_VARIANT_PROPERTY = new TSQLIdentifiers("SQL_VARIANT_PROPERTY");
		public static readonly TSQLIdentifiers MONTH = new TSQLIdentifiers("MONTH");
		public static readonly TSQLIdentifiers YEAR = new TSQLIdentifiers("YEAR");
		public static readonly TSQLIdentifiers DATEFROMPARTS = new TSQLIdentifiers("DATEFROMPARTS");
		public static readonly TSQLIdentifiers DATETIME2FROMPARTS = new TSQLIdentifiers("DATETIME2FROMPARTS");
		public static readonly TSQLIdentifiers DATETIMEFROMPARTS = new TSQLIdentifiers("DATETIMEFROMPARTS");
		public static readonly TSQLIdentifiers DATETIMEOFFSETFROMPARTS = new TSQLIdentifiers("DATETIMEOFFSETFROMPARTS");
		public static readonly TSQLIdentifiers SMALLDATETIMEFROMPARTS = new TSQLIdentifiers("SMALLDATETIMEFROMPARTS");
		public static readonly TSQLIdentifiers TIMEFROMPARTS = new TSQLIdentifiers("TIMEFROMPARTS");
		public static readonly TSQLIdentifiers DATEDIFF = new TSQLIdentifiers("DATEDIFF");
		public static readonly TSQLIdentifiers DATEDIFF_BIG = new TSQLIdentifiers("DATEDIFF_BIG");
		public static readonly TSQLIdentifiers DATEADD = new TSQLIdentifiers("DATEADD");
		public static readonly TSQLIdentifiers EOMONTH = new TSQLIdentifiers("EOMONTH");
		public static readonly TSQLIdentifiers SWITCHOFFSET = new TSQLIdentifiers("SWITCHOFFSET");
		public static readonly TSQLIdentifiers TODATETIMEOFFSET = new TSQLIdentifiers("TODATETIMEOFFSET");
		public static readonly TSQLIdentifiers ISDATE = new TSQLIdentifiers("ISDATE");
		public static readonly TSQLIdentifiers ISJSON = new TSQLIdentifiers("ISJSON");
		public static readonly TSQLIdentifiers JSON_VALUE = new TSQLIdentifiers("JSON_VALUE");
		public static readonly TSQLIdentifiers JSON_QUERY = new TSQLIdentifiers("JSON_QUERY");
		public static readonly TSQLIdentifiers JSON_MODIFY = new TSQLIdentifiers("JSON_MODIFY");
		public static readonly TSQLIdentifiers ABS = new TSQLIdentifiers("ABS");
		public static readonly TSQLIdentifiers ACOS = new TSQLIdentifiers("ACOS");
		public static readonly TSQLIdentifiers ASIN = new TSQLIdentifiers("ASIN");
		public static readonly TSQLIdentifiers ATAN = new TSQLIdentifiers("ATAN");
		public static readonly TSQLIdentifiers ATN2 = new TSQLIdentifiers("ATN2");
		public static readonly TSQLIdentifiers CEILING = new TSQLIdentifiers("CEILING");
		public static readonly TSQLIdentifiers COS = new TSQLIdentifiers("COS");
		public static readonly TSQLIdentifiers COT = new TSQLIdentifiers("COT");
		public static readonly TSQLIdentifiers DEGREES = new TSQLIdentifiers("DEGREES");
		public static readonly TSQLIdentifiers EXP = new TSQLIdentifiers("EXP");
		public static readonly TSQLIdentifiers FLOOR = new TSQLIdentifiers("FLOOR");
		public static readonly TSQLIdentifiers LOG = new TSQLIdentifiers("LOG");
		public static readonly TSQLIdentifiers LOG10 = new TSQLIdentifiers("LOG10");
		public static readonly TSQLIdentifiers PI = new TSQLIdentifiers("PI");
		public static readonly TSQLIdentifiers POWER = new TSQLIdentifiers("POWER");
		public static readonly TSQLIdentifiers RADIANS = new TSQLIdentifiers("RADIANS");
		public static readonly TSQLIdentifiers RAND = new TSQLIdentifiers("RAND");
		public static readonly TSQLIdentifiers ROUND = new TSQLIdentifiers("ROUND");
		public static readonly TSQLIdentifiers SIGN = new TSQLIdentifiers("SIGN");
		public static readonly TSQLIdentifiers SIN = new TSQLIdentifiers("SIN");
		public static readonly TSQLIdentifiers SQRT = new TSQLIdentifiers("SQRT");
		public static readonly TSQLIdentifiers SQUARE = new TSQLIdentifiers("SQUARE");
		public static readonly TSQLIdentifiers TAN = new TSQLIdentifiers("TAN");
		public static readonly TSQLIdentifiers CHOOSE = new TSQLIdentifiers("CHOOSE");
		public static readonly TSQLIdentifiers IIF = new TSQLIdentifiers("IIF");
		public static readonly TSQLIdentifiers APP_NAME = new TSQLIdentifiers("APP_NAME");
		public static readonly TSQLIdentifiers APPLOCK_MODE = new TSQLIdentifiers("APPLOCK_MODE");
		public static readonly TSQLIdentifiers APPLOCK_TEST = new TSQLIdentifiers("APPLOCK_TEST");
		public static readonly TSQLIdentifiers ASSEMBLYPROPERTY = new TSQLIdentifiers("ASSEMBLYPROPERTY");
		public static readonly TSQLIdentifiers COL_LENGTH = new TSQLIdentifiers("COL_LENGTH");
		public static readonly TSQLIdentifiers COL_NAME = new TSQLIdentifiers("COL_NAME");
		public static readonly TSQLIdentifiers COLUMNPROPERTY = new TSQLIdentifiers("COLUMNPROPERTY");
		public static readonly TSQLIdentifiers DATABASE_PRINCIPAL_ID = new TSQLIdentifiers("DATABASE_PRINCIPAL_ID");
		public static readonly TSQLIdentifiers DATABASEPROPERTYEX = new TSQLIdentifiers("DATABASEPROPERTYEX");
		public static readonly TSQLIdentifiers DB_ID = new TSQLIdentifiers("DB_ID");
		public static readonly TSQLIdentifiers DB_NAME = new TSQLIdentifiers("DB_NAME");
		public static readonly TSQLIdentifiers FILE_ID = new TSQLIdentifiers("FILE_ID");
		public static readonly TSQLIdentifiers FILE_IDEX = new TSQLIdentifiers("FILE_IDEX");
		public static readonly TSQLIdentifiers FILE_NAME = new TSQLIdentifiers("FILE_NAME");
		public static readonly TSQLIdentifiers FILEGROUP_ID = new TSQLIdentifiers("FILEGROUP_ID");
		public static readonly TSQLIdentifiers FILEGROUP_NAME = new TSQLIdentifiers("FILEGROUP_NAME");
		public static readonly TSQLIdentifiers FILEGROUPPROPERTY = new TSQLIdentifiers("FILEGROUPPROPERTY");
		public static readonly TSQLIdentifiers FILEPROPERTY = new TSQLIdentifiers("FILEPROPERTY");
		public static readonly TSQLIdentifiers FULLTEXTCATALOGPROPERTY = new TSQLIdentifiers("FULLTEXTCATALOGPROPERTY");
		public static readonly TSQLIdentifiers FULLTEXTSERVICEPROPERTY = new TSQLIdentifiers("FULLTEXTSERVICEPROPERTY");
		public static readonly TSQLIdentifiers INDEX_COL = new TSQLIdentifiers("INDEX_COL");
		public static readonly TSQLIdentifiers INDEXKEY_PROPERTY = new TSQLIdentifiers("INDEXKEY_PROPERTY");
		public static readonly TSQLIdentifiers INDEXPROPERTY = new TSQLIdentifiers("INDEXPROPERTY");
		public static readonly TSQLIdentifiers OBJECT_DEFINITION = new TSQLIdentifiers("OBJECT_DEFINITION");
		public static readonly TSQLIdentifiers OBJECT_ID = new TSQLIdentifiers("OBJECT_ID");
		public static readonly TSQLIdentifiers OBJECT_NAME = new TSQLIdentifiers("OBJECT_NAME");
		public static readonly TSQLIdentifiers OBJECT_SCHEMA_NAME = new TSQLIdentifiers("OBJECT_SCHEMA_NAME");
		public static readonly TSQLIdentifiers OBJECTPROPERTY = new TSQLIdentifiers("OBJECTPROPERTY");
		public static readonly TSQLIdentifiers OBJECTPROPERTYEX = new TSQLIdentifiers("OBJECTPROPERTYEX");
		public static readonly TSQLIdentifiers ORIGINAL_DB_NAME = new TSQLIdentifiers("ORIGINAL_DB_NAME");
		public static readonly TSQLIdentifiers PARSENAME = new TSQLIdentifiers("PARSENAME");
		public static readonly TSQLIdentifiers SCHEMA_ID = new TSQLIdentifiers("SCHEMA_ID");
		public static readonly TSQLIdentifiers SCHEMA_NAME = new TSQLIdentifiers("SCHEMA_NAME");
		public static readonly TSQLIdentifiers SCOPE_IDENTITY = new TSQLIdentifiers("SCOPE_IDENTITY");
		public static readonly TSQLIdentifiers SERVERPROPERTY = new TSQLIdentifiers("SERVERPROPERTY");
		public static readonly TSQLIdentifiers STATS_DATE = new TSQLIdentifiers("STATS_DATE");
		public static readonly TSQLIdentifiers TYPE_ID = new TSQLIdentifiers("TYPE_ID");
		public static readonly TSQLIdentifiers TYPE_NAME = new TSQLIdentifiers("TYPE_NAME");
		public static readonly TSQLIdentifiers TYPEPROPERTY = new TSQLIdentifiers("TYPEPROPERTY");
		public static readonly TSQLIdentifiers VERSION = new TSQLIdentifiers("VERSION");
		public static readonly TSQLIdentifiers RANK = new TSQLIdentifiers("RANK");
		public static readonly TSQLIdentifiers NTILE = new TSQLIdentifiers("NTILE");
		public static readonly TSQLIdentifiers DENSE_RANK = new TSQLIdentifiers("DENSE_RANK");
		public static readonly TSQLIdentifiers ROW_NUMBER = new TSQLIdentifiers("ROW_NUMBER");
		public static readonly TSQLIdentifiers PUBLISHINGSERVERNAME = new TSQLIdentifiers("PUBLISHINGSERVERNAME");
		public static readonly TSQLIdentifiers PWDCOMPARE = new TSQLIdentifiers("PWDCOMPARE");
		public static readonly TSQLIdentifiers PWDENCRYPT = new TSQLIdentifiers("PWDENCRYPT");
		public static readonly TSQLIdentifiers SUSER_ID = new TSQLIdentifiers("SUSER_ID");
		public static readonly TSQLIdentifiers SUSER_SID = new TSQLIdentifiers("SUSER_SID");
		public static readonly TSQLIdentifiers HAS_PERMS_BY_NAME = new TSQLIdentifiers("HAS_PERMS_BY_NAME");
		public static readonly TSQLIdentifiers SUSER_SNAME = new TSQLIdentifiers("SUSER_SNAME");
		public static readonly TSQLIdentifiers IS_MEMBER = new TSQLIdentifiers("IS_MEMBER");
		public static readonly TSQLIdentifiers IS_ROLEMEMBER = new TSQLIdentifiers("IS_ROLEMEMBER");
		public static readonly TSQLIdentifiers SUSER_NAME = new TSQLIdentifiers("SUSER_NAME");
		public static readonly TSQLIdentifiers IS_SRVROLEMEMBER = new TSQLIdentifiers("IS_SRVROLEMEMBER");
		public static readonly TSQLIdentifiers USER_ID = new TSQLIdentifiers("USER_ID");
		public static readonly TSQLIdentifiers LOGINPROPERTY = new TSQLIdentifiers("LOGINPROPERTY");
		public static readonly TSQLIdentifiers USER_NAME = new TSQLIdentifiers("USER_NAME");
		public static readonly TSQLIdentifiers ORIGINAL_LOGIN = new TSQLIdentifiers("ORIGINAL_LOGIN");
		public static readonly TSQLIdentifiers PERMISSIONS = new TSQLIdentifiers("PERMISSIONS");
		public static readonly TSQLIdentifiers ASCII = new TSQLIdentifiers("ASCII");
		public static readonly TSQLIdentifiers CHAR = new TSQLIdentifiers("CHAR");
		public static readonly TSQLIdentifiers CHARINDEX = new TSQLIdentifiers("CHARINDEX");
		public static readonly TSQLIdentifiers CONCAT_WS = new TSQLIdentifiers("CONCAT_WS");
		public static readonly TSQLIdentifiers DIFFERENCE = new TSQLIdentifiers("DIFFERENCE");
		public static readonly TSQLIdentifiers FORMAT = new TSQLIdentifiers("FORMAT");
		public static readonly TSQLIdentifiers LEFT = new TSQLIdentifiers("LEFT");
		public static readonly TSQLIdentifiers LEN = new TSQLIdentifiers("LEN");
		public static readonly TSQLIdentifiers LOWER = new TSQLIdentifiers("LOWER");
		public static readonly TSQLIdentifiers LTRIM = new TSQLIdentifiers("LTRIM");
		public static readonly TSQLIdentifiers NCHAR = new TSQLIdentifiers("NCHAR");
		public static readonly TSQLIdentifiers PATINDEX = new TSQLIdentifiers("PATINDEX");
		public static readonly TSQLIdentifiers QUOTENAME = new TSQLIdentifiers("QUOTENAME");
		public static readonly TSQLIdentifiers REPLACE = new TSQLIdentifiers("REPLACE");
		public static readonly TSQLIdentifiers REPLICATE = new TSQLIdentifiers("REPLICATE");
		public static readonly TSQLIdentifiers REVERSE = new TSQLIdentifiers("REVERSE");
		public static readonly TSQLIdentifiers RIGHT = new TSQLIdentifiers("RIGHT");
		public static readonly TSQLIdentifiers RTRIM = new TSQLIdentifiers("RTRIM");
		public static readonly TSQLIdentifiers SOUNDEX = new TSQLIdentifiers("SOUNDEX");
		public static readonly TSQLIdentifiers SPACE = new TSQLIdentifiers("SPACE");
		public static readonly TSQLIdentifiers STR = new TSQLIdentifiers("STR");
		public static readonly TSQLIdentifiers STRING_AGG = new TSQLIdentifiers("STRING_AGG");
		public static readonly TSQLIdentifiers STRING_ESCAPE = new TSQLIdentifiers("STRING_ESCAPE");
		public static readonly TSQLIdentifiers STRING_SPLIT = new TSQLIdentifiers("STRING_SPLIT");
		public static readonly TSQLIdentifiers STUFF = new TSQLIdentifiers("STUFF");
		public static readonly TSQLIdentifiers SUBSTRING = new TSQLIdentifiers("SUBSTRING");
		public static readonly TSQLIdentifiers TRANSLATE = new TSQLIdentifiers("TRANSLATE");
		public static readonly TSQLIdentifiers TRIM = new TSQLIdentifiers("TRIM");
		public static readonly TSQLIdentifiers UNICODE = new TSQLIdentifiers("UNICODE");
		public static readonly TSQLIdentifiers UPPER = new TSQLIdentifiers("UPPER");
		public static readonly TSQLIdentifiers ERROR_PROCEDURE = new TSQLIdentifiers("ERROR_PROCEDURE");
		public static readonly TSQLIdentifiers ERROR_SEVERITY = new TSQLIdentifiers("ERROR_SEVERITY");
		public static readonly TSQLIdentifiers ERROR_STATE = new TSQLIdentifiers("ERROR_STATE");
		public static readonly TSQLIdentifiers FORMATMESSAGE = new TSQLIdentifiers("FORMATMESSAGE");
		public static readonly TSQLIdentifiers GET_FILESTREAM_TRANSACTION_CONTEXT = new TSQLIdentifiers("GET_FILESTREAM_TRANSACTION_CONTEXT");
		public static readonly TSQLIdentifiers GETANSINULL = new TSQLIdentifiers("GETANSINULL");
		public static readonly TSQLIdentifiers BINARY_CHECKSUM = new TSQLIdentifiers("BINARY_CHECKSUM");
		public static readonly TSQLIdentifiers HOST_ID = new TSQLIdentifiers("HOST_ID");
		public static readonly TSQLIdentifiers CHECKSUM = new TSQLIdentifiers("CHECKSUM");
		public static readonly TSQLIdentifiers HOST_NAME = new TSQLIdentifiers("HOST_NAME");
		public static readonly TSQLIdentifiers COMPRESS = new TSQLIdentifiers("COMPRESS");
		public static readonly TSQLIdentifiers ISNULL = new TSQLIdentifiers("ISNULL");
		public static readonly TSQLIdentifiers CONNECTIONPROPERTY = new TSQLIdentifiers("CONNECTIONPROPERTY");
		public static readonly TSQLIdentifiers ISNUMERIC = new TSQLIdentifiers("ISNUMERIC");
		public static readonly TSQLIdentifiers CONTEXT_INFO = new TSQLIdentifiers("CONTEXT_INFO");
		public static readonly TSQLIdentifiers MIN_ACTIVE_ROWVERSION = new TSQLIdentifiers("MIN_ACTIVE_ROWVERSION");
		public static readonly TSQLIdentifiers CURRENT_REQUEST_ID = new TSQLIdentifiers("CURRENT_REQUEST_ID");
		public static readonly TSQLIdentifiers NEWID = new TSQLIdentifiers("NEWID");
		public static readonly TSQLIdentifiers CURRENT_TRANSACTION_ID = new TSQLIdentifiers("CURRENT_TRANSACTION_ID");
		public static readonly TSQLIdentifiers NEWSEQUENTIALID = new TSQLIdentifiers("NEWSEQUENTIALID");
		public static readonly TSQLIdentifiers DECOMPRESS = new TSQLIdentifiers("DECOMPRESS");
		public static readonly TSQLIdentifiers ROWCOUNT_BIG = new TSQLIdentifiers("ROWCOUNT_BIG");
		public static readonly TSQLIdentifiers ERROR_LINE = new TSQLIdentifiers("ERROR_LINE");
		public static readonly TSQLIdentifiers SESSION_CONTEXT = new TSQLIdentifiers("SESSION_CONTEXT");
		public static readonly TSQLIdentifiers ERROR_MESSAGE = new TSQLIdentifiers("ERROR_MESSAGE");
		public static readonly TSQLIdentifiers SESSION_ID = new TSQLIdentifiers("SESSION_ID");
		public static readonly TSQLIdentifiers ERROR_NUMBER = new TSQLIdentifiers("ERROR_NUMBER");
		public static readonly TSQLIdentifiers XACT_STATE = new TSQLIdentifiers("XACT_STATE");
		public static readonly TSQLIdentifiers TEXTPTR = new TSQLIdentifiers("TEXTPTR");
		public static readonly TSQLIdentifiers TEXTVALID = new TSQLIdentifiers("TEXTVALID");
		public static readonly TSQLIdentifiers COLUMNS_UPDATED = new TSQLIdentifiers("COLUMNS_UPDATED");
		public static readonly TSQLIdentifiers EVENTDATA = new TSQLIdentifiers("EVENTDATA");
		public static readonly TSQLIdentifiers TRIGGER_NESTLEVEL = new TSQLIdentifiers("TRIGGER_NESTLEVEL");

#pragma warning restore 1591

		private readonly string Identifier;

		private TSQLIdentifiers(
			string identifier)
		{
			Identifier = identifier;
			if (identifier.Length > 0)
			{
				identifierLookup[identifier] = this;
			}
		}

		public static TSQLIdentifiers Parse(
			string token)
		{
			if (
				!string.IsNullOrEmpty(token) &&
				identifierLookup.ContainsKey(token))
			{
				return identifierLookup[token];
			}
			else
			{
				return TSQLIdentifiers.None;
			}
		}

		public static bool IsIdentifier(
			string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
			{
				return identifierLookup.ContainsKey(token);
			}
			else
			{
				return false;
			}
		}

		public bool In(params TSQLIdentifiers[] identifiers)
		{
			return
				identifiers != null &&
				identifiers.Contains(this);
		}

#pragma warning disable 1591

		public static bool operator ==(
			TSQLIdentifiers a,
			TSQLIdentifiers b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(
			TSQLIdentifiers a,
			TSQLIdentifiers b)
		{
			return !(a == b);
		}

		public bool Equals(TSQLIdentifiers obj)
		{
			return Identifier == obj.Identifier;
		}

		public override bool Equals(object obj)
		{
			if (obj is TSQLIdentifiers)
			{
				return Equals((TSQLIdentifiers)obj);
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode();
		}

#pragma warning restore 1591
	}
}
