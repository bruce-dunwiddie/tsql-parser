using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Expressions.Parsers
{
	internal class TSQLArgumentListParser
	{
		public TSQLArgumentList Parse(ITSQLTokenizer tokenizer)
		{
			List<TSQLExpression> arguments = new List<TSQLExpression>();

			TSQLValueExpressionParser factory = new TSQLValueExpressionParser();

			List<TSQLToken> tokens = new List<TSQLToken>();

			// need to do this before starting the argument loop
			// so we can handle an empty argument list of just whitespace
			// and comments
			while (
				tokenizer.Current != null &&
				(
					tokenizer.Current.IsComment() ||
					tokenizer.Current.IsWhitespace()
				))
			{
				tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}

			while (
				tokenizer.Current != null &&
				!tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
			{
				while (
					tokenizer.Current != null &&
					(
						tokenizer.Current.IsComment() ||
						tokenizer.Current.IsWhitespace()
					))
				{
					tokens.Add(tokenizer.Current);

					tokenizer.MoveNext();
				}
				
				TSQLExpression argument = factory.Parse(tokenizer);

				tokens.AddRange(argument.Tokens);

				arguments.Add(argument);

				if (tokenizer.Current.IsCharacter(TSQLCharacters.Comma))
				{
					tokens.Add(tokenizer.Current);
				}

				if (
					tokenizer.Current != null &&
					!tokenizer.Current.IsCharacter(TSQLCharacters.CloseParentheses))
				{
					tokenizer.MoveNext();
				}
			}

			TSQLArgumentList argList = new TSQLArgumentList(
				arguments);

			argList.Tokens.AddRange(tokens);

			return argList;
		}
	}
}
