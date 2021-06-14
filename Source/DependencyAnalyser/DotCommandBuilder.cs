using System.Collections.Generic;
using System.Text;

namespace DependencyAnalyser
{
    /// <summary>
    /// Builds a Dot command for a given dependency graph.
    /// </summary>
    public static class DotCommandBuilder
    {
        public static string Generate(DependencyGraph<string> graph, FilterPreferences filterPreferences)
        {
            return Generate(graph, filterPreferences, string.Empty);
        }

        public static string Generate(DependencyGraph<string> graph, FilterPreferences filterPreferences, string extraCommands)
        {
            // TODO can this first loop be replaced with LINQ, maybe with a zip?
            var idsByNameMap = new Dictionary<string, int>();
            var id = 1;
            foreach (var nodeName in graph.Nodes)
            {
                idsByNameMap.Add(nodeName, id);
                id++;
            }

            var commandText = new StringBuilder();
            commandText.Append("digraph G {\r\n");

            // handle extra commands
            if (extraCommands.Trim().Length > 0)
            {
                commandText.Append("    ");
                commandText.Append(extraCommands.Trim());
                commandText.Append("\r\n");
            }

            var nodeLabels = new StringBuilder();

            foreach (var dependant in graph.Nodes)
            {
                // make sure the dependant should be plotted
                if (!filterPreferences.IncludeInPlot(dependant))
                    continue;

                var dependantId = idsByNameMap[dependant];

                // 1 [label="SampleProject",shape=circle,hight=0.12,width=0.12,fontsize=1];
                nodeLabels.AppendFormat("    {0} [label=\"{1}\"];\r\n", dependantId, dependant);

                foreach (var dependency in graph.GetDependenciesForNode(dependant))
                {
                    var dependencyId = idsByNameMap[dependency];
                    if (!filterPreferences.IncludeInPlot(dependency))
                        continue;
                    commandText.AppendFormat("    {0} -> {1};\r\n", dependantId, dependencyId);
                }
            }

            commandText.Append(nodeLabels);
            commandText.Append("}");

            return commandText.ToString();
        }
    }
}