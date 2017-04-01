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
	public class MultilineCommentTokenTests
	{
		[Test]
		public void MultilineCommentToken_Simple()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/* blah */ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/* blah */"),
						new TSQLWhitespace(10, " ")
					},
				tokens);
		}

		[Test]
		public void MultilineCommentToken_MultiParens()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/* blah **/ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/* blah **/"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
		}

		[Test]
		public void MultilineCommentToken_SpanLines()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/* blah\r\nblah */ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/* blah\r\nblah */"),
						new TSQLWhitespace(16, " ")
					},
				tokens);
		}

		[Test]
		public void MultilineCommentToken_Nesting()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/* blah /* blah */ */ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/* blah /* blah */ */"),
						new TSQLWhitespace(21, " ")
					},
				tokens);
		}

		[Test]
		public void MultilineCommentToken_RequireFullEnding()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/*/ blah */ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/*/ blah */"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
		}

		[Test]
		public void MultilineCommentToken_Comment()
		{
			TSQLMultilineComment token = new TSQLMultilineComment(0, "/* blah */");
			Assert.AreEqual(" blah ", token.Comment);
		}
	}
}
