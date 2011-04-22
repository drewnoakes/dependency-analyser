using System.Text;

namespace Drew.DependencyPlotter
{
	/// <summary>
	/// Base class for dependancy analysers.  Subclasses are intended to populate the DependancyGraph property instance.
	/// </summary>
	public abstract class DependancyAnalyser
	{
		protected StringBuilder _messages = new StringBuilder();
		protected DependancyGraph _graph = new DependancyGraph();

		public DependancyGraph DependancyGraph
		{
			get
			{
				return _graph;
			}
		}

		#region Dot command generation

		//		public string GenerateDotCommand()
		//		{
		//			return GenerateDotCommand(String.Empty);
		//		}
		//
		//		public string GenerateDotCommand(string extraCommands)
		//		{
		//			Hashtable idsByProjectNameMap = new Hashtable();
		//			foreach (ProjectInfo projectInfo in _projectInfoList)
		//			{
		//				idsByProjectNameMap.Add(projectInfo.Name, projectInfo.Id);
		//			}
		//			StringBuilder commandText = new StringBuilder();
		//			commandText.Append("digraph G {\r\n");
		//
		//			// handle extra commands
		//			if (extraCommands.Trim().Length>0) 
		//			{
		//				commandText.Append('\t');
		//				commandText.Append(extraCommands.Trim());
		//				commandText.Append("\r\n");
		//			}
		//
		//			foreach (ProjectInfo projectInfo in _projectInfoList)
		//			{
		//				if (!projectInfo.IncludeInPlot) 
		//				{
		//					continue;
		//				}
		//				foreach (string dependantProjectName in projectInfo.DependantProjectNames)
		//				{
		//					string projectWithDependancyId = projectInfo.Id;
		//					string dependancyProjectId = (string)idsByProjectNameMap[dependantProjectName];
		//					if (dependancyProjectId==null) 
		//					{
		//
		//						continue;
		//					}
		//					commandText.AppendFormat("\t{0} -> {1};\r\n", projectWithDependancyId, dependancyProjectId);
		//				}
		//			}
		//			// 1 [label="BP.Openbooks.Web",shape=circle,hight=0.12,width=0.12,fontsize=1];
		//			foreach (ProjectInfo projectInfo in _projectInfoList)
		//			{
		//				if (!projectInfo.IncludeInPlot) 
		//				{
		//					continue;
		//				}
		//				commandText.AppendFormat("\t{0} [label=\"{1}\"];\r\n", projectInfo.Id, projectInfo.Name);
		//			}
		//			commandText.Append("}");
		//			return commandText.ToString();
		//		}

		#endregion

		#region Message handling 

		/// <summary>
		/// Obtain any messages generated during the processing of dependancies.
		/// </summary>
		public string GetMessages()
		{
			return _messages.ToString();
		}

		internal void AddMessage(string message)
		{
			StartNewMessage();
			_messages.Append(message);
		}

		internal void AddMessage(string message, params object[] format)
		{
			StartNewMessage();
			_messages.AppendFormat(message, format);
		}

		void StartNewMessage()
		{
			if (_messages.ToString().Length > 0)
			{
				_messages.Append("\r\n");
			}
		}

		#endregion
	}
}
