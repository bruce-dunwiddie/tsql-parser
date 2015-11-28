using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLNumericLiteral : TSQLLiteral
	{
		public TSQLNumericLiteral(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text,
				TokenType.NumericLiteral)
		{

		}
	}
}
