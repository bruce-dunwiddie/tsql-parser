using System;

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
				TokenType.Character)
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
