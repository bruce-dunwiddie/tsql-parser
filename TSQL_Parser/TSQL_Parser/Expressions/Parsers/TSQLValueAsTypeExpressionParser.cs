using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;
using TSQL.Tokens.Parsers;

namespace TSQL.Expressions.Parsers
{
	/// <summary>
	///		Used to parse the unique argument format of the CAST function.
	/// </summary>
	internal class TSQLValueAsTypeExpressionParser
	{
		public TSQLArgumentList Parse(ITSQLTokenizer tokenizer)
		{
			List<TSQLToken> tokens = new List<TSQLToken>();

			// need to do this before starting the argument loop
			// so we can handle an empty argument list of just whitespace
			// and comments
			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				tokens);

			TSQLValueAsTypeExpression argument = new TSQLValueAsTypeExpression();

			TSQLExpression expression = new TSQLValueExpressionParser().Parse(tokenizer);

			argument.Expression = expression;

			tokens.AddRange(expression.Tokens);

			TSQLTokenParserHelper.ReadCommentsAndWhitespace(
				tokenizer,
				tokens);

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.AS))
			{
				throw new InvalidOperationException("AS expected.");
			}

			tokens.Add(tokenizer.Current);

			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
				tokenizer,
				tokens);

			argument.DataType = tokenizer.Current.AsIdentifier;

			tokens.Add(tokenizer.Current);

			// reading until closing paren
			TSQLTokenParserHelper.ReadThroughAnyCommentsOrWhitespace(
				tokenizer,
				tokens);

			TSQLArgumentList argList = new TSQLArgumentList(
				new List<TSQLExpression> { argument });

			argList.Tokens.AddRange(tokens);

			return argList;
		}
	}
}
