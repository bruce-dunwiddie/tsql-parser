using System;

using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLStatementParserFactory
	{
		public ITSQLStatementParser Create(ITSQLTokenizer tokenizer)
		{
			if (tokenizer.Current.IsKeyword(TSQLKeywords.SELECT) ||
				// e.g. (SELECT 1)
				tokenizer.Current.IsCharacter(TSQLCharacters.OpenParentheses))
			{
				return new TSQLSelectStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.WITH))
			{
				// this parser will parse the CTE's from the WITH clause and
				// then return the correct statement parser, e.g. SELECT, UPDATE, etc
				return new TSQLWithClauseStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.MERGE))
			{
				return new TSQLMergeStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.UPDATE))
			{
				return new TSQLUpdateStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.DELETE))
			{
				return new TSQLDeleteStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.INSERT))
			{
				return new TSQLInsertStatementParser(tokenizer);
			}
			else if (tokenizer.Current.IsKeyword(TSQLKeywords.EXECUTE) ||
				tokenizer.Current.Type == TSQLTokenType.Identifier)
			{
				// TODO: create split for EXECUTE AS
				return new TSQLExecuteStatementParser(tokenizer);
			}
			else
			{
				// TODO: add CREATE, ALTER, DROP
				return new TSQLUnknownStatementParser(tokenizer);
			}
		}
	}
}
