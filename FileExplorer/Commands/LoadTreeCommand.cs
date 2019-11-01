using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;

namespace FileExplorer.Commands
{
    public class LoadTreeCommand:Command
    {
        public LoadTreeCommand(TreeView treeView, TreeNode targetNode, IFileService service)
        {
        }

        public override void Execute()
        {

        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override bool CanDo
        {
            get
            {

                return true;
            }
        }
    }
}
