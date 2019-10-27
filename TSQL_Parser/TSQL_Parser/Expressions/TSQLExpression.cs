using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public abstract class TSQLExpression
	{
		private readonly List<TSQLToken> _tokens = new List<TSQLToken>();

		public List<TSQLToken> Tokens
		{
			get
			{
				return _tokens;
			}
		}
	}
}
