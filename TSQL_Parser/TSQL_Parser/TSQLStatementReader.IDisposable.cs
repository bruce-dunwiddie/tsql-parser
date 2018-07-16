using System;

namespace TSQL
{
	partial class TSQLStatementReader : IDisposable
	{
		#region IDisposable pattern

		private bool _disposed = false;

		void IDisposable.Dispose()
		{
			if (!_disposed)
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
			if (!_disposed)
			{
				// managed resource releases
				//if (disposing)
				//{

				//}

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

				_disposed = true;
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
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().FullName, "This object has been previously disposed." +
					" Methods on this object can no longer" +
					" be called.");
			}
		}

		#endregion
	}
}
