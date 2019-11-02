using System;
using System.Collections.Generic;
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
        public RefreshCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service) : base(cache, listView, pathTbBox, service)
        {
        }
        public override void Execute()
        {
            if (Cache.HistoryMark < 0)
            {
                Cache.HistoryMark++;
                Cache.PathHistory.Add(PathTextBox.Text);
            }
            if (PathTextBox.Text == Environment.MachineName)
                ListView_LoadRoots();
            else
                ListView_LoadItems(PathTextBox.Text);
        }

        public override bool CanDo => true;
    }
}
