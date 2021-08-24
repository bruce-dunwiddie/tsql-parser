using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Statements;
using TSQL.Statements.Parsers;
using TSQL.Tokens;
using TSQL.Tokens.Parsers;

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
				List<TSQLToken> tokens = new List<TSQLToken>();

				tokens.Add(tokenizer.Current);

				while (
					tokenizer.MoveNext() &&
					(
						tokenizer.Current.IsWhitespace() ||
						tokenizer.Current.IsComment())
					)
				{
					tokens.Add(tokenizer.Current);
				}

				if (tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
				{
					TSQLSubqueryExpression subquery = new TSQLSubqueryExpression();

					subquery.Tokens.AddRange(tokens);

					TSQLSelectStatement select = new TSQLSelectStatementParser(tokenizer).Parse();

					subquery.Select = select;

					subquery.Tokens.AddRange(select.Tokens);

					if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
					{
						subquery.Tokens.Add(tokenizer.Current);

						tokenizer.MoveNext();
					}

					return subquery;
				}
				else
				{
					TSQLGroupedExpression group = new TSQLGroupedExpression();

					group.Tokens.AddRange(tokens);

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
				TSQLFunctionExpression function = new TSQLFunctionExpression();

				function.Name = tokenizer.Current.Text;

				function.Tokens.Add(tokenizer.Current);

				if (tokenizer.MoveNext() &&
					tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
				{
					function.Tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();

					TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(
						tokenizer);

					function.Tokens.AddRange(arguments.Tokens);

					function.Arguments = arguments;

					if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
					{
						function.Tokens.Add(tokenizer.Current);
					}

					while (
						tokenizer.MoveNext() &&
						(
							tokenizer.Current.IsWhitespace() ||
							tokenizer.Current.IsComment())
						)
					{
						function.Tokens.Add(tokenizer.Current);
					}
				}

				return function;
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

				// TODO: handle window functions with multiple parts

				List<TSQLToken> tokens = new List<TSQLToken>();

				tokens.Add(tokenizer.Current);

				while (tokenizer.MoveNext())
				{
					if (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
					{
						tokens.Add(tokenizer.Current);

						tokenizer.MoveNext();

						TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(
							tokenizer);

						TSQLFunctionExpression function = new TSQLFunctionExpression();

						function.Tokens.AddRange(tokens);
						function.Tokens.AddRange(arguments.Tokens);

						function.Arguments = arguments;

						if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
						{
							function.Tokens.Add(tokenizer.Current);
						}

						function.Name =
							String.Join(
								"",
								tokens
									.Where(t => !t.IsComment() && !t.IsWhitespace())
									.Select(t => t.Text));

						tokenizer.MoveNext();

						TSQLTokenParserHelper.ReadCommentsAndWhitespace(
							tokenizer,
							function);

						if (tokenizer.Current.IsKeyword(TSQLKeywords.OVER))
						{
							function.Tokens.Add(tokenizer.Current);

							tokenizer.MoveNext();

							TSQLTokenParserHelper.ReadCommentsAndWhitespace(
								tokenizer,
								function);

							if (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
							{
								function.Tokens.Add(tokenizer.Current);

								// recursively look for final close parens
								TSQLTokenParserHelper.ReadUntilStop(
									tokenizer,
									function,
									new List<TSQLFutureKeywords> { },
									new List<TSQLKeywords> { },
									lookForStatementStarts: false);

								if (tokenizer.Current != null &&
									tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
								{
									function.Tokens.Add(tokenizer.Current);

									tokenizer.MoveNext();
								}
							}
						}

						return function;
					}
					else if (tokenizer.Current.Text == "*")
					{
						// e.g. p.*

						TSQLMulticolumnExpression multi = new TSQLMulticolumnExpression();

						multi.Tokens.AddRange(tokens);

						multi.Tokens.Add(tokenizer.Current);

						string alias =
							String.Join(
								"",
								tokens
									.Where(t => !t.IsComment() && !t.IsWhitespace())
									.Select(t => t.Text));

						// trim off the last period
						multi.TableAlias = alias.Substring(0, alias.Length - 1);

						while (
							tokenizer.MoveNext() &&
							(
								tokenizer.Current.IsWhitespace() ||
								tokenizer.Current.IsComment())
							)
						{
							multi.Tokens.Add(tokenizer.Current);
						}

						return multi;
					}
					else if (
						tokenizer.Current.IsCharacter(TSQLCharacters.Comma) ||
						tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses) ||
						tokenizer.Current.Type == TSQLTokenType.Keyword ||
						tokenizer.Current.Type == TSQLTokenType.Operator)
					{
						TSQLColumnExpression column = new TSQLColumnExpression();

						column.Tokens.AddRange(tokens);

						string alias =
							String.Join(
								"",
								tokens
									.Where(t => !t.IsComment() && !t.IsWhitespace())
									.Select(t => t.Text));

						// trim off the last period
						if (alias.Length > 1)
						{
							column.TableAlias = alias.Substring(0, alias.Length - 1);
						}

						tokens.Reverse();

						column.Column = tokens
							.Where(t => !t.IsComment() && !t.IsWhitespace())
							.Select(t => t.Text)
							.First();

						return column;
					}
					else
					{
						tokens.Add(tokenizer.Current);
					}
				}

				return null;
			}
			else
			{
				return null;
			}
		}
	}
}
