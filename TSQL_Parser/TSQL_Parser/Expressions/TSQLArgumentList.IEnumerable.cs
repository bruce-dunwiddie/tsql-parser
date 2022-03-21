using System;
using System.Collections;
using System.Collections.Generic;

namespace TSQL.Expressions
{
	public partial class TSQLArgumentList : IList<TSQLExpression>
	{
		bool ICollection<TSQLExpression>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		void ICollection<TSQLExpression>.Add(TSQLExpression item)
		{
			throw new NotImplementedException();
		}

		void ICollection<TSQLExpression>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<TSQLExpression>.Contains(TSQLExpression item)
		{
			return arguments.Contains(item);
		}

		void ICollection<TSQLExpression>.CopyTo(TSQLExpression[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		IEnumerator<TSQLExpression> IEnumerable<TSQLExpression>.GetEnumerator()
		{
			return arguments.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return arguments.GetEnumerator();
		}

		int IList<TSQLExpression>.IndexOf(TSQLExpression item)
		{
			return arguments.IndexOf(item);
		}

		void IList<TSQLExpression>.Insert(int index, TSQLExpression item)
		{
			throw new NotImplementedException();
		}

		bool ICollection<TSQLExpression>.Remove(TSQLExpression item)
		{
			throw new NotImplementedException();
		}

		void IList<TSQLExpression>.RemoveAt(int index)
		{
			throw new NotImplementedException();
		}
	}
}
