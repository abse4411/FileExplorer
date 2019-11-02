using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;

namespace FileExplorer.Commands
{
    public class RefreshCommand: NavigateCommand
    {
        public string Path { get; }

        public RefreshCommand(PathHistoryCache cache, ListView listView, string path, IFileService service) : base(cache, listView, service)
        {
            Path = path;
        }
        public override async Task<ExecuteResult> ExecuteAsync()
        {
            if (string.IsNullOrWhiteSpace(Path))
                return new ExecuteResult(true, String.Empty);
            if (Cache.HistoryMark < 0)
            {
                Cache.HistoryMark++;
                Cache.PathHistory.Add(Path);
            }
            try
            {
                if (Path == Environment.MachineName)
                    ListView_LoadRoots();
                else if (!await ListView_LoadItems(Path))
                {
                    return new ExecuteResult(false, $"Directory \"{Path}\" does not exist");
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            return new ExecuteResult(true, String.Empty);
        }

        public override bool CanDo => true;
    }
}
