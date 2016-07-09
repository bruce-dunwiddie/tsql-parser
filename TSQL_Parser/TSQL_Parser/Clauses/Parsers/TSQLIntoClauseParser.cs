using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	public class TSQLIntoClauseParser : ITSQLClauseParser
	{
		public TSQLIntoClause Parse(TSQLTokenizer tokenizer)
		{
			TSQLIntoClause into = new TSQLIntoClause();

			while (
				tokenizer.Read() &&
				!(
					tokenizer.Current is TSQLCharacter &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				) &&
				!(
					tokenizer.Current.Type == TSQLTokenType.Keyword &&
					(
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FROM ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.UNION ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.EXCEPT ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.INTERSECT ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.ORDER ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.FOR ||
						tokenizer.Current.AsKeyword.Keyword == TSQLKeywords.OPTION
					)
				))
			{
				into.Tokens.Add(tokenizer.Current);
			}

			return into;
		}

		TSQLClause ITSQLClauseParser.Parse(TSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
