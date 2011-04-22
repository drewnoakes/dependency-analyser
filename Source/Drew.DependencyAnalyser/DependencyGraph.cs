using System.Collections.Generic;
using System.Linq;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Models a directed graph, intended to represent a dependency hierarchy.
    /// </summary>
    public sealed class DependencyGraph<T>
    {
        private readonly HashSet<T> _nodes = new HashSet<T>();
        private readonly Dictionary<T, HashSet<T>> _dependenciesByNode = new Dictionary<T, HashSet<T>>();

        public void AddDependency(T dependant, T dependency)
        {
            // take note of these nodes
            _nodes.Add(dependant);
            _nodes.Add(dependency);

            // get the list of dependencies for this dependant (create if it doesn't exist yet)
            HashSet<T> dependencySet;
            if (!_dependenciesByNode.TryGetValue(dependant, out dependencySet))
                dependencySet = _dependenciesByNode[dependant] = new HashSet<T>();

            dependencySet.Add(dependency);
        }

        public IEnumerable<T> GetNodes()
        {
            return _nodes;
        }

        public IEnumerable<T> GetDependenciesForNode(T dependant)
        {
            HashSet<T> dependencyList;
            return _dependenciesByNode.TryGetValue(dependant, out dependencyList)
                       ? dependencyList
                       : Enumerable.Empty<T>();
        }
    }
}