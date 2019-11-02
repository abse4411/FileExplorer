using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.Core.Services
{
    public interface IDialogService
    {
        Result ShowExceptionDialog(string title, string message);
        Result ShowErrorDialog(string title, string message);
        Result ShowConfirmDialog(string title, string message);
        Result ShowWarmingDialog(string title, string message);
        Result ShowInformationDialog(string title, string message);
        Result ShowQuestionDialog(string title, string message);
    }
}
