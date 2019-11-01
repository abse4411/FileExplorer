using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using FileExplorer.Infrastructure.Services;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public IFileService Service { get; }
        public IList<string> PathHistory { get; private set; }
        public int HistoryMark { get; private set; }

        public Form1()
        {
            InitializeComponent();
            Service = new FileService();
            PathHistory = new List<string>(20);
            HistoryMark = 0;
            PrepareData();
        }

        private async void PrepareData()
        {
            this.FileTree.ImageList = this.SmallIconList;
            this.FileList.SmallImageList = this.SmallIconList;
            this.FileList.LargeImageList = this.LargeIconList;

            TreeView_LoadRoots();
            ListView_LoadRoots();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var path = this.PathTb.Text;
            if (string.IsNullOrWhiteSpace(path))
                return;

            this.FileList.Items.Clear();
            this.FileList.BeginUpdate();
            var list = await Service.GetFileItemsAsync(path);
            var items = await ListViewItemFactory.GetDetailItemsAsync(list);
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }
            this.FileList.EndUpdate();
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            var nodes = await TreeNodeFactory.GetRootNodesAsync();
            this.FileTree.Nodes.Clear();
            this.FileTree.BeginUpdate();
            foreach (var node in nodes)
            {
                this.FileTree.Nodes.Add(node);
            }
            this.FileTree.EndUpdate();

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
            var nodes = await TreeNodeFactory.GetRootNodesAsync();
            foreach (var node in nodes)
            {
                this.FileTree.Nodes.Add(node);
            }
            this.FileTree.EndUpdate();
        }

        private void TreeView_LoadItems()
        {

        }

        private void ListView_LoadRoots()
        {
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
                            this.PathTb.Text = selectedItem.Name;
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
            if (HistoryMark <= 0)
                return;
            HistoryMark--;
            var path = PathHistory[HistoryMark];
            this.PathTb.Text = path;
            ListView_LoadItems(path);
        }

        private void ForwardBtn_Click(object sender, EventArgs e)
        {
            if (HistoryMark >= PathHistory.Count)
                return;
            HistoryMark++;
            var path = PathHistory[HistoryMark];
            this.PathTb.Text = path;
            ListView_LoadItems(path);
        }
    }
}
