using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

namespace Tests
{
	[TestFixture]
	public class Parsing
	{
		[Test]
		public void Parse_SimpleBracketedIdentifier()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a]");
			CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]")
					},
				tokens);
		}

		[Test]
		public void Parse_EscapedBracketedIdentifier1()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a]]]");
			CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]]]")
					},
				tokens);
		}

		[Test]
		public void Parse_EscapedBracketedIdentifier2()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("[a]]a]");
			CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLIdentifier(0, "[a]]a]")
					},
				tokens);
		}

		[Test]
		public void Parse_GO()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("GO");
			CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLKeyword(0, "GO")
					},
				tokens);
		}

		[Test]
		public void Parse_GOFromStream()
		{
			using (TextReader reader = new StringReader("GO"))
			using (TSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				CompareStreamToList(
					new List<TSQLToken>()
						{
							new TSQLKeyword(0, "GO")
						},
						tokenizer);
			}
		}

		[Test]
		public void Parse_uspSearchCandidateResumes()
		{
			using (StreamReader reader = new StreamReader("./Scripts/AdventureWorks2014.dbo.uspSearchCandidateResumes.sql"))
			using (TSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				CompareStreamStartToList(
					new List<TSQLToken>()
						{
							new TSQLKeyword(0, "USE"),
							new TSQLIdentifier(4, "[AdventureWorks2014]"),
							new TSQLKeyword(26, "GO")
						},
						tokenizer);
			}
		}

		private void CompareStreamStartToList(List<TSQLToken> expected, TSQLTokenizer tokenizer)
		{
			CompareTokenListStart(expected, tokenizer.ToList());
		}

		private void CompareTokenListStart(List<TSQLToken> expected, List<TSQLToken> actual)
		{
			Assert.AreEqual(expected == null, actual == null);
			if (expected != null && actual != null)
			{
				Assert.IsTrue(actual.Count >= expected.Count);
				for (int index = 0; index < expected.Count; index++)
				{
					Assert.AreEqual(expected[index], actual[index]);
				}
			}
		}

		private void CompareStreamToList(List<TSQLToken> expected, TSQLTokenizer tokenizer)
		{
			CompareTokenLists(expected, tokenizer.ToList());
		}

		private void CompareTokenLists(List<TSQLToken> expected, List<TSQLToken> actual)
		{
			Assert.AreEqual(expected == null, actual == null);
			if (expected != null && actual != null)
			{
				Assert.AreEqual(expected.Count, actual.Count);
				for (int index = 0; index < expected.Count; index++)
				{
					Assert.AreEqual(expected[index], actual[index]);
				}
			}
		}
	}
}
