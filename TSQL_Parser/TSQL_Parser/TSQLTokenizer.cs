using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;

using TSQL.IO;
using TSQL.Tokens;

namespace TSQL
{
	public partial class TSQLTokenizer
	{
		private TSQLCharacterReader _charReader = null;
		private TSQLToken _current = null;
		private bool _hasMore = true;
		private bool _hasExtra = false;
		private TSQLToken _extraToken;

		public TSQLTokenizer(
			TextReader inputStream)
		{
			_charReader = new TSQLCharacterReader(inputStream);
		}

		public bool UseQuotedIdentifiers { get; set; }

		public bool IncludeWhitespace { get; set; }

		public bool Read()
		{
			CheckDisposed();

			if (_hasMore)
			{
				if (_hasExtra)
				{
					_current = _extraToken;
					_hasExtra = false;
				}
				else
				{
					if (IncludeWhitespace)
					{
						_hasMore = _charReader.Read();
					}
					else
					{
						_hasMore = _charReader.ReadNextNonWhitespace();
					}

					if (_hasMore)
					{
						SetCurrent();
					}
				}
			}

			return _hasMore;
		}

		private StringBuilder characterHolder = new StringBuilder();

		private void SetCurrent()
		{
			characterHolder.Length = 0;
			int startPosition = _charReader.Position;

			if (
				IncludeWhitespace &&
				char.IsWhiteSpace(_charReader.Current))
			{
				do
				{
					characterHolder.Append(_charReader.Current);
				} while (
					_charReader.Read() &&
					char.IsWhiteSpace(_charReader.Current));

				if (!_charReader.EOF)
				{
					_charReader.Putback();
				}
			}
			else
			{
				characterHolder.Append(_charReader.Current);

				switch (_charReader.Current)
				{
					// all single character sequences with no optional double character sequence
					case '.':
					case ',':
					case ';':
					case '(':
					case ')':
					case '~':
						{


							break;
						}
					// --
					// -=
					// -
					case '-':
						{
							if (_charReader.Read())
							{
								if (_charReader.Current == '-')
								{
									do
									{
										characterHolder.Append(_charReader.Current);
									} while (
										_charReader.Read() &&
										_charReader.Current != '\r' &&
										_charReader.Current != '\n');

									if (!_charReader.EOF)
									{
										_charReader.Putback();
									}
								}
								else if (_charReader.Current == '=')
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
							}

							break;
						}
					// /* */
					// /=
					// /
					case '/':
						{
							if (_charReader.Read())
							{
								if (_charReader.Current == '*')
								{
									characterHolder.Append(_charReader.Current);

									while (
										_charReader.Read() &&
										!(
											_charReader.Current == '/' &&
											characterHolder[characterHolder.Length - 1] == '*'
										))
									{
										characterHolder.Append(_charReader.Current);
									}

									if (!_charReader.EOF)
									{
										characterHolder.Append(_charReader.Current);
									}
								}
								else if (_charReader.Current == '=')
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
							}

							break;
						}
					// <>
					// <=
					// <
					case '<':
						{
							if (_charReader.Read())
							{
								if (
									_charReader.Current == '>' ||
									_charReader.Current == '='
								)
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
							}

							break;
						}
					// !=
					// !<
					// !>
					case '!':
						{
							if (_charReader.Read())
							{
								if (
									_charReader.Current == '=' ||
									_charReader.Current == '<' ||
									_charReader.Current == '>'
								)
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
							}

							break;
						}
					// =*
					// =
					case '=':
						{
							if (_charReader.Read())
							{
								if (
									_charReader.Current == '*'
								)
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
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
							if (_charReader.Read())
							{
								if (_charReader.Current == '=')
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
							}

							break;
						}
					// N''
					case 'N':
						{
							if (_charReader.Read())
							{
								if (_charReader.Current == '\'')
								{
									characterHolder.Append(_charReader.Current);
									goto case '\'';
								}
								else if (_charReader.Current == '\"')
								{
									characterHolder.Append(_charReader.Current);
									goto case '\"';
								}
								else
								{
									_charReader.Putback();
									goto default;
								}
							}

							break;
						}
					// ::
					case ':':
						{
							if (_charReader.Read())
							{
								if (_charReader.Current == ':')
								{
									characterHolder.Append(_charReader.Current);
								}
								else
								{
									_charReader.Putback();
								}
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

							if (_charReader.Current == '[')
							{
								escapeChar = ']';
							}
							else
							{
								escapeChar = _charReader.Current;
							}

							bool stillEscaped;

							// read until '
							// UNLESS the ' is doubled up
							do
							{
								while (
									_charReader.Read() &&
									_charReader.Current != escapeChar)
								{
									characterHolder.Append(_charReader.Current);
								};

								if (!_charReader.EOF)
								{
									characterHolder.Append(_charReader.Current);
								}

								stillEscaped =
									!_charReader.EOF && 
									_charReader.Read() && 
									_charReader.Current == escapeChar;

								if (stillEscaped)
								{
									characterHolder.Append(_charReader.Current);
								}
							} while (stillEscaped);

							if (!_charReader.EOF)
							{
								_charReader.Putback();
							}

							break;
						}
					// numeric literals
					// 0x69048AEFDD010E
					// 0x
					// 1894.1204
					// 0.5E-2
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
					// $45.56
					case '$':
					// other Unicode currency symbols recognized by SSMS
					case '£':
					case '¢':
					case '¤':
					case '¥':
					case '€':
					case '₡':
					case '₱':
					case '﷼':
					case '₩':
					case '₮':
					case '₨':
					case '₫':
					case '฿':
					case '៛':
					case '₪':
					case '₭':
					case '₦':
					case '৲':
					case '৳':
					case '﹩':
					case '₠':
					case '₢':
					case '₣':
					case '₤':
					case '₥':
					case '₧':
					case '₯':
					case '₰':
					case '＄':
					case '￠':
					case '￡':
					case '￥':
					case '￦':
						{
							bool foundEnd = false;

							while (
								!foundEnd &&
								_charReader.Read())
							{
								switch (_charReader.Current)
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
											characterHolder.Append(_charReader.Current);

											break;
										}
								}
							}

							if (foundEnd)
							{
								_charReader.Putback();
							}

							break;
						}
					default:
						{
							bool foundEnd = false;

							while (
								!foundEnd &&
								_charReader.Read())
							{
								switch (_charReader.Current)
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
											characterHolder.Append(_charReader.Current);

											break;
										}
								}
							}

							if (foundEnd)
							{
								_charReader.Putback();
							}

							break;
						}
				}
			}

			_current = DetermineTokenType(
				characterHolder.ToString(),
				startPosition,
				startPosition + characterHolder.Length - 1);
		}

		private TSQLToken DetermineTokenType(
			string tokenValue,
			int startPosition,
			int endPosition)
		{
			if (
				char.IsWhiteSpace(tokenValue[0]))
			{
				return
					new TSQLWhitespace(
						startPosition,
						tokenValue);
			}
			else if (tokenValue.StartsWith("@"))
			{
				return
					new TSQLVariable(
						startPosition,
						tokenValue);
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
				return
					new TSQLMultilineComment(
						startPosition,
						tokenValue);
			}
			else if (
				tokenValue.StartsWith("\'") ||
				tokenValue.StartsWith("N\'") ||
				(
					!UseQuotedIdentifiers &&
					(
						tokenValue.StartsWith("\"") ||
						tokenValue.StartsWith("N\"")
					)
				))
			{
				return
					new TSQLStringLiteral(
						startPosition,
						tokenValue);
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
						new TSQLIdentifier(
							startPosition,
							tokenValue);
				}
				// $45.56
				else
				{
					return
						new TSQLNumericLiteral(
							startPosition,
							tokenValue);
				}
			}
			else if (
				char.IsDigit(tokenValue[0]) ||
				CharUnicodeInfo.GetUnicodeCategory(tokenValue[0]) == UnicodeCategory.CurrencySymbol)
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
			else
			{
				return
					new TSQLIdentifier(
						startPosition,
						tokenValue);
			}
		}

		public TSQLToken Current
		{
			get
			{
				CheckDisposed();
				return _current;
			}
		}

		public TSQLToken Next()
		{
			if (Read())
			{
				return Current;
			}
			else
			{
				return null;
			}
		}

		public static List<TSQLToken> ParseTokens(
			string definition,
			bool useQuotedIdentifiers = false,
			bool includeWhitespace = false)
		{
			return new TSQLTokenizer(new StringReader(definition))
			{
				UseQuotedIdentifiers = useQuotedIdentifiers,
				IncludeWhitespace = includeWhitespace
			}.ToList();
		}
		
	}
}
