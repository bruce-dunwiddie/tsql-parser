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
				!(
					tokenizer.Current.Type == TSQLTokenType.Character &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.Type == TSQLTokenType.Character &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.CloseParentheses
				) &&
				(
					nestedLevel > 0 ||
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(

							TSQLKeywords.INNER,
							TSQLKeywords.OUTER,
							TSQLKeywords.JOIN,
							TSQLKeywords.WHEN,
							TSQLKeywords.OUTPUT
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
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