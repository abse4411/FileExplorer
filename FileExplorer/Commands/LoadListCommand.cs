using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;

namespace FileExplorer.Commands
{
    public class LoadListCommand : NavigateCommand
    {
        public string Path { get; }

        public LoadListCommand(PathHistoryCache cache, ListView listView, string path, IFileService service) : base(cache, listView, service)
        {
            Path = path;
        }

        public override async Task<ExecuteResult> ExecuteAsync()
        {
            if(string.IsNullOrWhiteSpace(Path))
                return new ExecuteResult(true, String.Empty);
            Cache.HistoryMark++;
            if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
                Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
            Cache.PathHistory.Add(Path);
            if (Path == Environment.MachineName)
                ListView_LoadRoots();
            else if (!await ListView_LoadItems(Path))
            {
                return new ExecuteResult(false, $"Directory \"{Path}\" does not exist");
            }
            return new ExecuteResult(true, String.Empty);
        }

        public override bool CanDo
        {
            get
            {
                if (Cache.HistoryMark > -1 && Cache.PathHistory[Cache.HistoryMark] == Path)
                    return false;
                return true;
            }
        }
    }
}
