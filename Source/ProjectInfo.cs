namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Container for information about a Visual Studio project.
	/// </summary>
	public class ProjectInfo
	{
		public string RelativePath;
		public string Name;

		public ProjectInfo(string relativePath, string name)
		{
			this.RelativePath = relativePath;
			this.Name = name;
		}
	}
}
