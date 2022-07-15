using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Elements;
using TSQL.Expressions;
using TSQL.Tokens;

namespace Tests.Clauses
{
	[TestFixture(Category = "Clause Parsing")]
	public class SelectClauseTests
	{
		[Test]
		public void SelectClause_StopAtFrom()
		{
			using (StringReader reader = new StringReader(@"select a from b;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(2, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(1, select.Columns.Count);
				Assert.IsNull(select.Columns[0].ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, select.Columns[0].Expression.Type);

				TSQLColumnExpression column = select.Columns[0].Expression.AsColumn;
				Assert.IsNull(column.TableReference);
				Assert.AreEqual("a", column.Column.Name);
			}
		}

		[Test]
		public void SelectClause_Comments()
		{
			using (StringReader reader = new StringReader(
				@"select top 1
					oh.TaxAmt / oh.SubTotal /* tax percent */
				from
					Sales.SalesOrderHeader oh;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(11, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(1, select.Columns.Count);
				Assert.IsNull(select.Columns[0].ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Operation, select.Columns[0].Expression.Type);

				TSQLOperationExpression operationExpression = select.Columns[0].Expression.AsOperation;
				Assert.AreEqual("/", operationExpression.Operator.Text);
				Assert.AreEqual(TSQLExpressionType.Column, operationExpression.LeftSide.Type);

				TSQLColumnExpression leftSide = operationExpression.LeftSide.AsColumn;
				Assert.AreEqual("oh", leftSide.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("TaxAmt", leftSide.Column.Name);
				Assert.AreEqual(TSQLExpressionType.Column, operationExpression.RightSide.Type);

				TSQLColumnExpression rightSide = operationExpression.RightSide.AsColumn;
				Assert.AreEqual("oh", rightSide.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("SubTotal", rightSide.Column.Name);
				Assert.AreEqual(" tax percent ", select.Columns.Last().Tokens.Last().AsMultilineComment.Comment);
			}
		}

		[Test]
		public void SelectClause_SanityCheck()
		{
			using (StringReader reader = new StringReader(
				@"bogus"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());
				Exception ex = Assert.Throws<InvalidOperationException>(
					delegate
					{
						TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
					});
			}
		}

		[Test]
		public void SelectClause_ColumnAliasSyntaxes()
		{
			using (StringReader reader = new StringReader(
				@"SELECT
					ProductID,
					p.[Name],
					p.ProductNumber [Number],
					p.MakeFlag AS IsMake,
					IsFinishedGoods = p.FinishedGoodsFlag,
					@Category AS ProductCategory,
					p.*
				FROM
					Production.Product p;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(31, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(7, select.Columns.Count);

				TSQLSelectColumn column = select.Columns[0];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.IsNull(column.Expression.AsColumn.TableReference);
				Assert.AreEqual("ProductID", column.Expression.AsColumn.Column.Name);

				column = select.Columns[1];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("Name", column.Expression.AsColumn.Column.Name);

				column = select.Columns[2];

				Assert.AreEqual("Number", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("ProductNumber", column.Expression.AsColumn.Column.Name);

				column = select.Columns[3];

				Assert.AreEqual("IsMake", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("MakeFlag", column.Expression.AsColumn.Column.Name);

				column = select.Columns[4];

				Assert.AreEqual("IsFinishedGoods", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("FinishedGoodsFlag", column.Expression.AsColumn.Column.Name);

				column = select.Columns[5];

				Assert.AreEqual("ProductCategory", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Variable, column.Expression.Type);
				Assert.AreEqual("@Category", column.Expression.AsVariable.Variable.Text);

				column = select.Columns[6];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Multicolumn, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsMulticolumn.TableReference.Single().AsIdentifier.Name);
			}
		}

		[Test]
		public void SelectClause_VariableAssignment()
		{
			using (StringReader reader = new StringReader(
				@"SELECT @id = p.ProductID
				FROM Production.Product p
				WHERE
					p.[Name] = 'Blade';"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(6, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(1, select.Columns.Count);

				TSQLSelectColumn column = select.Columns[0];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.VariableAssignment, column.Expression.Type);

				TSQLVariableAssignmentExpression assignmentExpression = column.Expression.AsVariableAssignment;
				Assert.AreEqual("=", assignmentExpression.Operator.Text);

				Assert.AreEqual("@id", assignmentExpression.Variable.Text);

				TSQLColumnExpression columnExpression = assignmentExpression.ValueExpression.AsColumn;
				Assert.AreEqual("p", columnExpression.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("ProductID", columnExpression.Column.Name);
			}
		}

		[Test]
		public void SelectClause_WindowedAggregate()
		{
			using (StringReader reader = new StringReader(
				@"SELECT
					p.ProductID,
					p.[Name],
					ROW_NUMBER() OVER (
						PARTITION BY 
							p.DaysToManufacture 
						ORDER BY 
							p.[Name]) AS GroupNumber
				FROM
					Production.Product p
				ORDER BY
					p.DaysToManufacture,
					p.[Name];"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(27, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(3, select.Columns.Count);

				TSQLSelectColumn column = select.Columns[0];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("ProductID", column.Expression.AsColumn.Column.Name);

				column = select.Columns[1];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("Name", column.Expression.AsColumn.Column.Name);

				column = select.Columns[2];

				Assert.AreEqual("GroupNumber", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Function, column.Expression.Type);
				Assert.AreEqual("ROW_NUMBER", column.Expression.AsFunction.Function.Name);
			}
		}

		[Test]
		public void SelectClause_FullyQualifiedFunction()
		{
			using (StringReader reader = new StringReader(
				@"SELECT
					p.ProductID, 
					p.[Name],
					Test.dbo.Multiply(p.SafetyStockLevel, p.StandardCost) AS RestockCost
				FROM
					Production.Product p
				ORDER BY
					p.[Name];"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(25, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);

				Assert.AreEqual(3, select.Columns.Count);

				TSQLSelectColumn column = select.Columns[0];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("ProductID", column.Expression.AsColumn.Column.Name);

				column = select.Columns[1];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Column, column.Expression.Type);
				Assert.AreEqual("p", column.Expression.AsColumn.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("Name", column.Expression.AsColumn.Column.Name);

				column = select.Columns[2];

				Assert.AreEqual("RestockCost", column.ColumnAlias.Name);
				Assert.AreEqual(TSQLExpressionType.Function, column.Expression.Type);

				TSQLFunctionExpression functionExpression = column.Expression.AsFunction;

				Assert.AreEqual("Multiply", functionExpression.Function.Name);
				Assert.AreEqual(3, functionExpression.QualifiedPath.Count);
				Assert.AreEqual("Test", functionExpression.QualifiedPath[0].AsIdentifier.Name);
				Assert.AreEqual(".", functionExpression.QualifiedPath[1].AsCharacter.Text);
				Assert.AreEqual("dbo", functionExpression.QualifiedPath[2].AsIdentifier.Name);

				TSQLColumnExpression argumentExpression = functionExpression.Arguments[0].AsColumn;
				Assert.AreEqual("p", argumentExpression.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("SafetyStockLevel", argumentExpression.Column.Name);

				argumentExpression = functionExpression.Arguments[1].AsColumn;
				Assert.AreEqual("p", argumentExpression.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("StandardCost", argumentExpression.Column.Name);
			}
		}

		[Test]
		public void SelectClause_UnaryOperator()
		{
			using (StringReader reader = new StringReader(
				@"SELECT
					+1;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());

				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(3, select.Tokens.Count);
				Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon));

				Assert.AreEqual(1, select.Columns.Count);

				TSQLSelectColumn column = select.Columns[0];

				Assert.IsNull(column.ColumnAlias);
				Assert.AreEqual(TSQLExpressionType.Operation, column.Expression.Type);

				TSQLOperationExpression tsqlOperator = column.Expression.AsOperation;

				Assert.AreEqual("+", tsqlOperator.Operator.Text);
				Assert.IsNull(tsqlOperator.LeftSide);

				Assert.AreEqual(TSQLExpressionType.Constant, tsqlOperator.RightSide.Type);
				Assert.AreEqual(1, tsqlOperator.RightSide.AsConstant.Literal.AsNumericLiteral.Value);
			}
		}
	}
}
