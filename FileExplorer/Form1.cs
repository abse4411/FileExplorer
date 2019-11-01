using System;
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

        public Form1()
        {
            InitializeComponent();
            Service = new FileService();
            Cache=new PathHistoryCache();
            Invoker =new CommandManager();
            PathHistory = new List<string>(20);
            HistoryMark = -1;
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

        private void TreeView_LoadItems()
        {

        }

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
        private async void ListView_LoadItems(string path)
        {
            if (!Directory.Exists(path))
                return;

            this.PathTb.Text = path;
            this.FileList.Clear();
            this.FileList.BeginUpdate();
            var headers = ListViewItemFactory.GetFIleHeaderItems();
            foreach (var header in headers)
            {
                this.FileList.Columns.Add(header);
            }
            var list = await Service.GetFileItemsAsync(path);
            var items = await ListViewItemFactory.GetDetailItemsAsync(list);
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }
            this.FileList.EndUpdate();
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

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

                            Cache.HistoryMark++;
                            if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
                                Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
                            Cache.PathHistory.Add(selectedItem.Name);
                            ListView_LoadItems(selectedItem.Name);
                            break;
                        case FactoryConstants.File:
                            System.Diagnostics.Process.Start(selectedItem.Name);
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
            Invoker.Execute(CommandFactory.GetBackCommand(Cache,FileList,PathTb,Service));
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

        private async void FileTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var targetNode = e.Node;
            if (targetNode != null)
            {
                if (targetNode.Tag is string type)
                {
                    switch (type)
                    {
                        case FactoryConstants.Driver:
                        case FactoryConstants.Folder:
                            this.FileTree.BeginUpdate();
                            foreach (TreeNode node in targetNode.Nodes)
                            {
                                if (node.Tag is string nodeType && nodeType.Equals(FactoryConstants.Folder))
                                {
                                    node.Nodes.Clear();
                                    var nodes = await TreeNodeFactory.GetNodesAsync(node.Name);
                                    foreach (var n in nodes)
                                    {
                                        node.Nodes.Add(n);
                                    }
                                }
                            }
                            this.FileTree.EndUpdate();
                            break;
                        default:
                            return;
                    }
                }
            }
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
                            HistoryMark++;
                            if (HistoryMark <= PathHistory.Count - 1)
                                PathHistory.RemoveRange(HistoryMark, PathHistory.Count - HistoryMark);
                            PathHistory.Add(selectedNode.Name);
                            if(type.Equals(FactoryConstants.PC))
                                ListView_LoadRoots();
                            else
                                ListView_LoadItems(selectedNode.Name);
                            break;
                        default:
                            return;
                    }
                }
                this.FileTree.SelectedNode = null;
            }
        }

    }
}
