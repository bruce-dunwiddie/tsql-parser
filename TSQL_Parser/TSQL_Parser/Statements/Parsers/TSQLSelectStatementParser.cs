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
		public TSQLSelectStatement Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			TSQLSelectClause selectClause = new TSQLSelectClauseParser().Parse(tokenizer);

			select.Select = selectClause;

			select.Tokens.AddRange(selectClause.Tokens);

			if (tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				TSQLIntoClause intoClause = new TSQLIntoClauseParser().Parse(tokenizer);

				select.Into = intoClause;

				select.Tokens.AddRange(intoClause.Tokens);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.FROM))
			{
				TSQLFromClause fromClause = new TSQLFromClauseParser().Parse(tokenizer);

				select.From = fromClause;

				select.Tokens.AddRange(fromClause.Tokens);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.WHERE))
			{
				TSQLWhereClause whereClause = new TSQLWhereClauseParser().Parse(tokenizer);

				select.Where = whereClause;

				select.Tokens.AddRange(whereClause.Tokens);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.GROUP))
			{
				TSQLGroupByClause groupByClause = new TSQLGroupByClauseParser().Parse(tokenizer);

				select.GroupBy = groupByClause;

				select.Tokens.AddRange(groupByClause.Tokens);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.HAVING))
			{
				TSQLHavingClause havingClause = new TSQLHavingClauseParser().Parse(tokenizer);

				select.Having = havingClause;

				select.Tokens.AddRange(havingClause.Tokens);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.ORDER))
			{
				TSQLOrderByClause orderByClause = new TSQLOrderByClauseParser().Parse(tokenizer);

				select.OrderBy = orderByClause;

				select.Tokens.AddRange(orderByClause.Tokens);
			}

			// order for OPTION and FOR doesn't seem to matter
			while (
				tokenizer.Current.IsKeyword(TSQLKeywords.FOR) ||
				tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
			{
				if (tokenizer.Current.IsKeyword(TSQLKeywords.FOR))
				{
					TSQLForClause forClause = new TSQLForClauseParser().Parse(tokenizer);

					select.For = forClause;

					select.Tokens.AddRange(forClause.Tokens);
				}

				if (tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
				{
					TSQLOptionClause optionClause = new TSQLOptionClauseParser().Parse(tokenizer);

					select.Option = optionClause;

					select.Tokens.AddRange(optionClause.Tokens);
				}
			}

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type == TSQLTokenType.Keyword)
			{
				tokenizer.Putback();
			}

			return select;
		}

		TSQLStatement ITSQLStatementParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
