namespace FileExplorer.Core.Models
{
    public class FileItemInfo
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool IsDirectory { get; set; }
        public FileItemInfo() { }
        public FileItemInfo(string name,string fullName,bool isDirectory)
        {
            Name = name;
            FullName = fullName;
            IsDirectory = isDirectory;
        }
    }
}
