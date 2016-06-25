using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL
{
	public partial class TSQLCharacterReader
	{
		private class BufferedTextReader : ICharacterReader
		{
			private TextReader _inputStream = null;
			private char[] buffer = new char[1024];
			private int position = 0;
			private int read = 0;
			private bool hasMore = true;

			public BufferedTextReader(TextReader inputStream)
			{
				_inputStream = inputStream;
			}

			public int Read()
			{
				if (hasMore)
				{
					if (position >= read)
					{
						read = _inputStream.Read(buffer, 0, buffer.Length);
						position = 0;
						hasMore = read > 0;
						if (!hasMore)
						{
							return -1;
						}
					}

					return buffer[position++];
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
