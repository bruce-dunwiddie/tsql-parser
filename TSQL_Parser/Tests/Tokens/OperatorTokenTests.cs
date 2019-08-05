using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;
using TSQL.Tokens;

namespace Tests.Tokens
{
	public class OperatorTokenTests
	{
		[Test]
		public void Operator_MinusEquals()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("-= ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "-="),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_DivideEquals()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("/= ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "/="),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_LessThan()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("< ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "<"),
						new TSQLWhitespace(1, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_LessThanEquals()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("<= ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "<="),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_NotEqual()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("<> ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "<>"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_NotEqualExclamation()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("!= ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "!="),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_NotGreaterThan()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("!> ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "!>"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_NotLessThan()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens("!< ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "!<"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}

		[Test]
		public void Operator_Scope()
		{
			List<TSQLToken> tokens = TSQLTokenizer.ParseTokens(":: ", useQuotedIdentifiers: false, includeWhitespace: true);
			TokenComparisons.CompareTokenLists(
				new List<TSQLToken>()
					{
						new TSQLOperator(0, "::"),
						new TSQLWhitespace(2, " ")
					},
				tokens);
		}
	}
}
