using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	public class TSQLSystemVariable : TSQLVariable
	{
		internal TSQLSystemVariable(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			Variable = TSQLVariables.Parse(text);
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.SystemVariable;
			}
		}

#pragma warning restore 1591

		public TSQLVariables Variable
		{
			get;
			private set;
		}
	}
}
