using System;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser.Tests
{
    /// <summary>
    /// Static helper class with enhanced assertions, for use within NUnit tests.
    /// </summary>
    [TestFixture]
    public sealed class TestHelper
    {
        public static void AssertEqualArrays(object[] array1, object[] array2)
        {
            Assertion.AssertEquals("same number of elements", array1.Length, array2.Length);
            Assertion.AssertEquals("same type", array1.GetType(), array2.GetType());
            
            if (array1.Length == 0)
                return;

            for (var arrayIndex = 0; arrayIndex < array1.Length; arrayIndex++)
                Assertion.AssertEquals(array1[arrayIndex], array2[arrayIndex]);
        }

        #region Tests

        [Test]
        public void AssertEqualArrays()
        {
            AssertEqualArrays(new[] {"A", "B", "C"}, new[] {"A", "B", "C"});
        }

        [Test]
        public void AssertEqualArrays_SameTypedZeroLengthArrays()
        {
            AssertEqualArrays(new string[] {}, new string[] {});
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AssertEqualArrays_DifferentlyTypedZeroLengthArrays()
        {
            AssertEqualArrays(new string[] {}, new Random[] {});
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AssertEqualArrays_DifferentNumberOfElements()
        {
            AssertEqualArrays(new[] {"A", "B", "C"}, new[] {"A", "B"});
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void AssertEqualArrays_DifferentElements()
        {
            AssertEqualArrays(new[] {"A", "B", "C"}, new[] {"D", "E", "F"});
        }

        #endregion
    }
}
