using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using TSQL;
using TSQL.Statements;
using TSQL.Tokens;

namespace Tests.Statements
{
	[TestFixture(Category ="Statement Parsing")]
	public class GeneralStatementTests
	{
		[Test]
		public void UnknownStatement_MissingFirstToken()
		{
			// regression test for reported bug
			// https://github.com/bruce-dunwiddie/tsql-parser/issues/3
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"DECLARE @Location VARCHAR(4)
				DECLARE @ReportDate DATETIME

				SET @Location = '1010'
				SET @ReportDate = '01/09/18'",
				includeWhitespace: false);

			Assert.IsTrue(statements[0].Tokens[0].IsKeyword(TSQLKeywords.DECLARE));
		}

		[Test]
		public void CreateTableStatement_FutureKeywordsAsIdentifier()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"create table output (id int); create table using (id int);",
				includeWhitespace: true);

			Assert.IsNotNull(statements);
			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(TSQLStatementType.Unknown, statements[0].Type);
			Assert.AreEqual(TSQLStatementType.Unknown, statements[1].Type);
			Assert.IsNotNull(statements[0].Tokens[4].AsIdentifier);
			Assert.IsNotNull(statements[1].Tokens[4].AsIdentifier);
		}
	}
}
