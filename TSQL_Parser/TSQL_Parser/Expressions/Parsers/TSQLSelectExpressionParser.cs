using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLSelectExpressionParser
	{
		public TSQLExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLExpression expression = ParseNext(tokenizer);

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Operator) &&

				// check for operator =, when expression type is column, and don't parse operator if found
				// e.g. IsFinishedGoods = p.FinishedGoodsFlag

				(
					tokenizer.Current.Text != "=" ||
					expression?.Type != TSQLExpressionType.Column
				))
			{
				if (
					expression?.Type == TSQLExpressionType.Variable &&

					// https://docs.microsoft.com/en-us/sql/t-sql/language-elements/compound-operators-transact-sql

					new string[] {
						"=",
						"+=",
						"-=",
						"*=",
						"/=",
						"%=",
						"&=",
						"|="
					}.Contains(tokenizer.Current.AsOperator.Text))
				{
					return new TSQLVariableAssignmentExpressionParser().Parse(
						tokenizer,
						expression.AsVariable);
				}
				else
				{
					return new TSQLOperationExpressionParser().Parse(
						tokenizer,
						expression);
				}
			}
			else
			{
				return expression;
			}
		}

		private static TSQLExpression ParseNext(
			ITSQLTokenizer tokenizer)
		{
			return new TSQLValueExpressionParser().ParseNext(
					tokenizer);
		}
	}
}
