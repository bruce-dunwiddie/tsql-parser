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
	public class NumericLiteralTokenTests
	{
		[Test]
		public void NumericLiteral_ComplicatedExponential()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("54.0e-4 ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLNumericLiteral(0, "54.0e-4"),
						new TSQLWhitespace(7, " ")
					},
				tokens);
		}

		[Test]
		public void NumericLiteral_SubtractionVsExponential()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("1-2 54.0e-4 ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLNumericLiteral(0, "1"),
						new TSQLOperator(1, "-"),
						new TSQLNumericLiteral(2, "2"),
						new TSQLWhitespace(3, " "),
                        new TSQLNumericLiteral(4, "54.0e-4"),
						new TSQLWhitespace(11, " ")
					},
				tokens);
		}
	}
}
