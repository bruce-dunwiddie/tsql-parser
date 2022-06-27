using System;

namespace TSQL.Tokens
{
	public class TSQLIncompleteComment : TSQLIncompleteToken
	{
		internal TSQLIncompleteComment(
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
				return TSQLTokenType.IncompleteComment;
			}
		}

#pragma warning restore 1591
	}
}
