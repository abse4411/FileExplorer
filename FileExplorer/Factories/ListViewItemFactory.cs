using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Model;

namespace FileExplorer.Factories
{
    static class ListViewItemFactory
    {
        private static readonly IList<ColumnHeader> _headers;
        static ListViewItemFactory()
        {
            string[] headerStrings=new[] {"Name", "LastWriteTime", "LastAccessTime", "CreationTime", "Type" ,"Size"};
            _headers = new List<ColumnHeader>();
            foreach (var str in headerStrings)
            {
                _headers.Add(new ColumnHeader
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
                        lvi.SubItems.Add(item.Length.ToString());
                        lvi.Tag = FactoryConstants.File;
                        lvi.ToolTipText = item.Name;
                    }
                    result.Add(lvi);
                }
                return result;
            });
        }

        public static IList<ColumnHeader> GetHeaderItems()
        {
            return _headers;
        }
    }
}
