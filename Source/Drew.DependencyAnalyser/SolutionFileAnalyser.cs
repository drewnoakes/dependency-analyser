using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given Visual Studio solution (.sln) file.
    /// </summary>
    public static class SolutionFileAnalyser
    {
        public static async Task AnalyseAsync(string solutionPath, DependencyGraph<string> graph, ILogger logger)
        {
            var instance = MSBuildLocator.RegisterDefaults();

            logger.WriteLine($"Located MSBuild at: {instance.MSBuildPath}");

            using var workspace = MSBuildWorkspace.Create();

            workspace.WorkspaceFailed += (o, e) => logger.WriteLine(e.Diagnostic.Message);

            logger.WriteLine($"Loading solution: {solutionPath}");

            var solution = await workspace.OpenSolutionAsync(solutionPath);
            
            logger.WriteLine($"Finished loading solution: {solutionPath}");

            var projectById = new Dictionary<Guid, Project>();

            foreach (var project in solution.Projects)
            {
                projectById.Add(project.Id.Id, project);
            }

            foreach (var project in solution.Projects)
            {
                foreach (var projectReference in project.ProjectReferences)
                {
                    var referencedProject = projectById[projectReference.ProjectId.Id];

                    graph.AddDependency(project.Name, referencedProject.Name);
                }
            }
        }
    }
}