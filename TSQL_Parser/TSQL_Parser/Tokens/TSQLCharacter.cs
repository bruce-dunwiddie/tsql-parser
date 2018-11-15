using System;

namespace TSQL.Tokens
{
	public class TSQLCharacter : TSQLToken
	{
		internal TSQLCharacter(
			int beginPosition,
			string text) :
			base(
				beginPosition,
				text)
		{
			Character = TSQLCharacters.Parse(text);
		}

#pragma warning disable 1591

		public override TSQLTokenType Type
		{
			get
			{
				return TSQLTokenType.Character;
			}
		}

#pragma warning restore 1591

		public TSQLCharacters Character
		{
			get;
			private set;
		}
	}
}
