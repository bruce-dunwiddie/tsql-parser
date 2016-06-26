using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements
{
	public partial class TSQLStatementReader : IEnumerator, IEnumerable, IEnumerator<TSQLStatement>, IEnumerable<TSQLStatement>
	{
		IEnumerator<TSQLStatement> IEnumerable<TSQLStatement>.GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		bool IEnumerator.MoveNext()
		{
			return Read();
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		void IEnumerator.Reset()
		{
			throw new NotImplementedException();
		}
	}
}
