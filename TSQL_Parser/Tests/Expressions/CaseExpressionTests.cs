using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using TSQL;
using TSQL.Statements;
using TSQL.Tokens;

using Tests.Tokens;
using TSQL.Expressions.Parsers;

namespace Tests.Expressions
{
	[TestFixture(Category = "Expression Parsing")]
	public class CaseExpressionTests
	{
		[Test]
		public void CaseExpression_Dont_Overrun()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
					BEGIN
						SELECT CASE WHEN 1 = 2 THEN 0 ELSE 1 END
					END"
					// normalizing line endings to unix format to ensure passing
					// tests in various environments
					.Replace("\r", ""),
				includeWhitespace: false);

			Assert.AreEqual(3, statements.Count);
			Assert.AreEqual(1, statements[0].Tokens.Count);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(18, "SELECT"),
						new TSQLKeyword(25, "CASE"),
						new TSQLKeyword(30, "WHEN"),
						new TSQLNumericLiteral(35, "1"),
						new TSQLOperator(37, "="),
						new TSQLNumericLiteral(39, "2"),
						new TSQLKeyword(41, "THEN"),
						new TSQLNumericLiteral(46, "0"),
						new TSQLKeyword(48, "ELSE"),
						new TSQLNumericLiteral(53, "1"),
						new TSQLKeyword(55, "END")
					},
				statements[1].Tokens);

			Assert.AreEqual(1, statements[2].Tokens.Count);
			Assert.IsTrue(statements[2].Tokens[0].IsKeyword(TSQLKeywords.END));
		}

		[Test]
		public void CaseExpression_Regression_Duplicated_Case()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"select case when abi = '03654' then 0 else 1 end as abi;",
				includeWhitespace: false);

			Assert.AreEqual(1, statements.Count);

			List<TSQLToken> tokens = statements[0].Tokens;

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "select"),
						new TSQLKeyword(7, "case"),
						new TSQLKeyword(12, "when"),
						new TSQLIdentifier(17, "abi"),
						new TSQLOperator(21, "="),
						new TSQLStringLiteral(23, "'03654'"),
						new TSQLKeyword(31, "then"),
						new TSQLNumericLiteral(36, "0"),
						new TSQLKeyword(38, "else"),
						new TSQLNumericLiteral(43, "1"),
						new TSQLKeyword(45, "end"),
						new TSQLKeyword(49, "as"),
						new TSQLIdentifier(52, "abi")
					},
				tokens);
		}

		[TestCase(false, 30)]
		[TestCase(true, 31)]
		public void CaseExpression_Should_Include_TrailingWhitespace_If_Flag_Is_True(bool includeWhiteSpace, int expectedEndPosition)
		{
			//                  0123456789012345678901234567890123456789
			const string sql = "CASE 1 WHEN 2 THEN 2 ELSE 3 END ";

			TSQLTokenizer tokenizer = new TSQLTokenizer(sql)
			{
				IncludeWhitespace = includeWhiteSpace
			};

			Assert.IsTrue(tokenizer.MoveNext());

			var expression = new TSQLCaseExpressionParser().Parse(tokenizer);
			Assert.NotNull(expression.InputExpression);

			var expectedInputTokenExpression = includeWhiteSpace
				? new List<TSQLToken>
				{
					new TSQLNumericLiteral(5, "1"),
					new TSQLWhitespace(6, " "),
				}
				: new List<TSQLToken>
				{
					new TSQLNumericLiteral(5, "1"),
				};

			TokenComparisons.CompareTokenLists(expectedInputTokenExpression, expression.InputExpression.Tokens);
			Assert.AreEqual(0, expression.BeginPosition);
			Assert.AreEqual(expectedEndPosition, expression.EndPosition);

		}

		[Test]
		public void CaseExpression_Else_Is_Optional()
		{
			const string sql = @"CASE 
WHEN TBL.COL = 25 THEN 30
END";

			TSQLTokenizer tokenizer = new TSQLTokenizer(sql);

			Assert.IsTrue(tokenizer.MoveNext());

			var expression = new TSQLCaseExpressionParser().Parse(tokenizer);
			Assert.AreEqual(1, expression.WhenExpressions.Count);
			Assert.AreEqual("TBL . COL = 25", expression.WhenExpressions.First().TokensAsText());
			CollectionAssert.AllItemsAreNotNull(expression.Tokens);
			Assert.AreEqual(0, expression.BeginPosition);
		}

		[Test]
		public void CaseExpression_Inside_CaseExpression()
		{
			const string sql = @"CASE 10
WHEN 20 THEN 30
ELSE CASE 40
 WHEN 50 THEN 60
 ELSE 70
 END
END";

			TSQLTokenizer tokenizer = new TSQLTokenizer(sql);

			Assert.IsTrue(tokenizer.MoveNext());

			var expression = new TSQLCaseExpressionParser().Parse(tokenizer);
			CollectionAssert.AllItemsAreNotNull(expression.Tokens);
			Assert.AreEqual(0, expression.BeginPosition);

		}

		[TestCase("CASE END", "should have a WHEN keyword")]
		// [TestCase("CASE WHEN END THEN END END", "should be a THEN keyword")]
		[TestCase("CASE WHEN", "should be an expression")]
		[TestCase("CASE WHEN END", "should be a THEN keyword")]
		public void CaseExpression_ParseException(string sql, string message)
		{
			TSQLTokenizer tokenizer = new TSQLTokenizer(sql);

			Assert.IsTrue(tokenizer.MoveNext());

			var exception = Assert.Throws<TSQLParseException>(() => new TSQLCaseExpressionParser().Parse(tokenizer));
			StringAssert.Contains(message, exception.Message);
		}

		[Test]
		public void CaseExpression_Trailing_Comments_Stress_Test()
		{
			const string sql = @"CASE  -- COMMENT
  T.COL * 25 -- COMMENT

WHEN /* COMMENT */ 20 * 25 -- COMMENT 
 THEN -- COMMENT
 30 -- COMMENT
ELSE -- COMMENT 

CASE /* MULTI-LINE 
        COMMENT */

 40 -- COMMENT
 WHEN /* COMMENT */ 50 /* COMMENT */ THEN /* COMMENT */
              
                     60

 ELSE 70
 END
      -- COMMENT 
         END /* MULTILINE COMMENT
  */
";

			TSQLTokenizer tokenizer = new TSQLTokenizer(sql);

			Assert.IsTrue(tokenizer.MoveNext());

			var expression = new TSQLCaseExpressionParser().Parse(tokenizer);
			CollectionAssert.AllItemsAreNotNull(expression.Tokens);
			Assert.AreEqual(0, expression.BeginPosition);
		}
	}
}
