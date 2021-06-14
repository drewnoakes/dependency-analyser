using System;
using System.IO;
using System.Reflection;

namespace DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given assembly (.dll, .exe) file.
    /// </summary>
    public static class AssemblyAnalyser
    {
        public static void Analyze(Assembly assembly, DependencyGraph<string> graph, ILogger logger)
        {
            var assemblyPath = GetAssemblyPath(assembly);

            logger.WriteLine("----------------------------------------------------------------");
            logger.WriteLine($"Processing assembly: {assembly.FullName}");

            var assemblyName = assembly.FullName.Split(',')[0];
            var dependencies = assembly.GetReferencedAssemblies();

            logger.WriteLine($"{assemblyName} depends upon:");

            foreach (var dependantAssemblyName in dependencies)
            {
                logger.WriteLine("- {dependantAssemblyName.Name}");

                if (!graph.AddDependency(assemblyName, dependantAssemblyName.Name))
                {
                    // Prevent stack overflow
                    continue;
                }

                try
                {
                    var dependantAssembly = LoadAssemblyFromAssemblyName(assemblyPath, dependantAssemblyName);
                    
                    Analyze(dependantAssembly, graph, logger);
                }
                catch (Exception e)
                {
                    logger.WriteLine($"Unable to load assembly: {dependantAssemblyName.FullName}");
                    logger.WriteLine(e.ToString());
                }
            }

            static string GetAssemblyPath(Assembly assembly)
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

            static Assembly LoadAssemblyFromAssemblyName(string assemblyPath, AssemblyName assemblyName)
            {
                Assembly? assembly = null;

                // try to load assembly using standard Fusion rules
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch
                { }

                // if no assembly was found, try loading from explicit path
                if (assembly == null)
                {
                    try
                    {
                        assembly = Assembly.LoadFrom(Path.Combine(assemblyPath, assemblyName.Name + ".DLL"));
                    }
                    catch
                    {
                        assembly = Assembly.LoadFrom(Path.Combine(assemblyPath, assemblyName.Name + ".EXE"));
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
}
