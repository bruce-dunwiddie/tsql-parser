using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLOperator : TSQLToken
	{
		public TSQLOperator(
			int beginPostion,
			int endPosition,
			string text) :
			base(
				beginPostion,
				endPosition,
				text)
		{

		}
	}
}
