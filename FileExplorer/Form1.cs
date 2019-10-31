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
            Service =new FileService();
            PrepareDataForView();
        }

        private void PrepareDataForView()
        {
            this.FileTree.ImageList = this.SmallIconList;
            this.FileList.SmallImageList = this.SmallIconList;
            this.FileList.LargeImageList = this.LargeIconList;
            var headers = ListViewItemFactory.GetHeaderItems();
            foreach (var header in headers)
            {
                this.FileList.Columns.Add(header);
            }
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var path = this.PathTb.Text;
            if (string.IsNullOrWhiteSpace(path))
                return;
            
            this.FileList.BeginUpdate();
            this.FileList.Items.Clear();
            var list =await Service.GetFileItemsAsync(path);
            var items =await ListViewItemFactory.GetDetailItemsAsync(list);
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }
            this.FileList.EndUpdate();
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.Details;
        }

        private void SmallBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.SmallIcon;
        }

        private void LargeBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.LargeIcon;
        }

        private void ListBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.List;
        }
    }
}
