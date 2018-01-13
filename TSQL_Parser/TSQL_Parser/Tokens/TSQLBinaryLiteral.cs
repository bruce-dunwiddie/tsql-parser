using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	public class TSQLBinaryLiteral : TSQLLiteral
	{
		internal TSQLBinaryLiteral(
			int beginPostion,
			string text) :
			base(
				beginPostion,
				text)
		{

		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.BinaryLiteral;
			}
		}

#pragma warning restore 1591
	}
}
