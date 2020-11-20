using System;
using System.Globalization;

namespace TSQL.Tokens
{
	public class TSQLNumericLiteral : TSQLLiteral
	{
		internal TSQLNumericLiteral(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			// have to handle exponent notation, e.g. 0.5E-2.
			// https://docs.microsoft.com/en-us/sql/t-sql/data-types/decimal-and-numeric-transact-sql
			// can be up to max size of decimal, which is 38 places of precision.

			Value = Double.Parse(Text, NumberStyles.Any, new CultureInfo("en-US"));
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.NumericLiteral;
			}
		}

#pragma warning restore 1591

		public double Value
		{
			get;

			private set;
		}
	}
}
