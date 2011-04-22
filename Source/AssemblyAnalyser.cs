using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Builds a DependancyGraph from a given assembly (.dll, .exe) file.
	/// </summary>
	public class AssemblyAnalyser : DependancyAnalyser
	{
		ArrayList _knownAssemblies = new ArrayList();
		ArrayList _projectInfoArrayList = new ArrayList();

		string _assemblyPath;

		public AssemblyAnalyser(Assembly assembly)
		{
			_assemblyPath = GetAssemblyPath(assembly);
			ProcessAssembly(assembly);
		}

		string GetAssemblyPath(Assembly assembly)
		{
			String assemblyPath;
			assemblyPath = Path.GetDirectoryName(assembly.CodeBase);
			if (assemblyPath.StartsWith("file:")) 
			{
				assemblyPath = assemblyPath.Substring(5);
				while (assemblyPath.StartsWith(@"\") || assemblyPath.StartsWith("/"))
				{
					assemblyPath = assemblyPath.Substring(1);
				}
			}
			return assemblyPath;
		}

		/// <summary>
		/// Merges the specified assembly and its dependancies into this analyser's current dependancy graph.  Any
		/// dependancies will also be processed (this method may recurse).
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

			string assemblyName = assembly.FullName.Split(',')[0];

			ProjectInfo projectInfo = new ProjectInfo(assembly.CodeBase, assemblyName);

			AssemblyName[] dependancies = assembly.GetReferencedAssemblies();

			AddMessage("{0} depends upon:", assemblyName);

			foreach (AssemblyName dependantAssemblyName in dependancies)
			{
				AddMessage("- {0}", dependantAssemblyName.Name);

				DependancyGraph.AddDependancy(assemblyName, dependantAssemblyName.Name);

				try 
				{
					Assembly dependantAssembly = LoadAssemblyFromAssemblyName(dependantAssemblyName);
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

		Assembly LoadAssemblyFromAssemblyName(AssemblyName assemblyName)
		{
			Assembly assembly = null;
			
			// try to load assembly using standard Fusion rules
			try 
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch { }

			// if no assembly was found, try loading from explicit path
			if (assembly==null) 
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
			if (assembly==null) 
			{
				throw new ApplicationException("Unable to load assembly: " + assemblyName.Name);
			}

			return assembly;
		}
	}
}
