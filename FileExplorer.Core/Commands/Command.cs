using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Commands
{
    public interface ICommand
    {
        Task<ExecuteResult> ExecuteAsync();

        Task<ExecuteResult> Undo();

        bool CanDo { get; }

        bool CanUndo { get; }
    }
}
