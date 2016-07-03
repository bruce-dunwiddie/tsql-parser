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
				text)
		{
			Comment = Text.Substring(2);
		}

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.SingleLineComment;
			}
		}
	}
}
