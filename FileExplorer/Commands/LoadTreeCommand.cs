using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;

namespace FileExplorer.Commands
{
    public class LoadTreeCommand : ICommand
    {
        public TreeNode TargetNode { get; }
        public TreeView TreeView { get; }

        public LoadTreeCommand(TreeView treeView, TreeNode targetNode)
        {
            TargetNode = targetNode;
            TreeView = treeView;
        }

        public async Task<ExecuteResult> ExecuteAsync()
        {
            var type = TargetNode.Tag as string;
            switch (type)
            {
                case FactoryConstants.Driver:
                case FactoryConstants.Folder:
                case FactoryConstants.PC:
                    IList<TreeNode> newNodes;
                    if (type == FactoryConstants.PC)
                        newNodes = await TreeNodeFactory.GetRootNodesAsync();
                    else
                    {
                        if (!Directory.Exists(TargetNode.Name))
                        {
                            TreeView.BeginUpdate();
                            var fatherNode = TargetNode.Parent;
                            fatherNode.Nodes.Remove(TargetNode);
                            TreeView.EndUpdate();
                            return new ExecuteResult(false, $"Directory \"{TargetNode.Name}\" does not exist");
                        }
                        try
                        {
                            newNodes = await TreeNodeFactory.GetNodesAsync(TargetNode.Name);
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Debug.WriteLine(e);
                            return new ExecuteResult(false, e.Message);
                        }
                    }

                    List<KeyValuePair<TreeNode, IList<TreeNode>>> nodesList=new List<KeyValuePair<TreeNode, IList<TreeNode>>>();
                    foreach (TreeNode node in newNodes)
                    {
                        if (node.Tag is string nodeType && nodeType.Equals(FactoryConstants.Folder))
                        {
                            node.Nodes.Clear();
                            if (!Directory.Exists(node.Name))
                            {
                                TargetNode.Nodes.Remove(node);
                                continue;
                            }
                            IList<TreeNode> nodes;
                            try
                            {
                                nodes = await TreeNodeFactory.GetNodesAsync(node.Name);
                            }
                            catch (UnauthorizedAccessException e)
                            {
                                Debug.WriteLine(e);
                                continue;
                            }
                            nodesList.Add(new KeyValuePair<TreeNode, IList<TreeNode>>(node,nodes));
                        }
                    }
                    TreeView.BeginUpdate();
                    TargetNode.Nodes.Clear();
                    foreach (var pair in nodesList)
                    {
                        var node = pair.Key;
                        foreach (var n in pair.Value)
                        {
                            node.Nodes.Add(n);
                        }
                    }
                    foreach (var n in newNodes)
                        TargetNode.Nodes.Add(n);
                    TreeView.EndUpdate();
                    break;
                default:
                    return new ExecuteResult(false, $"Unknown tree node tag:{type}"); ;
            }
            return new ExecuteResult(true, String.Empty);
        }

        public Task<ExecuteResult> Undo()
        {
            throw new NotImplementedException();
        }

        public bool CanDo
        {
            get
            {
                if (TargetNode?.Tag is string)
                    return true;
                return false;
            }
        }

        public bool CanUndo { get; } = false;
    }
}
