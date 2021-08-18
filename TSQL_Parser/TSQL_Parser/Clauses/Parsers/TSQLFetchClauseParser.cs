using System;
using System.Collections.Generic;
using TSQL.Tokens;

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
            
            TSQLSubqueryHelper.ReadUntilStop(
                tokenizer,
                fetchClause,
                new List<TSQLFutureKeywords>
                {
                    Capacity = 0
                },
                new List<TSQLKeywords>
                {
                },
                true);

            return fetchClause;
        }
    }
}