using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Expressions;
using TSQL.Statements;
using TSQL.Tokens;

namespace Tests.Expressions
{
	[TestFixture(Category = "Expression Parsing")]
	public class CaseExpressionTests
	{
		[Test]
		public void CaseExpression_Dont_Overrun()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
					BEGIN
						SELECT CASE WHEN 1 = 2 THEN 0 ELSE 1 END
					END",
				includeWhitespace: false);

			Assert.AreEqual(3, statements.Count);
			Assert.AreEqual(1, statements[0].Tokens.Count);
			Assert.AreEqual(11, statements[1].Tokens.Count);
			Assert.AreEqual(1, statements[2].Tokens.Count);
			Assert.IsTrue(statements[2].Tokens[0].IsKeyword(TSQLKeywords.END));
		}
	}
}
