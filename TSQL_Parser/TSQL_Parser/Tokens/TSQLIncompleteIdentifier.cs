using System;

namespace TSQL.Tokens
{
	public class TSQLIncompleteIdentifier : TSQLIncompleteToken
	{
		internal TSQLIncompleteIdentifier(
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
				return TSQLTokenType.IncompleteIdentifier;
			}
		}

#pragma warning restore 1591
	}
}
