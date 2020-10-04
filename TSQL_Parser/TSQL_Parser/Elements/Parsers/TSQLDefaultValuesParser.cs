using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Tokens;

namespace TSQL.Elements.Parsers
{
	internal class TSQLDefaultValuesParser
	{
		public TSQLDefaultValues Parse(ITSQLTokenizer tokenizer)
		{
			TSQLDefaultValues defaultValues = new TSQLDefaultValues();

			if (!tokenizer.Current.IsKeyword(TSQLKeywords.DEFAULT))
			{
				throw new InvalidOperationException("DEFAULT expected.");
			}

			defaultValues.Tokens.Add(tokenizer.Current);

			if (tokenizer.MoveNext() &&
				tokenizer.Current.IsKeyword(TSQLKeywords.VALUES))
			{
				defaultValues.Tokens.Add(tokenizer.Current);

				tokenizer.MoveNext();
			}			

			return defaultValues;
		}
	}
}
