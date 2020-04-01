using System;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOnClauseParser
	{
		public TSQLOnClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOnClause on = new TSQLOnClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.ON))
			{
				throw new InvalidOperationException("ON expected.");
			}

			on.Tokens.Add(tokenizer.Current);

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
					(
						tokenizer.Current.Type != TSQLTokenType.Keyword &&
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT) &&
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.USING)
					) ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(

							TSQLKeywords.INNER,
							TSQLKeywords.OUTER,
							TSQLKeywords.JOIN,
							TSQLKeywords.WHEN
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				) &&
				!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					on,
					ref nestedLevel);
			}

			return on;
		}
	}
}