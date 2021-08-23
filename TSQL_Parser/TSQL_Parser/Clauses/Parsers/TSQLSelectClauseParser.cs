using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Elements;
using TSQL.Elements.Parsers;
using TSQL.Statements;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLSelectClauseParser
	{
		public TSQLSelectClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSelectClause select = new TSQLSelectClause();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
			{
				throw new InvalidOperationException("SELECT expected.");
			}

			select.Tokens.Add(tokenizer.Current);

			tokenizer.MoveNext();

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				select);

			if (tokenizer.Current.IsKeyword(TSQLKeywords.ALL) ||
				tokenizer.Current.IsKeyword(TSQLKeywords.DISTINCT))
			{
				select.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();

				TSQLTokenParserHelper.ReadCommentsAndWhitespace(
					tokenizer,
					select);
			}

			if (tokenizer.Current.IsKeyword(TSQLKeywords.TOP))
			{
				select.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();

				TSQLTokenParserHelper.ReadCommentsAndWhitespace(
					tokenizer,
					select);

				if (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);
				}

				if (tokenizer.Current != null &&
					tokenizer.Current.Type == TSQLTokenType.NumericLiteral)
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);
				}

				if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);
				}

				if (tokenizer.Current.IsKeyword(TSQLKeywords.PERCENT))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);
				}

				if (tokenizer.Current.IsKeyword(TSQLKeywords.WITH))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);

					if (tokenizer.Current != null &&
						tokenizer.Current.Type == TSQLTokenType.Identifier &&
						tokenizer.Current.AsIdentifier.Text.Equals(
							"TIES",
							StringComparison.InvariantCultureIgnoreCase))
					{
						select.Tokens.Add(tokenizer.Current);

						tokenizer.MoveNext();

						TSQLTokenParserHelper.ReadCommentsAndWhitespace(
							tokenizer,
							select);
					}
				}
			}

			// SELECT @ID = 1

			// TODO: put some more work into this branch to handle returning variables as columns.
			// e.g. SELECT @ID AS ID

			if (tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Variable,
					TSQLTokenType.SystemVariable))
			{
				select.Tokens.Add(tokenizer.Current);

				TSQLTokenParserHelper.ReadUntilStop(
					tokenizer,
					select,
					new List<TSQLFutureKeywords>() { },
					new List<TSQLKeywords>() {
						TSQLKeywords.INTO,
						TSQLKeywords.FROM,
						TSQLKeywords.WHERE,
						TSQLKeywords.GROUP,
						TSQLKeywords.HAVING,
						TSQLKeywords.ORDER,
						TSQLKeywords.UNION,
						TSQLKeywords.EXCEPT,
						TSQLKeywords.INTERSECT,
						TSQLKeywords.FOR,
						TSQLKeywords.OPTION
					},
					lookForStatementStarts: true);
			}
			else
			{
				while (
					tokenizer.Current != null &&
					!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
					!tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses) &&
					!(
						tokenizer.Current.Type == TSQLTokenType.Keyword &&
						(
							tokenizer.Current.AsKeyword.Keyword.In(
								TSQLKeywords.INTO,
								TSQLKeywords.FROM,
								TSQLKeywords.WHERE,
								TSQLKeywords.GROUP,
								TSQLKeywords.HAVING,
								TSQLKeywords.ORDER,
								TSQLKeywords.UNION,
								TSQLKeywords.EXCEPT,
								TSQLKeywords.INTERSECT,
								TSQLKeywords.FOR,
								TSQLKeywords.OPTION) ||
							tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
						)
					))
				{
					TSQLSelectColumn column = new TSQLSelectColumnParser().Parse(tokenizer);

					select.Tokens.AddRange(column.Tokens);

					select.Columns.Add(column);

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						select);

					if (tokenizer.Current.IsCharacter(TSQLCharacters.Comma))
					{
						select.Tokens.Add(tokenizer.Current);

						tokenizer.MoveNext();

						TSQLTokenParserHelper.ReadCommentsAndWhitespace(
							tokenizer,
							select);
					}
				}
			}

			return select;
		}
	}
}
