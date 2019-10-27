using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Expressions;
using TSQL.Tokens;

namespace TSQL.Statements
{
	public abstract class TSQLStatement : TSQLExpression
	{
		public abstract TSQLStatementType Type
		{
			get;
		}

		public TSQLSelectStatement AsSelect
		{
			get
			{
				return this as TSQLSelectStatement;
			}
		}
	}
}
