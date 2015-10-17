using NUnit.Framework;

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

            CollectionAssert.AreEqual(new[] { "A", "B" }, graph.Nodes);
            CollectionAssert.AreEqual(new[] { "B" }, graph.GetDependenciesForNode("A"));
        }

        [Test]
        public void TransitiveReduceThreeNodes()
        {
            var graph = new DependencyGraph<char>();
            graph.AddDependency('a', 'b');
            graph.AddDependency('b', 'c');
            graph.AddDependency('a', 'c');

            graph.TransitiveReduce();

            CollectionAssert.AreEqual(new[] { 'a', 'b', 'c' }, graph.Nodes);
            CollectionAssert.AreEqual(new[] { 'b' }, graph.GetDependenciesForNode('a'));
            CollectionAssert.AreEqual(new[] { 'c' }, graph.GetDependenciesForNode('b'));
        }

        [Test]
        public void TransitiveReduceCascade()
        {
            var graph = new DependencyGraph<char>();

            for (var c1 = 'a'; c1 < 'f'; c1++)
                for (var c2 = (char)(c1 + 1); c2 < 'f'; c2++)
                    graph.AddDependency(c1, c2);

            graph.TransitiveReduce();

            for (var c = 'a'; c < 'f' - 1; c++)
                CollectionAssert.AreEqual(new[] { (char)(c + 1) }, graph.GetDependenciesForNode(c), $"Dep of {c} should be {(char)(c+1)}");
        }

        [Test]
        public void TransitiveReduceCycles()
        {
            var graph = new DependencyGraph<char>();

            graph.AddDependency('a', 'b'); // a <--> b
            graph.AddDependency('b', 'a');

            graph.AddDependency('a', 'c'); // a --> c
            graph.AddDependency('b', 'c'); // b --> c

            graph.TransitiveReduce();

            // NOTE result here depends upon order of enumeration from HashSet<char>, making this test potentially fragile

            CollectionAssert.AreEqual(new[] { 'b' }, graph.GetDependenciesForNode('a'));
            CollectionAssert.AreEqual(new[] { 'a', 'c' }, graph.GetDependenciesForNode('b'));
        }
    }
}
