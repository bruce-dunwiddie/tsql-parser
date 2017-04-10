using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.IO
{
	internal interface ICharacterReader : IDisposable, IEnumerator, IEnumerable, IEnumerator<char>, IEnumerable<char>
	{
		
    }
}
