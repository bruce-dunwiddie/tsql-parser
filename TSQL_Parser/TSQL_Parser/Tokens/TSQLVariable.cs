using System;

namespace TSQL.Tokens
{
	public class TSQLVariable : TSQLToken
	{
		public TSQLVariable(
			int beginPostion,
			string text) :
			base(
				beginPostion,
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
