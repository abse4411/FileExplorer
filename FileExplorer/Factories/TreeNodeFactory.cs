using System;
using System.Collections;
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
                var roots = DriveInfo.GetDrives();
                foreach (var root in roots)
                {
                    var node = new TreeNode($"{root.VolumeLabel} ({root.Name})", 2, 2)
                    {
                        Tag = FactoryConstants.Driver,
                        ToolTipText = root.Name,
                        Name = root.RootDirectory.Name
                    };
                    AddFolderNodes(root.RootDirectory.EnumerateDirectories(), node.Nodes);
                    AddFileNodes(root.RootDirectory.EnumerateFiles(), node.Nodes);

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
                DirectoryInfo directory=new DirectoryInfo(path);
                var dirs = directory.EnumerateDirectories();
                foreach (var dir in dirs)
                {
                    var node = new TreeNode(dir.Name, 0, 0)
                    {
                        Tag = FactoryConstants.Folder,
                        ToolTipText = dir.Name,
                        Name = dir.Name
                    };
                    AddFolderNodes(dir.EnumerateDirectories(), node.Nodes);
                    AddFileNodes(dir.EnumerateFiles(), node.Nodes);
                    result.Add(node);
                }
                AddFileNodes(directory.EnumerateFiles(), result);
                return result;
            });
        }

        private static void AddFileNodes(IEnumerable<FileInfo> files,IList list)
        {
            foreach (var item in files)
            {
                list.Add(new TreeNode(item.Name, 1, 1)
                {
                    Tag = FactoryConstants.File,
                    ToolTipText = item.Name,
                    Name = item.Name
                });
            }
        }

        private static void AddFolderNodes(IEnumerable<DirectoryInfo> dirs, IList list)
        {
            foreach (var item in dirs)
            {
                list.Add(new TreeNode(item.Name, 0, 0)
                {
                    Tag = FactoryConstants.Folder,
                    ToolTipText = item.Name,
                    Name = item.Name
                });
            }
        }
    }
}
