using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	public class TSQLCharacter : TSQLToken
	{
		public TSQLCharacter(
			int beginPostion,
			int endPosition,
			string text) :
			base(
				beginPostion,
				endPosition,
				text)
		{
			Character = TSQLCharacters.Parse(text);
		}

		public TSQLCharacters Character
		{
			get;
			private set;
		}
	}
}
