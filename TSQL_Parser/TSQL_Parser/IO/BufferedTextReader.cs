using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TSQL.IO
{
	internal class BufferedTextReader : ICharacterReader
	{
		private TextReader _inputStream = null;
		private char[] _buffer = new char[1024];
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

		#region IDisposable pattern

		private bool disposed = false;

		void IDisposable.Dispose()
		{
			if (!disposed)
			{
				Dispose(true);
			}
		}

		/// <summary>
		///		Closes and releases all related resources.
		/// </summary>
		/// <param name="disposing">
		///		Whether this call is coming from an explicit call,
		///		instead of from the implicit GC finalizer call.
		/// </param>
		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				// managed resource releases
				if (disposing)
				{

				}

				// unmanaged resource releases
				try
				{
					(_inputStream as IDisposable).Dispose();
				}
				catch (Exception)
				{
					// can't handle Dispose throwing exceptions
				}
				_inputStream = null;

				disposed = true;
			}
		}

		#endregion
	}
}
