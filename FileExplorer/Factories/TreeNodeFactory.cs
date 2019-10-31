using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Model;

namespace FileExplorer.Factories
{
    public static class TreeNodeFactory
    {
        public static IList<TreeNode> GetNodes(string path)
        {
            var result = new List<TreeNode>();
            TreeNode node=new TreeNode();
            TreeView view=new TreeView();
            view.Nodes.Add(node);

            return result;
        }
    }
}
