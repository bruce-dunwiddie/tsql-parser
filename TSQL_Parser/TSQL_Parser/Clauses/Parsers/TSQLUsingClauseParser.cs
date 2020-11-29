using System;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

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
							// ON is required in MERGE statement after USING
							TSQLKeywords.ON
						)
					)
				))
			{
				TSQLTokenParserHelper.RecurseParens(
					tokenizer,
					usingClause,
					ref nestedLevel);
			}

			return usingClause;
		}
	}
}