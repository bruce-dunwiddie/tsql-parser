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
	}
}
