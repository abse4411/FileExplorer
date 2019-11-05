using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Infrastructure.Services
{
    public class FileItemInfo
    {
        public string Name;
        public bool IsDirectory;
        public FileItemInfo() { }
        public FileItemInfo(string name,bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
        }
    }
}
