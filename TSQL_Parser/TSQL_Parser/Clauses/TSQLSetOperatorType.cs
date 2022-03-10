using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Clauses
{
	public enum TSQLSetOperatorType
	{
#pragma warning disable 1591

		Union,
		Intersect,
		Except

#pragma warning restore 1591
	}
}
