using System;

namespace TSQL.Tokens
{
	public class TSQLSingleLineComment : TSQLComment
	{
		internal TSQLSingleLineComment(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			Comment = Text.Substring(2);
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.SingleLineComment;
			}
		}

#pragma warning restore 1591
	}
}
