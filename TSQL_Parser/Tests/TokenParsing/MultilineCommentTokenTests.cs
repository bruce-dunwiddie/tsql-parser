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
	public class MultilineCommentTokenTests
	{
		[Test]
		public void MultilineCommentToken_Simple()
		{
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("/* blah */ ", useQuotedIdentifiers: false, includeWhitespace: true);
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
			List<TSQLToken> tokens = TSQLLexer.ParseTokens("/* blah **/ ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMultilineComment(0, "/* blah **/"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
		}
	}
}
