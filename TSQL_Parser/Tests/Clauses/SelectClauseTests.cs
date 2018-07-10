using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Clauses;
using TSQL.Clauses.Parsers;
using TSQL.Tokens;

namespace Tests.Clauses
{
	[TestFixture(Category = "Clause Parsing")]
	public class SelectClauseTests
	{
		[Test]
		public void SelectClause_StopAtFrom()
		{
			using (StringReader reader = new StringReader(@"select a from b;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());
				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(2, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);
			}
		}

		[Test]
		public void SelectClause_Comments()
		{
			using (StringReader reader = new StringReader(
				@"select top 1
					oh.TaxAmt / oh.SubTotal /* tax percent */
				from
					Sales.SalesOrderHeader oh;"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());
				TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
				Assert.AreEqual(11, select.Tokens.Count);
				Assert.AreEqual(TSQLKeywords.FROM, tokenizer.Current.AsKeyword.Keyword);
			}
		}

		[Test]
		public void SelectClause_SanityCheck()
		{
			using (StringReader reader = new StringReader(
				@"bogus"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());
				Exception ex = Assert.Throws<InvalidOperationException>(
					delegate
					{
						TSQLSelectClause select = new TSQLSelectClauseParser().Parse(tokenizer);
					});
			}
		}
	}
}
