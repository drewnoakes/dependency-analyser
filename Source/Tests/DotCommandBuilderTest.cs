using NUnit.Framework;

namespace Drew.DependencyPlotter.Tests
{
	/// <summary>
	/// Summary description for DotCommandBuilderTest.
	/// </summary>
	[TestFixture]
	public class DotCommandBuilderTest
	{
		public DotCommandBuilderTest() { }

		[Test]
		public void testGenerateDotCommand()
		{
			DependancyGraph graph = new DependancyGraph();
			graph.AddDependancy("A", "B");

			DotCommandBuilder builder = new DotCommandBuilder();
			string command = builder.GenerateDotCommand(graph);
			
			string expected = @"digraph G {
	1 -> 2;
	1 [label=""A""];
	2 [label=""B""];
}";

			Assertion.AssertEquals(expected, command);
		}

		[Test]
		public void testGenerateDotCommand_ExclusionList()
		{
			DependancyGraph graph = new DependancyGraph();
			graph.AddDependancy("A", "B");
			graph.AddDependancy("B", "C");
			graph.AddDependancy("C", "A");

			DotCommandBuilder builder = new DotCommandBuilder();
			builder.ExclusionList.Add("C");
			string command = builder.GenerateDotCommand(graph);
			
			string expected = @"digraph G {
	1 -> 2;
	1 [label=""A""];
	2 [label=""B""];
}";

			Assertion.AssertEquals(expected, command);
		}
	}
}
