using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLIntoClauseParser : ITSQLClauseParser
	{
		public TSQLIntoClause Parse(IEnumerator<TSQLToken> tokenizer)
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
				tokenizer.MoveNext() &&
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

		TSQLClause ITSQLClauseParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
