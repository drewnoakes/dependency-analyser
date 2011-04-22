using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WINGRAPHVIZLib;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser
{
    public partial class DependencyAnalyserForm : Form
    {
        private readonly FilterForm _filterForm;
        private string _tempPngImageFileName;
        private string _svgXml;
        private Image _dotImage;

        private DependencyAnalyserBase _analyser;
        private readonly DotCommandBuilder _dotCommandBuilder;
        private readonly TemporaryFileManager _tempFileManager = new TemporaryFileManager();

        public DependencyAnalyserForm()
        {
            _dotCommandBuilder = new DotCommandBuilder();
            _filterForm = new FilterForm(this);
            InitializeComponent();
            EnableAndDisableMenuItems();
        }

        private void EnableAndDisableMenuItems()
        {
            bool analyserAvailable = (_analyser != null);

            _mnuFileSavePng.Enabled = analyserAvailable;
            _mnuFileSaveSvg.Enabled = analyserAvailable;
            _mnuFileClose.Enabled = analyserAvailable;
            _mnuExclude.Enabled = analyserAvailable;
            _mnuFilter.Enabled = analyserAvailable;
            _mnuViewRefresh.Enabled = analyserAvailable;
            _mnuFilePrint.Enabled = analyserAvailable;
            _mnuFilePrintPreview.Enabled = analyserAvailable;
            _mnuFileMerge.Enabled = analyserAvailable && _analyser is AssemblyAnalyser;

            if (!analyserAvailable)
            {
                _mnuExclude.MenuItems.Clear();
                _filterForm.ClearDependencies();
            }
        }

        private void CloseAnalyser()
        {
            _analyser = null;
            _dotImage = null;
            _svgXml = null;
            UpdateImage();
            _txtDotScriptOutput.Text = String.Empty;
            _txtMessage.Text = String.Empty;
            EnableAndDisableMenuItems();
        }

        private void OpenFile(FileStream fileStream)
        {
            _analyser = fileStream.Name.EndsWith(".sln")
                            ? (DependencyAnalyserBase)new SolutionFileAnalyser(fileStream)
                            : new AssemblyAnalyser(Assembly.LoadFrom(fileStream.Name));

            RefreshAfterGraphModified();
        }

        private void RefreshAfterGraphModified()
        {
            PopulateExcludeMenu();
            EnableAndDisableMenuItems();
            UpdateImage();
        }

        #region Event handlers

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (WaitCursor())
            using (var fileStream = (FileStream)_openFileDialog.OpenFile())
                OpenFile(fileStream);
        }

        private void menuFileSavePng_Click(object sender, EventArgs e)
        {
            if (_savePngFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (WaitCursor())
            using (var fileStream = (FileStream)_savePngFileDialog.OpenFile())
                SavePngImage(fileStream);
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuFileClose_Click(object sender, EventArgs e)
        {
            CloseAnalyser();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void menuFileSaveSvg_Click(object sender, EventArgs e)
        {
            if (_saveSvgFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var fileStream = (FileStream)_saveSvgFileDialog.OpenFile())
                SaveSvgImage(fileStream);
        }

        private void menuViewRefresh_Click(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void menuFileMerge_Click(object sender, EventArgs e)
        {
            if (_analyser as AssemblyAnalyser == null)
                throw new Exception("cannot merge assembly when analyser is null, or not an AssemblyAnalyser");

            if (_openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var fileStream = (FileStream)_openFileDialog.OpenFile())
            {
                var filename = fileStream.Name.ToLower();
                if (!filename.EndsWith(".exe") && !filename.EndsWith(".dll"))
                {
                    MessageBox.Show("Only .dll or .exe assemblies may be merged");
                    return;
                }
                var assemblyToMerge = Assembly.LoadFrom(fileStream.Name);
                ((AssemblyAnalyser)_analyser).ProcessAssembly(assemblyToMerge);
            }

            RefreshAfterGraphModified();
        }

        #endregion

        #region Image handling

        private void SavePngImage(Stream fileStream)
        {
            _imgDotDiagram.Image.Save(fileStream, ImageFormat.Png);
            MessageBox.Show("Image saved.");
        }

        private void SaveSvgImage(Stream fileStream)
        {
            using (var writer = new StreamWriter(fileStream))
                writer.Write(_svgXml);
            MessageBox.Show("Image saved.");
        }

        private void UpdateImage()
        {
            Image dotImage = null;
            if (_analyser != null)
            {
                UpdateExclusionList();
                var aspectRatio = _imgDotDiagram.Width/(double)_imgDotDiagram.Height;
                const double widthInches = 100;
                var heightInches = (double)(int)(widthInches/aspectRatio);

                // node [color=lightblue2, style=filled];
                // page=""8.5,11"" 
                // size=""7.5, 10""
                // ratio=All
                //                widthInches = 75;
                //                heightInches = 100;

                var extraCommands = string.Format(@"size=""{0},{1}""
    center=""""
    ratio=All
    node[width=.25,hight=.375,fontsize=12,color=lightblue2,style=filled]"
                                                  , widthInches, heightInches);
                var dotCommand = _dotCommandBuilder.GenerateDotCommand(_analyser.DependencyGraph, extraCommands);
                _txtDotScriptOutput.Text = dotCommand;
                _dotImage = null;
                dotImage = CreateDotImage(dotCommand);
                _txtMessage.Text = _analyser.GetMessages();
            }
            _imgDotDiagram.Image = dotImage;
            _imgDotDiagram.Invalidate();
        }

        private Image CreateDotImage(string dotCommand)
        {
            if (_dotImage == null)
            {
                // a temp file to store image
                _tempPngImageFileName = _tempFileManager.CreateTemporaryFile();

                // generate dot image
                var dot = new DOTClass();
                dot.ToPNG(dotCommand).Save(_tempPngImageFileName);
                _dotImage = Image.FromFile(_tempPngImageFileName);

                // generate SVG
                _svgXml = dot.ToSvg(dotCommand);
            }
            return _dotImage;
        }

        #endregion

        #region Exclusion list code

        private void UpdateExclusionList()
        {
            foreach (MenuItem menuItem in _mnuExclude.MenuItems)
                AmendExclusionList(menuItem.Text, menuItem.Checked);
        }

        private void AmendExclusionList(string nodeName, bool exclude)
        {
            var isExcluded = _dotCommandBuilder.ExclusionList.Contains(nodeName);

            if (exclude && !isExcluded)
                _dotCommandBuilder.ExclusionList.Add(nodeName);
            else if (!exclude && isExcluded)
                _dotCommandBuilder.ExclusionList.Remove(nodeName);
        }

        private void PopulateExcludeMenu()
        {
            _mnuExclude.MenuItems.Clear();
            _filterForm.ClearDependencies();
            var nodeNames = _analyser.DependencyGraph.GetNodes().OrderBy(s => s);
            foreach (var nodeName in nodeNames)
            {
                var excludeMenuItem = new MenuItem(nodeName, excludeMenu_Click);
                _mnuExclude.MenuItems.Add(excludeMenuItem);
                //Add to the include dialog
                _filterForm.AddDependency(nodeName);
            }
        }

        private static void excludeMenu_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
        }

        #endregion

        #region Help & about

        private static void ShowAbout()
        {
            MessageBox.Show(string.Format(".NET Assembly Dependency Analyser v{0}\n\nCopyright Drew Noakes 2003-{1}\n\nThanks to John Maher\n\nLatest version at http://www.drewnoakes.com/code/\nCharts provided using Dot & Wingraphviz", 
                Assembly.GetExecutingAssembly().GetName().Version,
                DateTime.Now.Year));
        }

        #endregion

        #region GUI support and feedback to user

        private IDisposable WaitCursor()
        {
            var reverter = new CursorReverter(Cursor, this);
            Cursor = Cursors.WaitCursor;
            return reverter;
        }

        private sealed class CursorReverter : IDisposable
        {
            private readonly Cursor _cursor;
            private readonly Form _form;

            public CursorReverter(Cursor cursor, Form form)
            {
                _cursor = cursor;
                _form = form;
            }

            public void Dispose()
            {
                _form.Cursor = _cursor;
            }
        }

        #endregion

        #region Printing

        private void menuFilePrint_Click(object sender, EventArgs e)
        {
            var printDocument = CreatePrintDocument();
            
            _printDialog.Document = printDocument;

            if (_printDialog.ShowDialog() != DialogResult.OK)
                return;

            using (WaitCursor())
                printDocument.Print();
        }

        private void menuFilePrintPreview_Click(object sender, EventArgs e)
        {
            using (WaitCursor())
            {
                _printPreviewDialog.Document = CreatePrintDocument();
                _printPreviewDialog.ShowDialog();
            }
        }

        private PrintDocument CreatePrintDocument()
        {
            if (_dotImage == null)
                throw new ApplicationException("unable to create print document when dot image is null");

            return new DependencyGraphPrintDocument(_dotImage);
        }

        #endregion

        private void menuFilter_Click(object sender, EventArgs e)
        {
            using (WaitCursor())
                _filterForm.ShowDialog();
        }
    }
}