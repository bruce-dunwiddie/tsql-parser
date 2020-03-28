using System;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	/// <summary>
	///		This parser adds AS to the allowed keywords within an INTO clause.
	/// </summary>
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
				) &&
				// since USING is a stop word but it's also a TSQLTokenType.Identifier
				// we need to check for it explicitly
				!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.USING)
			)
			{
				into.Tokens.Add(tokenizer.Current);
			}

			return into;
		}
	}
}