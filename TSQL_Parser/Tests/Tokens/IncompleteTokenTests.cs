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
	public class IncompleteTokenTests
	{
		[Test]
		public void IncompleteToken_StringLiteral()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("'", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIncompleteStringToken(0, "'")
					},
				tokens);
			Assert.IsFalse(tokens[0].IsComplete);
		}

		[Test]
		public void IncompleteToken_Identifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[dbo", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIncompleteIdentifierToken(0, "[dbo")
					},
				tokens);
			Assert.IsFalse(tokens[0].IsComplete);
		}

		[Test]
		public void IncompleteToken_Comment()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/* something", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIncompleteCommentToken(0, "/* something")
					},
				tokens);
			Assert.IsFalse(tokens[0].IsComplete);
		}

		[Test]
		public void IncompleteToken_DoubleQuoteIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens(
				"\"dbo", 
				includeWhitespace: true,
				useQuotedIdentifiers: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIncompleteIdentifierToken(0, "\"dbo")
					},
				tokens);
			Assert.IsFalse(tokens[0].IsComplete);
		}

		[Test]
		public void IncompleteToken_DoubleQuoteString()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens(
				"\"something", 
				includeWhitespace: true,
				useQuotedIdentifiers: false);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIncompleteStringToken(0, "\"something")
					},
				tokens);
			Assert.IsFalse(tokens[0].IsComplete);
		}
	}
}
