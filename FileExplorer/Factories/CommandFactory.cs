using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Commands;
using FileExplorer.Core.Services;

namespace FileExplorer.Factories
{
    static class CommandFactory
    {
        public static BackCommand GetBackCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service)
        {
            return new BackCommand(cache,listView,pathTbBox,service);
        }

        public static ForwardCommand GetForwardCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service)
        {
            return new ForwardCommand(cache, listView, pathTbBox, service);
        }

        public static LoadListCommand GetLoadCommand(PathHistoryCache cache, ListView listView, TextBox pathTbBox, IFileService service)
        {
            return new LoadListCommand(cache, listView, pathTbBox, service);
        }
    }
}
