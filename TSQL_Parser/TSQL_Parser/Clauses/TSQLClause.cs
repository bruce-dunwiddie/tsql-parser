using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses
{
	public abstract class TSQLClause
	{
		private List<TSQLToken> _tokens = new List<TSQLToken>();

		public List<TSQLToken> Tokens
		{
			get
			{
				return _tokens;
			}
		}
	}
}
