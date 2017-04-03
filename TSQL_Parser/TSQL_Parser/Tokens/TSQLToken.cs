using System;

namespace TSQL.Tokens
{
	public abstract class TSQLToken
	{
		protected TSQLToken(
			int beginPostion,
			string text)
		{
			BeginPostion = beginPostion;
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			Text = text;
		}

		public int BeginPostion
		{
			get;
			private set;
		}

		public int EndPosition
		{
			get
			{
				return BeginPostion + Length - 1;
			}
		}

		public int Length
		{
			get
			{
				return Text.Length;
			}
		}

		public string Text
		{
			get;
			private set;
		}

		public abstract TSQLTokenType Type
		{
			get;
		}

#pragma warning disable 1591

		public static bool operator ==(
			TSQLToken a,
			TSQLToken b)
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
			TSQLToken a,
			TSQLToken b)
		{
			return !(a == b);
		}

		private bool Equals(TSQLToken obj)
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
			return
				BeginPostion == obj.BeginPostion &&
				EndPosition == obj.EndPosition &&
				Text == obj.Text;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TSQLToken);
		}

		public override int GetHashCode()
		{
			// http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode/263416
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				hash = hash * 486187739 + BeginPostion.GetHashCode();
				hash = hash * 486187739 + Text.GetHashCode();
				hash = hash * 486187739 + Type.GetHashCode();
				return hash;
			}
		}

#pragma warning restore 1591

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLCharacter"/>.
		/// </summary>
		public TSQLCharacter AsCharacter
		{
			get
			{
				return this as TSQLCharacter;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLComment"/>.
		/// </summary>
		public TSQLComment AsComment
		{
			get
			{
				return this as TSQLComment;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLIdentifier"/>.
		/// </summary>
		public TSQLIdentifier AsIdentifier
		{
			get
			{
				return this as TSQLIdentifier;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLKeyword"/>.
		/// </summary>
		public TSQLKeyword AsKeyword
		{
			get
			{
				return this as TSQLKeyword;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLLiteral"/>.
		/// </summary>
		public TSQLLiteral AsLiteral
		{
			get
			{
				return this as TSQLLiteral;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLMultilineComment"/>.
		/// </summary>
		public TSQLMultilineComment AsMultilineComment
		{
			get
			{
				return this as TSQLMultilineComment;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLNumericLiteral"/>.
		/// </summary>
		public TSQLNumericLiteral AsNumericLiteral
		{
			get
			{
				return this as TSQLNumericLiteral;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLOperator"/>.
		/// </summary>
		public TSQLOperator AsOperator
		{
			get
			{
				return this as TSQLOperator;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLSingleLineComment"/>.
		/// </summary>
		public TSQLSingleLineComment AsSingleLineComment
		{
			get
			{
				return this as TSQLSingleLineComment;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLStringLiteral"/>.
		/// </summary>
		public TSQLStringLiteral AsStringLiteral
		{
			get
			{
				return this as TSQLStringLiteral;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLVariable"/>.
		/// </summary>
		public TSQLVariable AsVariable
		{
			get
			{
				return this as TSQLVariable;
			}
		}

		/// <summary>
		///		Fluent convenience shortcut for casting object
		///		as <see cref="TSQL.Tokens.TSQLWhitespace"/>.
		/// </summary>
		public TSQLWhitespace AsWhitespace
		{
			get
			{
				return this as TSQLWhitespace;
			}
		}
	}

	public static class TSQLTokenExtensions
	{
		public static bool IsKeyword(this TSQLToken token, TSQLKeywords keyword)
		{
			if (token == null)
			{
				return false;
			}

			if (token.Type != TSQLTokenType.Keyword)
			{
				return false;
			}

			if (token.AsKeyword.Keyword != keyword)
			{
				return false;
			}

			return true;
		}

		public static bool IsCharacter(this TSQLToken token, TSQLCharacters character)
		{
			if (token == null)
			{
				return false;
			}

			if (token.Type != TSQLTokenType.Character)
			{
				return false;
			}

			if (token.AsCharacter.Character != character)
			{
				return false;
			}

			return true;
		}
	}
}
