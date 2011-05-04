using System;
using System.Diagnostics;
using System.Windows.Forms;
using Drew.DependencyAnalyser.Controls;

// ReSharper disable InconsistentNaming

namespace Drew.DependencyAnalyser
{
    public sealed partial class FilterForm : Form
    {
        private readonly AssemblyFilterPreferences _filterPreferences;

        public FilterForm(AssemblyFilterPreferences filterPreferences)
        {
            _filterPreferences = filterPreferences;

            InitializeComponent();

            // populate tree
            // create any 'parent' nodes first
            foreach (var name in _filterPreferences.GetAllNames())
            {
                var bits = name.Split('.');
                var parent = _tree.Nodes;
                if (bits.Length > 1)
                {
                    for (var i = 0; i < bits.Length - 1; i++)
                    {
                        var bit = bits[i];
                        var bitNode = parent.ContainsKey(bit)
                            ? parent[bit]
                            : parent.Add(bit, bit);
                        parent = bitNode.Nodes;
                    }
                }
            }
            // create leaf nodes
            foreach (var name in _filterPreferences.GetAllNames())
            {
                var bits = name.Split('.');
                var parent = _tree.Nodes;
                for (var i = 0; i < bits.Length - 1; i++)
                    parent = parent[bits[i]].Nodes;
                var leafName = bits[bits.Length - 1];
                TreeNode leafNode;
                if (parent.ContainsKey(leafName))
                {
                    leafNode = parent[leafName].Nodes.Insert(0, leafName, "<exactly>");
                    leafNode.ToolTipText = string.Format("Select's {0} without child namespaces", name);
                }
                else
                {
                    leafNode = parent.Add(leafName, leafName);
                }
                Debug.Assert(leafNode.Tag == null);
                leafNode.Tag = name;
            }

            // expand, one level deep only
            foreach (TreeNode node in _tree.Nodes)
                node.Expand();

            // select nodes which are included
            foreach (var name in _filterPreferences.GetIncludedNames())
                _tree.CheckNodeByTag(name, TriStateTreeView.CheckState.Checked);
        }

        #region Button Click Handlers

        private void btnOK_Click(object sender, EventArgs e)
        {
            // apply changes to the filter preferences object
            foreach (var name in _filterPreferences.GetAllNames())
            {
                var node = _tree.FindNodeByTag(name);
                Debug.Assert(node != null);
                var include = _tree.GetChecked(node) != TriStateTreeView.CheckState.Unchecked;
                _filterPreferences.SetInclusion(name, include);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode rootNode in _tree.Nodes)
                _tree.SetChecked(rootNode, TriStateTreeView.CheckState.Checked);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (TreeNode rootNode in _tree.Nodes)
                _tree.SetChecked(rootNode, TriStateTreeView.CheckState.Unchecked);
        }

        #endregion
    }
}