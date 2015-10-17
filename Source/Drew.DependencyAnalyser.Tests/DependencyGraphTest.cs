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

            CollectionAssert.AreEqual(new[] { "A", "B" }, graph.Nodes.ToArray());
            CollectionAssert.AreEqual(new[] { "B" }, graph.GetDependenciesForNode("A").ToArray());
        }
    }
}
