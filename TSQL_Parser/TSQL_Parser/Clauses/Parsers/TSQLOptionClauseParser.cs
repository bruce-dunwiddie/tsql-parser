using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOptionClauseParser : ITSQLClauseParser
	{
		public TSQLOptionClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOptionClause option = new TSQLOptionClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
			{
				throw new InvalidOperationException("OPTION expected.");
			}

			option.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
			{
				option.Tokens.Add(tokenizer.Current);
			}

			do
			{
				if (tokenizer.Current != null)
				{
					option.Tokens.Add(tokenizer.Current);
				}
			} while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses));

			if (tokenizer.Current != null)
			{
				option.Tokens.Add(tokenizer.Current);
				tokenizer.MoveNext();
			}

			return option;
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
