using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
			string tsqlText) :
                this(new StringReader(tsqlText))
        {
			
		}

		public TSQLStatementReader(
			TextReader tsqlStream)
		{
			_tokenizer = new TSQLTokenizer(tsqlStream);
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

		public bool MoveNext()
		{
			CheckDisposed();

			if (_hasMore)
			{
                // push the tokenizer to the next token

                // eat up any tokens inbetween statements until we get to something that might start a new statement
                // which should be a keyword if the batch is valid

                // if the last statement parser did not swallow the final semicolon, or there were multiple semicolons, we will swallow it also
                while (
                    _tokenizer.MoveNext() &&
                    (
                        _tokenizer.Current.Type == TSQLTokenType.SingleLineComment ||
                        _tokenizer.Current.Type == TSQLTokenType.MultilineComment ||
                        _tokenizer.Current.Type == TSQLTokenType.Whitespace ||
                        (
                            _tokenizer.Current.Type == TSQLTokenType.Character &&
                            _tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
                        )
                    ));

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
