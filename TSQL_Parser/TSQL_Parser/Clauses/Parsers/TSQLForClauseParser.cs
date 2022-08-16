using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLForClauseParser
	{
		public TSQLForClause Parse(ITSQLTokenizer tokenizer)
		{
			// FOR XML AUTO
			TSQLForClause forClause = new TSQLForClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.FOR))
			{
				throw new InvalidOperationException("FOR expected.");
			}

			forClause.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
				tokenizer,
				forClause.Tokens);

			if (tokenizer.Current.AsIdentifier?.Text?.ToUpper() != "XML")
			{
				throw new InvalidOperationException("XML expected.");
			}

			forClause.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
				tokenizer,
				forClause.Tokens);

			// https://docs.microsoft.com/en-us/sql/relational-databases/xml/for-xml-sql-server?view=sql-server-ver16

			if (!new List<string>
				{
					"RAW",
					"AUTO",
					"EXPLICIT",
					"PATH"
				}.Contains(tokenizer.Current.AsIdentifier?.Text?.ToUpper()))
			{
				throw new InvalidOperationException("RAW, AUTO, EXPLICIT, or PATH expected.");
			}

			forClause.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				forClause,
				new List<TSQLFutureKeywords> { },
				new List<TSQLKeywords> { },
				lookForStatementStarts: true);

			return forClause;
		}
	}
}
