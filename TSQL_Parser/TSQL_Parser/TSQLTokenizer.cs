using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL
{
	public partial class TSQLTokenizer
	{
		private TSQLLexer _lexer = null;
		private TSQLToken _current = null;

		public TSQLTokenizer(
			TextReader inputStream)
		{
			_lexer = new TSQLLexer(inputStream);
		}

		public bool UseQuotedIdentifiers { get; set; }

		public bool IncludeWhitespace { get; set; }

		public bool Read()
		{
			CheckDisposed();

			bool hasNext;

			if (IncludeWhitespace)
			{
				hasNext = _lexer.Read();
			}
			else
			{
				hasNext = _lexer.ReadNextNonWhitespace();
			}

			if (hasNext)
			{
				SetCurrent();
			}

			return hasNext;
		}

		private StringBuilder characterHolder = new StringBuilder();

		private void SetCurrent()
		{
			characterHolder.Length = 0;
			int startPosition = _lexer.Position;

			if (
				IncludeWhitespace &&
				(
					_lexer.Current == ' ' ||
					_lexer.Current == '\t' ||
					_lexer.Current == '\r' ||
					_lexer.Current == '\n'
				))
			{
				do
				{
					characterHolder.Append(_lexer.Current);
				} while (
					_lexer.Read() &&
					(
						_lexer.Current == ' ' ||
						_lexer.Current == '\t' ||
						_lexer.Current == '\r' ||
						_lexer.Current == '\n'
					));
			}
			else
			{
				characterHolder.Append(_lexer.Current);

				switch (_lexer.Current)
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
							

							break;
						}
					// --
					// -=
					case '-':
						{
							if (_lexer.Read())
							{
								if (_lexer.Current == '-')
								{
									do
									{
										characterHolder.Append(_lexer.Current);
									} while (
										_lexer.Read() &&
										_lexer.Current != '\r' &&
										_lexer.Current != '\n');
                                }
								else if (_lexer.Current == '=')
								{
									characterHolder.Append(_lexer.Current);
								}
							}

							break;
						}
					// /* */
					// /=
					case '/':
						{
							if (_lexer.Read())
							{
								if (_lexer.Current == '*')
								{
									characterHolder.Append(_lexer.Current);

									do
									{
										while (
											_lexer.Read() &&
											_lexer.Current != '*')
										{
											characterHolder.Append(_lexer.Current);
										}

										if (!_lexer.EOF)
										{
											characterHolder.Append(_lexer.Current);
										}
									} while (
										_lexer.Read() &&
										_lexer.Current != '/');

									if (!_lexer.EOF)
									{
										characterHolder.Append(_lexer.Current);
									}
								}
								else if (_lexer.Current == '=')
								{
									characterHolder.Append(_lexer.Current);
								}
							}

							break;
						}
					// <>
					// <=
					case '<':
						{
							if (
								_lexer.Read() &&
								(
									_lexer.Current == '>' ||
									_lexer.Current == '='
								))
							{
								characterHolder.Append(_lexer.Current);
							}

							break;
						}
					// !=
					// !<
					// !>
					case '!':
						{
							if (
								_lexer.Read() &&
								(
									_lexer.Current == '=' ||
									_lexer.Current == '<' ||
									_lexer.Current == '>'
								))
							{
								characterHolder.Append(_lexer.Current);
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
								_lexer.Read() &&
								_lexer.Current == '=')
							{
								characterHolder.Append(_lexer.Current);
							}

							break;
						}
					// N''
					case 'N':
						{
							if (_lexer.Read())
							{
								if (_lexer.Current == '\'')
								{
									goto case '\'';
								}
								else if (_lexer.Current == '\"')
								{
									goto case '\"';
								}
								else
								{
									goto default;
								}
							}

							break;
						}
					// ::
					case ':':
						{
							if (
								_lexer.Read() &&
								_lexer.Current == ':')
							{
								characterHolder.Append(_lexer.Current);
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

							if (_lexer.Current == '[')
							{
								escapeChar = ']';
							}
							else
							{
								escapeChar = _lexer.Current;
							}

							bool stillEscaped;

							// read until '
							// UNLESS the ' is doubled up
							do
							{
								while (
									_lexer.Read() &&
									_lexer.Current != escapeChar)
								{
									characterHolder.Append(_lexer.Current);
								};

								if (!_lexer.EOF)
								{
									characterHolder.Append(_lexer.Current);
								}

								stillEscaped = _lexer.Read() && _lexer.Current == escapeChar;
								if (stillEscaped)
								{
									characterHolder.Append(_lexer.Current);
								}
                            } while (stillEscaped);

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
								_lexer.Read())
							{
								switch (_lexer.Current)
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
											characterHolder.Append(_lexer.Current);

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
								_lexer.Read())
							{
								switch (_lexer.Current)
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
											characterHolder.Append(_lexer.Current);

											break;
										}
								}
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
			if (tokenValue.StartsWith("--"))
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
			else if (
				tokenValue[0] == ' ' ||
				tokenValue[0] == '\t' ||
				tokenValue[0] == '\r' ||
				tokenValue[0] == '\n')
			{
				return
					new TSQLWhitespace(
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
