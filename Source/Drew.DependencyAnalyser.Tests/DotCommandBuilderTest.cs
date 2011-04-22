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

            var builder = new DotCommandBuilder();
            var command = builder.GenerateDotCommand(graph);
            
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

            var builder = new DotCommandBuilder();
            builder.ExclusionList.Add("C");
            var command = builder.GenerateDotCommand(graph);
            
            const string expected = @"digraph G {
    1 -> 2;
    1 [label=""A""];
    2 [label=""B""];
}";

            Assertion.AssertEquals(expected, command);
        }
    }
}
