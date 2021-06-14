using Xunit;

namespace Drew.DependencyAnalyser.Tests
{
    public sealed class DependencyGraphTest
    {
        [Fact]
        public void AddDependencyTest()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");

            Assert.Equal(new[] { "A", "B" }, graph.Nodes);
            Assert.Equal(new[] { "B" }, graph.GetDependenciesForNode("A"));
        }

        [Fact]
        public void TransitiveReduceThreeNodes()
        {
            var graph = new DependencyGraph<char>();
            graph.AddDependency('a', 'b');
            graph.AddDependency('b', 'c');
            graph.AddDependency('a', 'c');

            graph.TransitiveReduce();

            Assert.Equal(new[] { 'a', 'b', 'c' }, graph.Nodes);
            Assert.Equal(new[] { 'b' }, graph.GetDependenciesForNode('a'));
            Assert.Equal(new[] { 'c' }, graph.GetDependenciesForNode('b'));
        }

        [Fact]
        public void TransitiveReduceCascade()
        {
            var graph = new DependencyGraph<char>();

            for (var c1 = 'a'; c1 < 'f'; c1++)
                for (var c2 = (char)(c1 + 1); c2 < 'f'; c2++)
                    graph.AddDependency(c1, c2);

            graph.TransitiveReduce();

            for (var c = 'a'; c < 'f' - 1; c++)
                Assert.Equal(new[] { (char)(c + 1) }, graph.GetDependenciesForNode(c));
        }

        [Fact]
        public void TransitiveReduceCycles()
        {
            var graph = new DependencyGraph<char>();

            graph.AddDependency('a', 'b'); // a <--> b
            graph.AddDependency('b', 'a');

            graph.AddDependency('a', 'c'); // a --> c
            graph.AddDependency('b', 'c'); // b --> c

            graph.TransitiveReduce();

            // NOTE result here depends upon order of enumeration from HashSet<char>, making this test potentially fragile

            Assert.Equal(new[] { 'b' }, graph.GetDependenciesForNode('a'));
            Assert.Equal(new[] { 'a', 'c' }, graph.GetDependenciesForNode('b'));
        }
    }
}
