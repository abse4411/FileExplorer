using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Commands
{
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void Undo();

        public virtual bool CanDo { get; }

        public virtual bool CanUndo { get; }
    }
}
