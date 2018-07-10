using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL
{
	public struct TSQLIdentifiers
	{
		private static Dictionary<string, TSQLIdentifiers> identifierLookup =
			new Dictionary<string, TSQLIdentifiers>(StringComparer.InvariantCultureIgnoreCase);

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
