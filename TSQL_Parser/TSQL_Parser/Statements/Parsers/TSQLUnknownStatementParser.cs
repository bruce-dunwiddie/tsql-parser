using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLUnknownStatementParser : ITSQLStatementParser
	{
		public TSQLUnknownStatement Parse(IEnumerator<TSQLToken> tokenizer)
		{
			TSQLUnknownStatement statement = new TSQLUnknownStatement();

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

		TSQLStatement ITSQLStatementParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
