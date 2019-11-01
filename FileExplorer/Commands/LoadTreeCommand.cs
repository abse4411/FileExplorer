using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;

namespace FileExplorer.Commands
{
    public class LoadTreeCommand : Command
    {
        public TreeNode TargetNode { get; }
        public TreeView TreeView { get; }

        public LoadTreeCommand(TreeView treeView, TreeNode targetNode)
        {
            TargetNode = targetNode;
            TreeView = treeView;
        }

        public override async void Execute()
        {
            var type = TargetNode.Tag as string;
            switch (type)
            {
                case FactoryConstants.Driver:
                case FactoryConstants.Folder:
                case FactoryConstants.PC:
                    TreeView.BeginUpdate();
                    TargetNode.Nodes.Clear();
                    IList<TreeNode> newNodes;
                    if (type == FactoryConstants.PC)
                        newNodes = await TreeNodeFactory.GetRootNodesAsync();
                    else
                        newNodes = await TreeNodeFactory.GetNodesAsync(TargetNode.Name);
                    foreach (var n in newNodes)
                        TargetNode.Nodes.Add(n);
                    foreach (TreeNode node in TargetNode.Nodes)
                    {
                        if (node.Tag is string nodeType && nodeType.Equals(FactoryConstants.Folder))
                        {
                            node.Nodes.Clear();
                            var nodes = await TreeNodeFactory.GetNodesAsync(node.Name);
                            foreach (var n in nodes)
                                node.Nodes.Add(n);
                        }
                    }
                    TreeView.EndUpdate();
                    break;
                default:
                    return;
            }
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override bool CanDo
        {
            get
            {
                if (TargetNode?.Tag is string)
                    return true;
                return false;
            }
        }

        public override bool CanUndo { get; } = false;
    }
}
