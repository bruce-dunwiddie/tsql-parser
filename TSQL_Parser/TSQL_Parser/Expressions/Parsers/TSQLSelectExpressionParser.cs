using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLSelectExpressionParser
	{
		public TSQLExpression Parse(ITSQLTokenizer tokenizer)
		{
			TSQLExpression expression = ParseNext(tokenizer);

			if (
				tokenizer.Current != null &&
				tokenizer.Current.Text != "*" &&
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
					return new TSQLOperatorExpressionParser().Parse(
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
							identityTokens[identityTokens.Count - 1]
								.AsIdentifier;

						if (identityTokens.Count > 1)
						{
							function.QualifiedPath =
								identityTokens
									.GetRange(
										0,
										identityTokens.Count - 2);
						}

						tokenizer.MoveNext();

						TSQLArgumentList arguments = new TSQLArgumentListParser().Parse(
							tokenizer);

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
					else if (tokenizer.Current.Text == "*")
					{
						#region parse multi column reference

						// e.g. p.*

						TSQLMulticolumnExpression multi = new TSQLMulticolumnExpression();

						multi.Tokens.AddRange(tokens);

						multi.Tokens.Add(tokenizer.Current);

						List<TSQLToken> columnReference = tokens
							.Where(t => !t.IsComment() && !t.IsWhitespace())
							.ToList();

						if (columnReference.Count > 0)
						{
							// p.* will have the single token p in the final list

							// AdventureWorks..ErrorLog.* will have 4 tokens in the final list
							// e.g. {AdventureWorks, ., ., ErrorLog}

							multi.TableReference = columnReference
								.GetRange(0, columnReference
									.FindLastIndex(t => t.IsCharacter(TSQLCharacters.Period)))
								.ToList();
						}

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

				return null;
			}
			else
			{
				return new TSQLValueExpressionParser().ParseNext(
					tokenizer);
			}
		}
	}
}
