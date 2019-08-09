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
    public class IdentifierTokenTests
	{
		[Test]
		public void IdentifierToken_SimpleIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("a ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "a"),
						new TSQLWhitespace(1, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_SimpleBracketedIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a] ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]"),
						new TSQLWhitespace(3, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_EscapedBracketedIdentifier1()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a]]] ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]]]"),
						new TSQLWhitespace(5, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_EscapedBracketedIdentifier2()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a]]a] ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]]a]"),
						new TSQLWhitespace(6, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_BracketedMultiLine()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a\r\nb] ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a\r\nb]"),
						new TSQLWhitespace(6, " ")
					},
				tokens);
			Assert.AreEqual("a\r\nb", tokens[0].AsIdentifier.Name);
		}

		[Test]
		public void IdentifierToken_StartWithN()
		{
			// unicode string literals are a special case that start with N
			// test here to make sure it gets parsed as an identifier token
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("Name ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "Name"),
						new TSQLWhitespace(4, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_OnlyN()
		{
			// unicode string literals are a special case that start with N
			// test here to make sure it gets parsed as an identifier token
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("N ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "N"),
						new TSQLWhitespace(1, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_QuotedIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("\"name\" ", useQuotedIdentifiers: true, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "\"name\""),
						new TSQLWhitespace(6, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_QuotedUnicodeIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("N\"name\" ", useQuotedIdentifiers: true, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "N\"name\""),
						new TSQLWhitespace(7, " ")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_NoWhitespace()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("select[id]from[blah]order by[orderdate];", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "select"),
						new TSQLIdentifier(6, "[id]"),
						new TSQLKeyword(10, "from"),
						new TSQLIdentifier(14, "[blah]"),
						new TSQLKeyword(20, "order"),
						new TSQLWhitespace(25, " "),
						new TSQLKeyword(26, "by"),
						new TSQLIdentifier(28, "[orderdate]"),
						new TSQLCharacter(39, ";")
					},
				tokens);
		}

		[Test]
		public void IdentifierToken_SimpleName()
		{
			TSQLIdentifier token = new TSQLIdentifier(0, "a");
			Assert.AreEqual("a", token.Name);
		}

		[Test]
		public void IdentifierToken_BracketedName()
		{
			TSQLIdentifier token = new TSQLIdentifier(0, "[a]");
			Assert.AreEqual("a", token.Name);
		}

		[Test]
		public void IdentifierToken_EscapedBracketedName()
		{
			TSQLIdentifier token = new TSQLIdentifier(0, "[a]]a]");
			Assert.AreEqual("a]a", token.Name);
		}

		[Test]
		public void IdentifierToken_QuotedName()
		{
			TSQLIdentifier token = new TSQLIdentifier(0, "\"name\"");
			Assert.AreEqual("name", token.Name);
		}

		[Test]
		public void IdentifierToken_QuotedUnicodeName()
		{
			TSQLIdentifier token = new TSQLIdentifier(0, "N\"name\"");
			Assert.AreEqual("name", token.Name);
		}
	}
}
