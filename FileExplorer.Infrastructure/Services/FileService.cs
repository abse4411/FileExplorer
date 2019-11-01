using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Model;
using FileExplorer.Core.Services;

namespace FileExplorer.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<IList<FileItem>> GetFileItemsAsync(string path)
        {
            return await Task.Run(() => GetFileItems(path,EnumerateFileItemOption.All));
        }

        public async Task<IList<FileItem>> GetFilesAsync(string path)
        {
            return await Task.Run(() => GetFileItems(path, EnumerateFileItemOption.File));
        }

        public async Task<IList<FileItem>> GetDirectoriesAsync(string path)
        {
            return await Task.Run(() => GetFileItems(path, EnumerateFileItemOption.Directory));
        }

        public async Task<IList<FileItem>> FindFileItemsAsync(string path, string pattern, SearchOption option)
        {
            return await Task.Run(() => GetFileItems(path,pattern, EnumerateFileItemOption.All,option));
        }

        public async Task<IList<FileItem>> FindFilesAsync(string path, string pattern, SearchOption option)
        {
            return await Task.Run(() => GetFileItems(path, pattern, EnumerateFileItemOption.File, option));
        }

        public async Task<IList<FileItem>> FindDirectoriesAsync(string path, string pattern, SearchOption option)
        {
            return await Task.Run(() => GetFileItems(path, pattern, EnumerateFileItemOption.Directory, option));
        }

        private FileItem CreateFileItem(FileSystemInfo info)
        {
            FileItem item = new FileItem
            {
                Name = info.Name,
                IsDirectory = false,
                FullName = info.FullName,
                CreationTime = info.CreationTime,
                LastAccessTime = info.LastAccessTime,
                LastWriteTime = info.LastWriteTime
            };
            if (info is FileInfo file)
            {
                item.Length = file.Length;
                item.IsDirectory = false;
            }
            else if (info is DirectoryInfo dir)
            {
                item.IsDirectory = true;
            }
            else
            {
                throw new InvalidCastException("info is not a FileInfo neither a DirectoryInfo");
            }
            return item;
        }

        private IList<FileItem> GetFileItems(string path,EnumerateFileItemOption option)
        {
            var result = new List<FileItem>();
            var dir = new DirectoryInfo(path);
            IEnumerable<FileSystemInfo> list;
            try
            {
                switch (option)
                {
                    case EnumerateFileItemOption.All:
                        list = dir.EnumerateFileSystemInfos();
                        break;
                    case EnumerateFileItemOption.File:
                        list = dir.EnumerateFiles();
                        break;
                    case EnumerateFileItemOption.Directory:
                        list = dir.EnumerateDirectories();
                        break;
                    default:
                        throw new ArgumentException("Invalid value", nameof(option));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return result;
            }
            foreach (var item in list)
            {
                result.Add(CreateFileItem(item));
            }
            return result;
        }

        private IList<FileItem> GetFileItems(string path,string pattern, EnumerateFileItemOption fetchOption, SearchOption option)
        {
            var dir = new DirectoryInfo(path);
            IEnumerable<FileSystemInfo> list;
            var result = new List<FileItem>();
            try
            {
                switch (fetchOption)
                {
                    case EnumerateFileItemOption.All:
                        list = dir.EnumerateFileSystemInfos(pattern, option);
                        break;
                    case EnumerateFileItemOption.File:
                        list = dir.EnumerateFiles(pattern, option);
                        break;
                    case EnumerateFileItemOption.Directory:
                        list = dir.EnumerateDirectories(pattern, option);
                        break;
                    default:
                        throw new ArgumentException("Invalid value", nameof(option));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e);
                return result;
            }
            foreach (var item in list)
            {
                result.Add(CreateFileItem(item));
            }
            return result;
        }
    }
}
