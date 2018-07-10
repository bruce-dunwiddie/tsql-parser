using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;

namespace TSQL.Clauses.Parsers
{
	internal class TSQLCommonTableExpressionClauseParser : ITSQLClauseParser
	{
		public TSQLCommonTableExpressionClause Parse(ITSQLTokenizer tokenizer)
		{
			TSQLCommonTableExpressionClause cte = new TSQLCommonTableExpressionClause();

			if (tokenizer.Current.Type != Tokens.TSQLTokenType.Identifier)
			{
				throw new InvalidOperationException("Identifier expected.");
			}

			cte.Name = tokenizer.Current.AsIdentifier.Name;

			tokenizer.MoveNext();

			return cte;
		}

		TSQLClause ITSQLClauseParser.Parse(ITSQLTokenizer tokenizer)
		{
			return Parse(tokenizer);
		}
	}
}
