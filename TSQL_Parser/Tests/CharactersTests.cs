using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TSQL;

namespace Tests
{
	public class CharactersTests
	{
		[Test]
		public void Characters_InTrue()
		{
			Assert.IsTrue(TSQLCharacters.Comma.In(
				TSQLCharacters.Comma,
				TSQLCharacters.Period));
		}

		[Test]
		public void Characters_InFalse()
		{
			Assert.IsFalse(TSQLCharacters.Comma.In(
				TSQLCharacters.Period));
		}

		[Test]
		public void Characters_InNull()
		{
			Assert.IsFalse(TSQLCharacters.Comma.In());
		}

		[Test]
		public void Characters_IsTrue()
		{
			Assert.IsTrue(TSQLCharacters.IsCharacter(","));
		}

		[Test]
		public void Characters_IsFalse()
		{
			Assert.IsFalse(TSQLCharacters.IsCharacter("a"));
		}

		[Test]
		public void Characters_IsNull()
		{
			Assert.IsFalse(TSQLCharacters.IsCharacter(null));
		}

		[Test]
		public void Characters_ParseNull()
		{
			Assert.AreEqual(TSQLCharacters.None, TSQLCharacters.Parse(null));
		}

		[Test]
		public void Characters_EqualsObjectTrue()
		{
			Assert.IsTrue(TSQLCharacters.Comma.Equals((object)TSQLCharacters.Comma));
		}

		[Test]
		public void Characters_EqualsObjectFalse()
		{
			Assert.IsFalse(TSQLCharacters.Comma.Equals((object)TSQLCharacters.Period));
		}

		[Test]
		public void Characters_GetHashCode()
		{
			Dictionary<TSQLCharacters, string> lookupTest = new Dictionary<TSQLCharacters, string>();

			lookupTest[TSQLCharacters.Comma] = ",";

			// this should call GetHashCode
			Assert.AreEqual(",", lookupTest[TSQLCharacters.Comma]);
		}
	}
}
