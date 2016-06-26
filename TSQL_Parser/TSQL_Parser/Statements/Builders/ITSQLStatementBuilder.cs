using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements.Builders
{
	public interface ITSQLStatementBuilder
	{
		TSQLStatement Build(TSQLTokenizer tokenizer);
	}
}
