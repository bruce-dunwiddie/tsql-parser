using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;

namespace TSQL.Clauses.Parsers
{
	internal static class TSQLSubqueryHelper
	{
		public static void RecurseParens(
			ITSQLTokenizer tokenizer,
			TSQLClause clause,
			ref int nestedLevel)
		{
			clause.Tokens.Add(tokenizer.Current);

			if (tokenizer.Current.Type == TSQLTokenType.Character)
			{
				TSQLCharacters character = tokenizer.Current.AsCharacter.Character;

				if (character == TSQLCharacters.OpenParentheses)
				{
					// should we recurse for correlated subqueries?
					nestedLevel++;

					if (tokenizer.MoveNext())
					{
						if (tokenizer.Current.IsKeyword(
							TSQLKeywords.SELECT))
						{
							TSQLSelectStatement selectStatement = new TSQLSelectStatementParser(tokenizer).Parse();

							clause.Tokens.AddRange(selectStatement.Tokens);

							if (tokenizer.Current.IsCharacter(
								TSQLCharacters.CloseParentheses))
							{
								nestedLevel--;
								clause.Tokens.Add(tokenizer.Current);
							}
						}
						else if (tokenizer.Current.IsCharacter(
							TSQLCharacters.CloseParentheses))
						{
							nestedLevel--;
							clause.Tokens.Add(tokenizer.Current);
						}
						else if (tokenizer.Current.IsCharacter(
							TSQLCharacters.OpenParentheses))
						{
							nestedLevel++;
							clause.Tokens.Add(tokenizer.Current);
						}
						else
						{
							clause.Tokens.Add(tokenizer.Current);
						}
					}
				}
				else if (character == TSQLCharacters.CloseParentheses)
				{
					nestedLevel--;
				}
			}
		}
	}
}
