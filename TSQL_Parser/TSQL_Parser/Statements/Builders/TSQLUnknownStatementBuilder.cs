using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Statements.Builders
{
	public class TSQLUnknownStatementBuilder : ITSQLStatementBuilder
	{
		public TSQLStatement Build(TSQLTokenizer tokenizer)
		{
			TSQLUnknownStatement statement = new TSQLUnknownStatement();

			while (
				tokenizer.Read() &&
				!(
					tokenizer.Current is TSQLCharacter &&
					tokenizer.Current.AsCharacter.Character == TSQLCharacters.Semicolon
				))
			{
				statement.Tokens.Add(tokenizer.Current);
			}

			return statement;
		}
	}
}
