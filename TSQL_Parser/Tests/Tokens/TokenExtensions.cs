using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace Tests.Tokens
{
	public static class TokenExtensions
	{
		public static List<string> Texts(this List<TSQLToken> tokens)
		{
			return tokens.Select(t => t.Text).ToList();
		}
	}
}
