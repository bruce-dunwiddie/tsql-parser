using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;

namespace Tests
{
	public class VariablesTests
	{
		[Test]
		public void Variables_InTrue()
		{
			Assert.IsTrue(TSQLVariables.IDENTITY.In(
				TSQLVariables.ERROR,
				TSQLVariables.IDENTITY));
		}

		[Test]
		public void Variables_InFalse()
		{
			Assert.IsFalse(TSQLVariables.IDENTITY.In(
				TSQLVariables.ERROR));
		}

		[Test]
		public void Variables_InNull()
		{
			Assert.IsFalse(TSQLVariables.IDENTITY.In());
		}

		[Test]
		public void Variables_IsTrue()
		{
			Assert.IsTrue(TSQLVariables.IsVariable("@@identity"));
		}

		[Test]
		public void Variables_IsFalse()
		{
			Assert.IsFalse(TSQLVariables.IsVariable("blah"));
		}

		[Test]
		public void Variables_IsNull()
		{
			Assert.IsFalse(TSQLVariables.IsVariable(null));
		}

		[Test]
		public void Variables_ParseNull()
		{
			Assert.AreEqual(TSQLVariables.None, TSQLVariables.Parse(null));
		}

		[Test]
		public void Variables_EqualsObjectTrue()
		{
			Assert.IsTrue(TSQLVariables.IDENTITY.Equals((object)TSQLVariables.IDENTITY));
		}

		[Test]
		public void Variables_EqualsObjectFalse()
		{
			Assert.IsFalse(TSQLVariables.IDENTITY.Equals((object)TSQLVariables.ERROR));
		}

		[Test]
		public void Variables_GetHashCode()
		{
			Dictionary<TSQLVariables, string> lookupTest = new Dictionary<TSQLVariables, string>();

			lookupTest[TSQLVariables.IDENTITY] = "@@identity";

			// this should call GetHashCode
			Assert.AreEqual("@@identity", lookupTest[TSQLVariables.IDENTITY]);
		}
	}
}
