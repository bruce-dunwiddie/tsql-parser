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
			string text) :
			base(
				beginPostion,
				text,
				TSQLTokenType.Character)
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
