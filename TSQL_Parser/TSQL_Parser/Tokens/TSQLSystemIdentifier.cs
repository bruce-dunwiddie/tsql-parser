using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	public class TSQLSystemIdentifier : TSQLIdentifier
	{
		internal TSQLSystemIdentifier(
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
				return TSQLTokenType.SystemIdentifier;
			}
		}

#pragma warning restore 1591

		public TSQLIdentifiers Identifier
		{
			get;
			private set;
		}
	}
}
