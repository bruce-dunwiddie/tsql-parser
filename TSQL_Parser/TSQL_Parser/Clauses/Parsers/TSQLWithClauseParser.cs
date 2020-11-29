using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

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

			// subqueries
			int nestedLevel = 0;

			int parenCount = 0;
			int identifierCount = 0;
			bool afterAs = false;

			while (
				tokenizer.MoveNext() &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
				!(
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses)
				) &&
				!(
					// only allow a set of parens at root level
					// if it's following an AS
					// or if it's the column list between the CTE name and the AS
					nestedLevel == 0 &&
					tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses) &&
					afterAs &&
					parenCount >= identifierCount
				) &&
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
				if (nestedLevel == 0)
				{
					if (afterAs && tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
					{
						parenCount++;
					}
					else if (tokenizer.Current.Type == TSQLTokenType.Identifier)
					{
						identifierCount++;
						afterAs = false;
					}
					else if (tokenizer.Current.IsKeyword(TSQLKeywords.AS))
					{
						afterAs = true;
					}
				}

				TSQLTokenParserHelper.RecurseParens(
					tokenizer,
					with,
					ref nestedLevel);
			}

			return with;
		}
	}
}
