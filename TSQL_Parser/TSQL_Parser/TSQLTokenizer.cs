using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL
{
	public static class TSQLTokenizer
	{
		public static List<TSQLToken> ParseTokens(
			string definition,
			bool useQuotedIdentifiers,
			bool includeWhitespace)
		{
			List<TSQLToken> tokens = new List<TSQLToken>();

			int position = 0;

			while (
				position != -1 &&
				position < definition.Length)
			{
				position = FindNextToken(
					position,
					definition,
					includeWhitespace);

				if (position != -1)
				{
					int endPosition = FindTokenEnd(
						position,
						definition);

					string tokenValue = definition.Substring(position, endPosition - position + 1);

					if (tokenValue.StartsWith("--"))
					{
						tokens.Add(
							new TSQLSingleLineComment(
								position,
								endPosition,
								tokenValue));
					}
					else if (tokenValue.StartsWith("/*"))
					{
						tokens.Add(
							new TSQLMultilineComment(
								position,
								endPosition,
								tokenValue));
					}
					else if (
						tokenValue.StartsWith("\'") ||
						tokenValue.StartsWith("N\'") ||
						(
							!useQuotedIdentifiers &&
							(
								tokenValue.StartsWith("\"") ||
								tokenValue.StartsWith("N\"")
							)
						))
					{
						tokens.Add(
							new TSQLStringLiteral(
								position,
								endPosition,
								tokenValue));
					}
					else if (
						tokenValue[0] == '0' ||
						tokenValue[0] == '1' ||
						tokenValue[0] == '2' ||
						tokenValue[0] == '3' ||
						tokenValue[0] == '4' ||
						tokenValue[0] == '5' ||
						tokenValue[0] == '6' ||
						tokenValue[0] == '7' ||
						tokenValue[0] == '8' ||
						tokenValue[0] == '9' ||
						tokenValue[0] == '$')
					{
						tokens.Add(
							new TSQLNumericLiteral(
								position,
								endPosition,
								tokenValue));
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
						tokens.Add(
							new TSQLOperator(
								position,
								endPosition,
								tokenValue));
					}
					else if (TSQLCharacters.IsCharacter(tokenValue))
					{
						tokens.Add(
							new TSQLCharacter(
								position,
								endPosition,
								tokenValue));
					}
					else if (TSQLKeywords.IsKeyword(tokenValue))
					{
						tokens.Add(
							new TSQLKeyword(
								position,
								endPosition,
								tokenValue));
					}
					else if (
						tokenValue[0] == ' ' ||
						tokenValue[0] == '\t' ||
						tokenValue[0] == '\r' ||
						tokenValue[0] == '\n')
					{
						tokens.Add(
							new TSQLWhitespace(
								position,
								endPosition,
								tokenValue));
					}
					else
					{
						tokens.Add(
							new TSQLIdentifier(
								position,
								endPosition,
								tokenValue));
					}

					position = endPosition + 1;
				}
			}

			return tokens;
		}

		private static int FindNextToken(
			int position,
			string definition,
			bool includeWhitespace)
		{
			bool tokenFound = false;

			while (
				!tokenFound &&
				position < definition.Length)
			{
				char currentLetter = definition[position];

				switch (currentLetter)
				{
					case ' ':
					case '\t':
					case '\r':
					case '\n':
						{
							if (includeWhitespace)
							{
								tokenFound = true;
							}
							else
							{
								// ignore
								position++;
							}

							break;
						}
					default:
						{
							tokenFound = true;

							break;
						}
				}
			}

			if (position == definition.Length)
			{
				position = -1;
			}

			return position;
		}

		private static int FindTokenEnd(
			int position,
			string definition)
		{
			if (
				definition[position] == ' ' ||
				definition[position] == '\t' ||
				definition[position] == '\r' ||
				definition[position] == '\n')
			{
				while (
					++position < definition.Length &&
					(
						definition[position] == ' ' ||
						definition[position] == '\t' ||
						definition[position] == '\r' ||
						definition[position] == '\n'
					)) ;			
			}
			else
			{
				switch (definition[position])
				{
					// all single character sequences with no optional double character sequence
					case '.':
					case ',':
					case ';':
					case '(':
					case ')':
					case '=':
					case '~':
						{
							position++;

							break;
						}
					// --
					// -=
					case '-':
						{
							if (++position < definition.Length)
							{
								if (definition[position] == '-')
								{
									// ignore thru end of line
									while (
										++position < definition.Length &&
										definition[position] != '\r' &&
										definition[position] != '\n') ;
								}
								else if (definition[position] == '=')
								{
									position++;
								}
							}

							break;
						}
					// /* */
					// /=
					case '/':
						{
							if (++position < definition.Length)
							{
								if (definition[position] == '*')
								{
									position++;

									do
									{
										// find next asterisk
										while (
											position < definition.Length - 1 &&
											definition[position] != '*')
										{
											position++;
										}

										if (position < definition.Length - 1)
										{
											position++;
										}
									} while (
										position < definition.Length - 1 &&
										definition[position] != '/');

									position++;
								}
								else if (definition[position] == '=')
								{
									position++;
								}
							}

							break;
						}
					// <>
					// <=
					case '<':
						{
							if (++position < definition.Length)
							{
								if (
									definition[position] == '>' ||
									definition[position] == '=')
								{
									position++;
								}
							}

							break;
						}
					// !=
					// !<
					// !>
					case '!':
						{
							if (++position < definition.Length)
							{
								if (
									definition[position] == '=' ||
									definition[position] == '<' ||
									definition[position] == '>')
								{
									position++;
								}
							}

							break;
						}
					// &=
					case '&':
					// |=
					case '|':
					// ^=
					case '^':
					// +=
					case '+':
					// *=
					case '*':
					// %=
					case '%':
					// >=
					case '>':
						{
							if (
								++position < definition.Length &&
								definition[position] == '=')
							{
								position++;
							}

							break;
						}
					// N''
					case 'N':
						{
							if (++position < definition.Length)
							{
								if (definition[position] == '\'')
								{
									goto case '\'';
								}
								else if (definition[position] == '\"')
								{
									goto case '\"';
								}
								else
								{
									bool foundEnd = false;

									do
									{
										switch (definition[position])
										{
											// running into a special character signals the end of a previous grouping of normal characters
											case ' ':
											case '\t':
											case '\r':
											case '\n':
											case '.':
											case ',':
											case ';':
											case '(':
											case ')':
											case '+':
											case '-':
											case '*':
											case '=':
											case '/':
											case '<':
											case '!':
											case '%':
											case '^':
											case '&':
											case '|':
											case '~':
											case ':':
											case '[':
												{
													foundEnd = true;

													break;
												}
											default:
												{
													position++;

													break;
												}
										}
									} while (
										!foundEnd &&
										position < definition.Length - 1);

									break;
								}
							}
							else
							{
								goto default;
							}
						}
					// ::
					case ':':
						{
							if (
								++position < definition.Length &&
								definition[position] == ':')
							{
								position++;
							}

							break;
						}
					// ''
					case '\'':
					// ""
					case '\"':
					// [dbo]
					case '[':
						{
							char escapeChar;

							if (definition[position] == '[')
							{
								escapeChar = ']';
							}
							else
							{
								escapeChar = definition[position];
							}

							// read until '
							// UNLESS the ' is doubled up
							do
							{
								while (
									++position < definition.Length &&
									definition[position] != escapeChar) ;

								if (position < definition.Length - 1)
								{
									position++;
								}
							} while (
								position < definition.Length &&
								definition[position] == escapeChar);

							break;
						}
					// numeric literals
					// 0x69048AEFDD010E
					// 0x
					// 1894.1204
					// 0.5E-2
					// $45.56
					// 123E-3
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					case '$':
						{
							bool foundEnd = false;

							while (
								!foundEnd &&
								++position < definition.Length)
							{
								switch (definition[position])
								{
									// running into a special character signals the end of a previous grouping of normal characters
									case ' ':
									case '\t':
									case '\r':
									case '\n':
									case ',':
									case ';':
									case '(':
									case ')':
									case '*':
									case '=':
									case '/':
									case '<':
									case '!':
									case '%':
									case '^':
									case '&':
									case '|':
									case '~':
									case ':':
									case '[':
										{
											foundEnd = true;

											break;
										}
									default:
										{


											break;
										}
								}
							}

							break;
						}
					default:
						{
							bool foundEnd = false;

							while (
								!foundEnd &&
								++position < definition.Length)
							{
								switch (definition[position])
								{
									// running into a special character signals the end of a previous grouping of normal characters
									case ' ':
									case '\t':
									case '\r':
									case '\n':
									case '.':
									case ',':
									case ';':
									case '(':
									case ')':
									case '+':
									case '-':
									case '*':
									case '=':
									case '/':
									case '<':
									case '!':
									case '%':
									case '^':
									case '&':
									case '|':
									case '~':
									case ':':
									case '[':
										{
											foundEnd = true;

											break;
										}
									default:
										{


											break;
										}
								}
							}

							break;
						}
				}
			}

			// returning the last position of the token
			return position - 1;
		}
	}
}
