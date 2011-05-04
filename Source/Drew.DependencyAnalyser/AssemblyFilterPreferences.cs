using System;
using System.Collections.Generic;
using System.Linq;

namespace Drew.DependencyAnalyser
{
    public sealed class AssemblyFilterPreferences
    {
        private readonly HashSet<string> _allNames = new HashSet<string>();
        private readonly HashSet<string> _includedNames = new HashSet<string>();

        public bool IncludeInPlot(string name)
        {
            return _includedNames.Contains(name);
        }

        public void SetAssemblyNames(IEnumerable<string> names)
        {
            // any names left in this set at the end will be removed
            var toRemove = new HashSet<string>(_allNames);

            foreach (var name in names)
            {
                // this name still exists, so we don't remove it
                toRemove.Remove(name);

                if (!_allNames.Contains(name))
                {
                    _allNames.Add(name);
                    
                    // new assemblies are included by default
                    _includedNames.Add(name);
                }
            }

            foreach (var name in toRemove)
            {
                _allNames.Remove(name);
                _includedNames.Remove(name);
            }
        }

        public void Exclude(string name)
        {
            if (!_allNames.Contains(name))
                throw new InvalidOperationException("Invalid name.");

            _includedNames.Remove(name);
        }

        public IEnumerable<string> GetAllNames()
        {
            return _allNames.OrderBy(name => name).ToList();
        }

        public IEnumerable<string> GetIncludedNames()
        {
            return _includedNames.OrderBy(name => name).ToList();
        }

        public void SetInclusion(string name, bool include)
        {
            if (!_allNames.Contains(name))
                throw new ArgumentException();
            
            if (include)
                _includedNames.Add(name);
            else
                _includedNames.Remove(name);
        }
    }
}