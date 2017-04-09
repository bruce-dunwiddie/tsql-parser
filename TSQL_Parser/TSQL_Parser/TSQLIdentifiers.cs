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

		public static TSQLIdentifiers None = new TSQLIdentifiers("");

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
