using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.IO
{
	internal partial class TSQLCharacterReader
	{
		private ICharacterReader _inputStream = null;
		private bool _hasMore = true;
		private bool _hasExtra = false;
		private char _extraChar;

		public TSQLCharacterReader(TextReader inputStream)
		{
			// can't take the risk that the passed in stream is not buffered
			// because of the high call number of Read
			_inputStream = new BufferedTextReader(inputStream);
			Position = -1;
		}

		public bool Read()
		{
			if (_hasMore)
			{
				if (_hasExtra)
				{
					Current = _extraChar;
					_hasExtra = false;
				}
				else
				{
					_hasMore = _inputStream.MoveNext();
					if (_hasMore)
					{
						Current = _inputStream.Current;
						Position++;
					}
					else
					{
						Current = char.MinValue;
					}
				}
			}

			return _hasMore;
		}

		public bool ReadNextNonWhitespace()
		{
			bool hasNext;

			do
			{
				hasNext = Read();
			} while (hasNext &&
				char.IsWhiteSpace(Current));

			return hasNext;
		}

		public void Putback()
		{
			_hasExtra = true;
			_extraChar = Current;
			_hasMore = true;
		}

		public char Current
		{
			get;

			private set;
		}

		public int Position
		{
			get;

			private set;
		}

		public bool EOF
		{
			get
			{
				return !_hasMore;
			}
		}
	}
}
