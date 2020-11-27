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

			int nestedLevel = 0;

			while (
				Tokenizer.MoveNext() &&
				!Tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					Tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				(
					nestedLevel > 0 ||
					Tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						Tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!Tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				Statement.Tokens.Add(Tokenizer.Current);

				if (Tokenizer.Current.Type == TSQLTokenType.Character)
				{
					TSQLCharacters character = Tokenizer.Current.AsCharacter.Character;

					if (character == TSQLCharacters.OpenParentheses)
					{
						// should we recurse for correlated subqueries?
						nestedLevel++;

						if (Tokenizer.MoveNext())
						{
							if (Tokenizer.Current.IsCharacter(
								TSQLCharacters.CloseParentheses))
							{
								nestedLevel--;
								Statement.Tokens.Add(Tokenizer.Current);
							}
							else if (Tokenizer.Current.IsCharacter(
								TSQLCharacters.OpenParentheses))
							{
								nestedLevel++;
								Statement.Tokens.Add(Tokenizer.Current);
							}
							else
							{
								Statement.Tokens.Add(Tokenizer.Current);
							}
						}
					}
					else if (character == TSQLCharacters.CloseParentheses)
					{
						nestedLevel--;
					}
				}
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
