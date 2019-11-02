using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;

namespace FileExplorer.Commands
{
    public class InitCommand:NavigateCommand
    {
        public TreeView FileTree { get; }
        public TextBox PathTb { get; }

        public InitCommand(PathHistoryCache cache, ListView listView, TreeView treeView,TextBox pathTb,IFileService service) : base(cache, listView, service)
        {
            FileTree = treeView;
            PathTb = pathTb;
        }

        public override Task<ExecuteResult> ExecuteAsync()
        {
            PathTb.Text = Environment.MachineName;
            Cache.HistoryMark++;
            Cache.PathHistory.Add(Environment.MachineName);
            try
            {
                TreeView_LoadRoots();
                ListView_LoadRoots();
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return Task.Run(() => new ExecuteResult(false, e.Message));
            }
            return Task.Run(() => new ExecuteResult(true, String.Empty));
        }

        private async void TreeView_LoadRoots()
        {
            FileTree.Nodes.Clear();
            FileTree.BeginUpdate();
            var rootNode = new TreeNode(Environment.MachineName, 3, 3)
            {
                Tag = FactoryConstants.PC,
                ToolTipText = Environment.MachineName,
                Name = Environment.MachineName
            };
            FileTree.EndUpdate();
            var nodes = await TreeNodeFactory.GetRootNodesAsync();
            FileTree.BeginUpdate();
            foreach (var node in nodes)
            {
                rootNode.Nodes.Add(node);
            }
            FileTree.Nodes.Add(rootNode);
            FileTree.EndUpdate();
        }

        public override bool CanDo => true;
    }
}
