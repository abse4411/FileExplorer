using System;
using System.Windows.Forms;
using FileExplorer.Core.Services;
using FileExplorer.Factories;
using FileExplorer.Infrastructure.Services;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        public IFileService Service { get;}

        public Form1()
        {
            InitializeComponent();
            Service=new FileService();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var path = this.PathTb.Text;
            if (string.IsNullOrWhiteSpace(path))
                return;
            this.FileList.Items.Clear();
            this.FileList.View = View.Details;
            this.FileList.BeginUpdate();
            var headers = ListViewItemFactory.GetHeaderItems();
            foreach (var header in headers)
            {
                this.FileList.Columns.Add(header);
            }
            var list =await Service.GetFileItems(path);
            var items = ListViewItemFactory.GetDetailItems(list);
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.FileList.EndUpdate();
        }
    }
}
