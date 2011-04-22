using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given assembly (.dll, .exe) file.
    /// </summary>
    public sealed class AssemblyAnalyser : DependencyAnalyserBase
    {
        private readonly List<Assembly> _knownAssemblies = new List<Assembly>();
        private readonly List<ProjectInfo> _projectInfoArrayList = new List<ProjectInfo>();
        private readonly string _assemblyPath;

        public AssemblyAnalyser(Assembly assembly)
        {
            _assemblyPath = GetAssemblyPath(assembly);
            ProcessAssembly(assembly);
        }

        /// <summary>
        /// Merges the specified assembly and its dependencies into this analyser's current dependency graph.  Any
        /// dependencies will also be processed (this method may recurse).
        /// </summary>
        public void ProcessAssembly(Assembly assembly)
        {
            if (assembly==null) 
            {
                throw new ArgumentException("Cannot process null assembly");
            }
            if (_knownAssemblies.Contains(assembly)) 
            {
                // this assembly has already been processed
                return;
            }
            _knownAssemblies.Add(assembly);

            AddMessage("----------------------------------------------------------------");
            AddMessage("Processing assembly: {0}", assembly.FullName);

            var assemblyName = assembly.FullName.Split(',')[0];
            var projectInfo = new ProjectInfo(assembly.CodeBase, assemblyName);
            var dependencies = assembly.GetReferencedAssemblies();

            AddMessage("{0} depends upon:", assemblyName);

            foreach (var dependantAssemblyName in dependencies)
            {
                AddMessage("- {0}", dependantAssemblyName.Name);

                DependencyGraph.AddDependency(assemblyName, dependantAssemblyName.Name);

                try 
                {
                    var dependantAssembly = LoadAssemblyFromAssemblyName(dependantAssemblyName);
                    ProcessAssembly(dependantAssembly);
                }
                catch (Exception e)
                {
                    AddMessage("Unable to load assembly: {0}", dependantAssemblyName.FullName);
                    AddMessage("{0}", e.ToString());
                }
            }

            _projectInfoArrayList.Add(projectInfo);
        }

        private static string GetAssemblyPath(Assembly assembly)
        {
            var assemblyPath = Path.GetDirectoryName(assembly.CodeBase);
            if (assemblyPath.StartsWith("file:")) 
            {
                assemblyPath = assemblyPath.Substring(5);
                while (assemblyPath.StartsWith(@"\") || assemblyPath.StartsWith("/"))
                    assemblyPath = assemblyPath.Substring(1);
            }
            return assemblyPath;
        }

        private Assembly LoadAssemblyFromAssemblyName(AssemblyName assemblyName)
        {
            Assembly assembly = null;

            // try to load assembly using standard Fusion rules
            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch
            {}

            // if no assembly was found, try loading from explicit path
            if (assembly == null)
            {
                try
                {
                    assembly = Assembly.LoadFrom(Path.Combine(_assemblyPath, assemblyName.Name + ".DLL"));
                }
                catch
                {
                    assembly = Assembly.LoadFrom(Path.Combine(_assemblyPath, assemblyName.Name + ".EXE"));
                }
            }

            if (assembly == null)
            {
                throw new Exception("Unable to load assembly: " + assemblyName.Name);
            }

            return assembly;
        }
    }
}
