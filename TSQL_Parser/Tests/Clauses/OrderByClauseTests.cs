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
using TSQL.Statements;

namespace Tests.Clauses
{
	[TestFixture(Category = "Clause Parsing")]
	public class OrderByClauseTests
	{
		[Test]
		public void OrderByClause_SanityCheck()
		{
			using (StringReader reader = new StringReader(
				@"bogus"))
			using (ITSQLTokenizer tokenizer = new TSQLTokenizer(reader))
			{
				Assert.IsTrue(tokenizer.MoveNext());
				Exception ex = Assert.Throws<InvalidOperationException>(
					delegate
					{
						TSQLOrderByClause orderBy = new TSQLOrderByClauseParser().Parse(tokenizer);
					});
			}
		}

		[Test]
		public void OrderByClause_OffsetFetch()
		{
			// regression test for https://github.com/bruce-dunwiddie/tsql-parser/issues/75

			List<TSQLStatement> statements = TSQLStatementReader.ParseStatements(
				@"
				SELECT * 
				FROM Product.Product P 
				ORDER BY P.ProductId 
				OFFSET (@page -1) * @RowPerPage ROWS 
				FETCH NEXT @RowPerPage ROWS ONLY",
				includeWhitespace: false);

			TSQLSelectStatement select = statements[0].AsSelect;

			Assert.AreEqual(1, statements.Count);
			Assert.AreEqual(26, select.Tokens.Count);
		}
	}
}
