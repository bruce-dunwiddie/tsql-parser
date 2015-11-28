using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using TSQL.Tokens;

namespace TSQL
{
	public partial class TSQLLexer
	{
		private TSQLTokenizer _tokenizer = null;
		private TSQLToken _current = null;

		public TSQLLexer(
			TextReader inputStream)
		{
			_tokenizer = new TSQLTokenizer(inputStream);
		}

		public bool UseQuotedIdentifiers { get; set; }

		public bool IncludeWhitespace { get; set; }

		public bool Read()
		{
			CheckDisposed();

			bool hasNext;

			if (IncludeWhitespace)
			{
				hasNext = _tokenizer.Read();
			}
			else
			{
				hasNext = _tokenizer.ReadNextNonWhitespace();
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
			int startPosition = _tokenizer.Position;

			if (
				IncludeWhitespace &&
				(
					_tokenizer.Current == ' ' ||
					_tokenizer.Current == '\t' ||
					_tokenizer.Current == '\r' ||
					_tokenizer.Current == '\n'
				))
			{
				do
				{
					characterHolder.Append(_tokenizer.Current);
				} while (
					_tokenizer.Read() &&
					(
						_tokenizer.Current == ' ' ||
						_tokenizer.Current == '\t' ||
						_tokenizer.Current == '\r' ||
						_tokenizer.Current == '\n'
					));

				if (!_tokenizer.EOF)
				{
					_tokenizer.Putback();
				}
			}
			else
			{
				characterHolder.Append(_tokenizer.Current);

				switch (_tokenizer.Current)
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
					// -
					case '-':
						{
							if (_tokenizer.Read())
							{
								if (_tokenizer.Current == '-')
								{
									do
									{
										characterHolder.Append(_tokenizer.Current);
									} while (
										_tokenizer.Read() &&
										_tokenizer.Current != '\r' &&
										_tokenizer.Current != '\n');

									if (!_tokenizer.EOF)
									{
										_tokenizer.Putback();
									}
								}
								else if (_tokenizer.Current == '=')
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
								}
							}

							break;
						}
					// /* */
					// /=
					// /
					case '/':
						{
							if (_tokenizer.Read())
							{
								if (_tokenizer.Current == '*')
								{
									characterHolder.Append(_tokenizer.Current);

									while (
										_tokenizer.Read() &&
										!(
											_tokenizer.Current == '/' &&
											characterHolder[characterHolder.Length - 1] == '*'
										))
									{
										characterHolder.Append(_tokenizer.Current);
									}

									if (!_tokenizer.EOF)
									{
										characterHolder.Append(_tokenizer.Current);
									}
								}
								else if (_tokenizer.Current == '=')
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
								}
							}

							break;
						}
					// <>
					// <=
					// <
					case '<':
						{
							if (_tokenizer.Read())
							{
								if (
									_tokenizer.Current == '>' ||
									_tokenizer.Current == '='
								)
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
								}
							}

							break;
						}
					// !=
					// !<
					// !>
					case '!':
						{
							if (_tokenizer.Read())
							{
								if (
									_tokenizer.Current == '=' ||
									_tokenizer.Current == '<' ||
									_tokenizer.Current == '>'
								)
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
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
							if (_tokenizer.Read())
							{
								if (_tokenizer.Current == '=')
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
								}
							}

							break;
						}
					// N''
					case 'N':
						{
							if (_tokenizer.Read())
							{
								if (_tokenizer.Current == '\'')
								{
									characterHolder.Append(_tokenizer.Current);
									goto case '\'';
								}
								else if (_tokenizer.Current == '\"')
								{
									characterHolder.Append(_tokenizer.Current);
									goto case '\"';
								}
								else
								{
									_tokenizer.Putback();
									goto default;
								}
							}

							break;
						}
					// ::
					case ':':
						{
							if (_tokenizer.Read())
							{
								if (_tokenizer.Current == ':')
								{
									characterHolder.Append(_tokenizer.Current);
								}
								else
								{
									_tokenizer.Putback();
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

							if (_tokenizer.Current == '[')
							{
								escapeChar = ']';
							}
							else
							{
								escapeChar = _tokenizer.Current;
							}

							bool stillEscaped;

							// read until '
							// UNLESS the ' is doubled up
							do
							{
								while (
									_tokenizer.Read() &&
									_tokenizer.Current != escapeChar)
								{
									characterHolder.Append(_tokenizer.Current);
								};

								if (!_tokenizer.EOF)
								{
									characterHolder.Append(_tokenizer.Current);
								}

								stillEscaped =
									!_tokenizer.EOF && 
									_tokenizer.Read() && 
									_tokenizer.Current == escapeChar;

								if (stillEscaped)
								{
									characterHolder.Append(_tokenizer.Current);
								}
							} while (stillEscaped);

							if (!_tokenizer.EOF)
							{
								_tokenizer.Putback();
							}

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
								_tokenizer.Read())
							{
								switch (_tokenizer.Current)
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
											characterHolder.Append(_tokenizer.Current);

											break;
										}
								}
							}

							if (foundEnd)
							{
								_tokenizer.Putback();
							}

							break;
						}
					default:
						{
							bool foundEnd = false;

							while (
								!foundEnd &&
								_tokenizer.Read())
							{
								switch (_tokenizer.Current)
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
											characterHolder.Append(_tokenizer.Current);

											break;
										}
								}
							}

							if (foundEnd)
							{
								_tokenizer.Putback();
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
			return new TSQLLexer(new StringReader(definition))
			{
				UseQuotedIdentifiers = useQuotedIdentifiers,
				IncludeWhitespace = includeWhitespace
			}.ToList();
		}
		
	}
}
