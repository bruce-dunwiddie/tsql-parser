using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLSystemVariable : TSQLVariable
	{
		internal TSQLSystemVariable(
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
