using System;

namespace TSQL.Tokens
{
	public class TSQLStringLiteral : TSQLLiteral
	{
		internal TSQLStringLiteral(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			// need to find unescaped value insides quote characters

			// there are three different possible quote characters that we might support for string literals

			// if this is a unicode string it will begin with N
			int quotePosition = 0;
			int length = text.Length - 2;
			if (text[0] == 'N')
			{
				quotePosition++;
				length--;
				IsUnicode = true;
			}
			else
			{
				IsUnicode = false;
			}

			// now we can find which quote character we're using
			QuoteCharacter = text[quotePosition];

			// trim off the leading and trailing quotes
			Value = text.Substring(quotePosition + 1, length);

			// now unescape doubled up quote characters
			Value = Value.Replace(new string(QuoteCharacter, 2), QuoteCharacter.ToString());

			// remove line continuation backslash
			// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/sql-server-utilities-statements-backslash?view=sql-server-2017
			Value = Value.Replace("\\\r\n", "");
			Value = Value.Replace("\\\n", "");
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.StringLiteral;
			}
		}

#pragma warning restore 1591

		/// <summary>
		///		Value inside quotes unescaped.
		/// </summary>
		public string Value
		{
			get;

			private set;
		}

		public char QuoteCharacter
		{
			get;

			private set;
		}

		public bool IsUnicode
		{
			get;

			private set;
		}
	}
}
