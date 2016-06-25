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
	public class KeywordTokenTests
	{
		[Test]
		public void KeywordToken_SimpleKeyword()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("GO ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "GO"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void KeywordToken_SimpleKeywordLowercase()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("go ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "go"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void KeywordToken_Keyword()
		{
			TSQLKeyword token = new TSQLKeyword(0, "go");
			Assert.AreEqual(TSQLKeywords.GO, token.Keyword);
		}

		[Test]
		public void KeywordToken_NoneKeyword()
		{
			TSQLKeyword token = new TSQLKeyword(0, "blah");
			Assert.AreEqual(TSQLKeywords.None, token.Keyword);
		}
	}
}
