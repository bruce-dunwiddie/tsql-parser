using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
