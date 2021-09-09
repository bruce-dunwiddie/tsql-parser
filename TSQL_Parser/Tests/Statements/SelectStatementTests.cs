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
			Assert.AreEqual(TSQLExpressionType.Operator, lvl2Expression.Type);
			Assert.AreEqual("-", lvl2Expression.AsOperator.Operator.Text);
			// (A/B)
			TSQLExpression lvl2aExpression = lvl2Expression.AsOperator.LeftSide;
			// 1
			TSQLExpression lvl2bExpression = lvl2Expression.AsOperator.RightSide;
			Assert.AreEqual(TSQLExpressionType.Grouped, lvl2aExpression.Type);
			Assert.AreEqual(TSQLExpressionType.Constant, lvl2bExpression.Type);
			Assert.AreEqual(1, lvl2bExpression.AsConstant.Literal.AsNumericLiteral.Value);
			// A/B
			TSQLExpression lvl3Expression = lvl2aExpression.AsGrouped.InnerExpression;
			Assert.AreEqual(TSQLExpressionType.Operator, lvl3Expression.Type);
			Assert.AreEqual("/", lvl3Expression.AsOperator.Operator.Text);
			// A
			TSQLExpression lvl3aExpression = lvl3Expression.AsOperator.LeftSide;
			// B
			TSQLExpression lvl3bExpression = lvl3Expression.AsOperator.RightSide;
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
	}
}
