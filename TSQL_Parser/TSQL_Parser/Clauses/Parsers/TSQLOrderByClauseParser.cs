using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLOrderByClauseParser
	{
		public TSQLOrderByClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLOrderByClause orderBy = new TSQLOrderByClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.ORDER))
			{
				throw new InvalidOperationException("ORDER expected.");
			}

			orderBy.Tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadUntilStop(
				tokenizer,
				orderBy,
				new List<TSQLFutureKeywords>()
				{
					TSQLFutureKeywords.OFFSET
				},
				new List<TSQLKeywords>() {
					TSQLKeywords.FOR,
					TSQLKeywords.OPTION
				},
				lookForStatementStarts: true);

			// have to handle OFFSET parsing specially because it can contain FETCH, which would otherwise
			// signal the start of a new statement instead of still being contained within OFFSET
			if (tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OFFSET))
			{
				TSQLOffsetClause offsetClause = new TSQLOffsetClauseParser().Parse(tokenizer);
				orderBy.Tokens.AddRange(offsetClause.Tokens);
			}
			
			return orderBy;
		}
	}
}
