using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Statements;

namespace Tests.Statements
{
	[TestFixture(Category = "Statement Parsing")]
	public class ExecuteStatementTests
	{
		[Test]
		public void ExecuteStatement_NoKeyword()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"sp_who2;",
				includeWhitespace: false);
			TSQLExecuteStatement exec = statements[0].AsExecute;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(1, exec.Tokens.Count);
		}

		[Test]
		public void ExecuteStatement_NoEnd()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"sp_who2 select 1",
				includeWhitespace: false);
			TSQLExecuteStatement exec = statements[0].AsExecute;

			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(1, exec.Tokens.Count);
		}
	}
}
