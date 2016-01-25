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
				text,
				TokenType.MultilineComment)
		{
			Comment = Text.Substring(2, Text.Length - 4);
		}
	}
}
