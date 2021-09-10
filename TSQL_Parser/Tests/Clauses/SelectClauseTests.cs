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
				var column = select.Columns[0].Expression.AsColumn;
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
				Assert.AreEqual(TSQLExpressionType.Operator, select.Columns[0].Expression.Type);
				var operatorExpression = select.Columns[0].Expression.AsOperator;
				Assert.AreEqual("/", operatorExpression.Operator.Text);
				Assert.AreEqual(TSQLExpressionType.Column, operatorExpression.LeftSide.Type);
				var leftSide = operatorExpression.LeftSide.AsColumn;
				Assert.AreEqual("oh", leftSide.TableReference.Single().AsIdentifier.Name);
				Assert.AreEqual("TaxAmt", leftSide.Column.Name);
				Assert.AreEqual(TSQLExpressionType.Column, operatorExpression.RightSide.Type);
				var rightSide = operatorExpression.RightSide.AsColumn;
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
	}
}
