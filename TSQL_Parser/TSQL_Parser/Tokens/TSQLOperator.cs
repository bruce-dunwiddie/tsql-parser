using System;

namespace TSQL.Tokens
{
	public class TSQLOperator : TSQLToken
	{
		public TSQLOperator(
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
				return TSQLTokenType.Operator;
			}
		}
	}
}
