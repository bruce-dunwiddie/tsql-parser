using System;

namespace TSQL.Tokens
{
	public abstract class TSQLLiteral : TSQLToken
	{
		internal protected TSQLLiteral(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{

		}
	}
}
