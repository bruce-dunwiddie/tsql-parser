using System;

namespace TSQL.Tokens
{
	public abstract class TSQLLiteral : TSQLToken
	{
		internal protected TSQLLiteral(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}
	}
}
