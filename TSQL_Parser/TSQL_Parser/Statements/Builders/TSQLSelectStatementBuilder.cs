using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Builders
{
	public class TSQLSelectStatementBuilder : ITSQLStatementBuilder
	{
		public TSQLStatement Build(TSQLTokenizer tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			TSQLKeyword keyword = tokenizer.Next().AsKeyword;

			if (keyword == null ||
				keyword.Keyword != TSQLKeywords.SELECT)
			{
				throw new ApplicationException("SELECT expected.");
			}

			select.Tokens.Add(keyword);

			// should whitespace be excluded from statement parsing logic?

			// assert SELECT

			// SELECT clause

			// can contain ALL, DISTINCT, TOP, PERCENT, WITH TIES

			// recursively walk down and back up parens

			// ends with FROM, semicolon, or keyword other than those listed above, when used outside of parens

			// should I differentiate keywords that start commands?

			// correlated subqueries
			// scalar function calls
			int nestedLevel = 0;

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
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTO ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FROM ||
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
						// should we recurse for correlated subqueries?
						nestedLevel++;
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			if (
				tokenizer.Current.Type == TSQLTokenType.Keyword &&
				tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTO
			)
			{
				while (
					tokenizer.Read() &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
					) &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						(
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FROM ||
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
					))
				{
					select.Tokens.Add(tokenizer.Current);
				}
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
			else if (tokenizer.Current != null)
			{
				tokenizer.Putback();
			}

			return select;
		}
	}
}
