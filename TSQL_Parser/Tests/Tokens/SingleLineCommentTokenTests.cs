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
	public class SingleLineCommentTokenTests
	{
		[Test]
		public void SingleLineCommentToken_CarriageReturn()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("--blah\r", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSingleLineComment(0, "--blah"),
						new TSQLWhitespace(6, "\r")
					},
				tokens);
		}

		[Test]
		public void SingleLineCommentToken_LineFeed()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("--blah\n", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSingleLineComment(0, "--blah"),
						new TSQLWhitespace(6, "\n")
					},
				tokens);
		}

		[Test]
		public void SingleLineCommentToken_Comment()
		{
			TSQLSingleLineComment token = new TSQLSingleLineComment(0, "-- blah");
			Assert.AreEqual(" blah", token.Comment);
		}
	}
}
