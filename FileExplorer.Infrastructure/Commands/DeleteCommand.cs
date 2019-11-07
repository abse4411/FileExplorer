using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Core.Commands;
using FileExplorer.Core.Services;
using FileExplorer.Infrastructure.Common;

namespace FileExplorer.Infrastructure.Commands
{
    public class DeleteCommand:ICommand
    {
        private readonly FileOperationCache _cache;
        private readonly IFileOperationService _service;

        public DeleteCommand(FileOperationCache cache, IFileOperationService service)
        {
            _cache = cache;
            _service = service;
        }
        public bool CanDo => _cache.SelectedFileItem?.Count > 0;

        public bool CanUndo => false;

        public async Task<ExecuteResult> ExecuteAsync()
        {
            try
            {
                _cache.RestoreInfos = await _service.DeleteFileItem(_cache.SelectedFileItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ExecuteResult(false, e.Message);
            }
            return new ExecuteResult(true, string.Empty);
        }

        public Task<ExecuteResult> Undo()
        {
            throw new NotImplementedException();
        }
    }
}
