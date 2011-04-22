using NUnit.Framework;
using System.Linq;

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class DependencyGraphTest
    {
        [Test]
        public void AddDependencyTest()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");

            TestHelper.AssertEqualArrays(new[] { "A", "B" }, graph.GetNodes().ToArray());
            TestHelper.AssertEqualArrays(new[] { "B" }, graph.GetDependenciesForNode("A").ToArray());
        }
    }
}
