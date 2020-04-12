using System;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLStatementParserFactory
	{
		public ITSQLStatementParser Create(ITSQLTokenizer tokenizer)
		{
			TSQLKeywords keyword = TSQLKeywords.None;
			TSQLTokenType type = tokenizer.Current.Type;

			if (type == TSQLTokenType.Keyword)
			{
				keyword = tokenizer.Current.AsKeyword.Keyword;
			}

			if (keyword == TSQLKeywords.SELECT)
			{
				return new TSQLSelectStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.WITH)
			{
				// this parser will parse the CTE's from the WITH clause and
				// then return the correct statement parser, e.g. SELECT, UPDATE, etc
				return new TSQLWithClauseStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.MERGE)
			{
				return new TSQLMergeStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.UPDATE)
			{
				return new TSQLUpdateStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.DELETE)
			{
				return new TSQLDeleteStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.INSERT)
			{
				return new TSQLInsertStatementParser(tokenizer);
			}
			else if (keyword == TSQLKeywords.EXECUTE ||
				type == TSQLTokenType.Identifier)
			{
				// TODO: create split for EXECUTE AS
				return new TSQLExecuteStatementParser(tokenizer);
			}
			else
			{
				return new TSQLUnknownStatementParser(tokenizer);
			}
		}
	}
}
