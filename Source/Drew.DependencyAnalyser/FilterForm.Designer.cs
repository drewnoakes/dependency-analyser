using Drew.DependencyAnalyser.Controls;

namespace Drew.DependencyAnalyser
{
    public partial class FilterForm
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._btnSelectNone = new System.Windows.Forms.Button();
            this._btnSelectAll = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this._tree = new Drew.DependencyAnalyser.Controls.TriStateTreeView();
            this.SuspendLayout();
            // 
            // _btnSelectNone
            // 
            this._btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnSelectNone.Location = new System.Drawing.Point(93, 414);
            this._btnSelectNone.Name = "_btnSelectNone";
            this._btnSelectNone.Size = new System.Drawing.Size(75, 23);
            this._btnSelectNone.TabIndex = 9;
            this._btnSelectNone.Text = "Select &None";
            this._btnSelectNone.UseVisualStyleBackColor = true;
            this._btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // _btnSelectAll
            // 
            this._btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnSelectAll.Location = new System.Drawing.Point(12, 414);
            this._btnSelectAll.Name = "_btnSelectAll";
            this._btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this._btnSelectAll.TabIndex = 8;
            this._btnSelectAll.Text = "Select &All";
            this._btnSelectAll.UseVisualStyleBackColor = true;
            this._btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(261, 414);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 14;
            this._btnCancel.Text = "&Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOK.Location = new System.Drawing.Point(180, 414);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 15;
            this._btnOK.Text = "&OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // _tree
            // 
            this._tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tree.ImageIndex = 1;
            this._tree.Location = new System.Drawing.Point(12, 12);
            this._tree.Name = "_tree";
            this._tree.SelectedImageIndex = 1;
            this._tree.Size = new System.Drawing.Size(324, 396);
            this._tree.TabIndex = 16;
            // 
            // FilterForm
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(348, 444);
            this.Controls.Add(this._tree);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnSelectNone);
            this.Controls.Add(this._btnSelectAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(364, 150);
            this.Name = "FilterForm";
            this.Text = "Filter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnSelectNone;
        private System.Windows.Forms.Button _btnSelectAll;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
        private TriStateTreeView _tree;
    }
}