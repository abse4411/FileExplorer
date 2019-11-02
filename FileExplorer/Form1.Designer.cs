namespace FileExplorer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SmallIconList = new System.Windows.Forms.ImageList(this.components);
            this.LargeIconList = new System.Windows.Forms.ImageList(this.components);
            this.DetailBtn = new System.Windows.Forms.Button();
            this.SmallBtn = new System.Windows.Forms.Button();
            this.LargeBtn = new System.Windows.Forms.Button();
            this.ListBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FileTree = new System.Windows.Forms.TreeView();
            this.FileList = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ForwardBtn = new System.Windows.Forms.Button();
            this.BackBtn = new System.Windows.Forms.Button();
            this.PathTb = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.RefreshBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SmallIconList
            // 
            this.SmallIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallIconList.ImageStream")));
            this.SmallIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallIconList.Images.SetKeyName(0, "folder_S.png");
            this.SmallIconList.Images.SetKeyName(1, "document_S.png");
            this.SmallIconList.Images.SetKeyName(2, "hard-disk_S.png");
            this.SmallIconList.Images.SetKeyName(3, "computer_S.png");
            this.SmallIconList.Images.SetKeyName(4, "left-arrow_S.png");
            this.SmallIconList.Images.SetKeyName(5, "right-arrow_S.png");
            this.SmallIconList.Images.SetKeyName(6, "refresh_S.png");
            // 
            // LargeIconList
            // 
            this.LargeIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LargeIconList.ImageStream")));
            this.LargeIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.LargeIconList.Images.SetKeyName(0, "folder_L.png");
            this.LargeIconList.Images.SetKeyName(1, "document_L.png");
            this.LargeIconList.Images.SetKeyName(2, "hard-disk_L.png");
            // 
            // DetailBtn
            // 
            this.DetailBtn.Location = new System.Drawing.Point(715, 4);
            this.DetailBtn.Name = "DetailBtn";
            this.DetailBtn.Size = new System.Drawing.Size(66, 24);
            this.DetailBtn.TabIndex = 3;
            this.DetailBtn.Text = "DetailBtn";
            this.DetailBtn.UseVisualStyleBackColor = true;
            this.DetailBtn.Click += new System.EventHandler(this.DetailBtn_Click);
            // 
            // SmallBtn
            // 
            this.SmallBtn.Location = new System.Drawing.Point(792, 4);
            this.SmallBtn.Name = "SmallBtn";
            this.SmallBtn.Size = new System.Drawing.Size(66, 24);
            this.SmallBtn.TabIndex = 4;
            this.SmallBtn.Text = "SmallBtn";
            this.SmallBtn.UseVisualStyleBackColor = true;
            this.SmallBtn.Click += new System.EventHandler(this.SmallBtn_Click);
            // 
            // LargeBtn
            // 
            this.LargeBtn.Location = new System.Drawing.Point(864, 4);
            this.LargeBtn.Name = "LargeBtn";
            this.LargeBtn.Size = new System.Drawing.Size(66, 24);
            this.LargeBtn.TabIndex = 5;
            this.LargeBtn.Text = "LargeBtn";
            this.LargeBtn.UseVisualStyleBackColor = true;
            this.LargeBtn.Click += new System.EventHandler(this.LargeBtn_Click);
            // 
            // ListBtn
            // 
            this.ListBtn.Location = new System.Drawing.Point(936, 4);
            this.ListBtn.Name = "ListBtn";
            this.ListBtn.Size = new System.Drawing.Size(66, 24);
            this.ListBtn.TabIndex = 6;
            this.ListBtn.Text = "ListBtn";
            this.ListBtn.UseVisualStyleBackColor = true;
            this.ListBtn.Click += new System.EventHandler(this.ListBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 623);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1204, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 122);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FileTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FileList);
            this.splitContainer1.Size = new System.Drawing.Size(1204, 501);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 8;
            // 
            // FileTree
            // 
            this.FileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTree.Location = new System.Drawing.Point(0, 0);
            this.FileTree.Name = "FileTree";
            this.FileTree.Size = new System.Drawing.Size(200, 501);
            this.FileTree.TabIndex = 0;
            this.FileTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterExpand);
            this.FileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterSelect);
            // 
            // FileList
            // 
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.HideSelection = false;
            this.FileList.Location = new System.Drawing.Point(0, 0);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(1000, 501);
            this.FileList.TabIndex = 0;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.FileList_ColumnClick);
            this.FileList.DoubleClick += new System.EventHandler(this.FileList_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.ForwardBtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BackBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.RefreshBtn, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.PathTb, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 92);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1204, 30);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // ForwardBtn
            // 
            this.ForwardBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ForwardBtn.AutoSize = true;
            this.ForwardBtn.ImageIndex = 5;
            this.ForwardBtn.ImageList = this.SmallIconList;
            this.ForwardBtn.Location = new System.Drawing.Point(30, 0);
            this.ForwardBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ForwardBtn.Name = "ForwardBtn";
            this.ForwardBtn.Size = new System.Drawing.Size(30, 30);
            this.ForwardBtn.TabIndex = 10;
            this.ForwardBtn.UseVisualStyleBackColor = true;
            this.ForwardBtn.Click += new System.EventHandler(this.ForwardBtn_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BackBtn.AutoSize = true;
            this.BackBtn.ImageIndex = 4;
            this.BackBtn.ImageList = this.SmallIconList;
            this.BackBtn.Location = new System.Drawing.Point(0, 0);
            this.BackBtn.Margin = new System.Windows.Forms.Padding(0);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(30, 30);
            this.BackBtn.TabIndex = 0;
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // PathTb
            // 
            this.PathTb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PathTb.Location = new System.Drawing.Point(60, 3);
            this.PathTb.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.PathTb.Name = "PathTb";
            this.PathTb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PathTb.Size = new System.Drawing.Size(891, 25);
            this.PathTb.TabIndex = 11;
            this.PathTb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PathTb_KeyUp);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(981, 4);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(223, 23);
            this.comboBox1.TabIndex = 12;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshBtn.AutoSize = true;
            this.RefreshBtn.ImageIndex = 6;
            this.RefreshBtn.ImageList = this.SmallIconList;
            this.RefreshBtn.Location = new System.Drawing.Point(951, 0);
            this.RefreshBtn.Margin = new System.Windows.Forms.Padding(0);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(30, 30);
            this.RefreshBtn.TabIndex = 13;
            this.RefreshBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 645);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ListBtn);
            this.Controls.Add(this.LargeBtn);
            this.Controls.Add(this.SmallBtn);
            this.Controls.Add(this.DetailBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList SmallIconList;
        private System.Windows.Forms.ImageList LargeIconList;
        private System.Windows.Forms.Button DetailBtn;
        private System.Windows.Forms.Button SmallBtn;
        private System.Windows.Forms.Button LargeBtn;
        private System.Windows.Forms.Button ListBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView FileTree;
        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.Button ForwardBtn;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.TextBox PathTb;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button RefreshBtn;
    }
}

