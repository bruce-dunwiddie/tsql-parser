using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLWithClauseStatementParser : ITSQLStatementParser
	{
		public TSQLWithClauseStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLStatement Parse()
		{
			TSQLWithClause with = new TSQLWithClauseParser().Parse(Tokenizer);

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.SELECT) ||
				Tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
			{
				return new TSQLSelectStatementParser(with, Tokenizer).Parse();
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.MERGE))
			{
				return new TSQLMergeStatementParser(with, Tokenizer).Parse();
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.UPDATE))
			{
				return new TSQLUpdateStatementParser(with, Tokenizer).Parse();
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.DELETE))
			{
				return new TSQLDeleteStatementParser(with, Tokenizer).Parse();
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.INSERT))
			{
				return new TSQLInsertStatementParser(with, Tokenizer).Parse();
			}
			else
			{
				// TSQLUnknownStatement doesn't have a With property
				return new TSQLUnknownStatementParser(with.Tokens, Tokenizer).Parse();
			}
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
