namespace TSQL.Tokens
{
	public class TSQLFutureKeyword : TSQLToken
	{
		internal TSQLFutureKeyword(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			Keyword = TSQLFutureKeywords.Parse(text);
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.FutureKeyword;
			}
		}

#pragma warning restore 1591

		public TSQLFutureKeywords Keyword
		{
			get;
			private set;
		}

		public override bool IsComplete
		{
			get
			{
				return true;
			}
		}
	}
}
