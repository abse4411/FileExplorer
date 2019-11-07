using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Infrastructure.Commands;
using FileExplorer.Infrastructure.Common;

namespace FileExplorer.Factories
{
    static class CommandFactory
    {
        public static InitCommand GetInitCommand(PathHistoryCache cache, ListView listView,TreeView treeView, TextBox pathTbBox, IFileService service)
        {
            return new InitCommand(cache, listView, treeView,pathTbBox, service);
        }

        public static BackCommand GetBackCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service)
        {
            return new BackCommand(cache,listView,pathTbBox,service);
        }

        public static ForwardCommand GetForwardCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service)
        {
            return new ForwardCommand(cache, listView, pathTbBox, service);
        }

        public static LoadListCommand GetLoadCommand(PathHistoryCache cache, ListView listView, string path, IFileService service)
        {
            return new LoadListCommand(cache, listView, path, service);
        }
        
        public static LoadTreeCommand GetLoadTreeCommand(TreeView treeView, TreeNode targetNode)
        {
            return new LoadTreeCommand(treeView, targetNode);
        }

        public static RefreshCommand GetRefreshCommand(PathHistoryCache cache, ListView listView, string path, IFileService service)
        {
            return new RefreshCommand(cache, listView, path, service);
        }

        public static SearchCommand GetSearchCommand(ListView listView, PathHistoryCache cache, string pattern, IFileService service)
        {
            return new SearchCommand( listView, cache, pattern,service);
        }

        public static CopyCommand GetCopyCommand(FileOperationCache cache, string targetPath, IFileOperationService service)
        {
            return new CopyCommand( cache, targetPath, service);
        }
        public static CutCommand GetCutCommand(FileOperationCache cache, string targetPath, IFileOperationService service)
        {
            return new CutCommand(cache, targetPath, service);
        }
        public static DeleteCommand GetDeleteCommand(FileOperationCache cache, IFileOperationService service)
        {
            return new DeleteCommand(cache, service);
        }
    }
}
