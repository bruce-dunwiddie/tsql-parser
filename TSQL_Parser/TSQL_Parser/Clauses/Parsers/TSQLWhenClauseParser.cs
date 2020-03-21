using System;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLWhenClauseParser
	{
		public TSQLWhenClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLWhenClause when = new TSQLWhenClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.WHEN))
			{
				throw new InvalidOperationException("WHEN expected.");
			}

			when.Tokens.Add(tokenizer.Current);

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
							TSQLKeywords.WHEN
						)
					)
				) &&
				!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT)
				)
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					when,
					ref nestedLevel);
			}

			return when;
		}
	}
}