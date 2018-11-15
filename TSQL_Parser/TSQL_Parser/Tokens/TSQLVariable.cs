using System;

namespace TSQL.Tokens
{
	public class TSQLVariable : TSQLToken
	{
		internal TSQLVariable(
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
				return TSQLTokenType.Variable;
			}
		}

#pragma warning restore 1591
	}
}
