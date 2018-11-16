using System;

namespace TSQL.Tokens
{
	public class TSQLKeyword : TSQLToken
	{
		internal TSQLKeyword(
			int beginPosition,
			string text) :
			base(
				beginPosition,
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
