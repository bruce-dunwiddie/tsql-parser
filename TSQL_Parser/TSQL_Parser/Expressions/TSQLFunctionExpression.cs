using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions
{
	public class TSQLFunctionExpression : TSQLExpression
	{
		public override TSQLExpressionType Type
		{
			get
			{
				return TSQLExpressionType.Function;
			}
		}

		public List<TSQLToken> QualifiedPath { get; internal set; }

		public TSQLToken Function { get; internal set; }

		public TSQLArgumentList Arguments { get; internal set; }
	}
}
