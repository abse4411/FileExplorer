using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileExplorer.Core.Services;

namespace FileExplorer.Services
{
    public class DialogService:IDialogService
    {
        public Result ShowExceptionDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop));
        }

        public Result ShowErrorDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        public Result ShowConfirmDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning));
        }

        public Result ShowWarmingDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
        }

        public Result ShowInformationDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information));
        }

        public Result ShowQuestionDialog(string title, string message)
        {
            return CreateResult(
                MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question));
        }

        private Result CreateResult(DialogResult result)
        {
            switch (result)
            {
                case DialogResult.Yes:
                    return Result.Yes;
                case DialogResult.OK:
                    return Result.OK;
                case DialogResult.Cancel:
                    return Result.Cancel;
                case DialogResult.No:
                    return Result.No;
                case DialogResult.Ignore:
                    return Result.Ignore;
                case DialogResult.Retry:
                    return Result.Retry;
                case DialogResult.Abort:
                    return Result.Abort;
                case DialogResult.None:
                    return Result.None;
                default:
                    return Result.None;
            }
        }
    }
}
