using System;

namespace TSQL.Tokens
{
	public class TSQLOperator : TSQLToken
	{
		internal TSQLOperator(
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
				return TSQLTokenType.Operator;
			}
		}

#pragma warning restore 1591
	}
}
