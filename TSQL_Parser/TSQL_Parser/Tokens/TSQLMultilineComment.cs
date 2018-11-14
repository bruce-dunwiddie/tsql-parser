using System;

namespace TSQL.Tokens
{
	public class TSQLMultilineComment : TSQLComment
	{
		internal TSQLMultilineComment(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			Comment = Text.Substring(2, Text.Length - 4);
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.MultilineComment;
			}
		}

#pragma warning restore 1591
	}
}
