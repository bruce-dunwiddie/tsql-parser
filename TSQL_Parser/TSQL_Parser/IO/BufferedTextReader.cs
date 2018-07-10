using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TSQL.IO
{
	internal partial class BufferedTextReader : ICharacterReader
	{
		private TextReader _inputStream = null;
		private readonly char[] _buffer = new char[1024];
		private int _position = 0;
		private int _read = 0;
		private bool _hasMore = true;

		public BufferedTextReader(TextReader inputStream)
		{
			_inputStream = inputStream;
		}

		char IEnumerator<char>.Current
		{
			get
			{
				if (_hasMore)
				{
					return _buffer[_position];
				}
				else
				{
					return char.MinValue;
				}
			}
		}

		bool IEnumerator.MoveNext()
		{
			if (_hasMore)
			{
				if (_position >= _read - 1)
				{
					_read = _inputStream.Read(_buffer, 0, _buffer.Length);
					_position = 0;
					_hasMore = _read > 0;
					return _hasMore;
				}

				_position++;
				return true;
			}
			else
			{
				return false;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return (this as IEnumerator<char>).Current;
			}
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException("Reset is not currently supported by the IEnumerator implementation supplied by " + GetType().FullName + ".");
		}

		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		
	}
}
