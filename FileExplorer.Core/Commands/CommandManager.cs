using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Commands
{
    public class CommandManager
    {
        private readonly Stack<Command> commands = new Stack<Command>();

        public void Execute(Command command)
        {
            commands.Push(command);
            if(command.CanDo)
                command.Execute();
            if (!command.CanUndo)
                commands.Pop();
        }

        public void Undo()
        {
            if (commands.Count > 0)
            {
                var command = commands.Pop();
                if(command.CanUndo)
                    command.Undo();
            }
        }
    }
}
