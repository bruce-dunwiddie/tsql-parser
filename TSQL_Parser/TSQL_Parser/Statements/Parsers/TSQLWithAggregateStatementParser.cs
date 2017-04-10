using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	// represents the CTE's portion of the statement along with the following SELECT/INSERT/UPDATE/DELETE statement
	internal class TSQLWithAggregateStatementParser : ITSQLStatementParser
	{
		public TSQLStatement Parse(IEnumerator<TSQLToken> tokenizer)
		{
			TSQLWithStatement with = new TSQLWithStatementParser().Parse(tokenizer);

			TSQLStatement statement = new TSQLStatementParserFactory().Create(tokenizer.Current).Parse(tokenizer);

			statement.Tokens.InsertRange(0, with.Tokens);

			return statement;
		}
	}
}
