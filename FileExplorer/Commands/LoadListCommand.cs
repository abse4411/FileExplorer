﻿using System;
using System.Diagnostics;
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
                return new ExecuteResult(false, "Path must be not empty or null");
            Cache.HistoryMark++;
            if (Cache.HistoryMark <= Cache.PathHistory.Count - 1)
                Cache.PathHistory.RemoveRange(Cache.HistoryMark, Cache.PathHistory.Count - Cache.HistoryMark);
            Cache.PathHistory.Add(Path);
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
