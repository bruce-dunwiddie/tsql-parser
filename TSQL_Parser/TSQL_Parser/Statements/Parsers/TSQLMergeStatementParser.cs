using System.Collections.Generic;
using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLMergeStatementParser : ITSQLStatementParser
	{
		public TSQLMergeStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLMergeStatementParser(TSQLWithClause with, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.With = with;

			Statement.Tokens.AddRange(with.Tokens);
		}

		private TSQLMergeStatement Statement { get; } = new TSQLMergeStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLMergeStatement Parse()
		{
			TSQLMergeClause mergeClause = new TSQLMergeClauseParser().Parse(Tokenizer);

			Statement.Merge = mergeClause;

			Statement.Tokens.AddRange(mergeClause.Tokens);

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.INTO))
			{
				TSQLIntoClause intoClause = new TSQLMergeIntoClauseParser().Parse(Tokenizer);

				Statement.Into = intoClause;

				Statement.Tokens.AddRange(intoClause.Tokens);
			}

			if (Tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.USING))
			{
				TSQLUsingClause usingClause = new TSQLUsingClauseParser().Parse(Tokenizer);

				Statement.Using = usingClause;

				Statement.Tokens.AddRange(usingClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.ON))
			{
				TSQLOnClause onClause = new TSQLOnClauseParser().Parse(Tokenizer);

				Statement.On = onClause;

				Statement.Tokens.AddRange(onClause.Tokens);
			}

			while (Tokenizer.Current.IsKeyword(TSQLKeywords.WHEN))
			{
				TSQLWhenClause whenClause = new TSQLWhenClauseParser().Parse(Tokenizer);

				Statement.When.Add(whenClause);

				Statement.Tokens.AddRange(whenClause.Tokens);
			}

			if (Tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				TSQLOutputClause outputClause = new TSQLOutputClauseParser().Parse(Tokenizer);

				Statement.Output = outputClause;

				Statement.Tokens.AddRange(outputClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.OPTION))
			{
				TSQLOptionClause optionClause = new TSQLOptionClauseParser().Parse(Tokenizer);

				Statement.Option = optionClause;

				Statement.Tokens.AddRange(optionClause.Tokens);
			}

			if (
				Tokenizer.Current?.AsKeyword != null &&
				Tokenizer.Current.AsKeyword.Keyword.IsStatementStart())
			{
				Tokenizer.Putback();
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}