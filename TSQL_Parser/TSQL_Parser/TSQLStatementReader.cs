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

		public TSQLStatementReader(TSQLTokenizer tokenizer)
		{
			_tokenizer = tokenizer;
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
						_tokenizer.Current.Type == TSQLTokenType.Whitespace
					))

				{

				}

				if (_tokenizer.Current == null)
				{
					_hasMore = false;

					return _hasMore;
				}

				if (_tokenizer.Current.Type != TSQLTokenType.Keyword)
				{
					// don't know what I want to do here yet

					_hasMore = false;

					return _hasMore;
				}

				TSQLKeywords keyword = (_tokenizer.Current as TSQLKeyword).Keyword;

				_current = new TSQLStatementParserFactory().Create(keyword).Parse(_tokenizer);
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
			string statements,
			bool useQuotedIdentifiers = false,
			bool includeWhitespace = false)
		{
			return new TSQLStatementReader(
				new TSQLTokenizer(
					new StringReader(
						statements))
				{
					IncludeWhitespace = includeWhitespace,
					UseQuotedIdentifiers = useQuotedIdentifiers
				}).ToList();
		}
	}
}
