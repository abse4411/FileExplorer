using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Models;
using FileExplorer.Core.Services;
using FileExplorer.Factories;

namespace FileExplorer.Commands
{
    public class SearchCommand: ICommand
    {
        public ListView ListView { get; }
        public PathHistoryCache Cache { get; }
        public string Pattern { get; }
        public IFileService Service { get; }

        public SearchCommand(ListView listView,PathHistoryCache cache,string pattern,IFileService service)
        {
            ListView = listView;
            Cache = cache;
            Pattern = pattern;
            Service = service;
        }

        public async Task<ExecuteResult> ExecuteAsync()
        {
            var path = Cache.PathHistory[Cache.HistoryMark];
            if (string.IsNullOrWhiteSpace(path) || 
                string.IsNullOrWhiteSpace(Pattern) || !Directory.Exists(path))
                return new ExecuteResult(false, "Invalid search path or pattern");
            ListView.Clear();
            ListView.BeginUpdate();
            var headers = ListViewItemFactory.GetFileHeaderItems();
            foreach (var header in headers)
            {
                ListView.Columns.Add(header);
            }
            ListView.EndUpdate();
            IList<FileItem> list;
            try
            {
                 list = await Service.FindFileItemsAsync(path, Pattern, SearchOption.AllDirectories);
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            var items = await ListViewItemFactory.GetDetailItemsAsync(list);
            ListView.BeginUpdate();
            foreach (var item in items)
            {
                ListView.Items.Add(item);
            }
            ListView.EndUpdate();
            ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                return new ExecuteResult(true, string.Empty);
        }

        public Task<ExecuteResult> Undo()
        {
            throw new NotImplementedException();
        }

        public bool CanDo => Cache.HistoryMark>-1;

        public bool CanUndo => false;
    }
}
