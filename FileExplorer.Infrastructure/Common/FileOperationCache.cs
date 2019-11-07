using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Models;

namespace FileExplorer.Infrastructure.Common
{
    public enum FileOperation
    {
        Copy,Cut,Paste,Delete,None
    }
    public class FileOperationCache
    {
        public IList<FileItemRestoreInfo> RestoreInfos { get; set; }
        public IList<FileItemInfo> SelectedFileItem { get; set; }
        public FileOperation CurrentOperation { get; set; }
    }
}
