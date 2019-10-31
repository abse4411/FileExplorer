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
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var path = this.PathTb.Text;
            if (string.IsNullOrWhiteSpace(path))
                return;

            this.FileList.Items.Clear();
            this.FileList.BeginUpdate();
            var list =await Service.GetFileItemsAsync(path);
            var items =await ListViewItemFactory.GetDetailItemsAsync(list);
            foreach (var item in items)
            {
                this.FileList.Items.Add(item);
            }
            this.FileList.EndUpdate();
            var nodes = TreeNodeFactory.GetNodes(path);
            this.FileTree.Nodes.Clear();
            this.FileTree.BeginUpdate();
            foreach (var node in nodes)
            {
                this.FileTree.Nodes.Add(node);
            }
            this.FileTree.EndUpdate();

        }

        private void DetailBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.Details;
            this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void SmallBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.SmallIcon;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void LargeBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.LargeIcon;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void ListBtn_Click(object sender, EventArgs e)
        {
            this.FileList.View = View.List;
            //this.FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
