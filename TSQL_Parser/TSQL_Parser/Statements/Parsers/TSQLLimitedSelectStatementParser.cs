using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Statements.Parsers
{
	/// <summary>
	///		This is a parser specifically made for parsing out the select
	///		statement as the second part of a UNION, EXCEPT, or INTERSECT.
	/// </summary>
	internal class TSQLLimitedSelectStatementParser
	{
		public TSQLLimitedSelectStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
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

			// ORDER not allowed

			// FOR not allowed

			// OPTION not allowed

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				Tokenizer,
				Statement);

			return Statement;
		}
	}
}
