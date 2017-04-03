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

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Keyword;
			}
		}

#pragma warning restore 1591

		public TSQLKeywords Keyword
		{
			get;
			private set;
		}
	}
}
