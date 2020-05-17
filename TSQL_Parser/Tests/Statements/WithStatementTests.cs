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
    [TestFixture(Category = "Statement Parsing")]
    public class WithStatementTests
    {
        [Test]
        public void WithStatement_Select()
        {
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"WITH [EMP_cte]([BusinessEntityID], [OrganizationNode], [FirstName], [LastName], [RecursionLevel]) -- CTE name and columns
				AS (
					SELECT e.[BusinessEntityID], e.[OrganizationNode], p.[FirstName], p.[LastName], 0 -- Get the initial list of Employees for Manager n
					FROM [HumanResources].[Employee] e 
						INNER JOIN [Person].[Person] p 
						ON p.[BusinessEntityID] = e.[BusinessEntityID]
					WHERE e.[BusinessEntityID] = @BusinessEntityID
					UNION ALL
					SELECT e.[BusinessEntityID], e.[OrganizationNode], p.[FirstName], p.[LastName], [RecursionLevel] + 1 -- Join recursive member to anchor
					FROM [HumanResources].[Employee] e 
						INNER JOIN [EMP_cte]
						ON e.[OrganizationNode].GetAncestor(1) = [EMP_cte].[OrganizationNode]
						INNER JOIN [Person].[Person] p 
						ON p.[BusinessEntityID] = e.[BusinessEntityID]
					)
				-- Join back to Employee to return the manager name 
				SELECT [EMP_cte].[RecursionLevel], [EMP_cte].[OrganizationNode].ToString() as [OrganizationNode], p.[FirstName] AS 'ManagerFirstName', p.[LastName] AS 'ManagerLastName',
					[EMP_cte].[BusinessEntityID], [EMP_cte].[FirstName], [EMP_cte].[LastName] -- Outer select from the CTE
				FROM [EMP_cte] 
					INNER JOIN [HumanResources].[Employee] e 
					ON [EMP_cte].[OrganizationNode].GetAncestor(1) = e.[OrganizationNode]
						INNER JOIN [Person].[Person] p 
						ON p.[BusinessEntityID] = e.[BusinessEntityID]
				ORDER BY [RecursionLevel], [EMP_cte].[OrganizationNode].ToString()
				OPTION (MAXRECURSION 25)",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			Assert.IsInstanceOf(typeof(TSQLSelectStatement), statements[0]);
			Assert.IsNotNull(statements[0].AsSelect.With);
			Assert.IsTrue(statements[0].AsSelect.With.Tokens[0].IsKeyword(TSQLKeywords.WITH));
			Assert.AreEqual(
				" Join back to Employee to return the manager name ",
				statements[0].AsSelect.With.Tokens.Last().AsSingleLineComment.Comment);
		}

		[Test]
		public void WithStatement_SelectInParens()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"WITH test AS
				(
					SELECT 1 AS value
				),
				test2 AS
				(
					SELECT 2 AS value
				)
				(
					SELECT *
					FROM
						test
				)",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			Assert.IsInstanceOf(typeof(TSQLSelectStatement), statements[0]);
			Assert.AreEqual(24, statements[0].AsSelect.Tokens.Count);
		}
	}
}
