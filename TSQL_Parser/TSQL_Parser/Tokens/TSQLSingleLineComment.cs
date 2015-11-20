using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	class TSQLSingleLineComment : TSQLComment
	{
		public TSQLSingleLineComment(
			int beginPostion,
			int endPosition,
			string text) :
			base(
				beginPostion,
				endPosition,
				text)
		{
			Comment = Text.Substring(2);
		}
	}
}
