using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL.Tokens;

namespace Tests.Tokens
{
	[TestFixture(Category = "Token Equality")]
	public class TokenEqualityTests
	{
		[Test]
		public void TokenEquality_SameToken()
		{
			TSQLKeyword token = new TSQLKeyword(0, "GO");
			Assert.IsTrue(token.Equals(token));
		}

		[Test]
		public void TokenEquality_SameValue()
		{
			TSQLKeyword token1 = new TSQLKeyword(0, "GO");
			TSQLKeyword token2 = new TSQLKeyword(0, "GO");
			Assert.IsTrue(token1.Equals(token2));
		}

		[Test]
		public void TokenEquality_SameValueAsObject()
		{
			TSQLKeyword token1 = new TSQLKeyword(0, "GO");
			Object token2 = new TSQLKeyword(0, "GO");
			Assert.IsTrue(token1.Equals(token2));
		}

		[Test]
		public void TokenEquality_EqualsOperator()
		{
			TSQLKeyword token1 = new TSQLKeyword(0, "GO");
			TSQLKeyword token2 = new TSQLKeyword(0, "GO");
			Assert.IsTrue(token1 == token2);
		}

		[Test]
		public void TokenEquality_EqualsOperatorNull()
		{
			TSQLKeyword token = new TSQLKeyword(0, "GO");
			Assert.IsFalse(null == token);
		}

		[Test]
		public void TokenEquality_NotEqualOperator()
		{
			TSQLKeyword token1 = new TSQLKeyword(0, "GO");
			TSQLKeyword token2 = new TSQLKeyword(0, "USE");
			Assert.IsTrue(token1 != token2);
		}

		[Test]
		public void TokenEquality_NotEqualOperatorNull()
		{
			TSQLKeyword token = new TSQLKeyword(0, "GO");
			Assert.IsTrue(null != token);
		}

		[Test]
		public void TokenEquality_Hashcode()
		{
			Dictionary<TSQLToken, string> lookup = new Dictionary<TSQLToken, string>();
			lookup[new TSQLKeyword(0, "GO")] = "some value";
			Assert.AreEqual("some value", lookup[new TSQLKeyword(0, "GO")]);
		}

		[Test]
		public void TokenEquality_DifferentHashcode()
		{
			Dictionary<TSQLToken, string> lookup = new Dictionary<TSQLToken, string>();
			lookup[new TSQLKeyword(0, "GO")] = "some value";
			Assert.IsFalse(lookup.ContainsKey(new TSQLKeyword(0, "USE")));
		}

		[Test]
		public void TokenEquality_NullTextException()
		{
			Assert.Throws<ArgumentNullException>(() => new TSQLKeyword(0, null));
		}
	}
}
