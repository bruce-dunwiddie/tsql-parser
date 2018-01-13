using System;
using System.Collections;
using System.Collections.Generic;

using TSQL.Tokens;

namespace TSQL
{
	partial class TSQLTokenizer : ITSQLTokenizer
	{
		#region IEnumerable/IEnumerator Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		IEnumerator<TSQLToken> IEnumerable<TSQLToken>.GetEnumerator()
		{
			return this;
		}

		object IEnumerator.Current
		{
			get
			{
				return (this as IEnumerator<TSQLToken>).Current;
			}
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException("Reset is not currently supported by the IEnumerator implementation supplied by " + GetType().FullName + ".");
		}

		#endregion
	}
}
