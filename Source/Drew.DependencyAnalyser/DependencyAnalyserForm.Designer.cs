using System.Windows.Forms;

namespace Drew.DependencyAnalyser
{
    partial class DependencyAnalyserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            // delete up our temporary files
            TemporaryFileManager.DeleteAllTemporaryFiles();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.MenuItem _mnuFile;
            System.Windows.Forms.MenuItem _mnuFileOpen;
            System.Windows.Forms.MenuItem _mnuFileSeparator;
            System.Windows.Forms.MenuItem _mnuFileExit;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DependencyAnalyserForm));
            this._mnuFileMerge = new System.Windows.Forms.MenuItem();
            this._mnuFileSavePng = new System.Windows.Forms.MenuItem();
            this._mnuFileSaveSvg = new System.Windows.Forms.MenuItem();
            this._mnuFilePrint = new System.Windows.Forms.MenuItem();
            this._mnuFilePrintPreview = new System.Windows.Forms.MenuItem();
            this._mnuAbout = new System.Windows.Forms.MenuItem();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._menu = new System.Windows.Forms.MainMenu(this.components);
            this._mnuFilter = new System.Windows.Forms.MenuItem();
            this._savePngFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._saveSvgFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this._printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this._tabPageMessages = new System.Windows.Forms.TabPage();
            this._txtMessage = new System.Windows.Forms.TextBox();
            this._tabPageDotOutput = new System.Windows.Forms.TabPage();
            this._txtDotScriptOutput = new System.Windows.Forms.TextBox();
            this._tabPageImage = new System.Windows.Forms.TabPage();
            this._imgDotDiagram = new System.Windows.Forms.PictureBox();
            this._tabControl = new System.Windows.Forms.TabControl();
            _mnuFile = new System.Windows.Forms.MenuItem();
            _mnuFileOpen = new System.Windows.Forms.MenuItem();
            _mnuFileSeparator = new System.Windows.Forms.MenuItem();
            _mnuFileExit = new System.Windows.Forms.MenuItem();
            this._tabPageMessages.SuspendLayout();
            this._tabPageDotOutput.SuspendLayout();
            this._tabPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._imgDotDiagram)).BeginInit();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mnuFile
            // 
            _mnuFile.Index = 0;
            _mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            _mnuFileOpen,
            this._mnuFileMerge,
            this._mnuFileSavePng,
            this._mnuFileSaveSvg,
            this._mnuFilePrint,
            this._mnuFilePrintPreview,
            _mnuFileSeparator,
            _mnuFileExit});
            _mnuFile.Text = "&File";
            // 
            // _mnuFileOpen
            // 
            _mnuFileOpen.Index = 0;
            _mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            _mnuFileOpen.Text = "&Open...";
            _mnuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // _mnuFileMerge
            // 
            this._mnuFileMerge.Index = 1;
            this._mnuFileMerge.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this._mnuFileMerge.Text = "Merge...";
            this._mnuFileMerge.Click += new System.EventHandler(this.menuFileMerge_Click);
            // 
            // _mnuFileSavePng
            // 
            this._mnuFileSavePng.Index = 2;
            this._mnuFileSavePng.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this._mnuFileSavePng.Text = "Save &PNG...";
            this._mnuFileSavePng.Click += new System.EventHandler(this.menuFileSavePng_Click);
            // 
            // _mnuFileSaveSvg
            // 
            this._mnuFileSaveSvg.Index = 3;
            this._mnuFileSaveSvg.Text = "Save &SVG...";
            this._mnuFileSaveSvg.Click += new System.EventHandler(this.menuFileSaveSvg_Click);
            // 
            // _mnuFilePrint
            // 
            this._mnuFilePrint.Index = 4;
            this._mnuFilePrint.Text = "Print...";
            this._mnuFilePrint.Click += new System.EventHandler(this.menuFilePrint_Click);
            // 
            // _mnuFilePrintPreview
            // 
            this._mnuFilePrintPreview.Index = 5;
            this._mnuFilePrintPreview.Text = "Print preview...";
            this._mnuFilePrintPreview.Click += new System.EventHandler(this.menuFilePrintPreview_Click);
            // 
            // _mnuFileSeparator
            // 
            _mnuFileSeparator.Index = 6;
            _mnuFileSeparator.Text = "-";
            // 
            // _mnuFileExit
            // 
            _mnuFileExit.Index = 7;
            _mnuFileExit.Text = "E&xit";
            _mnuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // _mnuAbout
            // 
            this._mnuAbout.Index = 2;
            this._mnuAbout.Text = "About...";
            this._mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "All supported files|*.sln;*.exe;*.dll|Solution files (*.sln)|*.sln|Assemblies (*." +
                "dll)|*.dll|Assemblies (*.exe)|*.exe|All files|*.exe";
            this._openFileDialog.Title = "Open File";
            // 
            // _menu
            // 
            this._menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            _mnuFile,
            this._mnuFilter,
            this._mnuAbout});
            // 
            // _mnuFilter
            // 
            this._mnuFilter.Index = 1;
            this._mnuFilter.Text = "F&ilter...";
            this._mnuFilter.Click += new System.EventHandler(this.menuFilter_Click);
            // 
            // _savePngFileDialog
            // 
            this._savePngFileDialog.DefaultExt = "png";
            this._savePngFileDialog.FileName = "DependencyGraph.png";
            this._savePngFileDialog.Filter = "PNG Images|*.png|All files|*.*";
            // 
            // _saveSvgFileDialog
            // 
            this._saveSvgFileDialog.FileName = "DependencyGraph.svg";
            this._saveSvgFileDialog.Filter = "SVG files (*.svg)|*.svg|All files|*.*";
            // 
            // _printPreviewDialog
            // 
            this._printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this._printPreviewDialog.Enabled = true;
            this._printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDialog.Icon")));
            this._printPreviewDialog.Name = "_printPreviewDialog";
            this._printPreviewDialog.Visible = false;
            // 
            // _tabPageMessages
            // 
            this._tabPageMessages.Controls.Add(this._txtMessage);
            this._tabPageMessages.Location = new System.Drawing.Point(4, 4);
            this._tabPageMessages.Name = "_tabPageMessages";
            this._tabPageMessages.Size = new System.Drawing.Size(680, 343);
            this._tabPageMessages.TabIndex = 2;
            this._tabPageMessages.Text = "Messages";
            // 
            // _txtMessage
            // 
            this._txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtMessage.Location = new System.Drawing.Point(0, 0);
            this._txtMessage.Multiline = true;
            this._txtMessage.Name = "_txtMessage";
            this._txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtMessage.Size = new System.Drawing.Size(680, 343);
            this._txtMessage.TabIndex = 0;
            // 
            // _tabPageDotOutput
            // 
            this._tabPageDotOutput.Controls.Add(this._txtDotScriptOutput);
            this._tabPageDotOutput.Location = new System.Drawing.Point(4, 4);
            this._tabPageDotOutput.Name = "_tabPageDotOutput";
            this._tabPageDotOutput.Size = new System.Drawing.Size(680, 343);
            this._tabPageDotOutput.TabIndex = 0;
            this._tabPageDotOutput.Text = "Dot Output";
            // 
            // _txtDotScriptOutput
            // 
            this._txtDotScriptOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtDotScriptOutput.Location = new System.Drawing.Point(0, 0);
            this._txtDotScriptOutput.Multiline = true;
            this._txtDotScriptOutput.Name = "_txtDotScriptOutput";
            this._txtDotScriptOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtDotScriptOutput.Size = new System.Drawing.Size(680, 343);
            this._txtDotScriptOutput.TabIndex = 0;
            // 
            // _tabPageImage
            // 
            this._tabPageImage.Controls.Add(this._imgDotDiagram);
            this._tabPageImage.Location = new System.Drawing.Point(4, 4);
            this._tabPageImage.Name = "_tabPageImage";
            this._tabPageImage.Size = new System.Drawing.Size(680, 343);
            this._tabPageImage.TabIndex = 1;
            this._tabPageImage.Text = "Image";
            // 
            // _imgDotDiagram
            // 
            this._imgDotDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imgDotDiagram.Location = new System.Drawing.Point(0, 0);
            this._imgDotDiagram.Name = "_imgDotDiagram";
            this._imgDotDiagram.Size = new System.Drawing.Size(680, 343);
            this._imgDotDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._imgDotDiagram.TabIndex = 0;
            this._imgDotDiagram.TabStop = false;
            // 
            // _tabControl
            // 
            this._tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this._tabControl.Controls.Add(this._tabPageImage);
            this._tabControl.Controls.Add(this._tabPageDotOutput);
            this._tabControl.Controls.Add(this._tabPageMessages);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(688, 369);
            this._tabControl.TabIndex = 0;
            // 
            // DependencyAnalyserForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(688, 369);
            this.Controls.Add(this._tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this._menu;
            this.Name = "DependencyAnalyserForm";
            this.Text = ".NET Assembly Dependency Analyser";
            this._tabPageMessages.ResumeLayout(false);
            this._tabPageMessages.PerformLayout();
            this._tabPageDotOutput.ResumeLayout(false);
            this._tabPageDotOutput.PerformLayout();
            this._tabPageImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._imgDotDiagram)).EndInit();
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _savePngFileDialog;
        private SaveFileDialog _saveSvgFileDialog;
        private PrintDialog _printDialog;
        private PrintPreviewDialog _printPreviewDialog;

        private MainMenu _menu;
        private MenuItem _mnuAbout;
        private MenuItem _mnuFileSavePng;
        private MenuItem _mnuFileSaveSvg;
        private MenuItem _mnuFilePrint;
        private MenuItem _mnuFilePrintPreview;
        private MenuItem _mnuFileMerge;
        private MenuItem _mnuFilter;
        private TabPage _tabPageMessages;
        private TextBox _txtMessage;
        private TabPage _tabPageDotOutput;
        private TextBox _txtDotScriptOutput;
        private TabPage _tabPageImage;
        private PictureBox _imgDotDiagram;
        private TabControl _tabControl;

        #endregion

    }
}