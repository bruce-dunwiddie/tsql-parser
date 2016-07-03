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
				text)
		{
			Keyword = TSQLKeywords.Parse(text);
		}

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Keyword;
			}
		}

		public TSQLKeywords Keyword
		{
			get;
			private set;
		}
	}
}
