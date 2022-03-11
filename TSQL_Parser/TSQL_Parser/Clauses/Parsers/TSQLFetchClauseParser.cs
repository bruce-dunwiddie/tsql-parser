using System;
using System.Collections.Generic;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
    internal class TSQLFetchClauseParser
    {
        public TSQLFetchClause Parse(ITSQLTokenizer tokenizer)
        {
            TSQLFetchClause fetchClause = new TSQLFetchClause();
            
            if (!tokenizer.Current.IsKeyword(TSQLKeywords.FETCH))
            {
                throw new InvalidOperationException("FETCH expected.");
            }
            
            fetchClause.Tokens.Add(tokenizer.Current);

            TSQLTokenParserHelper.ReadUntilStop(
                tokenizer,
                fetchClause,
                new List<TSQLFutureKeywords> { },
                new List<TSQLKeywords> { },
                true);

            return fetchClause;
        }
    }
}