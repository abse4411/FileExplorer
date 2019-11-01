using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Services;

namespace FileExplorer.Commands
{
    public class ForwardCommand: NavigateCommand
    {
        public ForwardCommand(PathHistoryCache cache, ListView listView,TextBox pathTbBox, IFileService service) 
            : base(cache, listView, pathTbBox,service)
        {
        }

        public override void Execute()
        {
            Cache.HistoryMark++;
            var path = Cache.PathHistory[Cache.HistoryMark];
            PathTextBox.Text = path;
            if (PathTextBox.Text == Environment.MachineName)
                ListView_LoadRoots();
            else
                ListView_LoadItems(path);
        }

        public override bool CanDo
        {
            get
            {
                if (Cache.HistoryMark>-1 &&  Cache.HistoryMark >= Cache.PathHistory.Count - 1)
                    return false;
                return true;
            }
        }

    }
}
