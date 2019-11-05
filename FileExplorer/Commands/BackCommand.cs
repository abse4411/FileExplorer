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
    public class BackCommand: NavigateCommand
    {
        public TextBox PathTbBox { get; }
        public BackCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service) 
            : base(cache, listView, service)
        {
            PathTbBox = pathTbBox;
        }

        public override async Task<ExecuteResult> ExecuteAsync()
        {
            Cache.HistoryMark--;
            var path = Cache.PathHistory[Cache.HistoryMark];
            PathTbBox.Text = path;
            try
            {
                if (path == Environment.MachineName)
                    ListView_LoadRoots();
                else if (!await ListView_LoadItems(path))
                {
                    return new ExecuteResult(false, $"Directory \"{path}\" does not exist");
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            return new ExecuteResult(true,String.Empty);
        }

        public override bool CanDo
        {
            get
            {
                if (Cache.HistoryMark <= 0)
                    return false;
                return true;
            }
        }
    }
}
