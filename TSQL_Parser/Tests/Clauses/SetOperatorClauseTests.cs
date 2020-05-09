using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Statements;

namespace Tests.Clauses
{
	[TestFixture(Category = "Clause Parsing")]
	public class SetOperatorClauseTests
	{
		[Test]
		public void SetOperatorClause_UnionDontOverrun()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
					SELECT TOP 1 L1.ID
					FROM
						(SELECT 2 AS ID) L1
					WHERE
						L1.ID = 2
					GROUP BY
						L1.ID
					HAVING COUNT(*) > 0

					UNION ALL

					SELECT TOP 1 L2.ID
					FROM
						(SELECT 1 AS ID) L2
					WHERE
						L2.ID = 1
					GROUP BY
						L2.ID
					HAVING COUNT(*) > 0

					-- the only table alias in scope is the first one
					-- but all rows from result of UNION are referenced
					ORDER BY L1.ID

					OPTION (FAST 2)

					FOR XML PATH;",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			Assert.IsNotNull(statements[0].AsSelect.SetOperator);
			Assert.AreEqual(36, statements[0].AsSelect.SetOperator.Tokens.Count);
			Assert.IsNotNull(statements[0].AsSelect.SetOperator.Select);
			Assert.AreEqual(34, statements[0].AsSelect.SetOperator.Select.Tokens.Count);
			Assert.IsNull(statements[0].AsSelect.SetOperator.Select.OrderBy);
			Assert.IsNotNull(statements[0].AsSelect.OrderBy);
		}

		[Test]
		public void SetOperatorClause_ParensRegression()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/69

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION (SELECT 2)",
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(7, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperator);
		}

		[Test]
		public void SetOperatorClause_MultiParens()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION ((SELECT 2)) SELECT 1",
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;
			TSQLSelectStatement select2 = statements[1].AsSelect;

			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(9, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperator);
			Assert.AreEqual(2, select2.Tokens.Count);
		}
	}
}
