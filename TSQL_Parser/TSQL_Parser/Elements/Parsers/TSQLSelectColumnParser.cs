using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Expressions;
using TSQL.Expressions.Parsers;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Elements.Parsers
{
	internal class TSQLSelectColumnParser
	{
		public TSQLSelectColumn Parse(ITSQLTokenizer tokenizer)
		{
			TSQLSelectColumn column = new TSQLSelectColumn();

			TSQLExpression columnExpression = new TSQLSelectExpressionParser().Parse(tokenizer);

			column.Expression = columnExpression;

			column.Tokens.AddRange(columnExpression.Tokens);

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				column);

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

				TSQLExpression actualColumn = new TSQLValueExpressionParser().Parse(tokenizer);

				column.Expression = actualColumn;
				column.ColumnAlias = columnExpression.AsColumn.Column;

				column.Tokens.AddRange(actualColumn.Tokens);
			}
			else
			{
				if (tokenizer.Current.IsKeyword(TSQLKeywords.AS))
				{
					column.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLTokenParserHelper.ReadCommentsAndWhitespace(
						tokenizer,
						column);
				}

				if (tokenizer.Current != null &&
					tokenizer.Current.Type.In(						
						TSQLTokenType.Identifier,
						TSQLTokenType.SystemIdentifier,
						TSQLTokenType.IncompleteIdentifier))
				{
					column.Tokens.Add(tokenizer.Current);

					if (tokenizer.Current.Type.In(
						TSQLTokenType.Identifier,
						TSQLTokenType.SystemIdentifier))
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
