using System;
using System.Collections;
using System.Collections.Generic;

using TSQL.Tokens;

namespace TSQL
{
	public partial class TSQLLexer : IEnumerator, IEnumerable, IEnumerator<TSQLToken>, IEnumerable<TSQLToken>
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

		// moving IEnumerator<TSQLToken>.Current to the main context

		bool IEnumerator.MoveNext()
		{
			return Read();
		}

		void IEnumerator.Reset()
		{
			throw new NotSupportedException("Reset is not currently supported by the IEnumerable implementation supplied by " + GetType().FullName + ".");
		}

		#endregion
	}
}
