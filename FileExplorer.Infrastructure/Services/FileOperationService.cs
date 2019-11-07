using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileExplorer.Core.Models;
using FileExplorer.Core.Services;

namespace FileExplorer.Infrastructure.Services
{
    public class FileOperationService: IFileOperationService
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

        public async Task<IList<FileItemRestoreInfo>> UndoCopy(IList<FileItemRestoreInfo> sources)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileItemRestoreInfo>();
                var undoList = sources.Reverse();
                foreach (var item in undoList)
                {
                    if (!item.IsDirectory)
                    {
                        try
                        {
                            File.Delete(item.TargetName);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine((e));
                        }
                        result.Add(new FileItemRestoreInfo
                        {
                            SourceName = item.TargetName,
                            TargetName = string.Empty,
                            IsDirectory = false
                        });
                    }
                    else
                    {
                        try
                        {
                            Directory.Delete(item.TargetName);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine((e));
                        }
                        result.Add(new FileItemRestoreInfo
                        {
                            SourceName = item.TargetName,
                            TargetName = string.Empty,
                            IsDirectory = true
                        });
                    }
                }
                return result;
            });
        }
        #endregion

        #region Move
        public async Task<IList<FileItemRestoreInfo>> MoveFileItem(IList<FileItemInfo> sources, string targetPath, bool overwrite)
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
                        if (MoveFile(item.FullName, targetName, overwrite))
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
                        MoveDirectory(directory, targetPath, overwrite, result);
                    }
                }

                return result;
            });
        }

        private static bool MoveFile(string sourceFullName, string targetFullName, bool overwrite)
        {
            try
            {
                if (overwrite)
                {
                    if (File.Exists(targetFullName))
                        File.Delete(targetFullName);
                }
                File.Move(sourceFullName, targetFullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        private static void MoveDirectory(DirectoryInfo sourceInfo, string targetPath, bool overwrite, IList<FileItemRestoreInfo> result)
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
                if (MoveFile(file.FullName, targetName, overwrite))
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
                MoveDirectory(dir, path, overwrite, result);
            }
            try
            {
                sourceInfo.Delete();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return;
            }
            result.Add(new FileItemRestoreInfo
            {
                SourceName = sourceInfo.FullName,
                TargetName = path,
                IsDirectory = true
            });
        }

        public async Task<IList<FileItemRestoreInfo>> UndoMove(IList<FileItemRestoreInfo> sources, bool overwrite)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileItemRestoreInfo>();
                var emptydirs = new List<string>();
                var undoList = sources.Reverse();
                foreach (var item in undoList)
                {
                    if (!item.IsDirectory)
                    {
                        if (MoveFile(item.TargetName, item.SourceName, overwrite))
                            result.Add(new FileItemRestoreInfo
                            {
                                SourceName = item.TargetName,
                                TargetName = item.SourceName,
                                IsDirectory = false
                            });
                    }
                    else
                    {
                        if (!Directory.Exists(item.SourceName))
                        {
                            try
                            {
                                Directory.CreateDirectory(item.SourceName);
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine((e));
                                continue;
                            }
                            result.Add(new FileItemRestoreInfo
                            {
                                SourceName = item.TargetName,
                                TargetName = item.SourceName,
                                IsDirectory = true
                            });
                            emptydirs.Add(item.TargetName);
                        }
                    }
                }
                foreach (var dir in emptydirs)
                {
                    try
                    {
                        Directory.Delete(dir);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine((e));
                        continue;
                    }
                }
                return result;
            });

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
                            DeleteDirectory(item.FullName);
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

        private static bool DeleteDirectory(string targetPath)
        {
            try
            {
                var files = Directory.EnumerateFiles(targetPath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
                var dirs = Directory.EnumerateDirectories(targetPath);
                foreach (var dir in dirs)
                {
                    DeleteDirectory(dir);
                }
                Directory.Delete(targetPath);
            }
            catch (Exception e)
            {
                Debug.WriteLine((e));
                return false;
            }
            return true;
        }
        #endregion
    }
}
