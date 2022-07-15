using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLVariableAssignmentExpressionParser
	{
		public TSQLVariableAssignmentExpression Parse(
			ITSQLTokenizer tokenizer,
			TSQLVariableExpression variable)
		{
			TSQLVariableAssignmentExpression opExpression = new TSQLVariableAssignmentExpression();

			opExpression.Variable = variable.Variable;
			opExpression.Operator = tokenizer.Current.AsOperator;

			opExpression.Tokens.AddRange(variable.Tokens);
			opExpression.Tokens.Add(tokenizer.Current);

			while (
				tokenizer.MoveNext() &&
				(
					tokenizer.Current.IsWhitespace() ||
					tokenizer.Current.IsComment()
				))
			{
				opExpression.Tokens.Add(tokenizer.Current);
			}

			TSQLExpression rightSide = new TSQLValueExpressionParser().Parse(
				tokenizer);

			// TODO: add test for nested operators like below

			// a + (b + (c + d))

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Operator))
			{
				rightSide = new TSQLOperationExpressionParser().Parse(
					tokenizer,
					rightSide);
			}

			opExpression.ValueExpression = rightSide;
			opExpression.Tokens.AddRange(rightSide.Tokens);

			return opExpression;
		}
	}
}
