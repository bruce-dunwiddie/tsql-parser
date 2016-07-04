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
	public class TSQLSelectClauseParser
	{
		public TSQLSelectClause Build(TSQLTokenizer tokenizer)
		{
			TSQLSelectClause select = new TSQLSelectClause();

			TSQLKeyword keyword = tokenizer.Next().AsKeyword;

			// assert SELECT

			if (keyword == null ||
				keyword.Keyword != TSQLKeywords.SELECT)
			{
				throw new ApplicationException("SELECT expected.");
			}

			select.Tokens.Add(keyword);

			// can contain ALL, DISTINCT, TOP, PERCENT, WITH TIES, AS

			// ends with FROM, semicolon, or keyword other than those listed above, when used outside of parens

			// recursively walk down and back up parens

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
						(
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ALL ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.AS ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.DISTINCT ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.PERCENT ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.TOP ||
							tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.WITH
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

						if (tokenizer.Read())
						{
							if (
								tokenizer.Current.Type == TSQLTokenType.Keyword &&
								tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.SELECT)
							{
								tokenizer.Putback();

								TSQLSelectStatement selectStatement = new TSQLSelectStatementParser().Parse(tokenizer);

								select.Tokens.AddRange(selectStatement.Tokens);
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
	}
}
