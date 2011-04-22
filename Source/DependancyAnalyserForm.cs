using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using WINGRAPHVIZLib;

namespace Drew.DependencyPlotter
{
	public class DependancyAnalyserForm : Form
	{
		#region GUI fields

		private OpenFileDialog openFileDialog1;
		private SaveFileDialog savePngFileDialog;
		private SaveFileDialog saveSvgFileDialog;
		private PrintDialog printDialog;
		private PrintPreviewDialog printPreviewDialog;

		private TabControl tabControl;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private TabPage tabPage3;
		
		private PictureBox dotImageBox;

		private MainMenu mainMenu1;
		private MenuItem menuItem1;
		private MenuItem menuItem2;
		private MenuItem menuItem3;
		private MenuItem menuItem4;
		private MenuItem menuItem6;
		private MenuItem menuExclude;
		private MenuItem menuViewRefresh;
		private MenuItem menuFileSavePng;
		private MenuItem menuFileSaveSvg;
		private MenuItem menuFilePrint;
		private MenuItem menuFilePrintPreview;
		private MenuItem menuFileMerge;
		private MenuItem menuFileExit;
		private MenuItem menuFileOpen;
		private MenuItem menuFileClose;

		private TextBox dotScriptOutput;
		private TextBox messageTextBox;

		#endregion

		string _tempPngImageFileName = null;
		string _svgXml = null;
		Image _dotImage = null;

		DependancyAnalyser _analyser = null;
		DotCommandBuilder _dotCommandBuilder;
		TemporaryFileManager _tempFileManager = new TemporaryFileManager();

		public DependancyAnalyserForm()
		{
			_dotCommandBuilder = new DotCommandBuilder();
			InitializeComponent();
			EnableAndDisableMenuItems();
		}

		void EnableAndDisableMenuItems()
		{
			bool analyserAvailable = (_analyser!=null);
			menuFileSavePng.Enabled = analyserAvailable;
			menuFileSaveSvg.Enabled = analyserAvailable;
			menuFileClose.Enabled = analyserAvailable;
			menuExclude.Enabled = analyserAvailable;
			menuViewRefresh.Enabled = analyserAvailable;
			menuFilePrint.Enabled = analyserAvailable;
			menuFilePrintPreview.Enabled = analyserAvailable;
			menuFileMerge.Enabled = analyserAvailable && _analyser is AssemblyAnalyser;
			if (!analyserAvailable) 
			{
				menuExclude.MenuItems.Clear();
			}
		}

		void CloseAnalyser()
		{
			_analyser = null;
			_dotImage = null;
			_svgXml = null;
			UpdateImage();
			dotScriptOutput.Text = String.Empty;
			messageTextBox.Text = String.Empty;
			EnableAndDisableMenuItems();
		}

		#region Windows Form Designer generated code
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DependancyAnalyserForm));
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuFileMerge = new System.Windows.Forms.MenuItem();
			this.menuFileSavePng = new System.Windows.Forms.MenuItem();
			this.menuFileSaveSvg = new System.Windows.Forms.MenuItem();
			this.menuFilePrint = new System.Windows.Forms.MenuItem();
			this.menuFilePrintPreview = new System.Windows.Forms.MenuItem();
			this.menuFileClose = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuViewRefresh = new System.Windows.Forms.MenuItem();
			this.menuExclude = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.dotImageBox = new System.Windows.Forms.PictureBox();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.dotScriptOutput = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.savePngFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.saveSvgFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.printDialog = new System.Windows.Forms.PrintDialog();
			this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.tabControl.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Solution files (*.sln)|*.sln|Assemblies (*.dll)|*.dll|Assemblies (*.exe)|*.exe|Al" +
				"l files|*.exe";
			this.openFileDialog1.FilterIndex = 2;
			this.openFileDialog1.Title = "Open File";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem6,
																					  this.menuExclude,
																					  this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuFileOpen,
																					  this.menuFileMerge,
																					  this.menuFileSavePng,
																					  this.menuFileSaveSvg,
																					  this.menuFilePrint,
																					  this.menuFilePrintPreview,
																					  this.menuFileClose,
																					  this.menuItem2,
																					  this.menuFileExit});
			this.menuItem1.Text = "&File";
			// 
			// menuFileOpen
			// 
			this.menuFileOpen.Index = 0;
			this.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuFileOpen.Text = "&Open...";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
			// 
			// menuFileMerge
			// 
			this.menuFileMerge.Index = 1;
			this.menuFileMerge.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.menuFileMerge.Text = "Merge...";
			this.menuFileMerge.Click += new System.EventHandler(this.menuFileMerge_Click);
			// 
			// menuFileSavePng
			// 
			this.menuFileSavePng.Index = 2;
			this.menuFileSavePng.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuFileSavePng.Text = "Save &PNG...";
			this.menuFileSavePng.Click += new System.EventHandler(this.menuFileSavePng_Click);
			// 
			// menuFileSaveSvg
			// 
			this.menuFileSaveSvg.Index = 3;
			this.menuFileSaveSvg.Text = "Save &SVG...";
			this.menuFileSaveSvg.Click += new System.EventHandler(this.menuFileSaveSvg_Click);
			// 
			// menuFilePrint
			// 
			this.menuFilePrint.Index = 4;
			this.menuFilePrint.Text = "Print...";
			this.menuFilePrint.Click += new System.EventHandler(this.menuFilePrint_Click);
			// 
			// menuFilePrintPreview
			// 
			this.menuFilePrintPreview.Index = 5;
			this.menuFilePrintPreview.Text = "Print preview...";
			this.menuFilePrintPreview.Click += new System.EventHandler(this.menuFilePrintPreview_Click);
			// 
			// menuFileClose
			// 
			this.menuFileClose.Index = 6;
			this.menuFileClose.Text = "&Close";
			this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 7;
			this.menuItem2.Text = "-";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Index = 8;
			this.menuFileExit.Text = "E&xit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuViewRefresh});
			this.menuItem6.Text = "&View";
			// 
			// menuViewRefresh
			// 
			this.menuViewRefresh.Index = 0;
			this.menuViewRefresh.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.menuViewRefresh.Text = "&Refresh";
			this.menuViewRefresh.Click += new System.EventHandler(this.menuViewRefresh_Click);
			// 
			// menuExclude
			// 
			this.menuExclude.Index = 2;
			this.menuExclude.Text = "E&xclude";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem4});
			this.menuItem3.Text = "&Help";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "About...";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(688, 369);
			this.tabControl.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.dotImageBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(680, 343);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Image";
			// 
			// dotImageBox
			// 
			this.dotImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dotImageBox.Location = new System.Drawing.Point(0, 0);
			this.dotImageBox.Name = "dotImageBox";
			this.dotImageBox.Size = new System.Drawing.Size(680, 343);
			this.dotImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.dotImageBox.TabIndex = 0;
			this.dotImageBox.TabStop = false;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.dotScriptOutput);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(680, 343);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Dot Output";
			// 
			// dotScriptOutput
			// 
			this.dotScriptOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dotScriptOutput.Location = new System.Drawing.Point(0, 0);
			this.dotScriptOutput.Multiline = true;
			this.dotScriptOutput.Name = "dotScriptOutput";
			this.dotScriptOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.dotScriptOutput.Size = new System.Drawing.Size(680, 343);
			this.dotScriptOutput.TabIndex = 0;
			this.dotScriptOutput.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.messageTextBox);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(680, 343);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Messages";
			// 
			// messageTextBox
			// 
			this.messageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messageTextBox.Location = new System.Drawing.Point(0, 0);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.messageTextBox.Size = new System.Drawing.Size(680, 343);
			this.messageTextBox.TabIndex = 0;
			this.messageTextBox.Text = "";
			// 
			// savePngFileDialog
			// 
			this.savePngFileDialog.DefaultExt = "png";
			this.savePngFileDialog.FileName = "DependancyGraph.png";
			this.savePngFileDialog.Filter = "PNG Images|*.png|All files|*.*";
			// 
			// saveSvgFileDialog
			// 
			this.saveSvgFileDialog.FileName = "DependancyGraph.svg";
			this.saveSvgFileDialog.Filter = "SVG files (*.svg)|*.svg|All files|*.*";
			// 
			// printPreviewDialog
			// 
			this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog.Enabled = true;
			this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
			this.printPreviewDialog.Location = new System.Drawing.Point(642, 17);
			this.printPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog.Name = "printPreviewDialog";
			this.printPreviewDialog.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog.Visible = false;
			// 
			// DependancyAnalyserForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 369);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "DependancyAnalyserForm";
			this.Text = ".NET Assembly Dependancy Analyser";
			this.tabControl.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void OpenFile(FileStream fileStream)
		{
			if (fileStream.Name.EndsWith(".sln")) 
			{
				_analyser = new SolutionFileAnalyser(fileStream);
			} 
			else 
			{
				_analyser = new AssemblyAnalyser(System.Reflection.Assembly.LoadFrom(fileStream.Name));
				fileStream.Close();
			}

			RefreshAfterGraphModified();
		}

		void RefreshAfterGraphModified()
		{
			PopulateExcludeMenu();
			EnableAndDisableMenuItems();
			UpdateImage();
		}

		#region Event handlers

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			DialogResult result = openFileDialog1.ShowDialog();
			if (result==DialogResult.OK) 
			{
				StartApplicationWait();
				FileStream fileStream = (FileStream)openFileDialog1.OpenFile();
				OpenFile(fileStream);
				EndApplicationWait();
			}
		}

		private void menuFileSavePng_Click(object sender, System.EventArgs e)
		{
			DialogResult result = savePngFileDialog.ShowDialog();
			if (result==DialogResult.OK) 
			{
				StartApplicationWait();
				FileStream fileStream = (FileStream)savePngFileDialog.OpenFile();
				SavePngImage(fileStream);
				EndApplicationWait();
			}
		}

		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuFileClose_Click(object sender, System.EventArgs e)
		{
			CloseAnalyser();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			ShowAbout();
		}

		private void menuFileSaveSvg_Click(object sender, System.EventArgs e)
		{
			DialogResult result = saveSvgFileDialog.ShowDialog();
			if (result==DialogResult.OK) 
			{
				FileStream fileStream = (FileStream)saveSvgFileDialog.OpenFile();
				SaveSvgImage(fileStream);
			}
		}

		private void menuViewRefresh_Click(object sender, System.EventArgs e)
		{
			UpdateImage();
		}

		private void menuFileMerge_Click(object sender, System.EventArgs e)
		{
			if (_analyser as AssemblyAnalyser==null)
			{
				throw new ApplicationException("cannot merge assembly when analyser is null, or not an AssemblyAnalyser");
			}
			DialogResult result = openFileDialog1.ShowDialog();
			if (result==DialogResult.OK) 
			{
				FileStream fileStream = (FileStream)openFileDialog1.OpenFile();
				string filename = fileStream.Name.ToLower();
				if (!filename.EndsWith(".exe") && !filename.EndsWith(".dll")) 
				{
					MessageBox.Show("Only .dll or .exe assemblies may be merged");
					return;
				}
				Assembly assemblyToMerge = System.Reflection.Assembly.LoadFrom(fileStream.Name);
				fileStream.Close();
				((AssemblyAnalyser)_analyser).ProcessAssembly(assemblyToMerge);

				RefreshAfterGraphModified();
			}
		}

		#endregion

		#region Image handling

		void SavePngImage(FileStream fileStream)
		{
			string fileName = fileStream.Name;
			fileStream.Close();
			dotImageBox.Image.Save(fileName, ImageFormat.Png);
			MessageBox.Show("Image saved.");
		}

		void SaveSvgImage(FileStream fileStream)
		{
			StreamWriter writer = new StreamWriter(fileStream);
			writer.Write(_svgXml);
			writer.Close();
			MessageBox.Show("Image saved.");
		}

		void UpdateImage()
		{
			Image dotImage = null;
			if (_analyser!=null) 
			{
				UpdateExclusionList();
				double aspectRatio = (double)dotImageBox.Width / (double)dotImageBox.Height;
				double widthInches = 100;
				double heightInches = (int)((double)widthInches / aspectRatio);

				// node [color=lightblue2, style=filled];
				// page=""8.5,11"" 
				// size=""7.5, 10""
				// ratio=All
				//				widthInches = 75;
				//				heightInches = 100;
				
				string extraCommands = String.Format(@"size=""{0},{1}""
	center=""""
	ratio=All
	node[width=.25,hight=.375,fontsize=12,color=lightblue2,style=filled]"
					, widthInches, heightInches);
				string dotCommand = _dotCommandBuilder.GenerateDotCommand(_analyser.DependancyGraph, extraCommands);
				dotScriptOutput.Text = dotCommand;
				_dotImage = null;
				dotImage = CreateDotImage(dotCommand);
				messageTextBox.Text = _analyser.GetMessages();
			}
			dotImageBox.Image = dotImage;
			dotImageBox.Invalidate();
		}

		Image CreateDotImage(string dotCommand)
		{
			if (_dotImage==null) 
			{
				// a temp file to store image
				_tempPngImageFileName = _tempFileManager.CreateTemporaryFile();

				// generate dot image
				DOTClass dot = new DOTClass();
				dot.ToPNG(dotCommand).Save(_tempPngImageFileName);
				_dotImage = Image.FromFile(_tempPngImageFileName);

				// generate SVG
				_svgXml = dot.ToSvg(dotCommand);
			}
			return _dotImage;
		}

		#endregion

		#region Exclusion list code

		void UpdateExclusionList()
		{
			foreach (MenuItem menuItem in menuExclude.MenuItems)
			{
				AmendExclusionList(menuItem.Text, menuItem.Checked);
			}
		}

		void AmendExclusionList(string nodeName, bool exclude)
		{
			if (exclude && !_dotCommandBuilder.ExclusionList.Contains(nodeName)) 
			{
				_dotCommandBuilder.ExclusionList.Add(nodeName);
			}
			else if (!exclude && _dotCommandBuilder.ExclusionList.Contains(nodeName)) 
			{
				_dotCommandBuilder.ExclusionList.Remove(nodeName);
			}
		}

		void PopulateExcludeMenu()
		{
			menuExclude.MenuItems.Clear();
			string[] nodeNames = _analyser.DependancyGraph.GetNodes();
			Array.Sort(nodeNames);
			foreach (string nodeName in nodeNames)
			{
				MenuItem excludeMenuItem = new MenuItem(nodeName, new EventHandler(excludeMenu_Click));
				menuExclude.MenuItems.Add(excludeMenuItem);
			}
		}

		private void excludeMenu_Click(object sender, System.EventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			menuItem.Checked = !menuItem.Checked;
		}

		#endregion

		#region Cleanup

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components!=null) 
				{
					components.Dispose();
				}
			}
			
			// delete up our temporary files
			_tempFileManager.DeleteAllTemporaryFiles();

			base.Dispose(disposing);
		}

		#endregion

		#region Help & about

		void ShowAbout()
		{
			MessageBox.Show(".NET Assembly Dependancy Plotter, by Drew Noakes, April 2003\nLatest version at http://www.drewnoakes.com/\nCharts provided using Dot & Wingraphviz");
		}

		#endregion

		#region GUI support and feedback to user

		void StartApplicationWait()
		{
			this.Cursor = Cursors.WaitCursor;
		}

		void EndApplicationWait()
		{
			this.Cursor = Cursors.Arrow;
		}

		#endregion

		#region Printing

		private void menuFilePrint_Click(object sender, EventArgs e)
		{
			PrintDocument printDocument = CreatePrintDocument();
			printDialog.Document = printDocument;
			DialogResult result = printDialog.ShowDialog();
			if (result==DialogResult.OK)
			{
				StartApplicationWait();
				printDocument.Print();
				EndApplicationWait();
			}
		}

		private void menuFilePrintPreview_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = CreatePrintDocument();
			printPreviewDialog.ShowDialog();
		}

		PrintDocument CreatePrintDocument()
		{
			if (_dotImage==null) 
			{
				throw new ApplicationException("unable to create print document when dot image is null");
			}
			return new DependancyGraphPrintDocument(_dotImage);
		}

		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DependancyAnalyserForm());
		}

	}
}
