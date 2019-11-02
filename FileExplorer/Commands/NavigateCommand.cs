using System;
using System.Collections.Generic;
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
    public abstract class NavigateCommand : Command
    {
        protected PathHistoryCache Cache { get; }
        protected ListView ListView { get; }
        protected IFileService Service { get; }

        protected NavigateCommand(PathHistoryCache cache, ListView listView, IFileService service)
        {
            Cache = cache;
            ListView = listView;
            Service = service;
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override bool CanUndo { get; } = false;

        protected void ListView_LoadRoots()
        {
            ListView.BeginUpdate();
            ListView.Clear();
            var headers = ListViewItemFactory.GetDriverHeaderItems();
            foreach (var header in headers)
            {
                ListView.Columns.Add(header);
            }
            var items = ListViewItemFactory.GetRootDetailIItems();
            foreach (var item in items)
            {
                ListView.Items.Add(item);
            }
            ListView.EndUpdate();
            ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        protected async Task<bool> ListView_LoadItems(string path)
        {
            if (!Directory.Exists(path))
            {
                ListView.Clear();
                return false;
            }
            ListView.BeginUpdate();
            ListView.Clear();
            var headers = ListViewItemFactory.GetFIleHeaderItems();
            foreach (var header in headers)
            {
                ListView.Columns.Add(header);
            }
            var list = await Service.GetFileItemsAsync(path);
            var items = await ListViewItemFactory.GetDetailItemsAsync(list);
            foreach (var item in items)
            {
                ListView.Items.Add(item);
            }
            ListView.EndUpdate();
            ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            return true;
        }
    }
}
