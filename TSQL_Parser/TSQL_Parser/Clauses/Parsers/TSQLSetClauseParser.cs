using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLSetClauseParser
	{
		public TSQLSetClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSetClause set = new TSQLSetClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.SET))
			{
				throw new InvalidOperationException("SET expected.");
			}

			set.Tokens.Add(tokenizer.Current);

			// TODO: parse this rare but valid horror scenario
			// update output
			// set output.output = 1
			// output deleted.*
			// maybe create assignment expression parser?

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
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT)
					) ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.FROM,
							TSQLKeywords.WHERE,
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLTokenParserHelper.RecurseParens(
					tokenizer,
					set,
					ref nestedLevel);
			}

			return set;
		}
	}
}
