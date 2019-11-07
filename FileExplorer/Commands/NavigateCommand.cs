using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Factories;

namespace FileExplorer.Commands
{
    public abstract class NavigateCommand : ICommand
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

        public Task<ExecuteResult> Undo()
        {
            throw new NotImplementedException();
        }

        public bool CanUndo { get; } = false;
        public abstract bool CanDo { get; }

        protected void ListView_LoadRoots()
        {
            ListView.Clear();
            ListView.BeginUpdate();
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
            ListView.Clear();
            if (!Directory.Exists(path))
            {
                return false;
            }
            ListView.BeginUpdate();
            var headers = ListViewItemFactory.GetFileHeaderItems();
            foreach (var header in headers)
            {
                ListView.Columns.Add(header);
            }
            ListView.EndUpdate();
            var list = await Service.GetFileItemsAsync(path);
            var items = await ListViewItemFactory.GetDetailItemsAsync(list);
            ListView.BeginUpdate();
            foreach (var item in items)
            {
                ListView.Items.Add(item);
            }
            ListView.EndUpdate();
            ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            return true;
        }

        public abstract Task<ExecuteResult> ExecuteAsync();
    }
}
