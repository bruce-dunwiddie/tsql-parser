using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Expressions;
using TSQL.Expressions.Parsers;
using TSQL.Tokens;

using Tests.Tokens;

namespace Tests.Expressions
{
	[TestFixture(Category = "Expression Parsing")]
	public class OperationExpressionTests
	{
		[Test]
		public void OperationExpression_Simple()
		{
			TSQLTokenizer tokenizer = new TSQLTokenizer(
				"+ 2 - 3")
			{
				IncludeWhitespace = true
			};

			TSQLConstantExpression leftSide = new TSQLConstantExpression()
			{
				Literal = new TSQLNumericLiteral(
					0,
					"1")
			};

			leftSide.Tokens.Add(leftSide.Literal);

			Assert.IsTrue(tokenizer.MoveNext());

			TSQLOperationExpression op = new TSQLOperationExpressionParser().Parse(
				tokenizer,
				leftSide);

			Assert.AreSame(leftSide, op.LeftSide);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						// bogus begin position because we made it up
						new TSQLNumericLiteral(0, "1"),

						new TSQLOperator(0, "+"),
						new TSQLWhitespace(1, " "),
						new TSQLNumericLiteral(2, "2"),
						new TSQLWhitespace(3, " "),
						new TSQLOperator(4, "-"),
						new TSQLWhitespace(5, " "),
						new TSQLNumericLiteral(6, "3")
					},
				op.Tokens);

			Assert.AreEqual("+", op.Operator.Text);

			Assert.AreEqual(TSQLExpressionType.Operation, op.RightSide.Type);
			TSQLOperationExpression rightSide = op.RightSide.AsOperation;
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLNumericLiteral(2, "2"),
						new TSQLWhitespace(3, " "),
						new TSQLOperator(4, "-"),
						new TSQLWhitespace(5, " "),
						new TSQLNumericLiteral(6, "3")
					},
				rightSide.Tokens);

			Assert.AreEqual("-", rightSide.Operator.Text);

			Assert.AreEqual(TSQLExpressionType.Constant, rightSide.LeftSide.Type);
			TSQLConstantExpression leftNumber = rightSide.LeftSide.AsConstant;
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLNumericLiteral(2, "2"),
						new TSQLWhitespace(3, " ")
					},
				leftNumber.Tokens);
			Assert.AreEqual(TSQLTokenType.NumericLiteral, leftNumber.Literal.Type);
			Assert.AreEqual(2, leftNumber.Literal.AsNumericLiteral.Value);

			Assert.AreEqual(TSQLExpressionType.Constant, rightSide.RightSide.Type);
			TSQLConstantExpression rightNumber = rightSide.RightSide.AsConstant;
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLNumericLiteral(6, "3")
					},
				rightNumber.Tokens);
			Assert.AreEqual(TSQLTokenType.NumericLiteral, rightNumber.Literal.Type);
			Assert.AreEqual(3, rightNumber.Literal.AsNumericLiteral.Value);
		}
	}
}
