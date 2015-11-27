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
			string text) :
			base(
				beginPostion,
				text)
		{
			Comment = Text.Substring(2);
		}
	}
}
