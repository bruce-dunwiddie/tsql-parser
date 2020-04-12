using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

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

			TSQLSubqueryHelper.ReadUntilStop(
				Tokenizer,
				Statement,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() { },
				lookForStatementStarts: true);

			if (
				Tokenizer.Current?.AsKeyword != null &&
				Tokenizer.Current.AsKeyword.Keyword.IsStatementStart())
			{
				Tokenizer.Putback();
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
