using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given Visual Studio solution (.sln) file.
    /// </summary>
    public sealed class SolutionFileAnalyser : DependencyAnalyserBase
    {
        private readonly string _solutionPath;

        internal SolutionFileAnalyser()
        {
            /* constructor for tests only */
        }

        public SolutionFileAnalyser(FileStream solutionFileStream)
        {
            // read solution file
            var solutionFileName = solutionFileStream.Name;
            _solutionPath = solutionFileName.Substring(0, solutionFileName.LastIndexOf(@"\"));
            var content = new StreamReader(solutionFileStream).ReadToEnd();

            var projectInfoList = ExtractProjectInfoFromSolution(content);

            foreach (var projectInfo in projectInfoList)
                ProcessProject(projectInfo);
        }

        private void ProcessProject(ProjectInfo projectInfo)
        {
            // TODO change this logic from exclusive to inclusive -- make sure project file type is supported
            if (projectInfo.RelativePath.StartsWith("http://"))
            {
                // TODO attempt to resolve URL to a filesystem address
                return;
            }
            else if (projectInfo.RelativePath.EndsWith(".dbp"))
            {
                // TODO don't know what to do with database projects just yet...
                return;
            }
            else if (projectInfo.RelativePath.EndsWith(".vdproj"))
            {
                // TODO don't know what to do with installer projects just yet...
                return;
            }

            string projectFilePathAbsolute = Path.Combine(_solutionPath, projectInfo.RelativePath);

            AddMessage("{0} depends upon:", projectInfo.Name);

            string projectFileContent = File.ReadAllText(projectFilePathAbsolute);
            string[] dependencies = ExtractDependenciesFromProject(projectFileContent);
            foreach (string dependency in dependencies)
            {
                AddMessage("- {0}", dependency);
                DependencyGraph.AddDependency(projectInfo.Name, dependency);
            }
        }

        #region Solution and project file parsing

        internal static ProjectInfo[] ExtractProjectInfoFromSolution(string content)
        {
            var projectPaths = new List<ProjectInfo>();
            string[] lines = content.Split('\n');
            if (lines.Length == 0 || !lines[0].Trim().StartsWith("Microsoft Visual Studio Solution File"))
            {
                throw new ApplicationException("Doesn't appear to be a valid Visual Studio Solution file");
            }

            bool done = false;
            int lineIndex = 1;

            while (!done)
            {
                if (lines[lineIndex].StartsWith("Project("))
                {
                    string[] lineParts = lines[lineIndex].Split(',');
                    if (lineParts.Length != 3)
                    {
                        throw new ApplicationException("Doesn't appear to be a valid Visual Studio Solution file");
                    }
                    string quotedProjectPath = lineParts[1].Trim();
                    string relativePath = quotedProjectPath.Substring(1, quotedProjectPath.Length - 2);
                    string projectName = lineParts[0].Split('"')[3];
                    projectPaths.Add(new ProjectInfo(relativePath, projectName));
                }
                else
                {
                    done = true;
                }
                lineIndex += 2; // skip the EndProject line
                if (lineIndex >= lines.Length)
                {
                    done = true;
                }
            }

            return projectPaths.ToArray();
        }

        internal static string[] ExtractDependenciesFromProject(string content)
        {
            var dependencyList = new List<string>();

            XmlDocument projectDocument = new XmlDocument();
            projectDocument.Load(new StringReader(content));

            XmlNode root = projectDocument.DocumentElement;
            XmlNodeList references = root.SelectNodes("/VisualStudioProject/CSHARP/Build/References/Reference");

            Console.WriteLine(references.Count);

            foreach (XmlNode reference in references)
            {
                XmlAttribute projectAttribute = reference.Attributes["Project"];
                if (projectAttribute != null)
                {
                    Console.WriteLine(projectAttribute.Value);
                    dependencyList.Add(reference.Attributes["Name"].Value);
                }
            }

            return dependencyList.ToArray();
        }

        #endregion
    }
}