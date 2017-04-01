using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	public class TSQLWhereClauseParser : ITSQLClauseParser
	{
		public TSQLWhereClause Parse(TSQLTokenizer tokenizer)
		{
			TSQLWhereClause where = new TSQLWhereClause();

            TSQLKeyword keyword = tokenizer.Current.AsKeyword;

            if (keyword == null ||
                keyword.Keyword != TSQLKeywords.WHERE)
            {
                throw new ApplicationException("WHERE expected.");
            }

            where.Tokens.Add(keyword);

            // subqueries
            int nestedLevel = 0;

			while (
				tokenizer.Read() &&
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
							TSQLKeywords.LIKE
						)
					)
				))
			{
				where.Tokens.Add(tokenizer.Current);

				if (tokenizer.Current.Type == TSQLTokenType.Character)
				{
					TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

					if (character == TSQLCharacters.OpenParentheses)
					{
						// should we recurse for subqueries?
						nestedLevel++;

						if (tokenizer.Read())
						{
							if (
								tokenizer.Current.Type == TSQLTokenType.Keyword &&
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.SELECT)
							{
								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								where.Tokens.AddRange(selectStatement.Tokens);

								if (
									tokenizer.Current != null &&
									tokenizer.Current.Type == TSQLTokenType.Character &&
									tokenizer.Current.AsCharacter.Character == TSQLCharacters.CloseParentheses)
								{
									nestedLevel--;
									where.Tokens.Add(tokenizer.Current);
								}
							}
							else
							{
								where.Tokens.Add(tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return where;
		}

		TSQLClause ITSQLClauseParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
