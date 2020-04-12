using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Tests
{
	public static class TestHelpers
	{
        public static void CompareArrays<T>(T[] expected, T[] actual)
        {
            Assert.AreEqual(
                expected == null, 
                actual == null, 
                "Expected {0}null value and {1}null found.", 
                expected == null ? "" : "not", 
                actual == null ? "" : "not");

            if (expected == null || actual == null)
                return;

            Assert.AreEqual(
                expected.LongLength, 
                actual.LongLength, 
                "Expected Length is {0} actual: {1}", 
                expected.LongLength, 
                actual.LongLength);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(
                    expected[i], 
                    actual[i], 
                    "Values on index {0} are not equal. Expected {1} actual: {2}", 
                    i, 
                    expected[i], 
                    actual[i]);
            }
        }
    }
}
