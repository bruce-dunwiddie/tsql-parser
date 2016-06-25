using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL
{
	public partial class TSQLTokenizer : IDisposable
	{
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
					(_tokenizer as IDisposable).Dispose();
				}
				catch (Exception)
				{
					// can't handle Dispose throwing exceptions
				}
				_tokenizer = null;

				disposed = true;
			}
		}

		/// <summary>
		///		Checks to see if object has already been disposed, which
		///		would make calling methods on the object invalid.
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		///		Methods were called after the object has been disposed.
		/// </exception>
		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(GetType().FullName, "This object has been previously disposed." +
					" Methods on this object can no longer" +
					" be called.");
			}
		}

		#endregion
	}
}
