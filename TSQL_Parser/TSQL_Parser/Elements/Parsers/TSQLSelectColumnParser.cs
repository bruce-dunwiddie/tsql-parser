using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Expressions;
using TSQL.Expressions.Parsers;
using TSQL.Tokens;

namespace TSQL.Elements.Parsers
{
	internal class TSQLSelectColumnParser
	{
		public TSQLSelectColumn Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSelectColumn column = new TSQLSelectColumn();

			TSQLExpression columnExpression = new TSQLExpressionFactory().Parse(tokenizer);

			column.Expression = columnExpression;

			column.Tokens.AddRange(columnExpression.Tokens);

			if (tokenizer.Current.IsKeyword(TSQLKeywords.AS))
			{
				column.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}

			while (
				tokenizer.Current.IsWhitespace() ||
				tokenizer.Current.IsComment())
			{
				column.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}

			if (tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Identifier,
					TSQLTokenType.IncompleteIdentifier))
			{
				column.Tokens.Add(tokenizer.Current);

				column.Alias = tokenizer.Current;
			}

			tokenizer.MoveNext();

			return column;
		}
	}
}
