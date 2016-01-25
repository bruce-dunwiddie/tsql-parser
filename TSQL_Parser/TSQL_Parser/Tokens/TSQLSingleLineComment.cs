using System;

namespace TSQL.Tokens
{
	public class TSQLSingleLineComment : TSQLComment
	{
		public TSQLSingleLineComment(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text,
				TokenType.SingleLineComment)
		{
			Comment = Text.Substring(2);
		}
	}
}
