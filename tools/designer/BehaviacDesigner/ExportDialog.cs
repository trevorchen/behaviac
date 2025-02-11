////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2009, Daniel Kollmann
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are permitted
// provided that the following conditions are met:
//
// - Redistributions of source code must retain the above copyright notice, this list of conditions
//   and the following disclaimer.
//
// - Redistributions in binary form must reproduce the above copyright notice, this list of
//   conditions and the following disclaimer in the documentation and/or other materials provided
//   with the distribution.
//
// - Neither the name of Daniel Kollmann nor the names of its contributors may be used to endorse
//   or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY
// WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
////////////////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// The above software in this distribution may have been modified by THL A29 Limited ("Tencent Modifications").
//
// All Tencent Modifications are Copyright (C) 2015 THL A29 Limited.
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Behaviac.Design.Nodes;
using Behaviac.Design.Properties;

namespace Behaviac.Design
{
    internal partial class ExportDialog : Form
    {
        private BehaviorTreeList _behaviorTreeList;
        private BehaviorNode _node;
        private bool _ignoreErrors;
        private TreeNode _selectedTreeRoot;

        private bool _isDirty = false;

        public bool ExportCustomizedMeta() {
            return this.exportCustomizedMetaCheckBox.Checked;
        }

        public ExportDialog(BehaviorTreeList behaviorTreeList, BehaviorNode node, bool ignoreErrors, TreeNode selectedTreeRoot, int formatIndex) {
            InitializeComponent();

            _behaviorTreeList = behaviorTreeList;
            _node = node;
            _ignoreErrors = ignoreErrors;
            _selectedTreeRoot = selectedTreeRoot;

            // add all valid exporters to the format combobox
            Debug.Check(Workspace.Current != null);
            int exportIndex = -1;

            for (int i = 0; i < Plugin.Exporters.Count; ++i) {
                ExporterInfo info = Plugin.Exporters[i];

                if (node != null || info.MayExportAll) {
                    int index = this.exportSettingGridView.Rows.Add();
                    DataGridViewRow row = this.exportSettingGridView.Rows[index];

                    bool isExported = Workspace.Current.ShouldBeExported(info.ID);
                    if (info.ID == Plugin.SourceLanguage && this.ExportCustomizedMeta())
                    {
                        isExported = true;
                    }
                    row.Cells["Enable"].Value = isExported;
                    row.Cells["Format"].Value = info.Description;
                    row.Cells["Format"].ReadOnly = true;
                    row.Cells["Setting"].Value = info.HasSettings ? "..." : "";
                    row.Cells["Setting"].ReadOnly = true;

                    if (Workspace.Current.ShouldBeExported(info.ID) &&
                        (exportIndex == -1 || info.HasSettings)) {
                        exportIndex = i;
                    }
                }
            }

            changeExportFormat(exportIndex);
        }

        /// <summary>
        /// Keeps the check code from looping because of its own checking
        /// </summary>
        private bool _isCheckingNodes = false;

        private bool _isCheckingByTypeChanged = false;

        /// <summary>
        /// Checks or unchecks all the child nodes.
        /// </summary>
        /// <param name="node">The node whose children will be checked or unchecked.</param>
        /// <param name="check">Determines whether to check or to uncheck the nodes.</param>
        private void CheckNodes(TreeNode node, bool check) {
            foreach(TreeNode child in node.Nodes) {
                CheckNodes(child, check);
            }

            node.Checked = check;
        }

        private bool hasNodeDisabled(TreeNode node) {
            if (node.ForeColor == SystemColors.GrayText) {
                return true;
            }

            foreach(TreeNode child in node.Nodes) {
                if (hasNodeDisabled(child)) {
                    return true;
                }
            }

            return false;
        }

        private void treeView_BeforeCheck(object sender, TreeViewCancelEventArgs e) {
            if (hasNodeDisabled(e.Node)) {
                e.Cancel = true;

                string msg = string.Format(Resources.ExportBehaviorError, e.Node.Text);
                MessageBox.Show(msg, Resources.BehaviorError);
            }
        }

        /// <summary>
        /// Handles actions that occur after the user checked/unchecked an item.
        /// </summary>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e) {
            // if the code is currently updating the checking we skip any post actions.
            if (_isCheckingNodes) {
                return;
            }

            // check if the user check a folder
            NodeTag nodetag = (NodeTag) e.Node.Tag;

            if (nodetag.Type == NodeTagType.BehaviorFolder) {
                // disable the post actions
                _isCheckingNodes = true;

                // check/uncheck all the children
                CheckNodes(e.Node, e.Node.Checked);

                // enable post actions
                _isCheckingNodes = false;
            }

            if (!_isCheckingByTypeChanged) {
                SetFileCountLabel();
            }
        }

        private bool isChildOf(TreeNode child, TreeNode parent) {
            if (parent != null) {
                while (child != null) {
                    if (child.Parent == parent) {
                        return true;
                    }

                    child = child.Parent;
                }
            }

            return false;
        }

        private TreeNode getNodeByTag(TreeNodeCollection treenodes, object tag) {
            foreach(TreeNode treeNode in treenodes) {
                if (treeNode.Tag == tag) {
                    return treeNode;
                }

                TreeNode node = getNodeByTag(treeNode.Nodes, tag);

                if (node != null) {
                    return node;
                }
            }

            return null;
        }

        private TreeNode getNodeByFilename(TreeNodeCollection treenodes, string filename) {
            foreach(TreeNode treeNode in treenodes) {
                NodeTag nodetag = (NodeTag)treeNode.Tag;

                if (nodetag.Filename == filename) {
                    return treeNode;
                }

                TreeNode node = getNodeByFilename(treeNode.Nodes, filename);

                if (node != null) {
                    return node;
                }
            }

            return null;
        }

        private void SetFileCount(TreeNodeCollection treenodes, ref int exportedFileCount, ref int uncheckedFilesCount, ref int errorsFilesCount) {
            foreach(TreeNode node in treenodes) {
                NodeTag nodetag = (NodeTag)node.Tag;

                if (nodetag.Type == NodeTagType.Behavior) {
                    if (node.Checked) {
                        exportedFileCount++;

                    } else {
                        uncheckedFilesCount++;

                        if (node.ForeColor == SystemColors.GrayText) {
                            errorsFilesCount++;
                        }
                    }
                }

                SetFileCount(node.Nodes, ref exportedFileCount, ref uncheckedFilesCount, ref errorsFilesCount);
            }
        }

        private void SetFileCountLabel() {
            int exportedFileCount = 0;
            int uncheckedFilesCount = 0;
            int errorsFilesCount = 0;
            SetFileCount(this.treeView.Nodes, ref exportedFileCount, ref uncheckedFilesCount, ref errorsFilesCount);

            this.fileCountLabel.Text = string.Format(Resources.ExportFileCountInfo, exportedFileCount, errorsFilesCount);
        }

        /// <summary>
        /// Check the tree nodes according to the behaviors which are ought to be exported.
        /// </summary>
        /// <param name="pool">The pool we are checking.</param>
        /// <param name="node">The behavior we want to export. Use null if all behaviors may be exported.</param>
        public void CheckExportTreeNodes(TreeView tv, TreeNodeCollection pool, BehaviorNode node, bool ignoreErrors, TreeNode subTreeRoot, List<string> referencedFiles) {
            foreach(TreeNode tnode in pool) {
                NodeTag nodetag = (NodeTag)tnode.Tag;
                bool shouldDoubleCheck = false;

                // if no behavior is given enable all behaviors to be exported.
                if (node == null) {
                    tnode.Checked = (subTreeRoot != null) ? isChildOf(tnode, subTreeRoot) : true;
                    shouldDoubleCheck = tnode.Checked;

                } else {
                    // if the behavior is new one compare the label
                    if (node.FileManager == null) {
                        tnode.Checked = (tnode.Text == ((Node)node).Label);
                    }

                    // otherwise compare the filename
                    else {
                        bool shouldCheck = (nodetag.Filename == node.Filename);

                        if (!shouldCheck && referencedFiles.Count > 0) {
                            string relativeFile = FileManagers.FileManager.GetRelativePath(nodetag.Filename);
                            shouldCheck = referencedFiles.Contains(relativeFile);
                        }

                        tnode.Checked = shouldCheck;
                        shouldDoubleCheck = true;
                    }
                }

                if (tnode.Checked && (node != null || subTreeRoot != null)) {
                    tv.SelectedNode = tnode;
                }

                if (nodetag.Type == NodeTagType.Behavior) {
                    if (shouldDoubleCheck) {
                        if (!ignoreErrors && (node == null || tnode.Text == ((Node)node).Label))
                        {
                            // Get or load the behavior.
                            BehaviorNode behavior = _behaviorTreeList.GetBehavior(nodetag, tnode.Text);

                            if (behavior != null && behavior is Behavior) {
                                // Check errors for this behavior.
                                List<Node.ErrorCheck> result = new List<Node.ErrorCheck>();
                                ((Behavior)behavior).CheckForErrors(behavior, result);

                                // If there is any error, this node should be disabled.
                                if (Plugin.GetErrorChecks(result).Count > 0) {
                                    tnode.Checked = false;
                                    tnode.ForeColor = SystemColors.GrayText;
                                }
                            }
                        }

                    } else {
                        tnode.Checked = false;
                    }

                } else {
                    tnode.Checked = false;
                }

                // check the child nodes
                CheckExportTreeNodes(tv, tnode.Nodes, node, ignoreErrors, subTreeRoot, referencedFiles);

                if (!tnode.Checked && tnode.Nodes.Count > 0) {
                    int checkedChildCount = 0;
                    foreach(TreeNode childNode in tnode.Nodes) {
                        if (childNode.Checked) {
                            checkedChildCount++;
                        }
                    }

                    if (checkedChildCount == tnode.Nodes.Count) {
                        tnode.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Copies the tree nodes from one treeview to another one. Used for the export dialogue. Internal use only.
        /// </summary>
        /// <param name="source">The nodes you want to copy.</param>
        /// <param name="target">The tree you want to copy the nodes to.</param>
        private void CopyTreeNodes(TreeNodeCollection source, TreeNodeCollection target) {
            foreach(TreeNode node in source) {
                NodeTag nodetag = (NodeTag)node.Tag;

                // duplicate the tree node with the export dialogue specific icons
                TreeNode newnode = new TreeNode(node.Text, nodetag.Type == NodeTagType.BehaviorFolder ? 0 : 1, nodetag.Type == NodeTagType.BehaviorFolder ? 0 : 1);
                newnode.Tag = node.Tag;

                // add the node
                target.Add(newnode);

                // copy its children
                CopyTreeNodes(node.Nodes, newnode.Nodes);
            }
        }

        private void changeExportFormat(int exportIndex) {
            if (exportIndex < 0) {
                return;
            }

            _isCheckingByTypeChanged = true;

            this.treeView.Nodes.Clear();
            CopyTreeNodes(_behaviorTreeList.GetBehaviorTreeNodes(), this.treeView.Nodes);

            TreeNode subTreeRoot = (_selectedTreeRoot != null) ? getNodeByTag(this.treeView.Nodes, _selectedTreeRoot.Tag) : null;
            ExporterInfo exporter = Plugin.Exporters[exportIndex];
            //BehaviorNode node = exporter.HasSettings ? null : _node;
            BehaviorNode node = _node;
            bool ignoreErrors = exporter.HasSettings ? false : _ignoreErrors;

            List<string> referencedFiles = new List<string>();

            if (node != null && node is Node) {
                ((Node)node).GetReferencedFiles(ref referencedFiles);
            }

            CheckExportTreeNodes(this.treeView, this.treeView.Nodes, node, ignoreErrors, subTreeRoot, referencedFiles);

            this.treeView.ExpandAll();

            if (node != null) {
                TreeNode treeNode = getNodeByFilename(this.treeView.Nodes, node.Filename);

                if (treeNode != null) {
                    treeNode.EnsureVisible();
                }

            } else if (this.treeView.Nodes.Count > 0) {
                this.treeView.Nodes[0].EnsureVisible();
            }

            this.treeView.Select();

            SetFileCountLabel();

            _isCheckingByTypeChanged = false;
        }

        private void exportSettingGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) {
                return;
            }

            DataGridViewRow row = this.exportSettingGridView.Rows[e.RowIndex];
            int exportIndex = Plugin.GetExporterIndex("", (string)row.Cells["Format"].Value);
            ExporterInfo info = Plugin.Exporters[exportIndex];

            if (e.ColumnIndex == 0) { // Enable
                Debug.Check(Workspace.Current != null);
                Workspace.Current.SetExportInfo(info.ID, (bool)row.Cells["Enable"].EditedFormattedValue, Workspace.Current.ExportedUnifiedFile(info.ID));

                exportIndex = -1;

                for (int index = 0; index < Plugin.Exporters.Count; ++index) {
                    info = Plugin.Exporters[index];

                    if (Workspace.Current.ShouldBeExported(info.ID) &&
                        (exportIndex == -1 || info.HasSettings)) {
                        exportIndex = index;
                    }
                }

                if (exportIndex != -1) {
                    changeExportFormat(exportIndex);
                    _isDirty = true;
                }

            } else if (e.ColumnIndex == 2) { // Settings
                if (info.HasSettings) {
                    using(ExportSettingDialog dialog = new ExportSettingDialog(info)) {
                        dialog.ShowDialog();
                    }
                }
            }
        }

        private void ExportDialog_FormClosed(object sender, FormClosedEventArgs e) {
            if (_isDirty) {
                _isDirty = false;
                Workspace.SaveWorkspaceFile(Workspace.Current);
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MainWindow.Instance.BehaviorTreeList.OpenBehavior(e.Node);
        }
    }
}
