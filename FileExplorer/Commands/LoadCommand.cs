using System;
using System.Windows.Forms;
using FileExplorer.Core.Services;

namespace FileExplorer.Commands
{
    public class LoadListCommand : NavigateCommand
    {
        public LoadListCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service) : base(cache, listView, pathTbBox, service)
        {
        }

        public override void Execute()
        {
            Cache.HistoryMark++;
            if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
                Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
            Cache.PathHistory.Add(PathTextBox.Text);
            if (PathTextBox.Text == Environment.MachineName)
                ListView_LoadRoots();
            else
                ListView_LoadItems(PathTextBox.Text);
        }

        public override bool CanDo
        {
            get
            {
                if (Cache.HistoryMark > -1 && Cache.PathHistory[Cache.HistoryMark] == PathTextBox.Text)
                    return false;
                return true;
            }
        }
    }
}
