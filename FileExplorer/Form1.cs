﻿using System;
using System.Collections;
using System.Windows.Forms;
using FileExplorer.Core.Services;
using FileExplorer.Infrastructure.Services;
using FileExplorer.Services;
using FileExplorer.ViewModels;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public IDialogService DialogService;
        public IFileService FileService { get; }
        public IFileOperationService FileOperationService { get; }

        public FileViewModel ViewModel;

        //public PathHistoryCache Cache { get; }
        //public CommandManager Invoker { get; }
        private readonly ListViewColumnSorter _lvwColumnSorter;
        //private int Count = 0;

        public Form1()
        {
            InitializeComponent();
            DialogService = new DialogService();
            FileService = new FileService();
            FileOperationService = new FileOperationService();
            ViewModel = new FileViewModel(FileList, FileTree, PathTb, FileService, FileOperationService, DialogService);
            //Cache = new PathHistoryCache();
            //Invoker=new CommandManager();
            _lvwColumnSorter = new ListViewColumnSorter();
            FileList.ListViewItemSorter = _lvwColumnSorter;
            FileList.ContextMenuStrip = contextMenuStrip1;
            PrepareData();
        }

        private async void PrepareData()
        {
            FileTree.ImageList = SmallIconList;
            FileList.SmallImageList = SmallIconList;
            FileList.LargeImageList = LargeIconList;
            //await Invoker.Execute(CommandFactory.GetInitCommand(Cache, FileList,FileTree, PathTb, FileService));
            await ViewModel.Init();
            UpdateCountLabel();
        }

        #region ChangeView

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileList.View = View.Details;
            //FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void samllIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileList.View = View.SmallIcon;
            //FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileList.View = View.LargeIcon;
            //FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileList.View = View.List;
            //FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        #endregion

        private async void FileList_DoubleClick(object sender, EventArgs e)
        {
            //if (FileList.SelectedItems.Count == 1)
            //{
            //    var selectedItem = FileList.SelectedItems[0];
            //    if (selectedItem.Tag is string type)
            //    {
            //        switch (type)
            //        {
            //            case FactoryConstants.Folder:
            //            case FactoryConstants.Driver:
            //                PathTb.Text = selectedItem.Name;
            //                var result=await Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb.Text, FileService));
            //                if (!result.IsSuccessful)
            //                    DialogService.ShowErrorDialog("Error", result.Message);
            //                UpdateCountLabel();
            //                break;
            //            case FactoryConstants.File:
            //                Process.Start(selectedItem.Name);
            //                break;
            //            default:
            //                return;
            //        }
            //    }
            //}
            await ViewModel.LoadListBySelectedItemAsync();
            UpdateCountLabel();
        }

        private async void BackBtn_Click(object sender, EventArgs e)
        {
            //var result=await Invoker.Execute(CommandFactory.GetBackCommand(Cache, FileList, PathTb, FileService));
            //if (!result.IsSuccessful)
            //    DialogService.ShowErrorDialog("Error", result.Message);
            await ViewModel.GoBackAsync();
            UpdateCountLabel();
        }

        private async void ForwardBtn_Click(object sender, EventArgs e)
        {
            //var result = await Invoker.Execute(CommandFactory.GetForwardCommand(Cache, FileList, PathTb, FileService));
            //if (!result.IsSuccessful)
            //    DialogService.ShowErrorDialog("Error", result.Message);
            await ViewModel.GoForwardAsync();
            UpdateCountLabel();
        }

        private async void FileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Count++;
            //Debug.WriteLine($"========={Count}");
            //var selectedNode = FileTree.SelectedNode;
            //if (selectedNode != null)
            //{
            //    if (selectedNode.Tag is string type)
            //    {
            //        switch (type)
            //        {
            //            case FactoryConstants.Driver:
            //            case FactoryConstants.Folder:
            //            case FactoryConstants.PC:
            //                PathTb.Text = selectedNode.Name;
            //                var result = await Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb.Text, FileService));
            //                if (!result.IsSuccessful)
            //                    DialogService.ShowErrorDialog("Error", result.Message);
            //                UpdateCountLabel();
            //                break;
            //            default:
            //                return;
            //        }
            //    }
            //}
            await ViewModel.LoadListBySelectedNodeAsync();
            UpdateCountLabel();
            //FileTree.SelectedNode = null;
        }

        private async void FileTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            await ViewModel.LoadTreeAsync(node);
            //if(node == null)
            //    return;
            //var result=await Invoker.Execute(CommandFactory.GetLoadTreeCommand(FileTree, node));
            //if (!result.IsSuccessful)
            //    DialogService.ShowErrorDialog("Error", result.Message);
        }

        #region Sort

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
                var compareResult =
                    _objectCompare.Compare(itemX?.SubItems[SortColumn].Text, itemY?.SubItems[SortColumn].Text);

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

        private void FileList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                _lvwColumnSorter.Order = _lvwColumnSorter.Order == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            FileList.Sort();
        }

        #endregion

        private async void PathTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //var result=await Invoker.Execute(CommandFactory.GetLoadCommand(Cache, FileList, PathTb.Text, FileService));
                //if (!result.IsSuccessful)
                //    DialogService.ShowErrorDialog("Error", result.Message);
                await ViewModel.LoadListAsync();
                UpdateCountLabel();
                e.Handled = true;
            }
        }

        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            //var result = await Invoker.Execute(CommandFactory.GetRefreshCommand(Cache, FileList, PathTb.Text, FileService));
            //if (!result.IsSuccessful)
            //    DialogService.ShowErrorDialog("Error", result.Message);
            await ViewModel.RefreshAsync();
            UpdateCountLabel();
        }

        private async void SearchBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.SelectedItem?.ToString()))
            {
                return;
            }

            //var result = await Invoker.Execute(CommandFactory.GetSearchCommand(FileList, Cache, SearchBox.Text, FileService));
            //if (!result.IsSuccessful)
            //    DialogService.ShowErrorDialog("Error", result.Message);
            await ViewModel.SearchAsync(SearchBox.SelectedItem.ToString());
            UpdateCountLabel();
        }

        private async void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(SearchBox.Text))
                    return;
                SearchBox.Items.Add(SearchBox.Text);
                await ViewModel.SearchAsync(SearchBox.Text);
                UpdateCountLabel();
                e.Handled = true;
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackBtn_Click(null, null);
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForwardBtn_Click(null, null);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshBtn_Click(null, null);
        }

        private void FileList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var count = FileList.SelectedItems?.Count ?? 0;
            SelectLabel.Text = count > 0 ? $"{count} item(s) selected       " : string.Empty;
        }

        private void UpdateCountLabel()
        {
            CountLabel.Text = $"{FileList.Items.Count} item(s)        |";
            FileList_ItemSelectionChanged(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewModel.CopyFileItem();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewModel.CutFileItem();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ViewModel.DeleteFileItemAsync();
        }

        private async void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ViewModel.PasteFileItemAsync();
        }

        private async void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ViewModel.UndoFileOperationAsync();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FileList.SelectedItems.Count > 0)
            {
                copyToolStripMenuItem1.Visible = true;
                cutToolStripMenuItem1.Visible = true;
                deleteToolStripMenuItem1.Visible = true;
                pasteToolStripMenuItem1.Visible = false;
            }
            else
            {
                copyToolStripMenuItem1.Visible = false;
                cutToolStripMenuItem1.Visible = false;
                deleteToolStripMenuItem1.Visible = false;
                pasteToolStripMenuItem1.Visible = true;
            }
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewModel.ShowCredits();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ViewModel.ShowAbout();
        }
    }
}
