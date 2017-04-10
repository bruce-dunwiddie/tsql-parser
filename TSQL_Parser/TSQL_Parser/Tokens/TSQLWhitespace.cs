using System;

namespace TSQL.Tokens
{
	public class TSQLWhitespace : TSQLToken
	{
		internal TSQLWhitespace(
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
				return TSQLTokenType.Whitespace;
			}
		}

#pragma warning restore 1591
	}
}
