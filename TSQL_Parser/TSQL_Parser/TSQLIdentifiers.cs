using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL
{
	public class TSQLIdentifiers
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

		private string Identifier;

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
			TSQLIdentifiers a,
			TSQLIdentifiers b)
		{
			return !(a == b);
		}

		public bool Equals(TSQLIdentifiers obj)
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
			return Identifier == obj.Identifier;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLIdentifiers);
		}

		public override int GetHashCode()
		{
			return Identifier.GetHashCode();
		}

#pragma warning restore 1591
	}
}
