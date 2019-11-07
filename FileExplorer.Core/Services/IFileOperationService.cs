using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Models;

namespace FileExplorer.Core.Services
{
    public interface IFileOperationService
    {
        Task<IList<FileItemRestoreInfo>> CopyFileItem(IList<FileItemInfo> sources, string targetPath,
            bool overwrite);
        Task<IList<FileItemRestoreInfo>> UndoCopy(IList<FileItemRestoreInfo> sources);
        Task<IList<FileItemRestoreInfo>> MoveFileItem(IList<FileItemInfo> sources, string targetPath,
            bool overwrite);
        Task<IList<FileItemRestoreInfo>> UndoMove(IList<FileItemRestoreInfo> sources, bool overwrite);
        Task<IList<FileItemRestoreInfo>> DeleteFileItem(IList<FileItemInfo> sources);
    }
}
