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
	}
}
