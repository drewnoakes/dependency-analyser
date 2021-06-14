using System;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DependencyAnalyser
{
    public partial class DependencyAnalyserForm : Form
    {
        private readonly FilterPreferences _filterPreferences = new();
        private readonly StringBuilderLogger _logger = new();

        private DependencyGraph<string> _graph = new();
        private PlotResult? _plotResult;

        public DependencyAnalyserForm()
        {
            InitializeComponent();
            EnableAndDisableMenuItems();
        }

        private void EnableAndDisableMenuItems()
        {
            var hasGraph = _graph.Nodes.Any();

            _mnuFileSavePng.Enabled = hasGraph;
            _mnuFileSaveSvg.Enabled = hasGraph;
            _mnuFilter.Enabled = hasGraph;
            _mnuFilePrint.Enabled = hasGraph;
            _mnuFilePrintPreview.Enabled = hasGraph;
            _mnuFileMerge.Enabled = hasGraph;
        }

        #region Event handlers

        private async void menuFileOpen_Click(object sender, EventArgs e)
        {
            // Start a new graph
            _graph = new DependencyGraph<string>();

            await MergeFileAsync(_openFileDialog.FileName);
        }

        private async void menuFileMerge_Click(object sender, EventArgs e)
        {
            await MergeFileAsync(_openFileDialog.FileName);
        }

        private async Task MergeFileAsync(string fileName)
        {
            try
            {
                if (_openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                using (WaitCursor())
                {
                    if (fileName.EndsWith(".sln"))
                    {
                        await SolutionFileAnalyser.AnalyseAsync(fileName, _graph, _logger);
                    }
                    else
                    {
                        var assembly = Assembly.LoadFrom(fileName);
                        AssemblyAnalyser.Analyze(assembly, _graph, _logger);
                    }

                    _filterPreferences.SetAssemblyNames(_graph.Nodes);
                    EnableAndDisableMenuItems();
                    UpdateImage();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
            MessageBox.Show($".NET Assembly Dependency Analyser v{Assembly.GetExecutingAssembly().GetName().Version}\n\nCopyright Drew Noakes 2003-{DateTime.Now.Year}.\n\nThanks to John Maher.\n\nLatest version at http://drewnoakes.com/code/dependency-analyser/\nCharts provided using Dot & Wingraphviz.");
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
            if (_plotResult?.SvgXml == null)
                throw new Exception("Unable to save SVG image.");
            
            using (var writer = new StreamWriter(fileStream))
                writer.Write(_plotResult.SvgXml);
            MessageBox.Show("SVG file saved.");
        }

        private void UpdateImage()
        {
            var aspectRatio = _imgDotDiagram.Width / (double)_imgDotDiagram.Height;

            _plotResult = DependencyPlotter.CalculatePlot(aspectRatio, _graph, _filterPreferences);

            _txtMessage.Text = _logger.ToString();
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
            if (_plotResult?.Image == null)
                throw new Exception("unable to create print document when dot image is null");

            return new DependencyGraphPrintDocument(_plotResult.Image);
        }

        #endregion
    }
}