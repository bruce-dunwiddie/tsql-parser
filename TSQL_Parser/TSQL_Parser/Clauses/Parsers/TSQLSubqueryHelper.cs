using System.Collections.Generic;
using System.Linq;
using TSQL.Expressions;
using TSQL.Expressions.Parsers;
using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
    internal static class TSQLSubqueryHelper
    {
        /// <summary>
        ///		This reads recursively through parenthesis and returns when it hits
        ///		one of the stop words outside of any nested parenthesis.
        /// </summary>
        public static void ReadUntilStop(
            ITSQLTokenizer tokenizer,
            TSQLExpression expression,
            List<TSQLFutureKeywords> futureKeywords,
            List<TSQLKeywords> keywords,
            bool lookForStatementStarts)
        {
            int nestedLevel = 0;

            while (GetContinue(tokenizer, nestedLevel, futureKeywords, keywords, lookForStatementStarts))
                RecurseParens(
                    tokenizer,
                    expression,
                    ref nestedLevel);
        }

        private static bool GetContinue(ITSQLTokenizer tokenizer, int nestedLevel, List<TSQLFutureKeywords> futureKeywords, List<TSQLKeywords> keywords, bool lookForStatementStarts)
        {
            var result = tokenizer.MoveNext();

            if (result)
            {
                result = result && !tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon);
                result = result && !(nestedLevel == 0 && tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses));

                var nestedLevelGreaterThan0 = nestedLevel > 0;
                var isFutureKeyword = tokenizer.Current.Type != TSQLTokenType.Keyword &&
                                      !futureKeywords.Any(fk => tokenizer.Current.IsFutureKeyword(fk));

                var isAKeywordType = tokenizer.Current.Type == TSQLTokenType.Keyword;

                if (isAKeywordType)
                {
                    var isInKeywordList = !keywords.Any(k => tokenizer.Current.AsKeyword.Keyword == k);
                    var isStatementStart = tokenizer.Current.AsKeyword.Keyword.IsStatementStart();
                    var isKeyword = isAKeywordType && isInKeywordList &&
                                    !(
                                        lookForStatementStarts &&
                                        isStatementStart
                                    );
                    
                    result = result && (nestedLevelGreaterThan0 || isFutureKeyword || isKeyword);
                }
            }

            return result;
        }

        public static void RecurseParens(
            ITSQLTokenizer tokenizer,
            TSQLExpression expression,
            ref int nestedLevel)
        {
            if (tokenizer.Current.Type == TSQLTokenType.Character)
            {
                expression.Tokens.Add(tokenizer.Current);

                TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

                if (character == TSQLCharacters.OpenParentheses)
                {
                    // should we recurse for correlated subqueries?
                    nestedLevel++;
                }
                else if (character == TSQLCharacters.CloseParentheses)
                {
                    nestedLevel--;
                }
            }
            else if (tokenizer.Current.IsKeyword(TSQLKeywords.CASE))
            {
                // not going to add CASE token directly because it will be contained
                // within the returned expression and we don't want to double up the
                // CASE token within the results.

                // CASE is a special situation because it's stop word (END) is part of
                // the expression itself and needs to be included in it's token list.
                // all other clauses stop at the beginning of the next clause and do
                // not include the stop token within their token list.
                TSQLCaseExpression caseExpression = new TSQLCaseExpressionParser().Parse(tokenizer);

                expression.Tokens.AddRange(caseExpression.Tokens);
            }
            else
            {
                expression.Tokens.Add(tokenizer.Current);
            }
        }
    }
}