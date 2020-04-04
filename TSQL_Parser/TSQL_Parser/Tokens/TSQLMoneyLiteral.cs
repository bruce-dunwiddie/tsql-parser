using System;
using System.Collections.Generic;
using System.Globalization;
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
			// https://docs.microsoft.com/en-us/sql/t-sql/data-types/money-and-smallmoney-transact-sql
			// max value = $922,337,203,685,477.5807.
			// https://docs.microsoft.com/en-us/dotnet/api/system.decimal
			// C# decimal supports scaled number up to 79,228,162,514,264,337,593,543,950,335.
			// exponents are not allowed in money literals.
			// can have sign indicators between money indicator and value, e.g. $-1.23.

			if (Text.Length > 1)
			{
				Value = Decimal.Parse(Text.Substring(1), NumberStyles.Any);
			}
			else
			{
				Value = 0m;
			}
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

		public decimal Value
		{
			get;

			private set;
		}
	}
}
