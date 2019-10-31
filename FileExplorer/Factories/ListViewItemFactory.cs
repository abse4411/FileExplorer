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
        public static IList<ListViewItem> GetDetailItems(IList<FileItem> list)
        {
            var result=new List<ListViewItem>();

            foreach (var item in list)
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.LastWriteTime.ToString(CultureInfo.CurrentCulture));
                lvi.SubItems.Add(item.LastAccessTime.ToString(CultureInfo.CurrentCulture));
                lvi.SubItems.Add(item.CreationTime.ToLongDateString());
                lvi.SubItems.Add(item.IsDirectory ? "Directory" : "File");
                lvi.SubItems.Add(item.IsDirectory ? string.Empty : item.Length.ToString());
                result.Add(lvi);
            }
            return result;
        }

        public static IList<ColumnHeader> GetHeaderItems()
        {
            return _headers;
        }
    }
}
