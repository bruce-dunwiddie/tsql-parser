using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLGroupByClauseParser : ITSQLClauseParser
	{
		public TSQLGroupByClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLGroupByClause groupBy = new TSQLGroupByClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.GROUP))
			{
				throw new InvalidOperationException("GROUP expected.");
			}

			groupBy.Tokens.Add(tokenizer.Current);

			// subqueries
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
						tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.BY,
							TSQLKeywords.NULL,
							TSQLKeywords.CASE,
							TSQLKeywords.WHEN,
							TSQLKeywords.THEN,
							TSQLKeywords.ELSE,
							TSQLKeywords.AND,
							TSQLKeywords.OR,
							TSQLKeywords.BETWEEN,
							TSQLKeywords.EXISTS,
							TSQLKeywords.END,
							TSQLKeywords.IN,
							TSQLKeywords.IS,
							TSQLKeywords.NOT,
							TSQLKeywords.OVER,
							TSQLKeywords.LIKE,
							TSQLKeywords.ALL,
							TSQLKeywords.WITH,
							TSQLKeywords.DISTINCT
						)
					)
				))
			{
				groupBy.Tokens.Add(tokenizer.Current);

				if (tokenizer.Current.Type == TSQLTokenType.Character)
				{
					TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

					if (character == TSQLCharacters.OpenParentheses)
					{
						// should we recurse for subqueries?
						nestedLevel++;

						if (tokenizer.MoveNext())
						{
							if (tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
							{
								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								groupBy.Tokens.AddRange(selectStatement.Tokens);

								if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
								{
									nestedLevel--;
									groupBy.Tokens.Add(tokenizer.Current);
								}
							}
							else if (tokenizer.Current.IsCharacter(
								TSQLCharacters.CloseParentheses))
							{
								nestedLevel--;
								groupBy.Tokens.Add(tokenizer.Current);
							}
							else if (tokenizer.Current.IsCharacter(
								TSQLCharacters.OpenParentheses))
							{
								nestedLevel++;
								groupBy.Tokens.Add(tokenizer.Current);
							}
							else
							{
								groupBy.Tokens.Add(tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return groupBy;
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
