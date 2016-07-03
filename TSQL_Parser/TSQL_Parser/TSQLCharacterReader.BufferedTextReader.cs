using System;
using System.IO;

namespace TSQL
{
	public partial class TSQLCharacterReader
	{
		private class BufferedTextReader : ICharacterReader
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

			public int Read()
			{
				if (_hasMore)
				{
					if (_position >= _read)
					{
						_read = _inputStream.Read(_buffer, 0, _buffer.Length);
						_position = 0;
						_hasMore = _read > 0;
						if (!_hasMore)
						{
							return -1;
						}
					}

					return _buffer[_position++];
				}
				else
				{
					return -1;
				}
			}

			#region IDisposable pattern

			private bool disposed = false;

			void IDisposable.Dispose()
			{
				if (!disposed)
				{
					Dispose(true);
					GC.SuppressFinalize(this);
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
}
