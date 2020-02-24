using System;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLMergeIntoClauseParser
	{
		public TSQLIntoClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLIntoClause into = new TSQLIntoClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				throw new InvalidOperationException("INTO expected.");
			}

			into.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				(
					tokenizer.Current.Type == TSQLTokenType.Identifier ||
					tokenizer.Current.IsCharacter(TSQLCharacters.Period) ||
					tokenizer.Current.Type == TSQLTokenType.Whitespace ||
					tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
					tokenizer.Current.Type == TSQLTokenType.MultilineComment ||
					tokenizer.Current.IsKeyword(TSQLKeywords.AS)
				))
			{
				into.Tokens.Add(tokenizer.Current);
			}

			return into;
		}
	}
}