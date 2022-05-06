using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLSetOperatorClauseParser
	{
		public TSQLSetOperatorClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSetOperatorClause set = null;

			if (tokenizer.Current.IsKeyword(TSQLKeywords.UNION))
			{
				set = new TSQLUnionClause();

				set.Tokens.Add(tokenizer.Current);

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					set.Tokens);

				if (tokenizer.Current.IsKeyword(TSQLKeywords.ALL))
				{
					set.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();
				}
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.EXCEPT))
			{
				set = new TSQLExceptClause();

				set.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.INTERSECT))
			{
				set = new TSQLIntersectClause();

				set.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				set);

			int level = 0;

			while (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
			{
				set.Tokens.Add(tokenizer.Current);

				level++;

				tokenizer.MoveNext();
			}

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				set);

			TSQLSelectStatement select = new TSQLLimitedSelectStatementParser(tokenizer).Parse();

			set.Select = select;

			set.Tokens.AddRange(select.Tokens);

			while (level > 0 && tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
			{
				set.Tokens.Add(tokenizer.Current);

				level--;

				tokenizer.MoveNext();
			}

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				set);

			return set;
		}
	}
}
