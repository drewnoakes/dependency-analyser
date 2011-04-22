using System;

using NUnit.Framework;

namespace Drew.DependencyPlotter.Tests
{
	/// <summary>
	/// Static helper class with enhanced assertions, for use within NUnit tests.
	/// </summary>
	[TestFixture]
	public class TestHelper
	{
		public TestHelper()
		{
		}

		public static void AssertEqualArrays(object[] array1, object[] array2)
		{
			Assertion.AssertEquals("same number of elements", array1.Length, array2.Length);
			Assertion.AssertEquals("same type", array1.GetType(), array2.GetType());
			if (array1.Length==0) 
			{
				return;
			}
			for (int arrayIndex=0; arrayIndex<array1.Length; arrayIndex++)
			{
				Assertion.AssertEquals(array1[arrayIndex], array2[arrayIndex]);
			}
		}

		#region Tests

		[Test]
		public void testAssertEqualArrays()
		{
			AssertEqualArrays(new string[] {"A", "B", "C"}, new string[] {"A", "B", "C"});
		}

		[Test]
		public void testAssertEqualArrays_SameTypedZeroLengthArrays()
		{
			AssertEqualArrays(new string[] {}, new string[] {});
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void testAssertEqualArrays_DifferentlyTypedZeroLengthArrays()
		{
			AssertEqualArrays(new string[] {}, new Random[] {});
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void testAssertEqualArrays_DifferentNumberOfElements()
		{
			AssertEqualArrays(new string[] {"A", "B", "C"}, new string[] {"A", "B"});
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void testAssertEqualArrays_DifferentElements()
		{
			AssertEqualArrays(new string[] {"A", "B", "C"}, new string[] {"D", "E", "F"});
		}

		#endregion
	}
}
