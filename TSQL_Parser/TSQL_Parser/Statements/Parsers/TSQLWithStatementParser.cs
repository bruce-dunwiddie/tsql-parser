using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	// represents only the CTE's portion of a larger SELECT/INSERT/UPDATE/DELETE statement
	internal class TSQLWithStatementParser : ITSQLStatementParser
	{
		public TSQLWithStatement Parse(ITSQLTokenizer tokenizer)
		{
			TSQLWithStatement statement = new TSQLWithStatement();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.WITH))
			{
				throw new ApplicationException("WITH expected.");
			}

			statement.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				tokenizer.Current.Type != TSQLTokenType.Identifier)
			{
				statement.Tokens.Add(tokenizer.Current);
			}

			while (
				tokenizer.Current != null &&
				tokenizer.Current.Type == TSQLTokenType.Identifier)
			{
				TSQLCommonTableExpressionClause cte =
					new TSQLCommonTableExpressionClauseParser().Parse(tokenizer);

				statement.Tokens.AddRange(cte.Tokens);

				if (
					tokenizer.Current != null &&
					tokenizer.Current.Type != TSQLTokenType.Identifier)
				{
					statement.Tokens.Add(tokenizer.Current);

					while (
						tokenizer.MoveNext() &&
						tokenizer.Current.Type != TSQLTokenType.Keyword)
					{
						statement.Tokens.Add(tokenizer.Current);
					}
				}
			}

			return statement;
		}

		TSQLStatement ITSQLStatementParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
