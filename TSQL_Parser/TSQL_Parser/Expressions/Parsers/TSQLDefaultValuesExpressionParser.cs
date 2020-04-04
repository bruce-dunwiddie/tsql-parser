using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses.Parsers;
using TSQL.Statements;
using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLDefaultValuesExpressionParser
	{
		public TSQLDefaultValuesExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLDefaultValuesExpression defaultValues = new TSQLDefaultValuesExpression();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.DEFAULT))
			{
				throw new InvalidOperationException("DEFAULT expected.");
			}

			defaultValues.Tokens.Add(tokenizer.Current);

			if (tokenizer.MoveNext() &&
				tokenizer.Current.IsKeyword(TSQLKeywords.VALUES))
			{
				defaultValues.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}			

			return defaultValues;
		}
	}
}
