using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Statements;

namespace Tests.Statements
{
	[TestFixture(Category = "Statement Parsing")]
	public class SelectStatementTests
	{
		[Test]
		public void SelectStatement_SelectLiteral()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;",
				includeWhitespace : true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(4, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("1", select.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(TSQLCharacters.Semicolon, select.Tokens[3].AsCharacter.Character);
		}

		[Test]
		public void SelectStatement_SelectLiteralNoWhitespace()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;",
				includeWhitespace: false);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(3, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual("1", select.Tokens[1].AsNumericLiteral.Text);
			Assert.AreEqual(TSQLCharacters.Semicolon, select.Tokens[2].AsCharacter.Character);
		}

		[Test]
		public void SelectStatement_TwoLiteralSelects()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select 1;select 2;",
				includeWhitespace: true);
			TSQLSelectStatement select1 = statements[0] as TSQLSelectStatement;
			TSQLSelectStatement select2 = statements[1] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(2, statements.Count);

			Assert.AreEqual(TSQLStatementType.Select, select1.Type);
			Assert.AreEqual(4, select1.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select1.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select1.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("1", select1.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(TSQLCharacters.Semicolon, select1.Tokens[3].AsCharacter.Character);

			Assert.AreEqual(TSQLStatementType.Select, select2.Type);
			Assert.AreEqual(4, select2.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select2.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select2.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("2", select2.Tokens[2].AsNumericLiteral.Text);
			Assert.AreEqual(TSQLCharacters.Semicolon, select2.Tokens[3].AsCharacter.Character);
		}

		[Test]
		public void SelectStatement_CorrelatedSelect()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select (select 1);",
				includeWhitespace: true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(8, statements[0].Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("(", select.Tokens[2].AsCharacter.Text);
			Assert.AreEqual("select", select.Tokens[3].AsKeyword.Text);
			Assert.AreEqual(" ", select.Tokens[4].AsWhitespace.Text);
			Assert.AreEqual("1", select.Tokens[5].AsNumericLiteral.Text);
			Assert.AreEqual(")", select.Tokens[6].AsCharacter.Text);
			Assert.AreEqual(";", select.Tokens[7].AsCharacter.Text);
		}

		[Test]
		public void SelectStatement_CommonSelect()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"select t.a, t.b, (select 1) as e
				into #tempt
				from
					[table] t
						inner join [table] t2 on
							t.id = t2.id
				where
					t.c = 5
				group by
					t.a,
					t.b
				having
					count(*) > 1
				order by
					t.a,
					t.b;",
				includeWhitespace: true);
			TSQLSelectStatement select = statements[0] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(TSQLStatementType.Select, statements[0].Type);
			Assert.AreEqual(99, select.Tokens.Count);
			Assert.AreEqual(TSQLKeywords.SELECT, select.Tokens[0].AsKeyword.Keyword);
			Assert.AreEqual(" ", select.Tokens[1].AsWhitespace.Text);
			Assert.AreEqual("t", select.Tokens[2].AsIdentifier.Name);
			Assert.AreEqual(TSQLCharacters.Period, select.Tokens[3].AsCharacter.Character);
			Assert.AreEqual(22, select.Select.Tokens.Count);
			Assert.AreEqual(4, select.Into.Tokens.Count);
			Assert.AreEqual(26, select.From.Tokens.Count);
			Assert.AreEqual(10, select.Where.Tokens.Count);
			Assert.AreEqual(13, select.GroupBy.Tokens.Count);
			Assert.AreEqual(11, select.Having.Tokens.Count);
			Assert.AreEqual(12, select.OrderBy.Tokens.Count);
		}

		[Test]
		public void SelectStatement_MultipleSelectsWithoutSemicolon()
		{
			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				"select top 1 * from dbo.ACCOUNTS select top 1 * from dbo.ACTIONS",
				includeWhitespace: true);
			TSQLSelectStatement select1 = statements[0] as TSQLSelectStatement;
			TSQLSelectStatement select2 = statements[1] as TSQLSelectStatement;

			Assert.IsNotNull(statements);
			Assert.AreEqual(2, statements.Count);

			Assert.AreEqual(TSQLStatementType.Select, select1.Type);

			Assert.AreEqual(TSQLStatementType.Select, select2.Type);
		}
	}
}
