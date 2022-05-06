using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens
{
	// using static extension methods instead of instance methods allows us to check against null
	public static class TSQLTokenExtensions
	{
		public static bool IsKeyword(this TSQLToken token, TSQLKeywords keyword)
		{
			if (token == null)
			{
				return false;
			}

			if (token.Type != TSQLTokenType.Keyword)
			{
				return false;
			}

			if (token.AsKeyword.Keyword != keyword)
			{
				return false;
			}

			return true;
		}

		public static bool IsCharacter(this TSQLToken token, TSQLCharacters character)
		{
			if (token == null)
			{
				return false;
			}

			if (token.Type != TSQLTokenType.Character)
			{
				return false;
			}

			if (token.AsCharacter.Character != character)
			{
				return false;
			}

			return true;
		}

		public static bool IsIdentifier(this TSQLToken token, TSQLIdentifiers identifier)
		{
			if (token == null)
			{
				return false;
			}

			if (token.Type != TSQLTokenType.SystemIdentifier)
			{
				return false;
			}

			if (token.AsSystemIdentifier.Identifier != identifier)
			{
				return false;
			}

			return true;
		}

		public static bool IsWhitespace(this TSQLToken token)
		{
			if (token == null)
			{
				return false;
			}
			else
			{
				return token.Type == TSQLTokenType.Whitespace;
			}
		}

		public static bool IsComment(this TSQLToken token)
		{
			if (token == null)
			{
				return false;
			}
			else
			{
				return token.Type.In(
					TSQLTokenType.SingleLineComment,
					TSQLTokenType.MultilineComment,
					TSQLTokenType.IncompleteComment);
			}
		}

		public static bool IsFutureKeyword(this TSQLToken token, TSQLFutureKeywords keyword)
		{
			if (token == null)
			{
				return false;
			}
			else
			{
				return TSQLFutureKeywords.Parse(token.Text) == keyword;
			}
		}
	}
}
