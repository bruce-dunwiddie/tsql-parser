using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

namespace Tests.TokenParsing
{
	[TestFixture(Category = "Token Parsing")]
	public class StringLiteralTokenTests
	{
		[Test]
		public void StringLiteralToken_EmptySingleQuote()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("'' ", useQuotedIdentifiers: false, includeWhitespace: true);
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
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("'name' ", useQuotedIdentifiers: false, includeWhitespace: true);
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
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("N'name' ", useQuotedIdentifiers: false, includeWhitespace: true);
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
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("\"\" ", useQuotedIdentifiers: false, includeWhitespace: true);
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
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("\"name\" ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "\"name\""),
						new TSQLWhitespace(6, " ")
					},
				tokens);
		}

		[Test]
		public void StringLiteralToken_DoubleQuoteUnicode()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("N\"name\" ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLStringLiteral(0, "N\"name\""),
						new TSQLWhitespace(7, " ")
					},
				tokens);
		}
	}
}
