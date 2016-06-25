using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

namespace Tests.Tokens
{
	[TestFixture(Category = "Token Parsing")]
	public class CharacterTokenTests
	{
		[Test]
		public void CharacterToken_Period()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens(". ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLCharacter(0, "."),
						new TSQLWhitespace(1, " ")
					},
				tokens);
		}

		[Test]
		public void CharacterToken_Character()
		{
			TSQLCharacter token = new TSQLCharacter(0, ".");
			Assert.AreEqual(TSQLCharacters.Period, token.Character);
		}
	}
}
