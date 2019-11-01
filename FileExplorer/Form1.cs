using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FileExplorer.Commands;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using FileExplorer.Infrastructure.Services;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public IFileService Service { get; }
        public PathHistoryCache Cache { get; }
        public CommandManager Invoker { get; }
        public List<string> PathHistory { get; private set; }
        public int HistoryMark { get; private set; }
        private ListViewColumnSorter lvwColumnSorter;

        public Form1()
        {
            InitializeComponent();
            Service = new FileService();
            Cache = new PathHistoryCache();
            Invoker = new CommandManager();
            PathHistory = new List<string>(20);
            HistoryMark = -1;
            lvwColumnSorter = new ListViewColumnSorter();
            this.FileList.ListViewItemSorter = lvwColumnSorter;
            PrepareData();
        }

        private void PrepareData()
        {
            this.FileTree.ImageList = this.SmallIconList;
            this.FileList.SmallImageList = this.SmallIconList;
            this.FileList.LargeImageList = this.LargeIconList;

            HistoryMark++;
            PathHistory.Add(Environment.MachineName);

            Cache.HistoryMark++;
            Cache.PathHistory.Add(Environment.MachineName);

            TreeView_LoadRoots();
            ListView_LoadRoots();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (HistoryMark > -1 && PathHistory[HistoryMark].Equals(this.PathTb.Text))
            //    return;
            //HistoryMark++;
            //if (HistoryMark <= PathHistory.Count - 1)
            //    PathHistory.RemoveRange(HistoryMark, PathHistory.Count - HistoryMark);
            //PathHistory.Add(this.PathTb.Text);
            //if (this.PathTb.Text == Environment.MachineName)
            //{
            //    ListView_LoadRoots();
            //}
            //else
            //{
            //    ListView_LoadItems(this.PathTb.Text);
            //}

            //if (Cache.HistoryMark > -1 && Cache.PathHistory[Cache.HistoryMark].Equals(this.PathTb.Text))
            //    return;
            //Cache.HistoryMark++;
            //if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
            //    Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
            //Cache.PathHistory.Add(this.PathTb.Text);
            //if (this.PathTb.Text == Environment.MachineName)
            //{
            //    ListView_LoadRoots();
            //}
            //else
            //{
            //    ListView_LoadItems(this.PathTb.Text);
            //}
            Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb, Service));
        }

        #region Hidden

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.Details;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void SmallBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.SmallIcon;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void LargeBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.LargeIcon;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void ListBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.List;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        #endregion


        private async void TreeView_LoadRoots()
        {
            this.FileTree.Nodes.Clear();
            this.FileTree.BeginUpdate();
            var rootNode = new TreeNode(Environment.MachineName, 3, 3)
            {
                Tag = FactoryConstants.PC,
                ToolTipText = Environment.MachineName,
                Name = Environment.MachineName
            };
            var nodes = await TreeNodeFactory.GetRootNodesAsync();
            foreach (var node in nodes)
            {
                rootNode.Nodes.Add(node);
            }

            this.FileTree.Nodes.Add(rootNode);
            this.FileTree.EndUpdate();
        }

        //private void TreeView_LoadItems()
        //{

        //}

        private void ListView_LoadRoots()
        {
            this.PathTb.Text = Environment.MachineName;
            this.FileList.Clear();
            this.FileList.BeginUpdate();
            var headers = ListViewItemFactory.GetDriverHeaderItems();
            foreach (var header in headers)
            {
                this.FileList.Columns.Add(header);
            }

            var items = ListViewItemFactory.GetRootDetailIItems();
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }

            this.FileList.EndUpdate();
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        //private async void ListView_LoadItems(string path)
        //{
        //    if (!Directory.Exists(path))
        //        return;

        //    this.PathTb.Text = path;
        //    this.FileList.Clear();
        //    this.FileList.BeginUpdate();
        //    var headers = ListViewItemFactory.GetFIleHeaderItems();
        //    foreach (var header in headers)
        //    {
        //        this.FileList.Columns.Add(header);
        //    }
        //    var list = await Service.GetFileItemsAsync(path);
        //    var items = await ListViewItemFactory.GetDetailItemsAsync(list);
        //    foreach (var item in items)
        //    {
        //        this.FileList.Items.Add(item);
        //    }
        //    this.FileList.EndUpdate();
        //    this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        //}

        private void FileList_DoubleClick(object sender, EventArgs e)
        {
            if (this.FileList.SelectedItems.Count == 1)
            {
                var selectedItem = this.FileList.SelectedItems[0];
                if (selectedItem.Tag is string type)
                {
                    switch (type)
                    {
                        case FactoryConstants.Folder:
                        case FactoryConstants.Driver:
                            //HistoryMark++;
                            //if (HistoryMark <= PathHistory.Count - 1)
                            //    PathHistory.RemoveRange(HistoryMark, PathHistory.Count - HistoryMark);
                            //PathHistory.Add(selectedItem.Name);
                            //ListView_LoadItems(selectedItem.Name);

                            //Cache.HistoryMark++;
                            //if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
                            //    Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
                            //Cache.PathHistory.Add(selectedItem.Name);
                            //ListView_LoadItems(selectedItem.Name);
                            PathTb.Text = selectedItem.Name;
                            Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb, Service));
                            break;
                        case FactoryConstants.File:
                            Process.Start(selectedItem.Name);
                            break;
                        default:
                            return;
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            //if (HistoryMark <= 0)
            //{
            //    ListView_LoadRoots();
            //    this.PathTb.Text = Environment.MachineName;
            //    return;
            //}
            //HistoryMark--;
            //var path = PathHistory[HistoryMark];
            //this.PathTb.Text = path;
            //if (this.PathTb.Text == Environment.MachineName)
            //{
            //    ListView_LoadRoots();
            //}
            //else
            //{
            //    ListView_LoadItems(path);
            //}
            Invoker.Execute(CommandFactory.GetBackCommand(Cache, FileList, PathTb, Service));
        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            //if (HistoryMark >-1 && HistoryMark >= PathHistory.Count - 1)
            //    return;
            //HistoryMark++;
            //var path = PathHistory[HistoryMark];
            //this.PathTb.Text = path;
            //if (this.PathTb.Text == Environment.MachineName)
            //{
            //    ListView_LoadRoots();
            //}
            //else
            //{
            //    ListView_LoadItems(path);
            //}
            Invoker.Execute(CommandFactory.GetForwardCommand(Cache, FileList, PathTb, Service));
        }

        private  void FileTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //var targetNode = e.Node;
            //if (targetNode != null)
            //{
            //    if (targetNode.Tag is string type)
            //    {
            //        switch (type)
            //        {
            //            case FactoryConstants.Driver:
            //            case FactoryConstants.Folder:
            //            case FactoryConstants.PC:
            //                this.FileTree.BeginUpdate();
            //                targetNode.Nodes.Clear();
            //                IList<TreeNode> newNodes;
            //                if (type ==FactoryConstants.PC)
            //                    newNodes =await TreeNodeFactory.GetRootNodesAsync();
            //                else
            //                    newNodes = await TreeNodeFactory.GetNodesAsync(targetNode.Name);
            //                foreach (var n in newNodes)
            //                    targetNode.Nodes.Add(n);
            //                foreach (TreeNode node in targetNode.Nodes)
            //                {
            //                    if (node.Tag is string nodeType && nodeType.Equals(FactoryConstants.Folder))
            //                    {
            //                        node.Nodes.Clear();
            //                        var nodes = await TreeNodeFactory.GetNodesAsync(node.Name);
            //                        foreach (var n in nodes)
            //                        {
            //                            node.Nodes.Add(n);
            //                        }
            //                    }
            //                }
            //                this.FileTree.EndUpdate();
            //                break;
            //            default:
            //                return;
            //        }
            //    }
            //}
        }

        private void FileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = this.FileTree.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is string type)
                {
                    switch (type)
                    {
                        case FactoryConstants.Driver:
                        case FactoryConstants.Folder:
                        case FactoryConstants.PC:
                            //HistoryMark++;
                            //if (HistoryMark <= PathHistory.Count - 1)
                            //    PathHistory.RemoveRange(HistoryMark, PathHistory.Count - HistoryMark);
                            //PathHistory.Add(selectedNode.Name);
                            //if(type.Equals(FactoryConstants.PC))
                            //    ListView_LoadRoots();
                            //else
                            //    ListView_LoadItems(selectedNode.Name);
                            PathTb.Text = selectedNode.Name;
                            Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb, Service));
                            break;
                        default:
                            return;
                    }
                }

                this.FileTree.SelectedNode = null;
            }
        }

        private async void FileTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //var targetNode = e.Node;
            //if (targetNode != null)
            //{
            //    if (targetNode.Tag is string type)
            //    {
            //        switch (type)
            //        {
            //            case FactoryConstants.Driver:
            //            case FactoryConstants.Folder:
            //            case FactoryConstants.PC:
            //                this.FileTree.BeginUpdate();
            //                targetNode.Nodes.Clear();
            //                IList<TreeNode> newNodes;
            //                if (type == FactoryConstants.PC)
            //                    newNodes = await TreeNodeFactory.GetRootNodesAsync();
            //                else
            //                    newNodes = await TreeNodeFactory.GetNodesAsync(targetNode.Name);
            //                foreach (var n in newNodes)
            //                    targetNode.Nodes.Add(n);
            //                foreach (TreeNode node in targetNode.Nodes)
            //                {
            //                    if (node.Tag is string nodeType && nodeType.Equals(FactoryConstants.Folder))
            //                    {
            //                        node.Nodes.Clear();
            //                        var nodes = await TreeNodeFactory.GetNodesAsync(node.Name);
            //                        foreach (var n in nodes)
            //                        {
            //                            node.Nodes.Add(n);
            //                        }
            //                    }
            //                }
            //                this.FileTree.EndUpdate();
            //                break;
            //            default:
            //                return;
            //        }
            //    }
            //}
            Invoker.Execute(CommandFactory.GetLoadTreeCommand(this.FileTree, e.Node));
        }

        private class ListViewItemComparer : IComparer
        {
            private int col;

            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem) x).SubItems[col].Text,
                    ((ListViewItem) y).SubItems[col].Text);
                return returnVal;
            }
        }

        private void FileList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                lvwColumnSorter.Order = lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.FileList.Sort();
        }

        public class ListViewColumnSorter : IComparer
        {

            /// <summary>
            /// Case insensitive comparer object
            /// </summary>
            private readonly CaseInsensitiveComparer _objectCompare;

            /// <summary>
            /// Class constructor.  Initializes various elements
            /// </summary>
            public ListViewColumnSorter()
            {
                // Initialize the column to '0'
                SortColumn = 0;

                // Initialize the sort order to 'none'
                Order = SortOrder.None;

                // Initialize the CaseInsensitiveComparer object
                _objectCompare = new CaseInsensitiveComparer();
            }

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                // Cast the objects to be compared to ListViewItem objects
                var itemX = (ListViewItem) x;
                var itemY = (ListViewItem) y;

                // Compare the two items
                var compareResult = _objectCompare.Compare(itemX.SubItems[SortColumn].Text, itemY.SubItems[SortColumn].Text);

                // Calculate correct return value based on object comparison
                if (Order == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (Order == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }

            /// <summary>
            /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
            /// </summary>
            public int SortColumn { set; get; }

            /// <summary>
            /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
            /// </summary>
            public SortOrder Order { set; get; }
        }
    }
}
