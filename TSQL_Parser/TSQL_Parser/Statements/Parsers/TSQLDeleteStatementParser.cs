using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLDeleteStatementParser : ITSQLStatementParser
	{
		public TSQLDeleteStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLDeleteStatementParser(TSQLWithClause with, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.With = with;

			Statement.Tokens.AddRange(with.Tokens);
		}

		private TSQLDeleteStatement Statement { get; } = new TSQLDeleteStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLDeleteStatement Parse()
		{
			TSQLDeleteClause deleteClause = new TSQLDeleteClauseParser().Parse(Tokenizer);

			Statement.Delete = deleteClause;

			Statement.Tokens.AddRange(deleteClause.Tokens);

			if (Tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				TSQLOutputClause outputClause = new TSQLOutputClauseParser().Parse(Tokenizer);

				Statement.Output = outputClause;

				Statement.Tokens.AddRange(outputClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.FROM))
			{
				TSQLFromClause fromClause = new TSQLFromClauseParser().Parse(Tokenizer);

				Statement.From = fromClause;

				Statement.Tokens.AddRange(fromClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.WHERE))
			{
				TSQLWhereClause whereClause = new TSQLWhereClauseParser().Parse(Tokenizer);

				Statement.Where = whereClause;

				Statement.Tokens.AddRange(whereClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
			{
				TSQLOptionClause optionClause = new TSQLOptionClauseParser().Parse(Tokenizer);

				Statement.Option = optionClause;

				Statement.Tokens.AddRange(optionClause.Tokens);
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
