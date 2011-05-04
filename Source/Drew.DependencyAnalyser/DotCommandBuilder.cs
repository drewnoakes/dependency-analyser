using System.Collections.Generic;
using System.Text;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Builds a Dot command for a given dependency graph.
    /// </summary>
    public sealed class DotCommandBuilder
    {
        public string GenerateDotCommand(DependencyGraph<string> graph, AssemblyFilterPreferences filterPreferences)
        {
            return GenerateDotCommand(graph, filterPreferences, string.Empty);
        }

        public string GenerateDotCommand(DependencyGraph<string> graph, AssemblyFilterPreferences filterPreferences, string extraCommands)
        {
            var nodes = graph.GetNodes();

            // TODO can this first loop be replaced with LINQ, maybe with a zip?
            var idsByNameMap = new Dictionary<string, int>();
            var id = 1;
            foreach (var nodeName in nodes)
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

            foreach (var dependant in nodes)
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

            commandText.Append(nodeLabels.ToString());
            commandText.Append("}");

            return commandText.ToString();
        }
    }
}