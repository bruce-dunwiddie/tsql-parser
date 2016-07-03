using System;

namespace TSQL.Tokens
{
	public enum TSQLTokenType
	{
		Character,
		Identifier,
		Keyword,
		MultilineComment,
		NumericLiteral,
		Operator,
		SingleLineComment,
		StringLiteral,
		Whitespace,
		Variable
	}
}
