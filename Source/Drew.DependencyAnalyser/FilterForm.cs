using System;
using System.Collections;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser
{
    public sealed partial class FilterForm : Form
    {
        private const int ListTabIndex = 0;
        private const int PrefixTabIndex = 1;

        private readonly Hashtable _groups;
        private readonly DependencyAnalyserForm _callingForm;

        public FilterForm(DependencyAnalyserForm caller)
        {
            InitializeComponent();

            _callingForm = caller;
            _groups = new Hashtable();
        }

        public void AddDependency(string strDependency)
        {
            AddAndCheck(clbIncludes, strDependency);
            AddGroupIfNeeded(strDependency);
        }

        public void ClearDependencies()
        {
            clbIncludes.Items.Clear();
        }

        #region Button Click Handlers

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancel();
        }

        private void OnCancel()
        {
            OnAccept();
        }

        private void OnAccept()
        {
            Hide();
            for (var r = 0; r < clbIncludes.Items.Count; r++)
            {
                var dependency = clbIncludes.Items[r].ToString().ToUpper();
                //Hardcoded index into the "Exclude" menu. 
                foreach (MenuItem menuCheck in _callingForm.Menu.MenuItems[2].MenuItems)//2 is the index into the "Exclude" menu. 
                {
                    if (menuCheck.Text.ToUpper() == dependency)
                        clbIncludes.SetItemChecked(r, !menuCheck.Checked);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Hide();

            for (var r = 0; r < clbIncludes.Items.Count; r++)
            {
                var isChecked = clbIncludes.GetItemChecked(r);
                var dependency = clbIncludes.Items[r].ToString().ToUpper();
                //Hardcoded index into the "Exclude" menu. 
                foreach (MenuItem menuCheck in _callingForm.Menu.MenuItems[2].MenuItems)//2 is the index into the "Exclude" menu. 
                {
                    if (menuCheck.Text.ToUpper() == dependency)
                        menuCheck.Checked = !isChecked;
                }
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            CheckUncheckAll(clbIncludes, false);
        }

        private void btnRemoveAllGroup_Click(object sender, EventArgs e)
        {
            CheckUncheckAll(clbSubset, false);
        }

        private void btnRemovePart_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case ListTabIndex:
                    for (var r = 0; r < clbSubset.Items.Count; r++)
                        if (clbSubset.GetItemChecked(r))
                            CheckUncheckAllDependencies(clbSubset.Items[r].ToString(), false);
                    break;
                case PrefixTabIndex:
                    var prefix = txtBeginsWith.Text;
                    ScanList(prefix, false);
                    break;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            CheckUncheckAll(clbIncludes, true);
        }

        private void btnSelectAllGroup_Click(object sender, EventArgs e)
        {
            CheckUncheckAll(clbSubset, true);
        }

        private void btnSelectPart_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case ListTabIndex:
                    for (var r = 0; r < clbSubset.Items.Count; r++)
                    {
                        if (clbSubset.GetItemChecked(r))
                            CheckUncheckAllDependencies(clbSubset.Items[r].ToString(), true);
                    }
                    break;
                case PrefixTabIndex:
                    var prefix = txtBeginsWith.Text;
                    ScanList(prefix, true);
                    break;
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Don't actually let the app close this form.  Instead, we hide it.
            OnCancel();
            e.Cancel = true;
        }

        private static void AddAndCheck(CheckedListBox clb, string strItem)
        {
            clb.Items.Add(strItem);
            clb.SetItemChecked(clb.Items.Count - 1, true);
        }

        private void AddGroupIfNeeded(string strDependency)
        {
            var intIndex = strDependency.IndexOf(".");
            var strKey = intIndex == -1 ? strDependency : strDependency.Substring(0, intIndex);

            if (!_groups.ContainsKey(strKey))
            {
                _groups.Add(strKey, strDependency);
                AddAndCheck(clbSubset, strKey);
            }
        }

        private void CheckUncheckAllDependencies(string strBase, bool isChecked)
        {
            for (var r = 0; r < clbIncludes.Items.Count; r++)
            {
                var dependency = clbIncludes.Items[r].ToString();
                if (dependency.Length >= strBase.Length && dependency.Substring(0, strBase.Length) == strBase)
                    clbIncludes.SetItemChecked(r, isChecked);
            }
        }

        private void CheckUncheckDependency(int intIndex, string strDependency, string strPrefix, bool isChecked)
        {
            if (strDependency.Length >= strPrefix.Length && strDependency.Substring(0, strPrefix.Length).ToUpper() == strPrefix.ToUpper())
                clbIncludes.SetItemChecked(intIndex, isChecked);
        }

        private static void CheckUncheckAll(CheckedListBox clb, bool isChecked)
        {
            for (var r = 0; r < clb.Items.Count; r++)
                clb.SetItemChecked(r, isChecked);
        }

        private void ScanList(string strPrefix, bool isChecked)
        {
            for (var r = 0; r < clbIncludes.Items.Count; r++)
            {
                var dependency = clbIncludes.Items[r].ToString();
                CheckUncheckDependency(r, dependency, strPrefix, isChecked);
            }
        }
    }
}