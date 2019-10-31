using System;
using System.Collections.Generic;
using System.IO;
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
            var dirs = Directory.EnumerateDirectories(path);
            foreach (var dir in dirs)
            {
                var node = new TreeNode(dir, 0, 0);
                var subDirs= Directory.EnumerateDirectories(Path.Combine(path,dir));
                foreach (var item in subDirs)
                {
                    node.Nodes.Add(new TreeNode(item, 0, 0));
                }
                var subFiles = Directory.EnumerateFiles(Path.Combine(path, dir));
                foreach (var item in subFiles)
                {
                    node.Nodes.Add(new TreeNode(item, 1, 1));
                }
                result.Add(node);
            }
            var files = Directory.EnumerateFiles(path);
            foreach (var file in files)
            {
                result.Add(new TreeNode(file, 1, 1));
            }
            return result;
        }
    }
}
