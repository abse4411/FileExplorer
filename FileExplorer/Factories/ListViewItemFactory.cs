using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Model;

namespace FileExplorer.Factories
{
    static class ListViewItemFactory
    {
        private static readonly IList<ColumnHeader> FIleHeaders;
        private static readonly IList<ColumnHeader> DriverHeaders;
        static ListViewItemFactory()
        {
            string[] headerStrings=new[] {"Name", "Last Write Time", "Last Access Time", "Creation Time", "Type" ,"Size(KB)"};
            FIleHeaders = new List<ColumnHeader>();
            foreach (var str in headerStrings)
            {
                FIleHeaders.Add(new ColumnHeader
                {
                    Text = str,
                    TextAlign = HorizontalAlignment.Left
                });
            }
            headerStrings = new[] { "Name", "Drive Type", "Drive Format", "IsReady", "Available Free Space(GB)", "Total Free Space(GB)", "Total Size(GB)" };
            DriverHeaders = new List<ColumnHeader>();
            foreach (var str in headerStrings)
            {
                DriverHeaders.Add(new ColumnHeader
                {
                    Text = str,
                    TextAlign = HorizontalAlignment.Left
                });
            }
        }
        public static async Task<IList<ListViewItem>> GetDetailItemsAsync(IList<FileItem> list)
        {
            return await Task.Run(() =>
            {
                var result = new List<ListViewItem>();

                foreach (var item in list)
                {
                    ListViewItem lvi = new ListViewItem(item.Name);
                    lvi.SubItems.Add(item.LastWriteTime.ToString(CultureInfo.CurrentCulture));
                    lvi.SubItems.Add(item.LastAccessTime.ToString(CultureInfo.CurrentCulture));
                    lvi.SubItems.Add(item.CreationTime.ToLongDateString());
                    lvi.Name = item.FullName;
                    if (item.IsDirectory)
                    {
                        lvi.SubItems.Add(FactoryConstants.Folder);
                        lvi.ImageIndex = 0;
                        lvi.SubItems.Add(string.Empty);
                        lvi.Tag = FactoryConstants.Folder;
                        lvi.ToolTipText = item.Name;
                    }
                    else
                    {
                        lvi.SubItems.Add(FactoryConstants.File);
                        lvi.ImageIndex = 1;
                        lvi.SubItems.Add((item.Length/ FactoryConstants.KB).ToString());
                        lvi.Tag = FactoryConstants.File;
                        lvi.ToolTipText = item.Name;
                    }
                    result.Add(lvi);
                }
                return result;
            });
        }

        public static IList<ColumnHeader> GetFIleHeaderItems()
        {
            return FIleHeaders;
        }

        public static IList<ColumnHeader> GetDriverHeaderItems()
        {
            return DriverHeaders;
        }

        public static IList<ListViewItem> GetRootDetailIItems()
        {
            var result = new List<ListViewItem>();
            var roots = DriveInfo.GetDrives();
            foreach (var root in roots)
            {
                ListViewItem lvi = new ListViewItem($"{root.VolumeLabel} ({root.Name})");
                lvi.ImageIndex = 2;
                lvi.SubItems.Add(root.DriveType.ToString());
                lvi.SubItems.Add(root.DriveFormat);
                lvi.SubItems.Add(root.IsReady.ToString());
                lvi.SubItems.Add((root.AvailableFreeSpace / FactoryConstants.GB).ToString());
                lvi.SubItems.Add((root.TotalFreeSpace / FactoryConstants.GB).ToString());
                lvi.SubItems.Add((root.TotalSize / FactoryConstants.GB).ToString());
                lvi.Name = root.RootDirectory.Name;
                lvi.Tag = FactoryConstants.Driver;
                lvi.ToolTipText = root.Name;
                result.Add(lvi);
            }
            return result;
        }
    }
}
