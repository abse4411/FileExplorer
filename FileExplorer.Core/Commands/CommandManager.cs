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

        public async Task<ExecuteResult> Execute(Command command)
        {
            if(command==null)
                throw new ArgumentNullException(nameof(command));
            ExecuteResult result;
            commands.Push(command);
            if (CanDo(command))
                result =await command.ExecuteAsync();
            else
                result=new ExecuteResult(false, $"Can execute this command {command}");
            if (!command.CanUndo)
                commands.Pop();

            return result;
        }

        public bool CanDo(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            return command.CanDo;
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
