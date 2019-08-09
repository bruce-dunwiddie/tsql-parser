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
	public class StringLiteralTokenTests
	{
		[Test]
		public void StringLiteralToken_EmptySingleQuote()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("'' ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "''"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_SingleQuote()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("'name' ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "'name'"),
						new TSQLWhitespace(6, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_SingleQuoteUnicode()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("N'name' ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "N'name'"),
						new TSQLWhitespace(7, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_EmptyDoubleQuote()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("\"\" ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "\"\""),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_DoubleQuote()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("\"name\" ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "\"name\""),
						new TSQLWhitespace(6, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_MultiLine()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("'a\r\nb' ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "'a\r\nb'"),
						new TSQLWhitespace(6, " ")
					},
				tokens);
			Assert.AreEqual("a\r\nb", tokens[0].AsStringLiteral.Value);
		}

		[Test]
		public void StringLiteralToken_LineContinuation()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("'a\\\r\nb' ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "'a\\\r\nb'"),
						new TSQLWhitespace(7, " ")
					},
				tokens);
			Assert.AreEqual("ab", tokens[0].AsStringLiteral.Value);
		}

		[Test]
		public void StringLiteralToken_SingleQuoteValue()
		{
			TSQLStringLiteral token = new TSQLStringLiteral(0, "'name'");
			Assert.AreEqual("name", token.Value);
			Assert.AreEqual('\'', token.QuoteCharacter);
			Assert.IsFalse(token.IsUnicode);
		}

		[Test]
		public void StringLiteralToken_SingleQuoteEscapedValue()
		{
			TSQLStringLiteral token = new TSQLStringLiteral(0, "'bob''s'");
			Assert.AreEqual("bob's", token.Value);
			Assert.AreEqual('\'', token.QuoteCharacter);
			Assert.IsFalse(token.IsUnicode);
		}

		[Test]
		public void StringLiteralToken_SingleQuoteUnicodeValue()
		{
			TSQLStringLiteral token = new TSQLStringLiteral(0, "N'name'");
			Assert.AreEqual("name", token.Value);
			Assert.AreEqual('\'', token.QuoteCharacter);
			Assert.IsTrue(token.IsUnicode);
		}

		[Test]
		public void StringLiteralToken_DoubleQuoteValue()
		{
			TSQLStringLiteral token = new TSQLStringLiteral(0, "\"name\"");
			Assert.AreEqual("name", token.Value);
			Assert.AreEqual('\"', token.QuoteCharacter);
			Assert.IsFalse(token.IsUnicode);
		}

		[Test]
		public void StringLiteralToken_DoubleQuoteUnicodeValue()
		{
			TSQLStringLiteral token = new TSQLStringLiteral(0, "N\"name\"");
			Assert.AreEqual("name", token.Value);
			Assert.AreEqual('\"', token.QuoteCharacter);
			Assert.IsTrue(token.IsUnicode);
		}
	}
}
