using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Services
{
    public class FileItemRestoreInfo
    {
        public string SourceName { get; set; }
        public string TargetName { get; set; }
        public bool IsDirectory { get; set; }
    }
}
