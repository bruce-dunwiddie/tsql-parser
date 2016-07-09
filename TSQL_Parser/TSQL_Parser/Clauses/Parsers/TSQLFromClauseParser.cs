using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	public class TSQLFromClauseParser : ITSQLClauseParser
	{
		public TSQLFromClause Parse(TSQLTokenizer tokenizer)
		{
			TSQLFromClause from = new TSQLFromClause();

			// derived tables
			// TVF
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
				from.Tokens.Add(tokenizer.Current);

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

			return from;
		}

		TSQLClause ITSQLClauseParser.Parse(TSQLTokenizer tokenizer)
		{
			throw new NotImplementedException();
		}
	}
}
