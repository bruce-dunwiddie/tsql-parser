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
	public class InsertStatementTests
	{
		[Test]
		public void InsertStatement_SimpleValues()
		{
			string sql = @"
				INSERT INTO Production.UnitMeasure  
				VALUES (N'FT', N'Feet', '20080414');";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(13, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Select);
			Assert.IsNull(insert.Default);
			Assert.IsNull(insert.Execute);
			Assert.AreEqual(5, insert.Insert.Tokens.Count);
			Assert.AreEqual(8, insert.Values.Tokens.Count);
		}

		[Test]
		public void InsertStatement_MultipleValues()
		{
			string sql = @"
				INSERT INTO Production.UnitMeasure  
				VALUES
					(N'FT2', N'Square Feet', '20080923'),
					(N'Y', N'Yards', '20080923'),
					(N'Y3', N'Cubic Yards', '20080923');";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(29, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Select);
			Assert.IsNull(insert.Default);
			Assert.IsNull(insert.Execute);
			Assert.AreEqual(5, insert.Insert.Tokens.Count);
			Assert.AreEqual(24, insert.Values.Tokens.Count);
		}

		[Test]
		public void InsertStatement_ValuesWithColumns()
		{
			string sql = @"
				INSERT INTO Production.UnitMeasure
					(Name, UnitMeasureCode, ModifiedDate)
				VALUES (N'Square Yards', N'Y2', GETDATE());";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(22, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Select);
			Assert.IsNull(insert.Default);
			Assert.IsNull(insert.Execute);
			Assert.AreEqual(12, insert.Insert.Tokens.Count);
			Assert.AreEqual(10, insert.Values.Tokens.Count);
		}

		[Test]
		public void InsertStatement_DefaultValues()
		{
			string sql = @"
				INSERT INTO T1 DEFAULT VALUES;";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(5, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Select);
			Assert.IsNull(insert.Values);
			Assert.IsNull(insert.Execute);
			Assert.AreEqual(3, insert.Insert.Tokens.Count);
			Assert.AreEqual(2, insert.Default.Tokens.Count);
		}

		[Test]
		public void InsertStatement_Select()
		{
			string sql = @"
				INSERT INTO dbo.EmployeeSales
				SELECT 'SELECT', sp.BusinessEntityID, c.LastName, sp.SalesYTD
				FROM Sales.SalesPerson AS sp
				INNER JOIN Person.Person AS c
					ON sp.BusinessEntityID = c.BusinessEntityID
				WHERE sp.BusinessEntityID LIKE '2%'
				ORDER BY sp.BusinessEntityID, c.LastName;";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(55, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Values);
			Assert.IsNull(insert.Default);
			Assert.IsNull(insert.Execute);
			Assert.AreEqual(5, insert.Insert.Tokens.Count);
			Assert.AreEqual(50, insert.Select.Tokens.Count);
		}

		[Test]
		public void InsertStatement_Exec()
		{
			string sql = @"
				INSERT INTO dbo.EmployeeSales
				EXECUTE dbo.uspGetEmployeeSales;";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);
			TSQLInsertStatement insert = statements[0].AsInsert;

			Assert.AreEqual(9, insert.Tokens.Count);
			Assert.IsNull(insert.With);
			Assert.IsNull(insert.Output);
			Assert.IsNull(insert.Select);
			Assert.IsNull(insert.Default);
			Assert.IsNull(insert.Values);
			Assert.AreEqual(5, insert.Insert.Tokens.Count);
			Assert.AreEqual(4, insert.Execute.Tokens.Count);
		}
	}
}
