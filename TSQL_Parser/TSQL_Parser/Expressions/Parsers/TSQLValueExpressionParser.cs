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
	internal class TSQLValueExpressionParser
	{
		public TSQLExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLExpression expression = ParseNext(tokenizer);

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Type.In(
					TSQLTokenType.Operator))
			{
				return new TSQLOperationExpressionParser().Parse(
					tokenizer,
					expression);
			}
			else
			{
				return expression;
			}
		}

		public TSQLExpression ParseNext(
			ITSQLTokenizer tokenizer)
		{
			if (tokenizer.Current == null)
			{
				return null;
			}

			// look at the current/first token to determine what to do

			if (tokenizer.Current.Text == "*")
			{
				TSQLMulticolumnExpression simpleMulti = new TSQLMulticolumnExpression();

				simpleMulti.Tokens.Add(tokenizer.Current);

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					simpleMulti.Tokens);

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

				// read through any whitespace so we can check specifically for a SELECT
				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					tokens);

				if (tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
				{
					#region parse subquery

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

					#endregion
				}
				else
				{
					#region parse expression contained/grouped inside parenthesis

					TSQLGroupedExpression group = new TSQLGroupedExpression();

					group.Tokens.AddRange(tokens);

					group.InnerExpression = 
						new TSQLValueExpressionParser().Parse(
							tokenizer);

					group.Tokens.AddRange(group.InnerExpression.Tokens);

					if (tokenizer.Current.IsCharacter(
						TSQLCharacters.CloseParentheses))
					{
						group.Tokens.Add(tokenizer.Current);
						tokenizer.MoveNext();
					}

					return group;

					#endregion
				}
			}
			else if (
					tokenizer.Current.IsKeyword(TSQLKeywords.DISTINCT) ||
					tokenizer.Current.IsKeyword(TSQLKeywords.ALL))
			{
				#region parse rest of expression contained inside parenthesis

				TSQLDuplicateSpecificationExpression distinct = new TSQLDuplicateSpecificationExpression();

				distinct.Tokens.Add(tokenizer.Current);

				if (tokenizer.Current.IsKeyword(TSQLKeywords.ALL))
				{
					distinct.DuplicateSpecificationType = TSQLDuplicateSpecificationExpression.TSQLDuplicateSpecificationType.All;
				}
				else
				{
					distinct.DuplicateSpecificationType = TSQLDuplicateSpecificationExpression.TSQLDuplicateSpecificationType.Distinct;
				}

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					distinct.Tokens);

				distinct.InnerExpression =
					new TSQLValueExpressionParser().Parse(
						tokenizer);

				distinct.Tokens.AddRange(distinct.InnerExpression.Tokens);

				return distinct;

				#endregion
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.Variable,
				TSQLTokenType.SystemVariable))
			{
				TSQLVariableExpression variable = new TSQLVariableExpression();
				variable.Tokens.Add(tokenizer.Current);
				variable.Variable = tokenizer.Current.AsVariable;

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					variable.Tokens);

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

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					constant.Tokens);

				return constant;
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.NULL))
			{
				TSQLNullExpression nullExp = new TSQLNullExpression();

				nullExp.Tokens.Add(tokenizer.Current);

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					nullExp.Tokens);

				return nullExp;
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

				column.Column = tokenizer.Current.AsSystemColumnIdentifier;

				column.Tokens.Add(tokenizer.Current);

				TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
					tokenizer,
					column.Tokens);

				return column;
			}
			else if (tokenizer.Current.Type.In(
				TSQLTokenType.Identifier,
				TSQLTokenType.SystemIdentifier,
				// RIGHT is a keyword, but can be used as a function call
				TSQLTokenType.Keyword))
			{
				// column, with or without alias, or with full explicit table name with up to 5 parts

				// or function, up to 4 part naming

				// find last token up to and including possible first paren
				// if *, then multi column
				// if paren, then function
				// else column

				// alias would be any tokens prior to last period, removing whitespace

				List<TSQLToken> tokens = new List<TSQLToken>();

				tokens.Add(tokenizer.Current);

				while (tokenizer.MoveNext())
				{
					if (tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
					{
						#region parse function

						TSQLFunctionExpression function = new TSQLFunctionExpression();

						function.Tokens.AddRange(tokens);
						function.Tokens.Add(tokenizer.Current);
						
						var identityTokens = tokens
							.Where(t => !t.IsComment() && !t.IsWhitespace())
							.ToList();

						function.Function =
							identityTokens[identityTokens.Count - 1];

						if (identityTokens.Count > 1)
						{
							function.QualifiedPath =
								identityTokens
									.GetRange(
										0,
										identityTokens.Count - 2);
						}

						tokenizer.MoveNext();

						TSQLArgumentList arguments = null;

						// CAST function has it's own very unique argument syntax
						if (function.Function.IsIdentifier(TSQLIdentifiers.CAST))
						{
							arguments = new TSQLValueAsTypeExpressionParser().Parse(
								tokenizer);
						}
						else
						{
							arguments = new TSQLArgumentListParser().Parse(
								tokenizer);
						}

						function.Tokens.AddRange(arguments.Tokens);

						function.Arguments = arguments;

						if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
						{
							function.Tokens.Add(tokenizer.Current);
						}

						tokenizer.MoveNext();

						TSQLTokenParserHelper.ReadCommentsAndWhitespace(
							tokenizer,
							function);

						// look for windowed aggregate
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

						#endregion
					}
					else if (tokenizer.Current.Text == "*" &&
						tokens.Last().IsCharacter(TSQLCharacters.Period))
					{
						#region parse multi column reference

						// e.g. p.*

						TSQLMulticolumnExpression multi = new TSQLMulticolumnExpression();

						multi.Tokens.AddRange(tokens);

						multi.Tokens.Add(tokenizer.Current);

						List<TSQLToken> columnReference = tokens
							.Where(t => !t.IsComment() && !t.IsWhitespace())
							.ToList();

						// p.* will have the single token p in the final list

						// AdventureWorks..ErrorLog.* will have 4 tokens in the final list
						// e.g. {AdventureWorks, ., ., ErrorLog}

						multi.TableReference = columnReference
							.GetRange(0, columnReference
								.FindLastIndex(t => t.IsCharacter(TSQLCharacters.Period)))
							.ToList();

						TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
							tokenizer,
							multi.Tokens);

						return multi;

						#endregion
					}
					else if (
						tokenizer.Current.IsCharacter(TSQLCharacters.Comma) ||
						tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses) ||
						tokenizer.Current.Type.In(
							TSQLTokenType.Keyword, 
							TSQLTokenType.Operator) ||

						// this will be a nasty check, but I don't want to copy the internal logic elsewhere

						// two identifiers in a row means that the second one is an alias
						(
							tokenizer.Current.Type.In(
								TSQLTokenType.Identifier, 
								TSQLTokenType.IncompleteIdentifier) &&
							tokens
								.Where(t => !t.IsComment() && !t.IsWhitespace())
								.LastOrDefault()
								?.Type.In(
									TSQLTokenType.Identifier,
									TSQLTokenType.BinaryLiteral,
									TSQLTokenType.MoneyLiteral,
									TSQLTokenType.NumericLiteral,
									TSQLTokenType.StringLiteral,
									TSQLTokenType.SystemColumnIdentifier,
									TSQLTokenType.SystemIdentifier,
									TSQLTokenType.SystemVariable,
									TSQLTokenType.Variable
									) == true // Operator '&&' cannot be applied to operands of type 'bool' and 'bool?'
						))
					{
						TSQLColumnExpression column = new TSQLColumnExpression();

						column.Tokens.AddRange(tokens);

						List<TSQLToken> columnReference = tokens
							.Where(t => !t.IsComment() && !t.IsWhitespace())
							.ToList();

						if (columnReference.Count > 1)
						{
							// p.ProductID will have the single token p in the final list

							// AdventureWorks..ErrorLog.ErrorLogID will have 4 tokens in the final list
							// e.g. {AdventureWorks, ., ., ErrorLog}

							column.TableReference = columnReference
								.GetRange(0, columnReference
									.FindLastIndex(t => t.IsCharacter(TSQLCharacters.Period)))
								.ToList();
						}

						column.Column = columnReference
							.Last()
							.AsIdentifier;

						return column;
					}
					else
					{
						tokens.Add(tokenizer.Current);
					}
				}

				// this is the fall through if none of the "returns" hit above

				// will also hit if we parse a simple single column expression, e.g. "SELECT blah"

				TSQLColumnExpression simpleColumn = new TSQLColumnExpression();

				simpleColumn.Tokens.AddRange(tokens);

				List<TSQLToken> simpleColumnReference = tokens
					.Where(t =>
						!t.IsComment() &&
						!t.IsWhitespace() &&
						!t.IsCharacter(TSQLCharacters.Semicolon))
					.ToList();

				if (simpleColumnReference.Count > 1)
				{
					// p.ProductID will have the single token p in the final list

					// AdventureWorks..ErrorLog.ErrorLogID will have 4 tokens in the final list
					// e.g. {AdventureWorks, ., ., ErrorLog}

					simpleColumn.TableReference = simpleColumnReference
						.GetRange(0, simpleColumnReference
							.FindLastIndex(t => t.IsCharacter(TSQLCharacters.Period)))
						.ToList();
				}

				simpleColumn.Column = simpleColumnReference
					.Last()
					.AsIdentifier;

				return simpleColumn;
			}
			else
			{
				return null;
			}
		}
	}
}
