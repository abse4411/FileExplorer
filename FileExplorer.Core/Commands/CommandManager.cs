using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Commands
{
    public class CommandManager
    {
        private readonly Stack<ICommand> commands = new Stack<ICommand>();

        public async Task<ExecuteResult> Execute(ICommand command)
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

        public bool CanDo(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            return command.CanDo;
        }

        public int CommandCount => commands.Count;

        public async Task<ExecuteResult> Undo()
        {
            ICommand command=null;
            if (commands.Count > 0)
            {
                command = commands.Pop();
                if(command.CanUndo)
                    return await command.Undo();
            }
            return new ExecuteResult(false, $"Can execute this command {command}");
        }
    }
}
