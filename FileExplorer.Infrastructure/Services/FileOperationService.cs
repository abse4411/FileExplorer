using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FileExplorer.Core.Services;

namespace FileExplorer.Infrastructure.Services
{
    public class FileOperationService
    {
        #region Copy
        public async Task<IList<FileItemRestoreInfo>> CopyFileItem(IList<FileItemInfo> sources, string targetPath, bool overwrite)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileItemRestoreInfo>();
                if (!Directory.Exists(targetPath))
                    throw new DirectoryNotFoundException(targetPath);
                foreach (var item in sources)
                {
                    if (!item.IsDirectory)
                    {
                        var targetName = Path.Combine(targetPath, item.Name);
                        if (CopyFile(item.FullName, targetName, overwrite))
                            result.Add(new FileItemRestoreInfo
                            {
                                SourceName = item.FullName,
                                TargetName = targetName,
                                IsDirectory = false
                            });
                    }
                    else
                    {
                        DirectoryInfo directory;
                        try
                        {
                            directory = new DirectoryInfo(item.FullName);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine((e));
                            continue;
                        }
                        CopyDirectory(directory, targetPath, overwrite, result);
                    }
                }
                return result;
            });
        }
        private static void CopyDirectory(DirectoryInfo sourceInfo, string targetPath, bool overwrite, IList<FileItemRestoreInfo> result)
        {
            string path = Path.Combine(targetPath, sourceInfo.Name);
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Debug.WriteLine((e));
                    return;
                }
                result.Add(new FileItemRestoreInfo
                {
                    SourceName = sourceInfo.FullName,
                    TargetName = path,
                    IsDirectory = true
                });
            }
            IEnumerable<FileInfo> files;
            try
            {
                files = sourceInfo.EnumerateFiles();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }
            foreach (var file in files)
            {
                var targetName = Path.Combine(path, file.Name);
                if (CopyFile(file.FullName, targetName, overwrite))
                    result.Add(new FileItemRestoreInfo
                    {
                        SourceName = file.FullName,
                        TargetName = targetName,
                        IsDirectory = false
                    });
            }
            IEnumerable<DirectoryInfo> dirs;
            try
            {
                dirs = sourceInfo.EnumerateDirectories();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }
            foreach (var dir in dirs)
            {
                CopyDirectory(dir, path, overwrite, result);
            }
        }
        private static bool CopyFile(string sourceFullName, string targetFullName, bool overwrite)
        {
            try
            {
                File.Copy(sourceFullName, targetFullName, overwrite);
            }
            catch (Exception e)
            {
                Debug.WriteLine((e));
                return false;
            }
            return true;
        }
        #endregion

        #region Move
        public async Task<IList<FileItemRestoreInfo>> MoveFileItem(IList<FileItemInfo> sources, string targetPath,bool overwrite)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileItemRestoreInfo>();
                if (!Directory.Exists(targetPath))
                    throw new DirectoryNotFoundException(targetPath);
                foreach (var item in sources)
                {
                    if (!item.IsDirectory)
                    {
                        var targetName = Path.Combine(targetPath, item.Name);
                        if (MoveFile(item.FullName, targetName))
                            result.Add(new FileItemRestoreInfo
                            {
                                SourceName = item.FullName,
                                TargetName = targetName,
                                IsDirectory = false
                            });
                    }
                    else
                    {
                        var targetName = Path.Combine(targetPath, item.Name);
                        if (MoveDirectory(item.FullName, targetName))
                            result.Add(new FileItemRestoreInfo
                            {
                                SourceName = item.FullName,
                                TargetName = targetName,
                                IsDirectory = true
                            });
                    }
                }

                return result;
            });
        }

        private static bool MoveFile(string sourceFullName, string targetFullName)
        {
            try
            {
                File.Move(sourceFullName, targetFullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        private static bool MoveDirectory(string sourcePath, string targetPath)
        {
            try
            {
                Directory.Move(sourcePath,targetPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        #endregion

        #region Delete

        public async Task<IList<FileItemRestoreInfo>> DeleteFileItem(IList<FileItemInfo> sources)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileItemRestoreInfo>();
                foreach (var item in sources)
                {
                    if (!item.IsDirectory)
                    {
                        try
                        {
                            File.Delete(item.FullName);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine((e));
                        }
                    }
                    else
                    {
                        try
                        {
                            Directory.Delete(item.FullName);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine((e));
                        }
                    }
                }
                return result;
            });
        }

        #endregion
    }
}
