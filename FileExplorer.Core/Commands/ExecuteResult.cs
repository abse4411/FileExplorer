using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Commands
{
    public class ExecuteResult
    {
        public bool IsSuccessful{get;}
        public string Message { get; }

        public ExecuteResult(bool isSuccessful,string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
