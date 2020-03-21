using System;

using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLMergeClauseParser
	{
		public TSQLMergeClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLMergeClause merge = new TSQLMergeClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.MERGE))
			{
				throw new InvalidOperationException("MERGE expected.");
			}

			merge.Tokens.Add(tokenizer.Current);

			// can contain TOP

			// ends with INTO, semicolon, or keyword other than those listed above, when used outside of parens

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
							TSQLKeywords.INTO,
							TSQLKeywords.AS,
							TSQLKeywords.ON,
							TSQLKeywords.WHEN
						) &&
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.USING) &&
						!tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT) &&
						!tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					merge,
					ref nestedLevel);
			}

			return merge;
		}
	}
}