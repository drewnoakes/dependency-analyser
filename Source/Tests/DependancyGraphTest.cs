using System;

using NUnit.Framework;

namespace Drew.DependencyPlotter.Tests
{
	/// <summary>
	/// Summary description for DependancyGraphTest.
	/// </summary>
	[TestFixture]
	public class DependancyGraphTest
	{
		[Test]
		public void testAddDependancy()
		{
			DependancyGraph graph = new DependancyGraph();
			graph.AddDependancy("A", "B");

			TestHelper.AssertEqualArrays(new String[] { "A", "B" }, graph.GetNodes());

			TestHelper.AssertEqualArrays(new String[] { "B" }, graph.GetDependanciesForNode("A"));
		}
	}
}
