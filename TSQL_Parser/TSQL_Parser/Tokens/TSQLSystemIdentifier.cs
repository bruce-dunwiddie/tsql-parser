using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		/*

		https://docs.microsoft.com/en-us/sql/t-sql/language-elements/reserved-keywords-transact-sql

		where not already in TSQLKeywords

		RAISERROR
		...

		*/

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.SystemVariable;
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
