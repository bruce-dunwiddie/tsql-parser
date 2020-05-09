using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLForClauseParser
	{
		public TSQLForClause Parse(ITSQLTokenizer tokenizer)
		{
			// FOR XML AUTO
			TSQLForClause forClause = new TSQLForClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.FOR))
			{
				throw new InvalidOperationException("FOR expected.");
			}

			forClause.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				(
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.OPTION
						) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				forClause.Tokens.Add(tokenizer.Current);
			}

			return forClause;
		}
	}
}
