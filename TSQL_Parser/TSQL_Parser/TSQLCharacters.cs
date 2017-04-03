using System;
using System.Collections.Generic;
using System.Linq;

namespace TSQL
{
	public class TSQLCharacters
	{
		private static Dictionary<string, TSQLCharacters> characterLookup =
			new Dictionary<string, TSQLCharacters>(StringComparer.InvariantCultureIgnoreCase);

		public static readonly TSQLCharacters None = new TSQLCharacters(string.Empty);

#pragma warning disable 1591

		public static readonly TSQLCharacters Comma = new TSQLCharacters(",");
		public static readonly TSQLCharacters Semicolon = new TSQLCharacters(";");
		public static readonly TSQLCharacters OpenParentheses = new TSQLCharacters("(");
		public static readonly TSQLCharacters CloseParentheses = new TSQLCharacters(")");
		public static readonly TSQLCharacters Space = new TSQLCharacters(" ");
		public static readonly TSQLCharacters Tab = new TSQLCharacters("\t");
		public static readonly TSQLCharacters CarriageReturn = new TSQLCharacters("\r");
		public static readonly TSQLCharacters LineFeed = new TSQLCharacters("\n");
		public static readonly TSQLCharacters Period = new TSQLCharacters(".");

#pragma warning restore 1591

		private string Token;

		private TSQLCharacters(
			string token)
		{
			Token = token;
			if (token.Length > 0)
			{
				characterLookup[token] = this;
			}
		}

		public static TSQLCharacters Parse(
			string token)
		{
			if (
				!string.IsNullOrEmpty(token) &&
				characterLookup.ContainsKey(token))
			{
				return characterLookup[token];
			}
			else
			{
				return TSQLCharacters.None;
			}
		}

		public static bool IsCharacter(
			string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
			{
				return characterLookup.ContainsKey(token);
			}
			else
			{
				return false;
			}
		}

		public bool In(params TSQLCharacters[] characters)
		{
			return
				characters != null &&
				characters.Contains(this);
		}

#pragma warning disable 1591

		public static bool operator ==(
			TSQLCharacters a,
			TSQLCharacters b)
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
			TSQLCharacters a,
			TSQLCharacters b)
		{
			return !(a == b);
		}

		public bool Equals(TSQLCharacters obj)
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
			return Token == obj.Token;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLCharacters);
		}

		public override int GetHashCode()
		{
			return Token.GetHashCode();
		}

#pragma warning restore 1591
	}

}
