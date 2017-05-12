using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLSelectClauseParser : ITSQLClauseParser
	{
		public TSQLSelectClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSelectClause select = new TSQLSelectClause();

			if (
                tokenizer.Current == null ||
                tokenizer.Current.Type != TSQLTokenType.Keyword ||
                tokenizer.Current.AsKeyword.Keyword != TSQLKeywords.SELECT)
			{
				throw new ApplicationException("SELECT expected.");
			}

			select.Tokens.Add(tokenizer.Current);

			// can contain ALL, DISTINCT, TOP, PERCENT, WITH TIES, AS

			// ends with FROM, semicolon, or keyword other than those listed above, when used outside of parens

			// recursively walk down and back up parens

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
							TSQLKeywords.ALL,
							TSQLKeywords.AS,
							TSQLKeywords.DISTINCT,
							TSQLKeywords.PERCENT,
							TSQLKeywords.TOP,
							TSQLKeywords.WITH,
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
							TSQLKeywords.IDENTITY,
							TSQLKeywords.LIKE
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

						if (tokenizer.MoveNext())
						{
							if (
								tokenizer.Current.Type == TSQLTokenType.Keyword &&
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.SELECT)
							{
								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								select.Tokens.AddRange(selectStatement.Tokens);

                                if (
                                    tokenizer.Current != null &&
                                    tokenizer.Current.Type == TSQLTokenType.Character &&
                                    tokenizer.Current.AsCharacter.Character == TSQLCharacters.CloseParentheses)
                                {
                                    nestedLevel--;
                                    select.Tokens.Add(tokenizer.Current);
                                }
							}
							else
							{
								select.Tokens.Add(tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return select;
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
