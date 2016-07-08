using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	public class TSQLSelectStatementParser : ITSQLStatementParser
	{
		public TSQLSelectStatement Parse(TSQLTokenizer tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			// should whitespace be excluded from statement parsing logic?

			// should I differentiate keywords that start commands?

			// correlated subqueries
			// scalar function calls

			// SELECT clause

			TSQLSelectClause selectClause = new TSQLSelectClauseParser().Build(tokenizer);

			select.Tokens.AddRange(selectClause.Tokens);

			int nestedLevel = 0;

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTO
			)
			{
				// TSQLIntoClause intoClause = new TSQLIntoClauseParser().Parse(tokenizer);

				// select.Tokens.AddRange(intoClause.Tokens);
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FROM
			)
			{
				// derived tables
				// TVF
				nestedLevel = 0;

				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					(
						nestedLevel > 0 ||
						!(
							tokenizer.Current.Type == TSQLTokenType.Keyword &&
							(
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.WHERE ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.GROUP ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.HAVING ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.UNION ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.EXCEPT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTERSECT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
							)
						)
					))
				{
					select.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type == TSQLTokenType.Character)
					{
						TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

						if (character == TSQLCharacters.OpenParentheses)
						{
							// should we recurse for derived tables?
							nestedLevel++;
						}
						else if (character == TSQLCharacters.CloseParentheses)
						{
							nestedLevel--;
						}
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.WHERE
			)
			{
				// subqueries
				nestedLevel = 0;

				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					(
						nestedLevel > 0 ||
						!(
							tokenizer.Current.Type == TSQLTokenType.Keyword &&
							(
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.GROUP ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.HAVING ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.UNION ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.EXCEPT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTERSECT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
							)
						)
					))
				{
					select.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type == TSQLTokenType.Character)
					{
						TSQLCharacters character = tokenizer.Current.AsCharacter.Character;


						if (character == TSQLCharacters.OpenParentheses)
						{
							// should we recurse for subqueries?
							nestedLevel++;
						}
						else if (character == TSQLCharacters.CloseParentheses)
						{
							nestedLevel--;
						}
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.GROUP
			)
			{
				// subqueries
				nestedLevel = 0;

				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					(
						nestedLevel > 0 ||
						!(
							tokenizer.Current.Type == TSQLTokenType.Keyword &&
							(
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.HAVING ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.UNION ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.EXCEPT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTERSECT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
							)
						)
					))
				{
					select.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type == TSQLTokenType.Character)
					{
						TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

						if (character == TSQLCharacters.OpenParentheses)
						{
							// should we recurse for subqueries?
							nestedLevel++;
						}
						else if (character == TSQLCharacters.CloseParentheses)
						{
							nestedLevel--;
						}
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.HAVING
			)
			{
				// subqueries
				nestedLevel = 0;

				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					(
						nestedLevel > 0 ||
						!(
							tokenizer.Current.Type == TSQLTokenType.Keyword &&
							(
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.UNION ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.EXCEPT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTERSECT ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
							)
						)
					))
				{
					select.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type == TSQLTokenType.Character)
					{
						TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

						if (character == TSQLCharacters.OpenParentheses)
						{
							// should we recurse for subqueries?
							nestedLevel++;
						}
						else if (character == TSQLCharacters.CloseParentheses)
						{
							nestedLevel--;
						}
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER
			)
			{
				// subqueries
				nestedLevel = 0;

				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					(
						nestedLevel > 0 ||
						!(
							tokenizer.Current.Type == TSQLTokenType.Keyword &&
							(
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
							)
						)
					))
				{
					select.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type == TSQLTokenType.Character)
					{
						TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

						if (character == TSQLCharacters.OpenParentheses)
						{
							// should we recurse for subqueries?
							nestedLevel++;
						}
						else if (character == TSQLCharacters.CloseParentheses)
						{
							nestedLevel--;
						}
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Character &&
				tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
			)
			{
				select.Tokens.Add(tokenizer.Current);
			}

			return select;
		}

		TSQLStatement ITSQLStatementParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
