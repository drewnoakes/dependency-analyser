using System.Collections.Generic;
using System.Diagnostics;
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

        public bool AddDependency(T dependant, T dependency)
        {
            // take note of these nodes
            _nodes.Add(dependant);
            _nodes.Add(dependency);

            // get the list of dependencies for this dependant (create if it doesn't exist yet)
            HashSet<T> dependencySet;
            if (!_dependenciesByNode.TryGetValue(dependant, out dependencySet))
                dependencySet = _dependenciesByNode[dependant] = new HashSet<T>();

            return dependencySet.Add(dependency);
        }

        public IEnumerable<T> Nodes => _nodes;

        public IEnumerable<T> GetDependenciesForNode(T dependant)
        {
            HashSet<T> dependencies;
            return _dependenciesByNode.TryGetValue(dependant, out dependencies)
                       ? dependencies
                       : Enumerable.Empty<T>();
        }

        /// <summary>
        /// Remove edges from the graph without changing the reachability of nodes.
        /// If a node has both a direct and transitive dependency upon another node, the direct dependency is removed.
        /// </summary>
        public void TransitiveReduce()
        {
            var visited = new HashSet<T>();
            var frontier = new Stack<T>();

            foreach (var root in _nodes)
            {
                HashSet<T> children;
                if (!_dependenciesByNode.TryGetValue(root, out children))
                    continue;

                var childList = children.ToList();
                for (var i = 0; i < childList.Count; i++)
                {
                    var child = childList[i];
                    Debug.Assert(!frontier.Any());

                    visited.Clear();
                    visited.Add(root);

                    // Perform a DFS from this child
                    frontier.Push(child);

                    while (frontier.Any())
                    {
                        var node = frontier.Pop();

                        // If we've already expanded this node, continue
                        if (!visited.Add(node))
                            continue;

                        foreach (var dependency in GetDependenciesForNode(node))
                            frontier.Push(dependency);

                        if (!Equals(child, node) && children.Contains(node))
                        {
                            // This node is directly linked to from root,
                            // however we arrived here via a longer, indirect path
                            // so we remove the shorter, direct path
                            children.Remove(node);
                            
                            // Update the list we're iterating over, adjusting the current index if required
                            var index = childList.IndexOf(node);
                            Debug.Assert(index != -1);
                            childList.RemoveAt(index);
                            if (index <= i)
                                i--;
                        }
                    }
                }
            }
        }
    }
}