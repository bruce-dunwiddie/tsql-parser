using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public abstract class TSQLLiteral : TSQLToken
	{
		protected TSQLLiteral(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}
	}
}
