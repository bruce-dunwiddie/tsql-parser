using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Statements.Parsers
{
	internal class TSQLExecuteStatementParser : ITSQLStatementParser
	{
		public TSQLExecuteStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		private TSQLExecuteStatement Statement { get; } = new TSQLExecuteStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLExecuteStatement Parse()
		{
			Statement.Tokens.Add(Tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				Tokenizer,
				Statement,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() { },
				lookForStatementStarts: true);

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
