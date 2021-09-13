using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Elements
{
	public abstract class TSQLElement
	{
		private readonly List<TSQLToken> _tokens = new List<TSQLToken>();

		public List<TSQLToken> Tokens
		{
			get
			{
				return _tokens;
			}
		}

		public int BeginPosition
		{
			get
			{
				return Tokens.First().BeginPosition;
			}
		}

		public int EndPosition
		{
			get
			{
				return Tokens.Last().EndPosition;
			}
		}

		public int Length
		{
			get
			{
				return EndPosition - BeginPosition + 1;
			}
		}
	}
}
