using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given Visual Studio solution (.sln) or project (e.g. .csproj) file.
    /// </summary>
    public static class SolutionAndProjectFileAnalyser
    {
        private static bool _isRegistered;

        public static async Task AnalyseAsync(string filePath, DependencyGraph<string> graph, ILogger logger)
        {
            if (!_isRegistered)
            {
                var instance = MSBuildLocator.QueryVisualStudioInstances().OrderByDescending(i => i.Version).First();

                logger.WriteLine($"Located MSBuild at: {instance.MSBuildPath}");

                MSBuildLocator.RegisterInstance(instance);

                _isRegistered = true;
            }

            using var workspace = MSBuildWorkspace.Create();

            workspace.WorkspaceFailed += (o, e) => logger.WriteLine(e.Diagnostic.Message);

            if (filePath.EndsWith(".sln"))
            {
                logger.WriteLine($"Loading solution: {filePath}");
                await workspace.OpenSolutionAsync(filePath);
                logger.WriteLine($"Finished loading solution: {filePath}");
            }
            else
            {
                logger.WriteLine($"Loading project: {filePath}");
                await workspace.OpenProjectAsync(filePath);
                logger.WriteLine($"Finished loading project: {filePath}");
            }

            var projectById = new Dictionary<Guid, Project>();

            foreach (var project in workspace.CurrentSolution.Projects)
            {
                projectById.Add(project.Id.Id, project);
            }

            foreach (var project in workspace.CurrentSolution.Projects)
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