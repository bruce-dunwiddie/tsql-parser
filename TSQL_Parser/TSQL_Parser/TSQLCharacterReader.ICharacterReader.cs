using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL
{
	public partial class TSQLCharacterReader
	{
		private interface ICharacterReader : IDisposable
		{
			int Read();
        }
	}
}
