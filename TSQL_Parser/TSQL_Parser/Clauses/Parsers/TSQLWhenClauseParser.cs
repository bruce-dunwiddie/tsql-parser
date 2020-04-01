using System;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	/// <summary>
	///		This parser is currently written to handle WHEN specifically inside a MERGE,
	///		and may not yet handle parsing WHEN within a CASE.
	/// </summary>
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

			// we don't have to worry about accidentally running into the next statement.

			// https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql
			// The MERGE statement requires a semicolon (;) as a statement terminator.
			// Error 10713 is raised when a MERGE statement is run without the terminator.

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
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT)
					) ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.WHEN,
							TSQLKeywords.OPTION
						)
					)
				))
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