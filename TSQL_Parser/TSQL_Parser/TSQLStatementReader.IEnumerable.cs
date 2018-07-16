using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;

namespace TSQL
{
	partial class TSQLStatementReader : IEnumerator, IEnumerable, IEnumerator<TSQLStatement>, IEnumerable<TSQLStatement>
	{
		IEnumerator<TSQLStatement> IEnumerable<TSQLStatement>.GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		object IEnumerator.Current
		{
			get
			{
				return (this as IEnumerator<TSQLStatement>).Current;
			}
		}

		void IEnumerator.Reset()
		{
            throw new NotSupportedException("Reset is not currently supported by the IEnumerator implementation supplied by " + GetType().FullName + ".");
        }
	}
}
