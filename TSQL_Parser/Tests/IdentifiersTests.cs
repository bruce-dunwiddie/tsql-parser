using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;

namespace Tests
{
	public class IdentifiersTests
	{
		[Test]
		public void Identifiers_InTrue()
		{
			Assert.IsTrue(TSQLIdentifiers.CONVERT.In(
				TSQLIdentifiers.CONVERT,
				TSQLIdentifiers.COALESCE));
		}

		[Test]
		public void Identifiers_InFalse()
		{
			Assert.IsFalse(TSQLIdentifiers.CONVERT.In(
				TSQLIdentifiers.COALESCE));
		}

		[Test]
		public void Identifiers_InNull()
		{
			Assert.IsFalse(TSQLIdentifiers.CONVERT.In());
		}

		[Test]
		public void Identifiers_IsTrue()
		{
			Assert.IsTrue(TSQLIdentifiers.IsIdentifier("convert"));
		}

		[Test]
		public void Identifiers_IsFalse()
		{
			Assert.IsFalse(TSQLIdentifiers.IsIdentifier("blah"));
		}

		[Test]
		public void Identifiers_IsNull()
		{
			Assert.IsFalse(TSQLIdentifiers.IsIdentifier(null));
		}

		[Test]
		public void Identifiers_ParseNull()
		{
			Assert.AreEqual(TSQLIdentifiers.None, TSQLIdentifiers.Parse(null));
		}

		[Test]
		public void Identifiers_EqualsObjectTrue()
		{
			Assert.IsTrue(TSQLIdentifiers.CONVERT.Equals((object)TSQLIdentifiers.CONVERT));
		}

		[Test]
		public void Identifiers_EqualsObjectFalse()
		{
			Assert.IsFalse(TSQLIdentifiers.CONVERT.Equals((object)TSQLIdentifiers.COALESCE));
		}

		[Test]
		public void Identifiers_GetHashCode()
		{
			Dictionary<TSQLIdentifiers, string> lookupTest = new Dictionary<TSQLIdentifiers, string>();

			lookupTest[TSQLIdentifiers.CONVERT] = "convert";

			// this should call GetHashCode
			Assert.AreEqual("convert", lookupTest[TSQLIdentifiers.CONVERT]);
		}
	}
}
