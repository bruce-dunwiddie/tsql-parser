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
				Assert.AreEqual("a", column.Column);
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
				Assert.AreEqual("oh", leftSide.TableReference);
				Assert.AreEqual("TaxAmt", leftSide.Column);
				Assert.AreEqual(TSQLExpressionType.Column, operatorExpression.RightSide.Type);
				var rightSide = operatorExpression.RightSide.AsColumn;
				Assert.AreEqual("oh", rightSide.TableReference);
				Assert.AreEqual("SubTotal", rightSide.Column);
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
	}
}
