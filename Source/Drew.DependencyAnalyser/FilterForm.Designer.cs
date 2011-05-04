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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.lblIncludes = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGroup = new System.Windows.Forms.TabPage();
            this.btnRemoveAllGroup = new System.Windows.Forms.Button();
            this.btnSelectAllGroup = new System.Windows.Forms.Button();
            this.clbSubset = new System.Windows.Forms.CheckedListBox();
            this.tpWildCard = new System.Windows.Forms.TabPage();
            this.txtBeginsWith = new System.Windows.Forms.TextBox();
            this.lblBeginsWith = new System.Windows.Forms.Label();
            this.btnRemovePart = new System.Windows.Forms.Button();
            this.btnSelectPart = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.clbIncludes = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpGroup.SuspendLayout();
            this.tpWildCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIncludes
            // 
            this.lblIncludes.AutoSize = true;
            this.lblIncludes.Location = new System.Drawing.Point(12, 9);
            this.lblIncludes.Name = "lblIncludes";
            this.lblIncludes.Size = new System.Drawing.Size(42, 13);
            this.lblIncludes.TabIndex = 13;
            this.lblIncludes.Text = "Include";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpGroup);
            this.tabControl1.Controls.Add(this.tpWildCard);
            this.tabControl1.Location = new System.Drawing.Point(303, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(178, 304);
            this.tabControl1.TabIndex = 12;
            // 
            // tpGroup
            // 
            this.tpGroup.Controls.Add(this.btnRemoveAllGroup);
            this.tpGroup.Controls.Add(this.btnSelectAllGroup);
            this.tpGroup.Controls.Add(this.clbSubset);
            this.tpGroup.Location = new System.Drawing.Point(4, 22);
            this.tpGroup.Name = "tpGroup";
            this.tpGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tpGroup.Size = new System.Drawing.Size(170, 278);
            this.tpGroup.TabIndex = 0;
            this.tpGroup.Text = "Groups";
            this.tpGroup.UseVisualStyleBackColor = true;
            // 
            // btnRemoveAllGroup
            // 
            this.btnRemoveAllGroup.Location = new System.Drawing.Point(92, 238);
            this.btnRemoveAllGroup.Name = "btnRemoveAllGroup";
            this.btnRemoveAllGroup.Size = new System.Drawing.Size(72, 23);
            this.btnRemoveAllGroup.TabIndex = 10;
            this.btnRemoveAllGroup.Text = "Clear All";
            this.btnRemoveAllGroup.UseVisualStyleBackColor = true;
            this.btnRemoveAllGroup.Click += new System.EventHandler(this.btnRemoveAllGroup_Click);
            // 
            // btnSelectAllGroup
            // 
            this.btnSelectAllGroup.Location = new System.Drawing.Point(9, 238);
            this.btnSelectAllGroup.Name = "btnSelectAllGroup";
            this.btnSelectAllGroup.Size = new System.Drawing.Size(68, 23);
            this.btnSelectAllGroup.TabIndex = 9;
            this.btnSelectAllGroup.Text = "&Select All";
            this.btnSelectAllGroup.UseVisualStyleBackColor = true;
            this.btnSelectAllGroup.Click += new System.EventHandler(this.btnSelectAllGroup_Click);
            // 
            // clbSubset
            // 
            this.clbSubset.CheckOnClick = true;
            this.clbSubset.FormattingEnabled = true;
            this.clbSubset.Location = new System.Drawing.Point(16, 18);
            this.clbSubset.Name = "clbSubset";
            this.clbSubset.Size = new System.Drawing.Size(138, 214);
            this.clbSubset.TabIndex = 0;
            // 
            // tpWildCard
            // 
            this.tpWildCard.Controls.Add(this.txtBeginsWith);
            this.tpWildCard.Controls.Add(this.lblBeginsWith);
            this.tpWildCard.Location = new System.Drawing.Point(4, 22);
            this.tpWildCard.Name = "tpWildCard";
            this.tpWildCard.Padding = new System.Windows.Forms.Padding(3);
            this.tpWildCard.Size = new System.Drawing.Size(170, 278);
            this.tpWildCard.TabIndex = 1;
            this.tpWildCard.Text = "WildCards";
            this.tpWildCard.UseVisualStyleBackColor = true;
            // 
            // txtBeginsWith
            // 
            this.txtBeginsWith.Location = new System.Drawing.Point(23, 32);
            this.txtBeginsWith.Name = "txtBeginsWith";
            this.txtBeginsWith.Size = new System.Drawing.Size(126, 20);
            this.txtBeginsWith.TabIndex = 1;
            // 
            // lblBeginsWith
            // 
            this.lblBeginsWith.AutoSize = true;
            this.lblBeginsWith.Location = new System.Drawing.Point(20, 16);
            this.lblBeginsWith.Name = "lblBeginsWith";
            this.lblBeginsWith.Size = new System.Drawing.Size(64, 13);
            this.lblBeginsWith.TabIndex = 0;
            this.lblBeginsWith.Text = "Begins With";
            // 
            // btnRemovePart
            // 
            this.btnRemovePart.Location = new System.Drawing.Point(195, 155);
            this.btnRemovePart.Name = "btnRemovePart";
            this.btnRemovePart.Size = new System.Drawing.Size(85, 23);
            this.btnRemovePart.TabIndex = 11;
            this.btnRemovePart.Text = "Remove Part";
            this.btnRemovePart.UseVisualStyleBackColor = true;
            this.btnRemovePart.Click += new System.EventHandler(this.btnRemovePart_Click);
            // 
            // btnSelectPart
            // 
            this.btnSelectPart.Location = new System.Drawing.Point(195, 116);
            this.btnSelectPart.Name = "btnSelectPart";
            this.btnSelectPart.Size = new System.Drawing.Size(85, 23);
            this.btnSelectPart.TabIndex = 10;
            this.btnSelectPart.Text = "Select Part";
            this.btnSelectPart.UseVisualStyleBackColor = true;
            this.btnSelectPart.Click += new System.EventHandler(this.btnSelectPart_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(195, 69);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(85, 23);
            this.btnRemoveAll.TabIndex = 9;
            this.btnRemoveAll.Text = "&Remove All";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(195, 36);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(85, 23);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "&Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // clbIncludes
            // 
            this.clbIncludes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.clbIncludes.CheckOnClick = true;
            this.clbIncludes.FormattingEnabled = true;
            this.clbIncludes.Location = new System.Drawing.Point(15, 25);
            this.clbIncludes.Name = "clbIncludes";
            this.clbIncludes.Size = new System.Drawing.Size(157, 304);
            this.clbIncludes.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(399, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(316, 335);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FilterForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(493, 368);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblIncludes);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnRemovePart);
            this.Controls.Add(this.btnSelectPart);
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.clbIncludes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FilterForm";
            this.Text = "Filter";
            this.tabControl1.ResumeLayout(false);
            this.tpGroup.ResumeLayout(false);
            this.tpWildCard.ResumeLayout(false);
            this.tpWildCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIncludes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGroup;
        private System.Windows.Forms.CheckedListBox clbSubset;
        private System.Windows.Forms.TabPage tpWildCard;
        private System.Windows.Forms.TextBox txtBeginsWith;
        private System.Windows.Forms.Label lblBeginsWith;
        private System.Windows.Forms.Button btnRemovePart;
        private System.Windows.Forms.Button btnSelectPart;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckedListBox clbIncludes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRemoveAllGroup;
        private System.Windows.Forms.Button btnSelectAllGroup;
    }
}