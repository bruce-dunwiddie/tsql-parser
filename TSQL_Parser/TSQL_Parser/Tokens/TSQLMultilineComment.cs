using System;

namespace TSQL.Tokens
{
	public class TSQLMultilineComment : TSQLComment
	{
		public TSQLMultilineComment(
			int beginPostion,
			string text) :
			base(
				beginPostion,
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
