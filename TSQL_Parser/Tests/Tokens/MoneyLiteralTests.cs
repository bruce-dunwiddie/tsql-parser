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
	public class MoneyLiteralTests
	{
		[Test]
		public void MoneyLiteral_Subtraction()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("$2-$1 ", includeWhitespace: true);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLMoneyLiteral(0, "$2"),
						new TSQLOperator(2, "-"),
						new TSQLMoneyLiteral(3, "$1"),
						new TSQLWhitespace(5, " ")
					},
				tokens);
		}
	}
}
