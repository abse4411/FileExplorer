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
        public static async Task<IList<TreeNode>> GetRootNodesAsync()
        {
            return await Task.Run(() =>
            {
                var result = new List<TreeNode>();
                var roots = Environment.GetLogicalDrives();
                foreach (var root in roots)
                {
                    var node = new TreeNode(root, 2, 2)
                    {
                        Tag = FactoryConstants.Folder,
                        ToolTipText = root
                    };
                    var subDirs = Directory.EnumerateDirectories(root);
                    foreach (var item in subDirs)
                    {
                        var name = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        node.Nodes.Add(new TreeNode(name, 0, 0)
                        {
                            Tag = FactoryConstants.Folder,
                            ToolTipText = name
                        });
                    }

                    var subFiles = Directory.EnumerateFiles(root);
                    foreach (var item in subFiles)
                    {
                        var name = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        node.Nodes.Add(new TreeNode(name, 1, 1)
                        {
                            Tag = FactoryConstants.File,
                            ToolTipText = name
                        });
                    }

                    result.Add(node);
                }
                return result;
            });

        }
        public static async Task<IList<TreeNode>> GetNodesAsync(string path)
        {
            return await Task.Run(() =>
            {
                var result = new List<TreeNode>();
                var dirs = Directory.EnumerateDirectories(path);
                foreach (var dir in dirs)
                {
                    var name = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    var node = new TreeNode(dir, 0, 0)
                    {
                        ToolTipText = name
                    };
                    var subDirs = Directory.EnumerateDirectories(Path.Combine(path, dir));
                    foreach (var item in subDirs)
                    {
                        var itemName = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        node.Nodes.Add(new TreeNode(itemName, 0, 0)
                        {
                            Tag = FactoryConstants.Folder,
                            ToolTipText = itemName
                        });
                    }

                    var subFiles = Directory.EnumerateFiles(Path.Combine(path, dir));
                    foreach (var item in subFiles)
                    {
                        var itemName = item.Substring(item.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        node.Nodes.Add(new TreeNode(itemName, 1, 1)
                        {
                            Tag = FactoryConstants.File,
                            ToolTipText = itemName
                        });
                    }

                    result.Add(node);
                }
                var files = Directory.EnumerateFiles(path);
                foreach (var file in files)
                {
                    var itemName = file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                    result.Add(new TreeNode(itemName, 1, 1));
                }
                return result;
            });
        }
    }
}
