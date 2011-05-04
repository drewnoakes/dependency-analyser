using System;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser
{
    public partial class DependencyAnalyserForm : Form
    {
        private readonly AssemblyFilterPreferences _filterPreferences = new AssemblyFilterPreferences();
        private readonly DependencyPlotter _plotter = new DependencyPlotter();
        private DependencyAnalyserBase _analyser;
        private PlotResult _plotResult;

        public DependencyAnalyserForm()
        {
            InitializeComponent();
            EnableAndDisableMenuItems();
        }

        private void EnableAndDisableMenuItems()
        {
            var analyserAvailable = (_analyser != null);

            _mnuFileSavePng.Enabled = analyserAvailable;
            _mnuFileSaveSvg.Enabled = analyserAvailable;
            _mnuFilter.Enabled = analyserAvailable;
            _mnuFilePrint.Enabled = analyserAvailable;
            _mnuFilePrintPreview.Enabled = analyserAvailable;
            _mnuFileMerge.Enabled = analyserAvailable && _analyser is AssemblyAnalyser;
        }

        #region Event handlers

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (WaitCursor())
            using (var fileStream = (FileStream)_openFileDialog.OpenFile())
            {
                _analyser = fileStream.Name.EndsWith(".sln")
                                ? (DependencyAnalyserBase)new SolutionFileAnalyser(fileStream)
                                : new AssemblyAnalyser(Assembly.LoadFrom(fileStream.Name));

                _filterPreferences.SetAssemblyNames(_analyser.DependencyGraph.GetNodes());
                EnableAndDisableMenuItems();
                UpdateImage();
            }
        }

        private void menuFileMerge_Click(object sender, EventArgs e)
        {
            if (_analyser as AssemblyAnalyser == null)
                throw new Exception("cannot merge assembly when analyser is null, or not an AssemblyAnalyser");

            if (_openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (WaitCursor())
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

                _filterPreferences.SetAssemblyNames(_analyser.DependencyGraph.GetNodes());
                EnableAndDisableMenuItems();
                UpdateImage();
            }
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

        private void menuFileSaveSvg_Click(object sender, EventArgs e)
        {
            if (_saveSvgFileDialog.ShowDialog() != DialogResult.OK)
                return;

            using (var fileStream = (FileStream)_saveSvgFileDialog.OpenFile())
                SaveSvgImage(fileStream);
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            // TODO make this a dialog with a link button
            MessageBox.Show(string.Format(".NET Assembly Dependency Analyser v{0}\n\nCopyright Drew Noakes 2003-{1}\n\nThanks to John Maher\n\nLatest version at http://drewnoakes.com/code/dependency-analyser/\nCharts provided using Dot & Wingraphviz", 
                                          Assembly.GetExecutingAssembly().GetName().Version,
                                          DateTime.Now.Year));
        }

        private void menuFilter_Click(object sender, EventArgs e)
        {
            using (WaitCursor())
            {
                var filterForm = new FilterForm(_filterPreferences);

                if (filterForm.ShowDialog() == DialogResult.OK)
                    UpdateImage();
            }
        }

        #endregion

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            
            TemporaryFileManager.DeleteAllTemporaryFiles();
        }

        #region Image handling

        private void SavePngImage(Stream fileStream)
        {
            _imgDotDiagram.Image.Save(fileStream, ImageFormat.Png);
            MessageBox.Show("PNG file saved.");
        }

        private void SaveSvgImage(Stream fileStream)
        {
            using (var writer = new StreamWriter(fileStream))
                writer.Write(_plotResult.SvgXml);
            MessageBox.Show("SVG file saved.");
        }

        private void UpdateImage()
        {
            var aspectRatio = _imgDotDiagram.Width / (double)_imgDotDiagram.Height;

            _plotResult = _analyser != null
                ? _plotter.CalculatePlot(aspectRatio, _analyser.DependencyGraph, _filterPreferences) 
                : new PlotResult();

            _txtMessage.Text = _analyser!=null ? _analyser.GetMessages() : null;
            _txtDotScriptOutput.Text = _plotResult.DotCommand;
            _imgDotDiagram.Image = _plotResult.Image;

            _imgDotDiagram.Invalidate();
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
            if (_plotResult.Image == null)
                throw new ApplicationException("unable to create print document when dot image is null");

            return new DependencyGraphPrintDocument(_plotResult.Image);
        }

        #endregion
    }
}