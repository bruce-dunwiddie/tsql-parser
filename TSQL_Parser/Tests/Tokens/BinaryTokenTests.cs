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
	public class BinaryTokenTests
	{
		[Test]
		public void BinaryToken_EmptyBinaryLiteral()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0x ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0x"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void BinaryToken_EmptyBinaryLiteralCaseInsensitive()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0X ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0X"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void BinaryToken_SimpleBinaryLiteral()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0x69048AEFDD010E ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0x69048AEFDD010E"),
						new TSQLWhitespace(16, " ")
					},
				tokens);
		}

		[Test]
		public void BinaryToken_BinaryLiteralEndWithLetter()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0x69048AEFDD010EJ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0x69048AEFDD010E"),
						new TSQLIdentifier(16, "J")
					},
				tokens);
		}
	}
}
