using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLWithClauseStatementParser : ITSQLStatementParser
	{
		public TSQLWithClauseStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLStatement Parse()
		{
			TSQLWithClause with = new TSQLWithClauseParser().Parse(Tokenizer);

			if (Tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.SELECT)
			{
				return new TSQLSelectStatementParser(with, Tokenizer).Parse();
			}
			else if (Tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.MERGE)
			{
				return new TSQLMergeStatementParser(with, Tokenizer).Parse();
			}
			else
			{
				return new TSQLUnknownStatementParser(with.Tokens, Tokenizer).Parse();
			}
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
