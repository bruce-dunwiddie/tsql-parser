using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLUnknownStatementParser : ITSQLStatementParser
	{
		public TSQLUnknownStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLUnknownStatementParser(List<TSQLToken> startTokens, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.Tokens.AddRange(startTokens);
		}

		private TSQLUnknownStatement Statement { get; } = new TSQLUnknownStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLUnknownStatement Parse()
		{
			Statement.Tokens.Add(Tokenizer.Current);

			while (
				Tokenizer.MoveNext() &&
				!(
					Tokenizer.Current is TSQLCharacter &&
					Tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				))
			{
				Statement.Tokens.Add(Tokenizer.Current);
			}

			if (
				Tokenizer.Current != null &&
				Tokenizer.Current is TSQLCharacter &&
				Tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon)
			{
				Statement.Tokens.Add(Tokenizer.Current);
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
