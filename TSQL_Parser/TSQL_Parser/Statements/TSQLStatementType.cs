using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Statements
{
	public enum TSQLStatementType
	{
#pragma warning disable 1591

		Select,
		//Insert,
		//Update,
		//Delete,
		//With,
		//Execute,
		//Merge,

#pragma warning restore 1591

		Unknown
	}
}
