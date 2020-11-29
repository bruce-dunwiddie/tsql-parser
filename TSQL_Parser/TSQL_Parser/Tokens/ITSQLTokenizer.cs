using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.Tokens
{
	internal interface ITSQLTokenizer : IEnumerator, IEnumerable, IEnumerator<TSQLToken>, IEnumerable<TSQLToken>
	{
		
	}
}
