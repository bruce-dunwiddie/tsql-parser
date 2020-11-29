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
		private TSQLTokenizer tokenizer = null;
		private bool hasMore = true;
		private TSQLStatement current = null;

		public TSQLStatementReader(
			string tsqlText) :
				this(new StringReader(tsqlText))
		{

		}

		public TSQLStatementReader(
			TextReader tsqlStream)
		{
			tokenizer = new TSQLTokenizer(tsqlStream);
			// move to first token
			tokenizer.MoveNext();
		}

		public bool UseQuotedIdentifiers
		{
			get
			{
				return tokenizer.UseQuotedIdentifiers;
			}

			set
			{
				tokenizer.UseQuotedIdentifiers = value;
			}
		}

		public bool IncludeWhitespace
		{
			get
			{
				return tokenizer.IncludeWhitespace;
			}

			set
			{
				tokenizer.IncludeWhitespace = value;
			}
		}

		public bool MoveNext()
		{
			CheckDisposed();

			if (hasMore)
			{
				// eat up any tokens inbetween statements until we get to something that might start a new statement
				// which should be a keyword if the batch is valid

				// if the last statement parser did not swallow the final semicolon, or there were multiple semicolons, we will swallow it also
				while (
					tokenizer.Current != null &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment() ||
						(
							tokenizer.Current.Type == TSQLTokenType.Character &&
							tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
						)
					) &&
					tokenizer.MoveNext());

				if (tokenizer.Current == null)
				{
					hasMore = false;

					return hasMore;
				}

				current = new TSQLStatementParserFactory().Create(tokenizer).Parse();
			}

			return hasMore;
		}

		public TSQLStatement Current
		{
			get
			{
				CheckDisposed();

				return current;
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
