using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Tokens.Parsers
{
	internal class TSQLTokenFactory
	{
		public TSQLToken Parse(
			string tokenValue,
			int startPosition,
			int endPosition,
			bool useQuotedIdentifiers)
		{
			if (
				char.IsWhiteSpace(tokenValue[0]))
			{
				return
					new TSQLWhitespace(
						startPosition,
						tokenValue);
			}
			else if (
				tokenValue[0] == '@')
			{
				if (TSQLVariables.IsVariable(tokenValue))
				{
					return
						new TSQLSystemVariable(
							startPosition,
							tokenValue);
				}
				else
				{
					return
						new TSQLVariable(
							startPosition,
							tokenValue);
				}
			}
			else if (tokenValue.StartsWith("--"))
			{
				return
					new TSQLSingleLineComment(
						startPosition,
						tokenValue);
			}
			else if (tokenValue.StartsWith("/*"))
			{
				if (tokenValue.EndsWith("*/"))
				{
					return
						new TSQLMultilineComment(
							startPosition,
							tokenValue);
				}
				else
				{
					return
						new TSQLIncompleteComment(
							startPosition,
							tokenValue);
				}
			}
			else if (
				tokenValue.StartsWith("'") ||
				tokenValue.StartsWith("N'"))
			{
				// make sure there's an even number of quotes so that it's closed properly
				if ((tokenValue.Split('\'').Length - 1) % 2 == 0)
				{
					return
						new TSQLStringLiteral(
							startPosition,
							tokenValue);
				}
				else
				{
					return
						new TSQLIncompleteString(
							startPosition,
							tokenValue);
				}
			}
			else if (
				!useQuotedIdentifiers &&
				tokenValue.StartsWith("\""))
			{
				// make sure there's an even number of quotes so that it's closed properly
				if ((tokenValue.Split('\"').Length - 1) % 2 == 0)
				{
					return
						new TSQLStringLiteral(
							startPosition,
							tokenValue);
				}
				else
				{
					return
						new TSQLIncompleteString(
							startPosition,
							tokenValue);
				}
			}
			else if (
				tokenValue[0] == '$')
			{
				// $IDENTITY
				if (
					tokenValue.Length > 1 &&
					char.IsLetter(tokenValue[1]))
				{
					return
						new TSQLSystemColumnIdentifier(
							startPosition,
							tokenValue);
				}
				// $45.56
				else
				{
					return
						new TSQLMoneyLiteral(
							startPosition,
							tokenValue);
				}
			}
			else if (CharUnicodeInfo.GetUnicodeCategory(tokenValue[0]) == UnicodeCategory.CurrencySymbol)
			{
				return
					new TSQLMoneyLiteral(
						startPosition,
						tokenValue);
			}
			else if (tokenValue.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				return
					new TSQLBinaryLiteral(
						startPosition,
						tokenValue);
			}
			else if (
				char.IsDigit(tokenValue[0]) ||
				(
					tokenValue[0] == '.' &&
					tokenValue.Length > 1 &&
					char.IsDigit(tokenValue[1])
				))
			{
				return
					new TSQLNumericLiteral(
						startPosition,
						tokenValue);
			}
			else if (
				tokenValue[0] == '=' ||
				tokenValue[0] == '~' ||
				tokenValue[0] == '-' ||
				tokenValue[0] == '+' ||
				tokenValue[0] == '*' ||
				tokenValue[0] == '/' ||
				tokenValue[0] == '<' ||
				tokenValue[0] == '>' ||
				tokenValue[0] == '!' ||
				tokenValue[0] == '&' ||
				tokenValue[0] == '|' ||
				tokenValue[0] == '^' ||
				tokenValue[0] == '%' ||
				tokenValue[0] == ':')
			{
				return
					new TSQLOperator(
						startPosition,
						tokenValue);
			}
			else if (TSQLCharacters.IsCharacter(tokenValue))
			{
				return
					new TSQLCharacter(
						startPosition,
						tokenValue);
			}
			else if (TSQLKeywords.IsKeyword(tokenValue))
			{
				return
					new TSQLKeyword(
						startPosition,
						tokenValue);
			}
			else if (TSQLIdentifiers.IsIdentifier(tokenValue))
			{
				return
					new TSQLSystemIdentifier(
						startPosition,
						tokenValue);
			}
			else
			{
				if (
					(
						tokenValue.StartsWith("[") &&
						!tokenValue.EndsWith("]")
					) ||
					(
						useQuotedIdentifiers &&
						tokenValue.StartsWith("\"") &&
						// see if there's an odd number of quotes
						(tokenValue.Split('\"').Length - 1) % 2 == 1
					))
				{
					return
						new TSQLIncompleteIdentifier(
							startPosition,
							tokenValue);
				}
				else
				{
					return
						new TSQLIdentifier(
							startPosition,
							tokenValue);
				}
			}
		}
	}
}
