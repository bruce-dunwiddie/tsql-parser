using System;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOutputClauseParser
	{
		public TSQLOutputClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOutputClause output = new TSQLOutputClause();

			if (!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				throw new InvalidOperationException("OUTPUT expected.");
			}

			output.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				(
					tokenizer.Current.IsKeyword(TSQLKeywords.AS) ||
					tokenizer.Current.Type == TSQLTokenType.Identifier ||
					tokenizer.Current.Type == TSQLTokenType.SystemColumnIdentifier ||
					tokenizer.Current.IsCharacter(TSQLCharacters.Period) ||
					tokenizer.Current.IsCharacter(TSQLCharacters.Comma) ||
					tokenizer.Current.Text == "*" ||
					tokenizer.Current.Type == TSQLTokenType.Whitespace ||
					tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
					tokenizer.Current.Type == TSQLTokenType.MultilineComment
				))
			{
				output.Tokens.Add(tokenizer.Current);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				TSQLIntoClause intoClause = new TSQLIntoClauseParser().Parse(tokenizer);

				output.Into = intoClause;

				output.Tokens.AddRange(intoClause.Tokens);
			}

			return output;
		}
	}
}