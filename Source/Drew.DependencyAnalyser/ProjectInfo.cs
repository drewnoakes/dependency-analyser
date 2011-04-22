namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Container for information about a Visual Studio project.
    /// </summary>
    public sealed class ProjectInfo
    {
        public string RelativePath { get; private set; }
        public string Name { get; private set; }

        public ProjectInfo(string relativePath, string name)
        {
            RelativePath = relativePath;
            Name = name;
        }
    }
}
