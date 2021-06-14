using Xunit;

namespace DependencyAnalyser.Tests
{
    public sealed class DotCommandBuilderTest
    {
        [Fact]
        public void GenerateDotCommand()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");

            var filterPreferences = new FilterPreferences();
            filterPreferences.SetAssemblyNames(graph.Nodes);
            var command = DotCommandBuilder.Generate(graph, filterPreferences);
            
            const string expected = @"digraph G {
    1 -> 2;
    1 [label=""A""];
    2 [label=""B""];
}";

            Assert.Equal(expected, command);
        }

        [Fact]
        public void GenerateDotCommand_ExclusionList()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");
            graph.AddDependency("B", "C");
            graph.AddDependency("C", "A");

            var filterPreferences = new FilterPreferences();
            filterPreferences.SetAssemblyNames(graph.Nodes);
            filterPreferences.Exclude("C");

            var command = DotCommandBuilder.Generate(graph, filterPreferences);
            
            const string expected = @"digraph G {
    1 -> 2;
    1 [label=""A""];
    2 [label=""B""];
}";

            Assert.Equal(expected, command);
        }
    }
}
