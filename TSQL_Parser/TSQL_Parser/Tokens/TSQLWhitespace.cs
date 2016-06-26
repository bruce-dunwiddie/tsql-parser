using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLWhitespace : TSQLToken
	{
		public TSQLWhitespace(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Whitespace;
			}
		}
	}
}
