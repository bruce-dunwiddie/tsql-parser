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

			// need to part TOP, DISTINCT, etc here

			TSQLSubqueryHelper.ReadUntilStop(
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

			// TODO: switch logic to use below once expression parsers are fully functional

			//while (tokenizer.MoveNext() &&
			//	!tokenizer.Current.IsCharacter(TSQLCharacters.Semicolon) &&
			//	!(
			//		tokenizer.Current.Type == TSQLTokenType.Keyword &&
			//		(
			//			tokenizer.Current.AsKeyword.Keyword.In(
			//				TSQLKeywords.INTO,
			//				TSQLKeywords.FROM,
			//				TSQLKeywords.WHERE,
			//				TSQLKeywords.GROUP,
			//				TSQLKeywords.HAVING,
			//				TSQLKeywords.ORDER,
			//				TSQLKeywords.UNION,
			//				TSQLKeywords.EXCEPT,
			//				TSQLKeywords.INTERSECT,
			//				TSQLKeywords.FOR,
			//				TSQLKeywords.OPTION) ||
			//			tokenizer.Current.AsKeyword.Keyword.IsStatementStart()
			//		)
			//	))
			//{
			//	if (tokenizer.Current.IsWhitespace() ||
			//		tokenizer.Current.IsComment() ||
			//		tokenizer.Current.IsCharacter(TSQLCharacters.Comma))
			//	{
			//		select.Tokens.Add(tokenizer.Current);
			//	}
			//	else
			//	{
			//		TSQLSelectColumn column = new TSQLSelectColumnParser().Parse(tokenizer);

			//		select.Tokens.AddRange(column.Tokens);

			//		select.Columns.Add(column);
			//	}
			//};

			return select;
		}
	}
}
