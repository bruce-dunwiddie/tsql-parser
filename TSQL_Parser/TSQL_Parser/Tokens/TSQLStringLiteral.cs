using System;

namespace TSQL.Tokens
{
	public class TSQLStringLiteral : TSQLLiteral
	{
		public TSQLStringLiteral(
			int beginPostion,
			string text) :
			base(
				beginPostion,
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

			// now unescape doubled up quote characters
			Value = text.Substring(quotePosition + 1, length)
				.Replace(new string(QuoteCharacter, 2), QuoteCharacter.ToString());
		}

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.StringLiteral;
			}
		}

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
