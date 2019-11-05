using FileExplorer.Commands;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer.ViewModels
{
    public class FileViewModel
    {
        private readonly PathHistoryCache _cache;
        public CommandManager Invoker { get; }
        public ListView FileList { get; }
        public TreeView FileTree { get; }
        public TextBox PathTb { get; }
        public IFileService FileService { get; }
        public IDialogService DialogService { get; }

        public FileViewModel(ListView listView, TreeView treeView, TextBox pathTb, IFileService service, IDialogService dialogService)
        {
            _cache = new PathHistoryCache();
            Invoker = new CommandManager();
            FileList = listView;
            FileTree = treeView;
            PathTb = pathTb;
            FileService = service;
            DialogService = dialogService;
        }

        private void ShowError(ExecuteResult result)
        {
            if (!result.IsSuccessful)
                DialogService.ShowErrorDialog("Error", result.Message);
        }
        public async void Init()
        {
            var result = await Invoker.Execute(CommandFactory.GetInitCommand(_cache, FileList, FileTree, PathTb, FileService));
            ShowError(result);
        }
        public async void Back()
        {
            var result = await Invoker.Execute(CommandFactory.GetBackCommand(_cache, FileList, PathTb, FileService));
            ShowError(result);
        }
        public async void Forward()
        {
            var result = await Invoker.Execute(CommandFactory.GetForwardCommand(_cache, FileList, PathTb, FileService));
            ShowError(result);
        }
        public async void Refresh()
        {
            var result = await Invoker.Execute(CommandFactory.GetRefreshCommand(_cache, FileList, PathTb.Text, FileService));
            ShowError(result);
        }
        public void LoadList()
        {
            LoadList(PathTb.Text);
        }
        public async void LoadList(string path)
        {
            var result = await Invoker.Execute(CommandFactory.GetLoadCommand(_cache, FileList, path, FileService));
            ShowError(result);
        }
        public void LoadListBySelectedNode()
        {
            var selectedNode = FileTree.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Tag is string type)
                {
                    switch (type)
                    {
                        case FactoryConstants.Driver:
                        case FactoryConstants.Folder:
                        case FactoryConstants.PC:
                            PathTb.Text = selectedNode.Name;
                            LoadList(selectedNode.Name);
                            break;
                        default:
                            return;
                    }
                }
            }
        }
        public void LoadListBySelectedItem()
        {
            if (FileList.SelectedItems.Count == 1)
            {
                var selectedItem = FileList.SelectedItems[0];
                if (selectedItem.Tag is string type)
                {
                    switch (type)
                    {
                        case FactoryConstants.Folder:
                        case FactoryConstants.Driver:
                            PathTb.Text = selectedItem.Name;
                            LoadList(selectedItem.Name);
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
        public async void Search(string pattern)
        {
            var result = await Invoker.Execute(CommandFactory.GetSearchCommand(FileList, _cache, pattern, FileService));
            ShowError(result);
        }
        public async void LoadTree(TreeNode node)
        {
            if (node == null)
                return;
            var result = await Invoker.Execute(CommandFactory.GetLoadTreeCommand(FileTree, node));
            ShowError(result);
        }
    }
}
