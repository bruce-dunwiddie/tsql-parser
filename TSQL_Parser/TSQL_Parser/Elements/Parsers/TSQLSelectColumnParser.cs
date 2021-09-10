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

			// check for operator =, when expression type is column, and return new column expression with alias
			// e.g. IsFinishedGoods = p.FinishedGoodsFlag

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type == TSQLTokenType.Operator &&
				tokenizer.Current.Text == "=" &&
				columnExpression.Type == TSQLExpressionType.Column
				)
			{
				column.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();

				TSQLExpression actualColumn = new TSQLExpressionFactory().Parse(tokenizer);

				column.Expression = actualColumn;
				column.ColumnAlias = columnExpression.AsColumn.Column;

				column.Tokens.AddRange(actualColumn.Tokens);
			}
			else
			{
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

					if (tokenizer.Current.Type == TSQLTokenType.Identifier)
					{
						column.ColumnAlias = tokenizer.Current.AsIdentifier;
					}

					tokenizer.MoveNext();
				}
			}

			return column;
		}
	}
}
