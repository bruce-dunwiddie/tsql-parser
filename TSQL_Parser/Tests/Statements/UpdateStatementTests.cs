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
	public class UpdateStatementTests
	{
		[Test]
		public void UpdateStatement_FullFunctionality()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				UPDATE t
				SET 
					t.Column1 = 'something', 
					t.Column2 = 2
				OUTPUT deleted.*
					INTO #holder (ID, Column1, Column2)
				FROM dbo.SomeTable t
				WHERE
					t.ID = 1
				OPTION (RECOMPILE);",
				includeWhitespace: false);
			TSQLUpdateStatement update = statements[0].AsUpdate;

			Assert.AreEqual(42, update.Tokens.Count);
			Assert.AreEqual(12, update.Set.Tokens.Count);
			Assert.AreEqual(13, update.Output.Tokens.Count);
			Assert.AreEqual(9, update.Output.Into.Tokens.Count);
			Assert.AreEqual(5, update.From.Tokens.Count);
			Assert.AreEqual(6, update.Where.Tokens.Count);
			Assert.AreEqual(4, update.Option.Tokens.Count);
		}
	}
}
