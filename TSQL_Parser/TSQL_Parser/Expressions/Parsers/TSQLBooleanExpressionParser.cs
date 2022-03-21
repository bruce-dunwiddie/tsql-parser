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
	//internal class TSQLBooleanExpressionParser
	//{
	//	public TSQLExpression Parse(ITSQLTokenizer tokenizer)
	//	{
	//		TSQLExpression expression = ParseNext(tokenizer);

	//		// TODO: add handling for logical operators, e.g. AND/OR.
	//		// new expression type?

	//		// TODO: add handling for logical subquery operators, e.g. EXISTS.

	//		// TODO: add handling for special keyword operators, e.g. BETWEEN and LIKE.

	//		if (
	//			tokenizer.Current != null &&
	//			tokenizer.Current.Type.In(
	//				TSQLTokenType.Operator))
	//		{
	//			return new TSQLOperatorExpressionParser().Parse(
	//				tokenizer,
	//				expression);
	//		}
	//		else
	//		{
	//			return expression;
	//		}
	//	}

	//	private static TSQLExpression ParseNext(
	//		ITSQLTokenizer tokenizer)
	//	{
	//		if (tokenizer.Current == null)
	//		{
	//			return null;
	//		}

	//		// look at the current/first token to determine what to do

	//		if (tokenizer.Current.IsCharacter(
	//			TSQLCharacters.OpenParentheses))
	//		{
	//			List<TSQLToken> tokens = new List<TSQLToken>();

	//			tokens.Add(tokenizer.Current);

	//			// read through any whitespace so we can check specifically for a SELECT
	//			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
	//				tokenizer,
	//				tokens);

	//			if (tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
	//			{
	//				#region parse subquery

	//				TSQLSubqueryExpression subquery = new TSQLSubqueryExpression();

	//				subquery.Tokens.AddRange(tokens);

	//				TSQLSelectStatement select = new TSQLSelectStatementParser(tokenizer).Parse();

	//				subquery.Select = select;

	//				subquery.Tokens.AddRange(select.Tokens);

	//				if (tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
	//				{
	//					subquery.Tokens.Add(tokenizer.Current);

	//					tokenizer.MoveNext();
	//				}

	//				return subquery;

	//				#endregion
	//			}
	//			else
	//			{
	//				#region parse expression contained/grouped inside parenthesis

	//				TSQLGroupedExpression group = new TSQLGroupedExpression();

	//				group.Tokens.AddRange(tokens);

	//				group.InnerExpression = 
	//					new TSQLBooleanExpressionParser().Parse(
	//						tokenizer);
	//				group.Tokens.AddRange(group.InnerExpression.Tokens);

	//				if (tokenizer.Current.IsCharacter(
	//					TSQLCharacters.CloseParentheses))
	//				{
	//					group.Tokens.Add(tokenizer.Current);
	//					tokenizer.MoveNext();
	//				}

	//				return group;

	//				#endregion
	//			}
	//		}
	//		else
	//		{
	//			return new TSQLValueExpressionParser().ParseNext(tokenizer);
	//		}
	//	}
	//}
}
