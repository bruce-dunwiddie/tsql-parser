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

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.MultilineComment;
			}
		}
	}
}
