using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSQL.IO
{
	internal interface ICharacterReader : IDisposable, IEnumerator<char>, IEnumerable<char>
	{
		
    }
}
