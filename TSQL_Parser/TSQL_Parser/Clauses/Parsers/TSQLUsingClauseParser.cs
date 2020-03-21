using System;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLUsingClauseParser
	{
		public TSQLUsingClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLUsingClause usingClause = new TSQLUsingClause();

			if (!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.USING))
			{
				throw new InvalidOperationException("USING expected.");
			}

			usingClause.Tokens.Add(tokenizer.Current);

			/* can contain:
			 *
			 * <table_source> ::=
			 * {
			 *     table_or_view_name [ [ AS ] table_alias ] [ <tablesample_clause> ]
			 *         [ WITH ( table_hint [ [ , ]...n ] ) ]
			 *   | rowset_function [ [ AS ] table_alias ]
			 *         [ ( bulk_column_alias [ ,...n ] ) ]
			 *   | user_defined_function [ [ AS ] table_alias ]
			 *   | OPENXML <openxml_clause>
			 *   | derived_table [ AS ] table_alias [ ( column_alias [ ,...n ] ) ]
			 *   | <joined_table>
			 *   | <pivoted_table>
			 *   | <unpivoted_table>
			 * }
			 * https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql?view=sql-server-ver15#syntax
			 */

			// ends with keyword other than those listed above, when used outside of parens

			// recursively walk down and back up parens

			int nestedLevel = 0;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				(
					nestedLevel > 0 ||
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.ON,
							TSQLKeywords.WHEN
						) &&
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					usingClause,
					ref nestedLevel);
			}

			return usingClause;
		}
	}
}