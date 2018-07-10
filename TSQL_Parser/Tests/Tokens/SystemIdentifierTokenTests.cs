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
	public class SystemIdentifierTokenTests
	{
		[Test]
		public void SystemIdentifierToken_SimpleIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("SYSTEM_USER ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSystemIdentifier(0, "SYSTEM_USER"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
			Assert.AreEqual(TSQLIdentifiers.SYSTEM_USER, tokens[0].AsSystemIdentifier.Identifier);
		}
	}
}
