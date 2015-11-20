using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLKeyword : TSQLToken
	{
		public TSQLKeyword(
			int beginPostion,
			int endPosition,
			string text) :
			base(
				beginPostion,
				endPosition,
				text)
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
