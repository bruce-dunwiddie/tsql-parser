using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	public class TSQLMoneyLiteral : TSQLLiteral
	{
		internal TSQLMoneyLiteral(
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
				return TSQLTokenType.MoneyLiteral;
			}
		}

#pragma warning restore 1591
	}
}
