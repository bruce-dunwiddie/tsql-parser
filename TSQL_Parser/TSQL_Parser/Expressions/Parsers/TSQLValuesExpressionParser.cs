using System;

using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLValuesExpressionParser
	{
		public TSQLValuesExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLValuesExpression valuesExpression = new TSQLValuesExpression();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.VALUES))
			{
				throw new InvalidOperationException("VALUES expected.");
			}

			valuesExpression.Tokens.Add(tokenizer.Current);

			int nestedLevel = 0;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				(
					nestedLevel > 0 ||
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.ON,
							TSQLKeywords.WHEN
						)
					)
				) &&
				!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					valuesExpression,
					ref nestedLevel);
			}

			return valuesExpression;
		}
	}
}