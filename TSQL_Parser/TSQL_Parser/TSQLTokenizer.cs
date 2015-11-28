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
		private TextReader _inputStream = null;
		private bool hasMore = true;
		private bool hasExtra = false;
		private char extraChar;

		public TSQLTokenizer(TextReader inputStream)
		{
			_inputStream = inputStream;
			Position = -1;
		}

		public bool Read()
		{
			if (hasMore)
			{
				if (hasExtra)
				{
					Current = extraChar;
					hasExtra = false;
				}
				else
				{
					int nextValue = _inputStream.Read();
					if (nextValue > -1)
					{
						Current = (char)nextValue;
						Position++;
					}
					else
					{
						Current = char.MinValue;
						hasMore = false;
					}
				}
			}

			return hasMore;
		}

		public bool ReadNextNonWhitespace()
		{
			bool hasNext;

			do
			{
				hasNext = Read();
			} while (hasNext &&
				(
					Current == ' ' ||
					Current == '\t' ||
					Current == '\n' ||
					Current == '\r'
				));

			return hasNext;
		}

		public void Putback()
		{
			hasExtra = true;
			extraChar = Current;
			hasMore = true;
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
				return !hasMore;
			}
		}
	}
}
