using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLSelectStatementParser : ITSQLStatementParser
	{
		public TSQLSelectStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLSelectStatementParser(TSQLWithClause with, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.With = with;

			Statement.Tokens.AddRange(with.Tokens);
		}

		private TSQLSelectStatement Statement { get; } = new TSQLSelectStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLSelectStatement Parse()
		{
			TSQLSelectClause selectClause = new TSQLSelectClauseParser().Parse(Tokenizer);

			Statement.Select = selectClause;

			Statement.Tokens.AddRange(selectClause.Tokens);

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				TSQLIntoClause intoClause = new TSQLIntoClauseParser().Parse(Tokenizer);

				Statement.Into = intoClause;

				Statement.Tokens.AddRange(intoClause.Tokens);
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

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.GROUP))
			{
				TSQLGroupByClause groupByClause = new TSQLGroupByClauseParser().Parse(Tokenizer);

				Statement.GroupBy = groupByClause;

				Statement.Tokens.AddRange(groupByClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.HAVING))
			{
				TSQLHavingClause havingClause = new TSQLHavingClauseParser().Parse(Tokenizer);

				Statement.Having = havingClause;

				Statement.Tokens.AddRange(havingClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.ORDER))
			{
				TSQLOrderByClause orderByClause = new TSQLOrderByClauseParser().Parse(Tokenizer);

				Statement.OrderBy = orderByClause;

				Statement.Tokens.AddRange(orderByClause.Tokens);
			}

			// order for OPTION and FOR doesn't seem to matter
			while (
				Tokenizer.Current.IsKeyword(TSQLKeywords.FOR) ||
				Tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
			{
				if (Tokenizer.Current.IsKeyword(TSQLKeywords.FOR))
				{
					TSQLForClause forClause = new TSQLForClauseParser().Parse(Tokenizer);

					Statement.For = forClause;

					Statement.Tokens.AddRange(forClause.Tokens);
				}

				if (Tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
				{
					TSQLOptionClause optionClause = new TSQLOptionClauseParser().Parse(Tokenizer);

					Statement.Option = optionClause;

					Statement.Tokens.AddRange(optionClause.Tokens);
				}
			}

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
