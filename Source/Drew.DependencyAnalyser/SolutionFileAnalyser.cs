using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Builds a DependencyGraph from a given Visual Studio solution (.sln) file.
    /// </summary>
    public sealed class SolutionFileAnalyser : DependencyAnalyserBase
    {
        private readonly string _solutionFileName;
        private readonly string _solutionPath;

        internal SolutionFileAnalyser()
        {
            /* constructor for tests only */
        }

        public SolutionFileAnalyser(FileStream solutionFileStream)
        {
            // read solution file
            _solutionFileName = solutionFileStream.Name;
            _solutionPath = _solutionFileName.Substring(0, _solutionFileName.LastIndexOf(@"\"));
            var content = ReadEntireStream(solutionFileStream);

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

            string projectFileContent = ReadEntireStream(File.OpenRead(projectFilePathAbsolute));
            string[] dependencies = ExtractDependenciesFromProject(projectFileContent);
            foreach (string dependency in dependencies)
            {
                AddMessage("- {0}", dependency);
                DependencyGraph.AddDependency(projectInfo.Name, dependency);
            }
        }

        /// <summary>
        /// Reads all bytes from a stream, and compiles a string representation by casting each byte to a char.
        /// </summary>
        private string ReadEntireStream(Stream stream)
        {
            StringWriter solutionFileContent = new StringWriter();
            byte[] bytes = new byte[1000];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            while (bytesRead > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    solutionFileContent.Write((char)bytes[i]);
                }
                bytesRead = stream.Read(bytes, 0, bytes.Length);
            }
            stream.Close();
            return solutionFileContent.ToString();
        }

        #region Solution and project file parsing

        internal ProjectInfo[] ExtractProjectInfoFromSolution(string content)
        {
            ArrayList projectPaths = new ArrayList();
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

            return (ProjectInfo[])projectPaths.ToArray(typeof (ProjectInfo));
        }

        internal string[] ExtractDependenciesFromProject(string content)
        {
            ArrayList dependencyList = new ArrayList();

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

            return (string[])dependencyList.ToArray(typeof (string));
        }

        #endregion
    }
}