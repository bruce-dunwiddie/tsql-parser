using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLUnknownStatementParser : ITSQLStatementParser
	{
		public TSQLUnknownStatement Parse(ITSQLTokenizer tokenizer)
		{
			TSQLUnknownStatement statement = new TSQLUnknownStatement();

			statement.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				!(
					tokenizer.Current is TSQLCharacter &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				))
			{
				statement.Tokens.Add(tokenizer.Current);
			}

			if (
				tokenizer.Current != null &&
				tokenizer.Current is TSQLCharacter &&
				tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon)
			{
				statement.Tokens.Add(tokenizer.Current);
			}

			return statement;
		}

		TSQLStatement ITSQLStatementParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
