using System;
using System.Collections.Generic;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
    internal class TSQLOffsetClauseParser
    {
        public TSQLOffsetClause Parse(ITSQLTokenizer tokenizer)
        {
            TSQLOffsetClause offset = new TSQLOffsetClause();

            if (!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OFFSET))
            {
                throw new InvalidOperationException("OFFSET expected.");
            }

            offset.Tokens.Add(tokenizer.Current);

            TSQLTokenParserHelper.ReadUntilStop(
                tokenizer,
                offset,
                new List<TSQLFutureKeywords> { },
                new List<TSQLKeywords>
                {
                    TSQLKeywords.FETCH
                },
                lookForStatementStarts: true);
            
            if (tokenizer.Current.IsKeyword(TSQLKeywords.FETCH))
            {
                TSQLFetchClause fetchClause = new TSQLFetchClauseParser().Parse(tokenizer);
                offset.Tokens.AddRange(fetchClause.Tokens);
            }

            return offset;
        }
    }
}