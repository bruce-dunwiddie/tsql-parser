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

            if (
                tokenizer.Current == null ||
                tokenizer.Current.Type != TSQLTokenType.Keyword ||
                tokenizer.Current.AsKeyword.Keyword != TSQLKeywords.INTO)
            {
                throw new ApplicationException("INTO expected.");
            }

            into.Tokens.Add(tokenizer.Current);

            while (
				tokenizer.Read() &&
				(
					tokenizer.Current.Type == TSQLTokenType.Identifier ||
					(
						tokenizer.Current.Type == TSQLTokenType.Character &&
						tokenizer.Current.AsCharacter.Character == TSQLCharacters.Period
					) ||
					tokenizer.Current.Type == TSQLTokenType.Whitespace ||
					tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
					tokenizer.Current.Type == TSQLTokenType.MultilineComment
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
