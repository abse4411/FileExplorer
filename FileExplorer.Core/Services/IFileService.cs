using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Models;

namespace FileExplorer.Core.Services
{
    public interface IFileService
    {
        Task<IList<FileItem>> GetFileItemsAsync(string path);
        Task<IList<FileItem>> GetFilesAsync(string path);
        Task<IList<FileItem>> GetDirectoriesAsync(string path);

        Task<IList<FileItem>> FindFileItemsAsync(string path,string pattern, SearchOption option);
        Task<IList<FileItem>> FindFilesAsync(string path, string pattern, SearchOption option);
        Task<IList<FileItem>> FindDirectoriesAsync(string path, string pattern, SearchOption option);

        
    }
}
