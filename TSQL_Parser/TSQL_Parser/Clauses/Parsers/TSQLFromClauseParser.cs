using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLFromClauseParser
	{
		public TSQLFromClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLFromClause from = new TSQLFromClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.FROM))
			{
				throw new InvalidOperationException("FROM expected.");
			}

			from.Tokens.Add(tokenizer.Current);

			// derived tables
			// TVF
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
							TSQLKeywords.WHERE,
							TSQLKeywords.ORDER,
							TSQLKeywords.FOR,
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					from,
					ref nestedLevel);
			}

			return from;
		}
	}
}
