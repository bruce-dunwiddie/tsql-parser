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
			// TODO: check for variable assignment, e.g. @id = p.id

			// should we return a null TSQLSelectColumn object in this case?

			// TODO: check for alternate column aliasing syntax, e.g. product_id = p.id

			TSQLSelectColumn column = new TSQLSelectColumn();

			TSQLExpression columnExpression = new TSQLExpressionFactory().Parse(tokenizer);

			column.Expression = columnExpression;

			column.Tokens.AddRange(columnExpression.Tokens);

			while (
				tokenizer.Current != null &&
				(
					tokenizer.Current.IsWhitespace() ||
					tokenizer.Current.IsComment())
				)
			{
				column.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}

			if (
				tokenizer.Current != null &&
				tokenizer.Current.IsKeyword(TSQLKeywords.AS))
			{
				column.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();

				while (
					tokenizer.Current.IsWhitespace() ||
					tokenizer.Current.IsComment())
				{
					column.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();
				}
			}

			if (tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Identifier,
					TSQLTokenType.IncompleteIdentifier))
			{
				column.Tokens.Add(tokenizer.Current);

				column.ColumnAlias = tokenizer.Current;

				tokenizer.MoveNext();
			}

			return column;
		}
	}
}
