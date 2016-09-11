using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL
{
	public partial class TSQLStatementReader
	{
		private TSQLTokenizer _tokenizer = null;
		private bool _hasMore = true;
		private TSQLStatement _current = null;

		public TSQLStatementReader(
			string tsqlText)
		{
			_tokenizer = new TSQLTokenizer(tsqlText);
		}

		public TSQLStatementReader(
			TextReader tsqlStream)
		{
			_tokenizer = new TSQLTokenizer(tsqlStream);
		}

		public TSQLStatementReader(TSQLTokenizer tokenizer)
		{
			_tokenizer = tokenizer;
		}

		public bool UseQuotedIdentifiers
		{
			get
			{
				return _tokenizer.UseQuotedIdentifiers;
			}

			set
			{
				_tokenizer.UseQuotedIdentifiers = value;
			}
		}

		public bool IncludeWhitespace
		{
			get
			{
				return _tokenizer.IncludeWhitespace;
			}

			set
			{
				_tokenizer.IncludeWhitespace = value;
			}
		}

		public bool Read()
		{
			CheckDisposed();

			if (_hasMore)
			{
				while (
					_tokenizer.Read() &&
					(
						_tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
						_tokenizer.Current.Type == TSQLTokenType.MultilineComment ||
						_tokenizer.Current.Type == TSQLTokenType.Whitespace ||
						(
							_tokenizer.Current.Type == TSQLTokenType.Character &&
							_tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
						)
					))

				{

				}

				if (_tokenizer.Current == null)
				{
					_hasMore = false;

					return _hasMore;
				}

				_current = new TSQLStatementParserFactory().Create(_tokenizer.Current).Parse(_tokenizer);
			}

			return _hasMore;
		}

		public TSQLStatement Current
		{
			get
			{
				CheckDisposed();

				return _current;
			}
		}

		public TSQLStatement Next()
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

		public static List<TSQLStatement> ParseStatements(
			string tsqlText,
			bool useQuotedIdentifiers = false,
			bool includeWhitespace = false)
		{
			return new TSQLStatementReader(
				tsqlText)
				{
					IncludeWhitespace = includeWhitespace,
					UseQuotedIdentifiers = useQuotedIdentifiers
				}.ToList();
		}
	}
}
