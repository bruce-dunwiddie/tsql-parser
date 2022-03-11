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
	public class ArgumentListTests
	{
		[Test]
		public void ArgumentList_Simple()
		{
			TSQLTokenizer tokenizer = new TSQLTokenizer(
				"dostuff( 1 , 2 )")
			{
				IncludeWhitespace = true
			};

			Assert.IsTrue(tokenizer.MoveNext());
			Assert.AreEqual("dostuff", tokenizer.Current.Text);
			Assert.IsTrue(tokenizer.MoveNext());
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses));
			Assert.IsTrue(tokenizer.MoveNext());

			TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(tokenizer);
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses));
			Assert.AreEqual(2, arguments.Count);

			Assert.AreEqual(2, arguments[0].Tokens.Count);
			Assert.AreEqual(TSQLExpressionType.Constant, arguments[0].Type);
			Assert.AreEqual(TSQLTokenType.NumericLiteral, arguments[0].AsConstant.Literal.Type);
			Assert.AreEqual(1, arguments[0].AsConstant.Literal.AsNumericLiteral.Value);

			Assert.AreEqual(2, arguments[1].Tokens.Count);
			Assert.AreEqual(TSQLExpressionType.Constant, arguments[1].Type);
			Assert.AreEqual(TSQLTokenType.NumericLiteral, arguments[1].AsConstant.Literal.Type);
			Assert.AreEqual(2, arguments[1].AsConstant.Literal.AsNumericLiteral.Value);

			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLWhitespace(8, " "),
						new TSQLNumericLiteral(9, "1"),
						new TSQLWhitespace(10, " "),
						new TSQLCharacter(11, ","),
						new TSQLWhitespace(12, " "),
						new TSQLNumericLiteral(13, "2"),
						new TSQLWhitespace(14, " ")
					},
				arguments.Tokens);
		}

		[Test]
		public void ArgumentList_Empty()
		{
			TSQLTokenizer tokenizer = new TSQLTokenizer(
				"getdate();")
			{
				IncludeWhitespace = true
			};

			Assert.IsTrue(tokenizer.MoveNext());
			Assert.AreEqual("getdate", tokenizer.Current.Text);
			Assert.IsTrue(tokenizer.MoveNext());
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses));
			Assert.IsTrue(tokenizer.MoveNext());

			TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(tokenizer);
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses));
			Assert.AreEqual(0, arguments.Count);
			Assert.AreEqual(0, arguments.Tokens.Count);
		}

		[Test]
		public void ArgumentList_OnlySpaces()
		{
			TSQLTokenizer tokenizer = new TSQLTokenizer(
				"getdate( );")
			{
				IncludeWhitespace = true
			};

			Assert.IsTrue(tokenizer.MoveNext());
			Assert.AreEqual("getdate", tokenizer.Current.Text);
			Assert.IsTrue(tokenizer.MoveNext());
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses));
			Assert.IsTrue(tokenizer.MoveNext());

			TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(tokenizer);
			Assert.IsTrue(tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses));
			Assert.AreEqual(0, arguments.Count);
			Assert.AreEqual(1, arguments.Tokens.Count);
		}
	}
}
