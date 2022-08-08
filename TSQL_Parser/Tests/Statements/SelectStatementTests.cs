using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Clauses;
using TSQL.Elements;
using TSQL.Expressions;
using TSQL.Statements;
using TSQL.Tokens;

namespace Tests.Statements
{
	[TestFixture(Category = "Statement Parsing")]
	public class SelectStatementTests
	{
		[Test]
		public void SelectStatement_SelectLiteral()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;",
				includeWhitespace : true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(3, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("1", select.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_SelectLiteralNoWhitespace()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;",
				includeWhitespace: false);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(2, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual("1", select.Tokens[1].AsNumericLiteral.Text);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_TwoLiteralSelects()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;select 2;",
				includeWhitespace: true);
			TSQLSelectStatement select1 = statements[0] as TSQLSelectStatement;
			TSQLSelectStatement select2 = statements[1] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(2, statements.Count);

			Assert.AreEqual(TSQLStatementType.Select, select1.Type);
			Assert.AreEqual(3, select1.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select1.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select1.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("1", select1.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(1, select1.Select.Columns.Count);

			Assert.AreEqual(TSQLStatementType.Select, select2.Type);
			Assert.AreEqual(3, select2.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select2.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select2.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("2", select2.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(1, select2.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_CorrelatedSelect()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select (select 1);",
				includeWhitespace: true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(7, statements[0].Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("(", select.Tokens[2].AsCharacter.Text);
			Assert.AreEqual("select", select.Tokens[3].AsKeyword.Text);
			Assert.AreEqual(" ", select.Tokens[4].AsWhitespace.Text);
			Assert.AreEqual("1", select.Tokens[5].AsNumericLiteral.Text);
			Assert.AreEqual(")", select.Tokens[6].AsCharacter.Text);
			Assert.AreEqual(1, select.Select.Columns.Count);
			Assert.AreEqual(
				TSQLExpressionType.Subquery, 
				select.Select.Columns[0].Expression.Type);
		}

		[Test]
		public void SelectStatement_CommonSelect()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"select t.a, t.b, (select 1) as e
				into #tempt
				from
					[table] t
						inner join [table] t2 on
							t.id = t2.id
				where
					t.c = 5
				group by
					t.a,
					t.b
				having
					count(*) > 1
				order by
					t.a,
					t.b;",
				includeWhitespace: true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(98, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			TSQLSelectClause selectClause = select.Select;
			Assert.AreEqual(3, selectClause.Columns.Count);
			TSQLSelectColumn column = selectClause.Columns[0];
			Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
			Assert.AreEqual("t", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
			Assert.AreEqual("a", column.Expression.AsColumn.Column.Name);
			column = selectClause.Columns[1];
			Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
			Assert.AreEqual("t", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
			Assert.AreEqual("b", column.Expression.AsColumn.Column.Name);
			column = selectClause.Columns[2];
			Assert.AreEqual("e", column.ColumnAlias.AsIdentifier.Name);
			Assert.AreEqual(TSQLExpressionType.Subquery, column.Expression.Type);
			TSQLSubqueryExpression subquery = column.Expression.AsSubquery;
			Assert.AreEqual(1, subquery.Select.Select.Columns.Count);
			Assert.AreEqual(1, subquery.Select.Select.Columns[0].Expression.AsConstant.Literal.AsNumericLiteral.Value);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("t", select.Tokens[2].AsIdentifier.Name);
			Assert.AreEqual(TSQLCharacters.Period, select.Tokens[3].AsCharacter.Character);
			Assert.AreEqual(22, select.Select.Tokens.Count);
			Assert.AreEqual(4, select.Into.Tokens.Count);
			Assert.AreEqual(26, select.From.Tokens.Count);
			Assert.AreEqual(10, select.Where.Tokens.Count);
			Assert.AreEqual(13, select.GroupBy.Tokens.Count);
			Assert.AreEqual(11, select.Having.Tokens.Count);
			Assert.AreEqual(12, select.OrderBy.Tokens.Count);
			Assert.AreEqual(3, select.Select.Columns.Count);
			Assert.AreEqual("e", select.Select.Columns[2].ColumnAlias.Text);
		}

		[Test]
		public void SelectStatement_MultipleSelectsWithoutSemicolon()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1 select 1",
				includeWhitespace: true);
			TSQLSelectStatement select1 = statements[0] as TSQLSelectStatement;
			TSQLSelectStatement select2 = statements[1] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(2, statements.Count);

			Assert.AreEqual(TSQLStatementType.Select, select1.Type);
			Assert.AreEqual(1, select1.Select.Columns.Count);

			Assert.AreEqual(TSQLStatementType.Select, select2.Type);
			Assert.AreEqual(1, select2.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_Option()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT *
				FROM
					Sales.SalesOrderHeader oh
				OPTION (FAST 10)",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(12, select.Tokens.Count);
			Assert.IsNotNull(select.Option);
			Assert.AreEqual(5, select.Option.Tokens.Count);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_DontEatFinalDescAsKeyword()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"select 1 as blah order by 1 desc select 1",
				includeWhitespace: false);

			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(TSQLStatementType.Select, statements[1].Type);

			TSQLSelectStatement select1 = statements[0] as TSQLSelectStatement;
			TSQLSelectStatement select2 = statements[1] as TSQLSelectStatement;

			Assert.AreEqual(8, select1.Tokens.Count);
			Assert.AreEqual(1, select1.Select.Columns.Count);

			Assert.AreEqual(2, select2.Tokens.Count);
			Assert.AreEqual(1, select2.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_WindowedAggregate()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(@"
				SELECT 
				*, 
				ROW_NUMBER() OVER (
				PARTITION BY 
				some_field 
				ORDER BY 
				some_other_field) AS some_row_number
				FROM my_db.my_schema.my_table", includeWhitespace:false);

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(23, select.Tokens.Count);

			Assert.AreEqual(17, select.Select.Tokens.Count);

			Assert.AreEqual(6, select.From.Tokens.Count);

			Assert.AreEqual(2, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_MultiLevelParens()
		{
			string query = "SELECT ((A/B)-1) FROM SomeTable";
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(query);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			TSQLSelectStatement selectStatement = statements[0].AsSelect;
			Assert.AreEqual(12, selectStatement.Tokens.Count);
			TSQLSelectClause selectClause = selectStatement.Select;
			Assert.AreEqual(1, selectClause.Columns.Count);
			// outer parens
			TSQLExpression lvl1Expression = selectClause.Columns[0].Expression;
			Assert.AreEqual(TSQLExpressionType.Grouped, lvl1Expression.Type);
			// contents of outer parens
			TSQLExpression lvl2Expression = lvl1Expression.AsGrouped.InnerExpression;
			Assert.AreEqual(TSQLExpressionType.Operation, lvl2Expression.Type);
			Assert.AreEqual("-", lvl2Expression.AsOperation.Operator.Text);
			// (A/B)
			TSQLExpression lvl2aExpression = lvl2Expression.AsOperation.LeftSide;
			// 1
			TSQLExpression lvl2bExpression = lvl2Expression.AsOperation.RightSide;
			Assert.AreEqual(TSQLExpressionType.Grouped, lvl2aExpression.Type);
			Assert.AreEqual(TSQLExpressionType.Constant, lvl2bExpression.Type);
			Assert.AreEqual(1, lvl2bExpression.AsConstant.Literal.AsNumericLiteral.Value);
			// A/B
			TSQLExpression lvl3Expression = lvl2aExpression.AsGrouped.InnerExpression;
			Assert.AreEqual(TSQLExpressionType.Operation, lvl3Expression.Type);
			Assert.AreEqual("/", lvl3Expression.AsOperation.Operator.Text);
			// A
			TSQLExpression lvl3aExpression = lvl3Expression.AsOperation.LeftSide;
			// B
			TSQLExpression lvl3bExpression = lvl3Expression.AsOperation.RightSide;
			Assert.AreEqual(TSQLExpressionType.Column, lvl3aExpression.Type);
			Assert.AreEqual("A", lvl3aExpression.AsColumn.Column.Name);
			Assert.IsNull(lvl3aExpression.AsColumn.TableReference);
			Assert.AreEqual(TSQLExpressionType.Column, lvl3bExpression.Type);
			Assert.AreEqual("B", lvl3bExpression.AsColumn.Column.Name);
			Assert.IsNull(lvl3bExpression.AsColumn.TableReference);
		}

		[Test]
		public void SelectStatement_CaseInJoin()
		{
			string query = @"SELECT a.*
				FROM SomeTable a JOIN SomeTable b
					ON CASE WHEN a.Value = 1 THEN 'One' ELSE 'Other' END = 'One'";

			var statements = TSQLStatementReader.ParseStatements(query, includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(4, select.Select.Tokens.Count);
			Assert.AreEqual(21, select.From.Tokens.Count);
			Assert.IsNull(select.Where);
			Assert.AreEqual(1, select.Select.Columns.Count);
			Assert.AreEqual(3, select.Select.Columns[0].Tokens.Count);
			Assert.AreEqual("a", select.Select.Columns[0].Expression.AsMulticolumn.TableReference.Single().Text);
		}

		[Test]
		public void SelectStatement_CaseInSelect()
		{
			string query = "SELECT Value, CASE WHEN Value = 1 THEN 'One' ELSE 'Other' END AS Cased FROM SomeTable";

			var statements = TSQLStatementReader.ParseStatements(query, includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(15, select.Select.Tokens.Count);
			Assert.AreEqual(2, select.Select.Columns.Count);
			Assert.AreEqual("Value", select.Select.Columns[0].Expression.AsColumn.Column.Name);
			TSQLSelectColumn column = select.Select.Columns[1];
			Assert.AreEqual("Cased", column.ColumnAlias.AsIdentifier.Name);
			Assert.AreEqual(TSQLExpressionType.Case, column.Expression.Type);
			Assert.AreEqual(2, select.From.Tokens.Count);
			Assert.AreEqual("FROM SomeTable",
				query.Substring(
					select.From.BeginPosition, 
					select.From.Length));
			Assert.AreEqual(2, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_CaseInWhere()
		{
			string query = "SELECT Value FROM SomeTable WHERE (CASE WHEN Value = 1 THEN 'One' ELSE 'Other' END) = 'One'";

			var statements = TSQLStatementReader.ParseStatements(query, includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(2, select.Select.Tokens.Count);
			Assert.AreEqual("Value", select.Select.Columns[0].Expression.AsColumn.Column.Name);
			Assert.AreEqual(2, select.From.Tokens.Count);
			Assert.AreEqual("FROM SomeTable",
				query.Substring(
					select.From.BeginPosition,
					select.From.Length));
			Assert.AreEqual(15, select.Where.Tokens.Count);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_TableHint()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT *
				FROM
					Sales.SalesOrderHeader oh WITH (NOLOCK)
				ORDER BY
					oh.OrderDate;",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);

			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.AreEqual(16, select.Tokens.Count);
			Assert.AreEqual("OrderDate", select.Tokens[15].Text);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_Dont_Overrun()
		{
			string sql = @"
				BEGIN
					SELECT 1
				END";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);

			Assert.AreEqual(3, statements.Count);
			Assert.AreEqual(1, statements[0].Tokens.Count);
			Assert.AreEqual(2, statements[1].Tokens.Count);
			Assert.AreEqual("SELECT 1",
				sql.Substring(
					statements[1].BeginPosition,
					statements[1].Length));
			Assert.AreEqual(1, statements[1].AsSelect.Select.Columns.Count);
			Assert.AreEqual(1, statements[2].Tokens.Count);
			Assert.IsTrue(statements[2].Tokens[0].IsKeyword(TSQLKeywords.END));
		}

		[Test]
		public void SelectStatement_StartWithParens()
		{
			string sql = @"(SELECT 1)";

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				sql,
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(4, select.Tokens.Count);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}

		[Test]
		public void SelectStatement_UnionDontOverrun()
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
			Assert.IsNotNull(statements[0].AsSelect.SetOperators.SingleOrDefault());
			Assert.AreEqual(36, statements[0].AsSelect.SetOperators.Single().Tokens.Count);
			Assert.IsNotNull(statements[0].AsSelect.SetOperators.Single().Select);
			Assert.AreEqual(34, statements[0].AsSelect.SetOperators.Single().Select.Tokens.Count);
			Assert.IsNull(statements[0].AsSelect.SetOperators.Single().Select.OrderBy);
			Assert.IsNotNull(statements[0].AsSelect.OrderBy);
		}

		[Test]
		public void SelectStatement_UnionDontOverrunWithWhitespace()
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
				includeWhitespace: true);

			Assert.AreEqual(1, statements.Count);
			Assert.IsNotNull(statements[0].AsSelect.SetOperators.SingleOrDefault());
			Assert.AreEqual(61, statements[0].AsSelect.SetOperators.Single().Tokens.Count);
			Assert.IsNotNull(statements[0].AsSelect.SetOperators.Single().Select);
			Assert.AreEqual(57, statements[0].AsSelect.SetOperators.Single().Select.Tokens.Count);
			Assert.IsNull(statements[0].AsSelect.SetOperators.Single().Select.OrderBy);
			Assert.IsNotNull(statements[0].AsSelect.OrderBy);
		}

		[Test]
		public void SelectStatement_UnionParensRegression()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/69

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION (SELECT 2)",
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(7, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperators.SingleOrDefault());
		}

		[Test]
		public void SelectStatement_UnionParensRegressionWithWhitespace()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/69

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION (SELECT 2)",
				includeWhitespace: true);

			TSQLSelectStatement select = statements[0].AsSelect;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(11, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperators.SingleOrDefault());
		}

		[Test]
		public void SelectStatement_UnionMultiParens()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION ((SELECT 2)) SELECT 1",
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;
			TSQLSelectStatement select2 = statements[1].AsSelect;

			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(9, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperators.SingleOrDefault());
			Assert.AreEqual(2, select2.Tokens.Count);
		}

		[Test]
		public void SelectStatement_UnionMultiParensWithWhitespace()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1 UNION ((SELECT 2)) SELECT 1",
				includeWhitespace: true);

			TSQLSelectStatement select = statements[0].AsSelect;
			TSQLSelectStatement select2 = statements[1].AsSelect;

			Assert.AreEqual(2, statements.Count);
			Assert.AreEqual(14, select.Tokens.Count);
			Assert.IsNotNull(select.SetOperators.SingleOrDefault());
			Assert.AreEqual(3, select2.Tokens.Count);
		}

		[Test]
		public void SelectStatement_MultipleSetOperatorsRegression()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/81
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1
				UNION
				SELECT 2
				UNION
				SELECT 3",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(8, select.Tokens.Count);

			Assert.AreEqual(2, select.SetOperators.Count);

			Assert.AreEqual(1, select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);

			Assert.AreEqual(2, select
				.SetOperators[0]
				.Select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);

			Assert.AreEqual(3, select
				.SetOperators[1]
				.Select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);
		}

		[Test]
		public void SelectStatement_MultipleSetOperatorsRegressionWithWhitespace()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/81
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1
				UNION
				SELECT 2
				UNION
				SELECT 3",
				includeWhitespace: true);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(15, select.Tokens.Count);

			Assert.AreEqual(2, select.SetOperators.Count);

			Assert.AreEqual(1, select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);

			Assert.AreEqual(2, select
				.SetOperators[0]
				.Select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);

			Assert.AreEqual(3, select
				.SetOperators[1]
				.Select
				.Select
				.Columns
				.Single()
				.Expression
				.AsConstant
				.Literal
				.AsNumericLiteral
				.Value);
		}

		[Test]
		public void SelectStatement_DistinctAndTop()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT DISTINCT TOP(5) FirstName
				FROM
					Person.Person p;",
				includeWhitespace: false);

			Assert.AreEqual("FirstName", statements
				.Single()
				.AsSelect
				.Select
				.Columns
				.Single()
				.Expression
				.AsColumn
				.Column
				.Name);
		}

		[Test]
		public void SelectStatement_CountAlias()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT COUNT(*) as count FROM sqlite_master",
				includeWhitespace: false);

			TSQLSelectColumn count = statements
				.Single()
				.AsSelect
				.Select
				.Columns
				.Single();

			Assert.AreEqual("COUNT", count.Expression.AsFunction.Function.Name);
			Assert.AreEqual("count", count.ColumnAlias.Name);
		}

		[Test]
		public void SelectStatement_CountAliasWithWhitespace()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT COUNT ( * ) as count FROM sqlite_master",
				includeWhitespace: true);

			TSQLSelectColumn count = statements
				.Single()
				.AsSelect
				.Select
				.Columns
				.Single();

			Assert.AreEqual("COUNT", count.Expression.AsFunction.Function.Name);
			Assert.AreEqual("count", count.ColumnAlias.Name);
		}

		[Test]
		public void SelectStatement_CASTMissingASRegression()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/89
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT CAST ( 123.45 AS INT ) ",
				includeWhitespace: true);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(14, select.Tokens.Count);
		}

		[Test]
		public void SelectStatement_system_user_Regression()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/93
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT system_user;",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(3, select.Tokens.Count);
			Assert.AreEqual("system_user", select.Select.Columns[0].Expression.AsColumn.Column.Name);
		}

		[Test]
		public void SelectStatement_system_user_Regression_without_semicolon()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT system_user",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(2, select.Tokens.Count);
			Assert.AreEqual("system_user", select.Select.Columns[0].Expression.AsColumn.Column.Name);
		}

		[Test]
		public void SelectStatement_CAST_argument_parsing()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/98
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT CAST(123.45 AS INT), CAST(456.321 AS VARCHAR(10)), Column_1 FROM MyTable",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(21, select.Tokens.Count);
			Assert.AreEqual(3, select.Select.Columns.Count);
			TSQLFunctionExpression function = select.Select.Columns[0].Expression.AsFunction;
			Assert.AreEqual(6, function.Tokens.Count);
			TSQLValueAsTypeExpression argument = function.Arguments[0].AsValueAsType;
			Assert.AreEqual(3, argument.Tokens.Count);
			Assert.AreEqual(123.45, argument.Expression.AsConstant.Literal.AsNumericLiteral.Value);
			Assert.AreEqual("INT", argument.DataType);
			function = select.Select.Columns[1].Expression.AsFunction;
			Assert.AreEqual(9, function.Tokens.Count);
			argument = function.Arguments[0].AsValueAsType;
			Assert.AreEqual(6, argument.Tokens.Count);
			Assert.AreEqual(456.321, argument.Expression.AsConstant.Literal.AsNumericLiteral.Value);
			Assert.AreEqual("VARCHAR(10)", argument.DataType);
		}

		[Test]
		public void SelectStatement_CAST_argument_parsing_with_whitespace()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/98
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT CAST(123.45 AS INT), CAST(456.321 AS VARCHAR(10) ), Column_1 FROM MyTable",
				includeWhitespace: true);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(31, select.Tokens.Count);
			Assert.AreEqual(3, select.Select.Columns.Count);
			TSQLFunctionExpression function = select.Select.Columns[0].Expression.AsFunction;
			Assert.AreEqual(8, function.Tokens.Count);
			TSQLValueAsTypeExpression argument = function.Arguments[0].AsValueAsType;
			Assert.AreEqual(5, argument.Tokens.Count);
			Assert.AreEqual(123.45, argument.Expression.AsConstant.Literal.AsNumericLiteral.Value);
			Assert.AreEqual("INT", argument.DataType);
			function = select.Select.Columns[1].Expression.AsFunction;
			Assert.AreEqual(12, function.Tokens.Count);
			argument = function.Arguments[0].AsValueAsType;
			Assert.AreEqual(9, argument.Tokens.Count);
			Assert.AreEqual(456.321, argument.Expression.AsConstant.Literal.AsNumericLiteral.Value);
			Assert.AreEqual("VARCHAR(10)", argument.DataType);
		}

		[Test]
		public void SelectStatement_ColumnAliasAsEquals()
		{
			// example from https://docs.microsoft.com/en-us/sql/t-sql/queries/select-examples-transact-sql?view=sql-server-ver16
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT p.Name AS ProductName, 
				NonDiscountSales = (OrderQty * UnitPrice),
				Discounts = ((OrderQty * UnitPrice) * UnitPriceDiscount)
				FROM Production.Product AS p 
				INNER JOIN Sales.SalesOrderDetail AS sod
				ON p.ProductID = sod.ProductID 
				ORDER BY ProductName DESC;",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(3, select.Select.Columns.Count);
			Assert.AreEqual("NonDiscountSales", select.Select.Columns[1].ColumnAlias.Name);
			Assert.AreEqual("Discounts", select.Select.Columns[2].ColumnAlias.Name);
		}

		[Test]
		public void SelectStatement_SELECT_DISTINCT()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/101
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 
					COUNT(DISTINCT id) AssetId_changes
				FROM [gs].[ESG_ResolvedGSID_Merged]",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(11, select.Tokens.Count);
			Assert.AreEqual(1, select.Select.Columns.Count);
			Assert.AreEqual("AssetId_changes", select.Select.Columns[0].ColumnAlias.Name);
			Assert.AreEqual("id", 
				select
				.Select
				.Columns[0]
				.Expression
				.AsFunction
				.Arguments[0]
				.AsDuplicateSpecification
				.InnerExpression
				.AsColumn
				.Column
				.Name);
		}

		[Test]
		public void SelectStatement_SELECT_NULL()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/104
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"SELECT 1, 2, 3, NULL, 5 FROM MyTable",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(12, select.Tokens.Count);
			Assert.AreEqual(5, select.Select.Columns.Count);
			Assert.AreEqual(TSQLExpressionType.Null, select.Select.Columns[3].Expression.Type);
			Assert.AreEqual(5, select.Select.Columns[4].Expression.AsConstant.Literal.AsNumericLiteral.Value);
		}

		[Test]
		public void SelectStatement_ChainedOperators()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/110
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"select isnull(a, 0) * isnull(b, 0) / 100 as Result from myTable",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);
			TSQLSelectStatement select = statements.Single().AsSelect;
			Assert.AreEqual(20, select.Tokens.Count);
			Assert.AreEqual(1, select.Select.Columns.Count);
		}
	}
}
