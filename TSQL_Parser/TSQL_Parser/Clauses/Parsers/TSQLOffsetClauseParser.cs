using System;
using System.Collections.Generic;
using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
    internal class TSQLOffsetClauseParser
    {
        public TSQLOffesetClause Parse(ITSQLTokenizer tokenizer)
        {
            TSQLOffesetClause offset = new TSQLOffesetClause();

            if (!tokenizer.Current.IsKeyword(TSQLKeywords.OFFSET))
            {
                throw new InvalidOperationException("OFFSET expected.");
            }

            offset.Tokens.Add(tokenizer.Current);
            
            TSQLSubqueryHelper.ReadUntilStop(
                tokenizer,
                offset,
                new List<TSQLFutureKeywords>
                {
                    Capacity = 0
                },
                new List<TSQLKeywords>
                {
                    TSQLKeywords.FETCH
                },
                true);
            
            if (tokenizer.Current.IsKeyword(TSQLKeywords.FETCH))
            {
                TSQLFetchClause fetchClause = new TSQLFetchClauseParser().Parse(tokenizer);
                offset.Tokens.AddRange(fetchClause.Tokens);
            }

            return offset;
        }
    }
}