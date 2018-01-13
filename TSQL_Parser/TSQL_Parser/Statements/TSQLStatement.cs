using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL.Statements
{
	public abstract class TSQLStatement
	{
		private List<TSQLToken> _tokens = new List<TSQLToken>();

		public abstract TSQLStatementType Type
		{
			get;
		}

		public List<TSQLToken> Tokens
		{
			get
			{
				return _tokens;
			}
		}
	}
}
