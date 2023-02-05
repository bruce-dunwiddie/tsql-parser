using System;
using TSQL.Elements;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLCaseExpressionParser
	{
		public TSQLCaseExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLCaseExpression caseExpression = new TSQLCaseExpression();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.CASE))
			{
				throw new InvalidOperationException("CASE expected.");
			}

			caseExpression.Tokens.Add(tokenizer.Current);

			tokenizer.MoveNext();

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				caseExpression.Tokens);

			var nextToken = tokenizer.Current ?? throw new TSQLParseException("CASE expression is incomplete. Expect WHEN or an input expression");

			TSQLToken whenToken = null;

			if (!nextToken.IsKeyword(TSQLKeywords.WHEN))
			{
				caseExpression.IsSimpleCaseExpression = true;
				var valueExpr = new TSQLValueExpressionParser().Parse(tokenizer);
				caseExpression.Tokens.AddRange(valueExpr.Tokens);

				TSQLTokenParserHelper.ReadCommentsAndWhitespace(
					tokenizer,
					caseExpression.Tokens);

				whenToken = tokenizer.Current;
			}
			else
			{
				caseExpression.IsSimpleCaseExpression = false;
				whenToken = nextToken;
			}

			if (!whenToken.IsKeyword(TSQLKeywords.WHEN))
			{
				throw new TSQLParseException("CASE expression is incorrect. It should have a WHEN keyword, instead we have: " + whenToken.Text);
			}

			// 'WHEN' keyword
			caseExpression.Tokens.Add(whenToken);

			do
			{
				if (!tokenizer.MoveNext())
				{
					throw new TSQLParseException("CASE expression is incomplete. After WHEN, there should be an expression");
				}

				TSQLTokenParserHelper.ReadCommentsAndWhitespace(
					tokenizer,
					caseExpression.Tokens);

				var whenExpr = new TSQLValueExpressionParser().Parse(tokenizer);
				caseExpression.Tokens.AddRange(whenExpr.Tokens);

				if (!tokenizer.Current.IsKeyword(TSQLKeywords.THEN))
				{
					throw new TSQLParseException(
						"CASE expression is incomplete. After WHEN, there should be a THEN keyword");
				}

				// 'THEN' keyword
				caseExpression.Tokens.Add(tokenizer.Current);

				if (!tokenizer.MoveNext())
				{
					throw new TSQLParseException(
						"CASE expression is incomplete. After THEN, there should be an expression");
				}

				TSQLTokenParserHelper.ReadCommentsAndWhitespace(
					tokenizer,
					caseExpression.Tokens);

				var thenExpr = new TSQLValueExpressionParser().Parse(tokenizer);
				caseExpression.Tokens.AddRange(thenExpr.Tokens);

			} while (tokenizer.Current.IsKeyword(TSQLKeywords.WHEN));

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.ELSE))
			{
				throw new TSQLParseException(
					"CASE expression is incomplete. There should be an ELSE keyword");
			}

			caseExpression.Tokens.Add(tokenizer.Current);

			if (!tokenizer.MoveNext())
			{
				throw new TSQLParseException(
					"CASE expression incomplete. There should be an expression after ELSE keyword");
			}

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				caseExpression.Tokens);

			var elseExpr = new TSQLValueExpressionParser().Parse(tokenizer);
			caseExpression.Tokens.AddRange(elseExpr.Tokens);

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.END))
			{
				throw new TSQLParseException("CASE expression incomplete. There should be an END keyword");
			}

			// 'END' keyword
			caseExpression.Tokens.Add(tokenizer.Current);

			// move past the END keyword
			tokenizer.MoveNext();

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				caseExpression.Tokens);

			return caseExpression;
		}

		private TSQLToken ReadNextToken(ITSQLTokenizer tokenizer)
		{
			if (!tokenizer.MoveNext()) return null;
			return tokenizer.Current;
		}

	}
}
