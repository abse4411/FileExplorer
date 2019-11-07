using System;
using System.Threading.Tasks;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Infrastructure.Common;

namespace FileExplorer.Infrastructure.Commands
{
    public class CutCommand:ICommand
    {
        private readonly FileOperationCache _cache;
        private readonly string _targetPath;
        private readonly IFileOperationService _service;

        public CutCommand(FileOperationCache cache, string targetPath, IFileOperationService service)
        {
            _cache = cache;
            _targetPath = targetPath;
            _service = service;
        }
        public bool CanDo => _cache.SelectedFileItem?.Count > 0;

        public bool CanUndo => _cache.RestoreInfos?.Count > 0;

        public async Task<ExecuteResult> ExecuteAsync()
        {
            if (string.IsNullOrWhiteSpace(_targetPath))
                return new ExecuteResult(false, "Path must be not empty or null");
            try
            {
                _cache.RestoreInfos = await _service.MoveFileItem(_cache.SelectedFileItem, _targetPath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            return new ExecuteResult(true, string.Empty);
        }

        public async Task<ExecuteResult> Undo()
        {
            try
            {
                _cache.RestoreInfos = await _service.UndoMove(_cache.RestoreInfos,true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            return new ExecuteResult(true, string.Empty);
        }
    }
}
