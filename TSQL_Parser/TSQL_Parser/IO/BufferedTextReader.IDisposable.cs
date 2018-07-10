using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.IO
{
	partial class BufferedTextReader : IDisposable
	{
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
				//if (disposing)
				//{

				//}

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
