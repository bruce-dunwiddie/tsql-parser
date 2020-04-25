using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	public class TSQLBinaryLiteral : TSQLLiteral
	{
		internal TSQLBinaryLiteral(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			// change 0xabcdef to 0xABCDEF
			Value = text.Substring(0, 2) + text.Substring(2).ToUpper();

			// remove line continuation backslash
			// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/sql-server-utilities-statements-backslash?view=sql-server-2017
			Value = Value.Replace("\\\r\n", "");
			Value = Value.Replace("\\\n", "");

			Values = StringToByteArray(Value);
		}

		private static byte[] StringToByteArray(String hex)
		{
			// for odd number hex, e.g. 0x100, the first charsInByte is only 1
			int charsInByte = (hex.Length % 2 == 0) ? 2 : 1;
			byte[] bytes = new byte[(hex.Length - 1) / 2];
			int i = 0, hexIndex = 2; // adding 2 to i to skip "0x" at the beginning of the string
			while (i < bytes.Length)
			{
				bytes[i++] = Convert.ToByte(hex.Substring(hexIndex, charsInByte), 16);
				hexIndex += charsInByte;
				charsInByte = 2;
			}
			return bytes;
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.BinaryLiteral;
			}
		}

#pragma warning restore 1591

		public string Value
		{
			get;

			private set;
		}

		public byte[] Values
		{
			get;

			private set;
		}
	}
}
