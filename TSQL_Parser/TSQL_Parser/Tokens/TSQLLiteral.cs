using System;

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
