using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL
{
	public class TSQLVariables
	{
		private static Dictionary<string, TSQLVariables> variableLookup =
			new Dictionary<string, TSQLVariables>(StringComparer.InvariantCultureIgnoreCase);
	}
}
