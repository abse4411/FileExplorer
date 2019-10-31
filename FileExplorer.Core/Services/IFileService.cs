using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Model;

namespace FileExplorer.Core.Services
{
    public interface IFileService
    {
        Task<IList<FileItem>> GetFileItems(string path);
        Task<IList<FileItem>> GetFiles(string path);
        Task<IList<FileItem>> GetDirectories(string path);

        Task<IList<FileItem>> FindFileItems(string path,string pattern, SearchOption option);
        Task<IList<FileItem>> FindFiles(string path, string pattern, SearchOption option);
        Task<IList<FileItem>> FindDirectories(string path, string pattern, SearchOption option);
    }
}
