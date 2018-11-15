using System;

namespace TSQL.Tokens
{
	public abstract class TSQLToken
	{
		internal protected TSQLToken(
			int beginPosition,
			string text)
		{
			BeginPosition = beginPosition;
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			Text = text;
		}

		public int BeginPosition
		{
			get;
			private set;
		}

		public int EndPosition
		{
			get
			{
				return BeginPosition + Length - 1;
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
		///		as <see cref="TSQL.Tokens.TSQLSystemIdentifier"/>.
		/// </summary>
		public TSQLSystemIdentifier AsSystemIdentifier
		{
			get
			{
				return this as TSQLSystemIdentifier;
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
		///		as <see cref="TSQL.Tokens.TSQLSystemVariable"/>.
		/// </summary>
		public TSQLSystemVariable AsSystemVariable
		{
			get
			{
				return this as TSQLSystemVariable;
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
