using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;

namespace TSQL
{
	internal interface ITSQLTokenizer : IEnumerator, IEnumerable, IEnumerator<TSQLToken>, IEnumerable<TSQLToken>
	{
		void Putback();
	}
}
