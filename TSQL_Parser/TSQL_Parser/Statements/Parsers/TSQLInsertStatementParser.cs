using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Elements;
using TSQL.Elements.Parsers;
using TSQL.Tokens;

namespace TSQL.Statements.Parsers
{
	internal class TSQLInsertStatementParser : ITSQLStatementParser
	{
		public TSQLInsertStatementParser(ITSQLTokenizer tokenizer)
		{
			Tokenizer = tokenizer;
		}

		public TSQLInsertStatementParser(TSQLWithClause with, ITSQLTokenizer tokenizer) :
			this(tokenizer)
		{
			Statement.With = with;

			Statement.Tokens.AddRange(with.Tokens);
		}

		private TSQLInsertStatement Statement { get; } = new TSQLInsertStatement();

		private ITSQLTokenizer Tokenizer { get; set; }

		public TSQLInsertStatement Parse()
		{
			TSQLInsertClause insertClause = new TSQLInsertClauseParser().Parse(Tokenizer);

			Statement.Insert = insertClause;

			Statement.Tokens.AddRange(insertClause.Tokens);

			if (Tokenizer.Current.IsFutureKeyword(TSQLFutureKeywords.OUTPUT))
			{
				TSQLOutputClause outputClause = new TSQLOutputClauseParser().Parse(Tokenizer);

				Statement.Output = outputClause;

				Statement.Tokens.AddRange(outputClause.Tokens);
			}

			if (Tokenizer.Current.IsKeyword(TSQLKeywords.VALUES))
			{
				TSQLValues values = new TSQLValuesParser().Parse(Tokenizer);

				Statement.Values = values;

				Statement.Tokens.AddRange(values.Tokens);
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.SELECT))
			{
				TSQLSelectStatement select = new TSQLSelectStatementParser(Tokenizer).Parse();

				Statement.Select = select;

				Statement.Tokens.AddRange(select.Tokens);
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.DEFAULT))
			{
				TSQLDefaultValues defaultValues = new TSQLDefaultValuesParser().Parse(Tokenizer);

				Statement.Default = defaultValues;

				Statement.Tokens.AddRange(defaultValues.Tokens);
			}
			else if (Tokenizer.Current.IsKeyword(TSQLKeywords.EXECUTE))
			{
				TSQLExecuteStatement exec = new TSQLExecuteStatementParser(Tokenizer).Parse();

				Statement.Execute = exec;

				Statement.Tokens.AddRange(exec.Tokens);
			}

			return Statement;
		}

		TSQLStatement ITSQLStatementParser.Parse()
		{
			return Parse();
		}
	}
}
