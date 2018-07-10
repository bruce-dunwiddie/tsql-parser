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
	public class SystemVariableTokenTests
	{
		[Test]
		public void SystemVariableToken_SimpleVariable()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("@@ROWCOUNT ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLSystemVariable(0, "@@ROWCOUNT"),
						new TSQLWhitespace(10, " ")
					},
				tokens);
			Assert.AreEqual(TSQLVariables.ROWCOUNT, tokens[0].AsSystemVariable.Variable);
			// testing some of the Equals overriding logic
			Assert.IsTrue(TSQLVariables.ROWCOUNT == tokens[0].AsSystemVariable.Variable);
			Assert.IsFalse(TSQLVariables.ROWCOUNT != tokens[0].AsSystemVariable.Variable);
		}

		[Test]
		public void SystemVariableToken_BogusVariable()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("@@BOGUS ", includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLVariable(0, "@@BOGUS"),
						new TSQLWhitespace(7, " ")
					},
				tokens);
			Assert.IsFalse(tokens[0] is TSQLSystemVariable);
		}
	}
}
