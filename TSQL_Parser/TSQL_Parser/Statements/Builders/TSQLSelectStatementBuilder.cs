using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Builders
{
	public class TSQLSelectStatementBuilder : ITSQLStatementBuilder
	{
		public TSQLStatement Build(TSQLTokenizer tokenizer)
		{
			TSQLSelectStatement select = new TSQLSelectStatement();

			TSQLKeyword keyword = tokenizer.Next().AsKeyword;

			if (keyword == null ||
				keyword.Keyword != TSQLKeywords.SELECT)
			{
				throw new ApplicationException("SELECT expected.");
			}

			select.Tokens.Add(keyword);

			while (
				tokenizer.Read() &&
				!(
					tokenizer.Current is TSQLCharacter &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				))
			{
				select.Tokens.Add(tokenizer.Current);
			}

			if (
				tokenizer.Current is TSQLCharacter &&
				tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
			)
			{
				select.Tokens.Add(tokenizer.Current);
			}

			return select;
		}
	}
}
