using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLWithClauseParser
	{
		public TSQLWithClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLWithClause with = new TSQLWithClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.WITH))
			{
				throw new InvalidOperationException("WITH expected.");
			}

			with.Tokens.Add(tokenizer.Current);

			TSQLSubqueryHelper.ReadUntilStop(
				tokenizer,
				with,
				new List<TSQLFutureKeywords>() { },
				new List<TSQLKeywords>() {
					TSQLKeywords.SELECT,
					TSQLKeywords.INSERT,
					TSQLKeywords.UPDATE,
					TSQLKeywords.DELETE,
					TSQLKeywords.MERGE
				},
				lookForStatementStarts: false);

			// subqueries
			int nestedLevel = 0;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				// TODO: only let code enter a new set of parens once
				(
					nestedLevel > 0 ||
					tokenizer.Current.Type != TSQLTokenType.Keyword ||
					(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						!tokenizer.Current.AsKeyword.Keyword.In
						(
							TSQLKeywords.SELECT,
							TSQLKeywords.INSERT,
							TSQLKeywords.UPDATE,
							TSQLKeywords.DELETE,
							TSQLKeywords.MERGE
						)
					)
				))
			{
				TSQLSubqueryHelper.RecurseParens(
					tokenizer,
					with,
					ref nestedLevel);
			}

			return with;
		}
	}
}
