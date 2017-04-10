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
	internal class TSQLSelectStatementParser : ITSQLStatementParser
	{
		public TSQLSelectStatement Parse(IEnumerator<TSQLToken> tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			// should whitespace be excluded from statement parsing logic?

			// should I differentiate keywords that start commands?

			// correlated subqueries
			// scalar function calls

			// SELECT clause

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

			if (tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon))
			{
				select.Tokens.Add(tokenizer.Current);
			}

			return select;
		}

		TSQLStatement ITSQLStatementParser.Parse(IEnumerator<TSQLToken> tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
