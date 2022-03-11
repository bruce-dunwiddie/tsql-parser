using System;
using System.Collections.Generic;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	/// <summary>
	///		This parser is currently written to handle WHEN specifically inside a MERGE,
	///		and may not yet handle parsing WHEN within a CASE.
	/// </summary>
	internal class TSQLWhenClauseParser
	{
		public TSQLWhenClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLWhenClause when = new TSQLWhenClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.WHEN))
			{
				throw new InvalidOperationException("WHEN expected.");
			}

			when.Tokens.Add(tokenizer.Current);

			// we don't have to worry about accidentally running into the next statement.

			// https://docs.microsoft.com/en-us/sql/t-sql/statements/merge-transact-sql
			// The MERGE statement requires a semicolon (;) as a statement terminator.
			// Error 10713 is raised when a MERGE statement is run without the terminator.

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				when,
				new List<TSQLFutureKeywords>() {
					TSQLFutureKeywords.OUTPUT
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.WHEN,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: false);

			return when;
		}
	}
}