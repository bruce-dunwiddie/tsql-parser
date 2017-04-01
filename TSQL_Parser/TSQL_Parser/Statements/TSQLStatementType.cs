using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements
{
	public enum TSQLStatementType
	{
		Select,
		Insert,
		Update,
		Delete,
		With,
		Execute,
		Merge,
		Unknown
	}
}
