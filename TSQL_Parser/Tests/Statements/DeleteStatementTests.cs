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
	public class DeleteStatementTests
	{
		[Test]
		public void DeleteStatement_FullFunctionality()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				DELETE t
				OUTPUT deleted.*
					INTO #holder (ID, Column1, Column2)
				FROM dbo.SomeTable t
				WHERE
					t.ID = 1
				OPTION (RECOMPILE);",
				includeWhitespace: false);
			TSQLDeleteStatement delete = statements[0].AsDelete;

			Assert.AreEqual(30, delete.Tokens.Count);
			Assert.AreEqual(13, delete.Output.Tokens.Count);
			Assert.AreEqual(9, delete.Output.Into.Tokens.Count);
			Assert.AreEqual(5, delete.From.Tokens.Count);
			Assert.AreEqual(6, delete.Where.Tokens.Count);
			Assert.AreEqual(4, delete.Option.Tokens.Count);
		}
	}
}
