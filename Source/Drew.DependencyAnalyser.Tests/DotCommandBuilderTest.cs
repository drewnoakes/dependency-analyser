using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser.Tests
{
    [TestFixture]
    public sealed class DotCommandBuilderTest
    {
        [Test]
        public void GenerateDotCommand()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");

            var filterPreferences = new AssemblyFilterPreferences();
            filterPreferences.SetAssemblyNames(graph.GetNodes());
            var command = new DotCommandBuilder().GenerateDotCommand(graph, filterPreferences);
            
            const string expected = @"digraph G {
    1 -> 2;
    1 [label=""A""];
    2 [label=""B""];
}";

            Assertion.AssertEquals(expected, command);
        }

        [Test]
        public void GenerateDotCommand_ExclusionList()
        {
            var graph = new DependencyGraph<string>();
            graph.AddDependency("A", "B");
            graph.AddDependency("B", "C");
            graph.AddDependency("C", "A");

            var filterPreferences = new AssemblyFilterPreferences();
            filterPreferences.SetAssemblyNames(graph.GetNodes());
            filterPreferences.Exclude("C");

            var command = new DotCommandBuilder().GenerateDotCommand(graph, filterPreferences);
            
            const string expected = @"digraph G {
    1 -> 2;
    1 [label=""A""];
    2 [label=""B""];
}";

            Assertion.AssertEquals(expected, command);
        }
    }
}
