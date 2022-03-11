using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLUpdateStatementParser : ITSQLStatementParser
	{
		public TSQLUpdateStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLUpdateStatementParser(TSQLWithClause with, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.With = with;

			Statement.Tokens.AddRange(with.Tokens);
		}

		private TSQLUpdateStatement Statement { get; } = new TSQLUpdateStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLUpdateStatement Parse()
		{
			TSQLUpdateClause updateClause = new TSQLUpdateClauseParser().Parse(Tokenizer);

			Statement.Update = updateClause;

			Statement.Tokens.AddRange(updateClause.Tokens);

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.SET))
			{
				TSQLSetClause setClause = new TSQLSetClauseParser().Parse(Tokenizer);

				Statement.Set = setClause;

				Statement.Tokens.AddRange(setClause.Tokens);
			}

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
