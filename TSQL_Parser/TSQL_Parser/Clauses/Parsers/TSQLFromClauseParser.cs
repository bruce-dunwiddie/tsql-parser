using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLFromClauseParser : ITSQLClauseParser
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
						tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.JOIN,
							TSQLKeywords.ON,
							TSQLKeywords.INNER,
							TSQLKeywords.LEFT,
							TSQLKeywords.RIGHT,
							TSQLKeywords.OUTER,
							TSQLKeywords.CROSS,
							TSQLKeywords.FULL,
							TSQLKeywords.AS,
							TSQLKeywords.PIVOT,
							TSQLKeywords.UNPIVOT,
							TSQLKeywords.WITH,
							TSQLKeywords.MERGE,
							TSQLKeywords.TABLESAMPLE,
							TSQLKeywords.FOR,
							TSQLKeywords.FROM, // FOR SYSTEM_TIME FROM
							TSQLKeywords.BETWEEN,
							TSQLKeywords.AND,
							TSQLKeywords.IN,
							TSQLKeywords.REPEATABLE,
							TSQLKeywords.ALL
						)
					)
				))
			{
				from.Tokens.Add(tokenizer.Current);

				if (tokenizer.Current.Type == TSQLTokenType.Character)
				{
					TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

					if (character == TSQLCharacters.OpenParentheses)
					{
						// should we recurse for derived tables?
						nestedLevel++;

						if (tokenizer.MoveNext())
						{
							if (
								tokenizer.Current.Type == TSQLTokenType.Keyword &&
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.SELECT)
							{
								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								from.Tokens.AddRange(selectStatement.Tokens);

								if (
									tokenizer.Current != null &&
									tokenizer.Current.Type == TSQLTokenType.Character &&
									tokenizer.Current.AsCharacter.Character == TSQLCharacters.CloseParentheses)
								{
									nestedLevel--;
									from.Tokens.Add(tokenizer.Current);
								}
							}
							else if (tokenizer.Current.IsCharacter(
								TSQLCharacters.CloseParentheses))
							{
								nestedLevel--;
								from.Tokens.Add(tokenizer.Current);
							}
							else
							{
								from.Tokens.Add(tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return from;
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
