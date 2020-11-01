using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLExpressionFactory
	{
		public TSQLExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLExpression expression = ParseNext(tokenizer);

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Operator))
			{
				return new TSQLOperatorExpressionParser().Parse(
					tokenizer,
					expression);
			}
			else
			{
				return expression;
			}
		}

		private TSQLExpression ParseNext(
			ITSQLTokenizer tokenizer)
		{
			if (tokenizer.Current == null)
			{
				return null;
			}

			if (tokenizer.Current.Text == "*")
			{
				TSQLMulticolumnExpression simpleMulti = new TSQLMulticolumnExpression();

				simpleMulti.Tokens.Add(tokenizer.Current);

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					simpleMulti.Tokens.Add(tokenizer.Current);
				}

				return simpleMulti;

				// still need to seperately check for p.* below
			}
			// this checks for unary operators, e.g. +, -, and ~
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.Operator))
			{
				return null;
			}
			else if (tokenizer.Current.IsCharacter(
				TSQLCharacters.OpenParentheses))
			{
				TSQLGroupedExpression group = new TSQLGroupedExpression();

				group.Tokens.Add(tokenizer.Current);

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					group.Tokens.Add(tokenizer.Current);
				}

				group.InnerExpression = Parse(tokenizer);
				group.Tokens.AddRange(group.InnerExpression.Tokens);

				if (tokenizer.Current.IsCharacter(
					TSQLCharacters.CloseParentheses))
				{
					group.Tokens.Add(tokenizer.Current);
					tokenizer.MoveNext();
				}

				return group;
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.Variable,
				TSQLTokenType.SystemVariable))
			{
				TSQLVariableExpression variable = new TSQLVariableExpression();
				variable.Tokens.Add(tokenizer.Current);
				variable.Variable = tokenizer.Current.AsVariable;

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					variable.Tokens.Add(tokenizer.Current);
				}

				return variable;
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.BinaryLiteral,
				TSQLTokenType.MoneyLiteral,
				TSQLTokenType.NumericLiteral,
				TSQLTokenType.StringLiteral,
				TSQLTokenType.IncompleteString))
			{
				TSQLConstantExpression constant = new TSQLConstantExpression();

				constant.Literal = tokenizer.Current.AsLiteral;

				constant.Tokens.Add(tokenizer.Current);

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					constant.Tokens.Add(tokenizer.Current);
				}

				return constant;
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.CASE))
			{
				return new TSQLCaseExpressionParser().Parse(tokenizer);
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.SystemColumnIdentifier,
				TSQLTokenType.IncompleteIdentifier))
			{
				TSQLColumnExpression column = new TSQLColumnExpression();

				column.Column = tokenizer.Current.AsSystemColumnIdentifier.Name;

				column.Tokens.Add(tokenizer.Current);

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					column.Tokens.Add(tokenizer.Current);
				}

				return column;
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.SystemIdentifier))
			{
				return new TSQLFunctionExpression();
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.Identifier))
			{
				// multi column with alias

				// or column, with or without alias, or with full explicit table name with up to 5 parts

				// or function, up to 4 part naming

				// find last token up to and including possible first paren
				// if *, then multi column
				// if paren, then function
				// else column

				// alias would be any tokens prior to last period, removing whitespace, concatenated together as string

				List<TSQLToken> tokens = new List<TSQLToken>();

				tokens.Add(tokenizer.Current);

				while (tokenizer.MoveNext())
				{
					if (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
					{
						tokens.Add(tokenizer.Current);

						TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(
							tokenizer);

						TSQLFunctionExpression function = new TSQLFunctionExpression();

						function.Tokens.AddRange(tokens);
						function.Tokens.AddRange(arguments.Tokens);

						function.Arguments = arguments;
					}
				}

				return new TSQLFunctionExpression();
			}
			else
			{
				return null;
			}
		}
	}
}
