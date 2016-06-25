using System;

namespace TSQL.Tokens
{
	public class TSQLVariable : TSQLToken
	{
		public TSQLVariable(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text,
				TSQLTokenType.Variable)
		{
			
		}
	}
}
