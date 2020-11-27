using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Elements;
using TSQL.Elements.Parsers;
using TSQL.Statements;
using TSQL.Tokens;

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
				if (tokenizer.Current.IsWhitespace() ||
					tokenizer.Current.IsComment() ||
					tokenizer.Current.IsCharacter(TSQLCharacters.Comma))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();
				}
				else if (tokenizer.Current.IsKeyword(TSQLKeywords.DISTINCT))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();
				}
				else if (tokenizer.Current.IsKeyword(TSQLKeywords.TOP))
				{
					select.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					//if (tokenizer.MoveNext())
					//{
					//	if (tokenizer.Current.Type == TSQLTokenType.NumericLiteral)
					//	{
					//		select.Tokens.Add(tokenizer.Current);
					//	}

					//	// TODO: how to handle TOP N vs TOP(N)
					//}
				}
				else
				{
					TSQLSelectColumn column = new TSQLSelectColumnParser().Parse(tokenizer);

					select.Tokens.AddRange(column.Tokens);

					select.Columns.Add(column);
				}
			};

			return select;
		}
	}
}
