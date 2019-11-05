using FileExplorer.Commands;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using System.Diagnostics;
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

        public bool CanGoBack => Invoker.CanDo(CommandFactory.GetBackCommand(_cache, FileList, PathTb, FileService));
        public bool CanGoForward=> Invoker.CanDo(CommandFactory.GetForwardCommand(_cache, FileList, PathTb, FileService));

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
    }
}
