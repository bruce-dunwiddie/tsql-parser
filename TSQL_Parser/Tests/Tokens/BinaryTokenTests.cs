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

		[Test]
		public void BinaryToken_EmbeddedLineContinuation()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0xabc\\\r\ndef ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0xabc\\\r\ndef"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
			Assert.AreEqual("0xABCDEF", tokens[0].AsBinaryLiteral.Value);
		}

		[Test]
		public void BinaryToken_ByteArrayValue()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0x68656C6C6F", includeWhitespace: false);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0x68656C6C6F")
					},
				tokens);

			TestHelpers.CompareArrays(
				new byte[] {
					(byte)'h',
					(byte)'e',
					(byte)'l',
					(byte)'l',
					(byte)'o'
				},
				tokens[0].AsBinaryLiteral.Values);
		}

		[Test]
		public void BinaryToken_ByteArrayValueLowercase()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("0x68656c6c6f", includeWhitespace: false);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLBinaryLiteral(0, "0x68656c6c6f")
					},
				tokens);

			TestHelpers.CompareArrays(
				new byte[] {
					(byte)'h',
					(byte)'e',
					(byte)'l',
					(byte)'l',
					(byte)'o'
				},
				tokens[0].AsBinaryLiteral.Values);
		}

		[Test]
		public void BinaryToken_OddLength()
		{
			// SQL Server is implicitly converting this to 0x0100
			TSQLBinaryLiteral token = new TSQLBinaryLiteral(0, "0x100");

			TestHelpers.CompareArrays(
				new byte[]
				{
					(byte)1,
					(byte)0
				},
				token.Values);
		}
	}
}
