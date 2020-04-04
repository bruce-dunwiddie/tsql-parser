namespace TSQL.Statements.Parsers
{
	internal class TSQLStatementParserFactory
	{
		public ITSQLStatementParser Create(ITSQLTokenizer tokenizer)
		{
			if (tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.SELECT)
			{
				return new TSQLSelectStatementParser(tokenizer);
			}
			else if (tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.WITH)
			{
				// this parser will parse the CTE's from the WITH clause and
				// then return the correct statement parser, e.g. SELECT, UPDATE, etc
				return new TSQLWithClauseStatementParser(tokenizer);
			}
			else if (tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.MERGE)
			{
				return new TSQLMergeStatementParser(tokenizer);
			}
			else if (tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.UPDATE)
			{
				return new TSQLUpdateStatementParser(tokenizer);
			}
			else if (tokenizer.Current.AsKeyword?.Keyword == TSQLKeywords.DELETE)
			{
				return new TSQLDeleteStatementParser(tokenizer);
			}
			else
			{
				return new TSQLUnknownStatementParser(tokenizer);

				// TODO: check for an EXEC without the keyword
				// tokenizer.Current.Type == Tokens.TSQLTokenType.Identifier?
			}
		}
	}
}
