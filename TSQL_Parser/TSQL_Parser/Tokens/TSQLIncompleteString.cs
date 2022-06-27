using System;

namespace TSQL.Tokens
{
	public class TSQLIncompleteString : TSQLIncompleteToken
	{
		internal TSQLIncompleteString(
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
				return TSQLTokenType.IncompleteString;
			}
		}

#pragma warning restore 1591
	}
}
