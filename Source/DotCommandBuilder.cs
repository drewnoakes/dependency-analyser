using System;
using System.Collections;
using System.Text;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Builds a Dot command for a given dependancy graph.
	/// </summary>
	public class DotCommandBuilder
	{
		ArrayList _exclusionList = new ArrayList();

		public DotCommandBuilder() { }

		public ArrayList ExclusionList
		{
			get
			{
				return _exclusionList;
			}
		}

		public string GenerateDotCommand(DependancyGraph graph)
		{
			return GenerateDotCommand(graph, String.Empty);
		}

		public string GenerateDotCommand(DependancyGraph graph, string extraCommands)
		{
			string[] nodes = graph.GetNodes();

			Hashtable idsByNameMap = new Hashtable();
			int id = 1;
			foreach (string nodeName in nodes)
			{
				idsByNameMap.Add(nodeName, id.ToString());
				id++;
			}

			StringBuilder commandText = new StringBuilder();
			commandText.Append("digraph G {\r\n");

			// handle extra commands
			if (extraCommands.Trim().Length>0) 
			{
				commandText.Append('\t');
				commandText.Append(extraCommands.Trim());
				commandText.Append("\r\n");
			}

			StringBuilder nodeLabels = new StringBuilder();

			foreach (string dependant in nodes)
			{
				// make sure the dependant should be plotted
				if (!IncludeInPlot(dependant)) 
				{
					continue;
				}

				string dependantId = (string)idsByNameMap[dependant];

				// 1 [label="BP.Openbooks.Web",shape=circle,hight=0.12,width=0.12,fontsize=1];
				nodeLabels.AppendFormat("\t{0} [label=\"{1}\"];\r\n", dependantId, dependant);

				foreach (string dependancy in graph.GetDependanciesForNode(dependant))
				{
					string dependancyId = (string)idsByNameMap[dependancy];
					if (dependancyId==null || !IncludeInPlot(dependancy)) 
					{
						continue;
					}
					commandText.AppendFormat("\t{0} -> {1};\r\n", dependantId, dependancyId);

				}
			}

			commandText.Append(nodeLabels.ToString());
			commandText.Append("}");

			return commandText.ToString();
		}

		protected virtual bool IncludeInPlot(string node)
		{
			return (!_exclusionList.Contains(node));
		}
	}
}
