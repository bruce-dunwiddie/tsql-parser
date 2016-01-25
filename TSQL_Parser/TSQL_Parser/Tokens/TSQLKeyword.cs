using System;

namespace TSQL.Tokens
{
	public class TSQLKeyword : TSQLToken
	{
		public TSQLKeyword(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text,
				TokenType.Keyword)
		{
			Keyword = TSQLKeywords.Parse(text);
		}

		public TSQLKeywords Keyword
		{
			get;
			private set;
		}
	}
}
