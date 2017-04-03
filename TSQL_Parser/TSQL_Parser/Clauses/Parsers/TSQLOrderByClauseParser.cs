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
	public class TSQLOrderByClauseParser : ITSQLClauseParser
	{
		public TSQLOrderByClause Parse(IEnumerator<TSQLToken> tokenizer)
		{
			TSQLOrderByClause orderBy = new TSQLOrderByClause();

			TSQLKeyword keyword = tokenizer.Current.AsKeyword;

			if (keyword == null ||
				keyword.Keyword != TSQLKeywords.ORDER)
			{
				throw new ApplicationException("ORDER expected.");
			}

			orderBy.Tokens.Add(keyword);

			// subqueries
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
							TSQLKeywords.ASC,
							TSQLKeywords.DESC,
							TSQLKeywords.FETCH,
							TSQLKeywords.COLLATE
						)
					)
				))
			{
				orderBy.Tokens.Add(tokenizer.Current);

				if (tokenizer.Current.Type == TSQLTokenType.Character)
				{
					TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

					if (character == TSQLCharacters.OpenParentheses)
					{
						// should we recurse for subqueries?
						nestedLevel++;

						if (tokenizer.MoveNext())
						{
							if (
								tokenizer.Current.Type == TSQLTokenType.Keyword &&
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.SELECT)
							{
								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								orderBy.Tokens.AddRange(selectStatement.Tokens);

								if (
									tokenizer.Current != null &&
									tokenizer.Current.Type == TSQLTokenType.Character &&
									tokenizer.Current.AsCharacter.Character == TSQLCharacters.CloseParentheses)
								{
									nestedLevel--;
									orderBy.Tokens.Add(tokenizer.Current);
								}
							}
							else
							{
								orderBy.Tokens.Add(tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return orderBy;
		}

		TSQLClause ITSQLClauseParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
