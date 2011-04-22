using System.Collections;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Models a directional graph, intended to represent a dependancy chart.
	/// </summary>
	public class DependancyGraph
	{
		ArrayList _nodes = new ArrayList();
		Hashtable _nodeDependancyListMap = new Hashtable();

		public DependancyGraph() { }

		public void AddDependancy(string dependant, string dependancy)
		{
			// take note of these nodes
			RememberNode(dependant);
			RememberNode(dependancy);

			// get the list of dependancies for this dependant (create if it doesn't exist yet)
			if (!_nodeDependancyListMap.ContainsKey(dependant))
			{
				_nodeDependancyListMap.Add(dependant, new ArrayList());
			}
			ArrayList dependancyList = (ArrayList)_nodeDependancyListMap[dependant];

			// add dependancy, if it doesn't already exist
			if (!dependancyList.Contains(dependancy)) 
			{
				dependancyList.Add(dependancy);
			}
		}

		public string[] GetNodes()
		{
			return (string[])_nodes.ToArray(typeof(string));
		}

		void RememberNode(string node)
		{
			if (!_nodes.Contains(node)) 
			{
				_nodes.Add(node);
			}
		}

		public string[] GetDependanciesForNode(string dependant)
		{
			ArrayList dependancyList = (ArrayList)_nodeDependancyListMap[dependant];
			if (dependancyList==null) 
			{
				dependancyList = new ArrayList();
			}
			return (string[])dependancyList.ToArray(typeof(string));
		}
	}
}
