using System;

namespace TSQL.Tokens
{
	public enum TokenType
	{
		Character,
		Identifier,
		Keyword,
		MultilineComment,
		NumericLiteral,
		Operator,
		SingleLineComment,
		StringLiteral,
		Whitespace
	}
}
