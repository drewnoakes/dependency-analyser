namespace Drew.DependencyAnalyser
{
    /// <summary>
    /// Container for information about a Visual Studio project.
    /// </summary>
    public sealed class ProjectInfo
    {
        public string RelativePath { get; }
        public string Name { get; }

        public ProjectInfo(string relativePath, string name)
        {
            RelativePath = relativePath;
            Name = name;
        }
    }
}
