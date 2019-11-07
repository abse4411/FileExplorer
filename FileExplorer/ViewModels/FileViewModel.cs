using System.Collections.Generic;
using FileExplorer.Commands;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Models;
using FileExplorer.Infrastructure.Common;

namespace FileExplorer.ViewModels
{
    public class FileViewModel
    {
        private readonly PathHistoryCache _cache;
        private FileOperation _currentOperation;
        public IList<FileItemInfo> _selectedFileItems;
        public CommandManager Invoker { get; }
        public ListView FileList { get; }
        public TreeView FileTree { get; }
        public TextBox PathTb { get; }
        public IFileService FileService { get; }
        public IFileOperationService FileOperationService { get; }
        public IDialogService DialogService { get; }

        public FileViewModel(ListView listView, TreeView treeView, TextBox pathTb, IFileService service,IFileOperationService fileOperationService, IDialogService dialogService)
        {
            _cache = new PathHistoryCache();
            _currentOperation = FileOperation.None;
            _selectedFileItems = new List<FileItemInfo>();
            Invoker = new CommandManager();
            FileList = listView;
            FileTree = treeView;
            PathTb = pathTb;
            FileService = service;
            FileOperationService = fileOperationService;
            DialogService = dialogService;
        }

        private void ShowError(ExecuteResult result)
        {
            if (!result.IsSuccessful)
                DialogService.ShowErrorDialog("Error", result.Message);
        }
        public async Task Init()
        {
            var result = await Invoker.Execute(CommandFactory.GetInitCommand(_cache, FileList, FileTree, PathTb, FileService));
            ShowError(result);
        }
        public async Task GoBackAsync()
        {
            var result = await Invoker.Execute(CommandFactory.GetBackCommand(_cache, FileList, PathTb, FileService));
            ShowError(result);
        }
        public async Task GoForwardAsync()
        {
            var result = await Invoker.Execute(CommandFactory.GetForwardCommand(_cache, FileList, PathTb, FileService));
            ShowError(result);
        }
        public async Task RefreshAsync()
        {
            var result = await Invoker.Execute(CommandFactory.GetRefreshCommand(_cache, FileList, PathTb.Text, FileService));
            ShowError(result);
        }
        public async Task LoadListAsync()
        {
            await LoadListAsync(PathTb.Text);
        }
        public async Task LoadListAsync(string path)
        {
            var result = await Invoker.Execute(CommandFactory.GetLoadCommand(_cache, FileList, path, FileService));
            ShowError(result);
        }
        public async Task LoadListBySelectedNodeAsync()
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
                            await LoadListAsync(selectedNode.Name);
                            break;
                        default:
                            return;
                    }
                }
            }
        }
        public async Task LoadListBySelectedItemAsync()
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
                            await LoadListAsync(selectedItem.Name);
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
        public async Task SearchAsync(string pattern)
        {
            var result = await Invoker.Execute(CommandFactory.GetSearchCommand(FileList, _cache, pattern, FileService));
            ShowError(result);
        }
        public async Task LoadTreeAsync(TreeNode node)
        {
            if (node == null)
                return;
            var result = await Invoker.Execute(CommandFactory.GetLoadTreeCommand(FileTree, node));
            ShowError(result);
        }

        private bool CreateFileItemInfosFromFileList()
        {
            if (FileList.SelectedItems.Count > 0)
            {
                _selectedFileItems.Clear();
                foreach (ListViewItem item in FileList.SelectedItems)
                {
                    bool isDirectory = false;
                    if (item.Tag is string type)
                    {
                        switch (type)
                        {
                            case FactoryConstants.Folder:
                                isDirectory = true;
                                break;
                            case FactoryConstants.File:
                                break;
                            default:
                                ShowError(new ExecuteResult(false, $"Can not operate those item(s)"));
                                return false;
                        }
                    }
                    _selectedFileItems.Add(new FileItemInfo(item.Text,item.Name, isDirectory));
                }

                return true;
            }
            else
            {
                ShowError(new ExecuteResult(false, "No files or directories are selected"));
                return false;
            }
        }
        public void CopyFileItem()
        {
            if (CreateFileItemInfosFromFileList())
            {
                _currentOperation = FileOperation.Copy;
            }
        }
        public void CutFileItem()
        {
            if (CreateFileItemInfosFromFileList())
            {
                _currentOperation = FileOperation.Cut;
            }
        }
        public async Task DeleteFileItemAsync()
        {
            if (CreateFileItemInfosFromFileList())
            {
                var result = await Invoker.Execute(CommandFactory.GetDeleteCommand(new FileOperationCache
                {
                    SelectedFileItem = _selectedFileItems
                },FileOperationService));
                ShowError(result);
                await RefreshAsync();
            }
        }
        public async Task PasteFileItemAsync()
        {
            ExecuteResult result;
            switch (_currentOperation)
            {
                case FileOperation.Copy:
                    result = await Invoker.Execute(CommandFactory.GetCopyCommand(new FileOperationCache
                    {
                        SelectedFileItem = _selectedFileItems
                    }, _cache.PathHistory[_cache.HistoryMark], FileOperationService));
                    ShowError(result);
                    await RefreshAsync();
                    break;
                case FileOperation.Cut:
                    result = await Invoker.Execute(CommandFactory.GetCutCommand(new FileOperationCache
                    {
                        SelectedFileItem = _selectedFileItems
                    }, _cache.PathHistory[_cache.HistoryMark], FileOperationService));
                    _currentOperation = FileOperation.None;
                    _selectedFileItems.Clear();
                    ShowError(result);
                    await RefreshAsync();
                    break;
                default:
                    ShowError(new ExecuteResult(false,$"Unknown FileOperation:{_currentOperation}"));
                    return;
            }
        }

        public async Task UndoFileOperationAsync()
        {
            var result =await Invoker.Undo();
            ShowError(result);
            _currentOperation = FileOperation.None;
            await RefreshAsync();
        }
    }
}
