namespace FileExplorer.Core.Models
{
    public class FileItemInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool IsDirectory { get; set; }
        public FileItemInfo() { }
        public FileItemInfo(string name,bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
        }
    }
}
