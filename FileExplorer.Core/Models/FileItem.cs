using System;

namespace FileExplorer.Core.Models
{
    public class FileItem
    {
        public string Name { get; set; }
        public bool IsDirectory { get; set; }
        public string FullName { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime CreationTime { get; set; }
        public long Length { get; set; }
    }
}
