using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQL.Statements.Parsers
{
	public class TSQLWithAggregateStatementParser : ITSQLStatementParser
	{
		public TSQLStatement Parse(TSQLTokenizer tokenizer)
		{
			TSQLWithStatement with = new TSQLWithStatementParser().Parse(tokenizer);

			TSQLStatement statement = new TSQLStatementParserFactory().Create(tokenizer.Current).Parse(tokenizer);

			statement.Tokens.InsertRange(0, with.Tokens);

			return statement;
		}
	}
}
