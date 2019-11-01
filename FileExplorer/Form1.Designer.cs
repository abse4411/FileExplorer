﻿namespace FileExplorer
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
            this.PathTb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SmallIconList = new System.Windows.Forms.ImageList(this.components);
            this.LargeIconList = new System.Windows.Forms.ImageList(this.components);
            this.DetailBtn = new System.Windows.Forms.Button();
            this.SmallBtn = new System.Windows.Forms.Button();
            this.LargeBtn = new System.Windows.Forms.Button();
            this.ListBtn = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.ListView();
            this.FileTree = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BackBtn = new System.Windows.Forms.Button();
            this.ForwardBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PathTb
            // 
            this.PathTb.Location = new System.Drawing.Point(216, 34);
            this.PathTb.Name = "PathTb";
            this.PathTb.Size = new System.Drawing.Size(509, 25);
            this.PathTb.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(741, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "LoadBtn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SmallIconList
            // 
            this.SmallIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallIconList.ImageStream")));
            this.SmallIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallIconList.Images.SetKeyName(0, "folder_S.png");
            this.SmallIconList.Images.SetKeyName(1, "document_S.png");
            this.SmallIconList.Images.SetKeyName(2, "hard-disk_S.png");
            this.SmallIconList.Images.SetKeyName(3, "computer_S.png");
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
            this.DetailBtn.Location = new System.Drawing.Point(720, 69);
            this.DetailBtn.Name = "DetailBtn";
            this.DetailBtn.Size = new System.Drawing.Size(66, 24);
            this.DetailBtn.TabIndex = 3;
            this.DetailBtn.Text = "DetailBtn";
            this.DetailBtn.UseVisualStyleBackColor = true;
            this.DetailBtn.Click += new System.EventHandler(this.DetailBtn_Click);
            // 
            // SmallBtn
            // 
            this.SmallBtn.Location = new System.Drawing.Point(792, 69);
            this.SmallBtn.Name = "SmallBtn";
            this.SmallBtn.Size = new System.Drawing.Size(66, 24);
            this.SmallBtn.TabIndex = 4;
            this.SmallBtn.Text = "SmallBtn";
            this.SmallBtn.UseVisualStyleBackColor = true;
            this.SmallBtn.Click += new System.EventHandler(this.SmallBtn_Click);
            // 
            // LargeBtn
            // 
            this.LargeBtn.Location = new System.Drawing.Point(864, 69);
            this.LargeBtn.Name = "LargeBtn";
            this.LargeBtn.Size = new System.Drawing.Size(66, 24);
            this.LargeBtn.TabIndex = 5;
            this.LargeBtn.Text = "LargeBtn";
            this.LargeBtn.UseVisualStyleBackColor = true;
            this.LargeBtn.Click += new System.EventHandler(this.LargeBtn_Click);
            // 
            // ListBtn
            // 
            this.ListBtn.Location = new System.Drawing.Point(936, 69);
            this.ListBtn.Name = "ListBtn";
            this.ListBtn.Size = new System.Drawing.Size(66, 24);
            this.ListBtn.TabIndex = 6;
            this.ListBtn.Text = "ListBtn";
            this.ListBtn.UseVisualStyleBackColor = true;
            this.ListBtn.Click += new System.EventHandler(this.ListBtn_Click);
            // 
            // FileList
            // 
            this.FileList.AllowColumnReorder = true;
            this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileList.FullRowSelect = true;
            this.FileList.HideSelection = false;
            this.FileList.Location = new System.Drawing.Point(0, 0);
            this.FileList.Name = "FileList";
            this.FileList.ShowItemToolTips = true;
            this.FileList.Size = new System.Drawing.Size(815, 509);
            this.FileList.TabIndex = 0;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.DoubleClick += new System.EventHandler(this.FileList_DoubleClick);
            // 
            // FileTree
            // 
            this.FileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTree.Location = new System.Drawing.Point(0, 0);
            this.FileTree.Name = "FileTree";
            this.FileTree.ShowNodeToolTips = true;
            this.FileTree.Size = new System.Drawing.Size(200, 509);
            this.FileTree.TabIndex = 7;
            this.FileTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.FileTree_BeforeExpand);
            this.FileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 99);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FileTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FileList);
            this.splitContainer1.Size = new System.Drawing.Size(1019, 509);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 8;
            // 
            // BackBtn
            // 
            this.BackBtn.Location = new System.Drawing.Point(12, 34);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(96, 25);
            this.BackBtn.TabIndex = 9;
            this.BackBtn.Text = "BackBtn";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // ForwardBtn
            // 
            this.ForwardBtn.Location = new System.Drawing.Point(114, 34);
            this.ForwardBtn.Name = "ForwardBtn";
            this.ForwardBtn.Size = new System.Drawing.Size(98, 25);
            this.ForwardBtn.TabIndex = 10;
            this.ForwardBtn.Text = "ForwardBtn";
            this.ForwardBtn.UseVisualStyleBackColor = true;
            this.ForwardBtn.Click += new System.EventHandler(this.ForwardBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 620);
            this.Controls.Add(this.ForwardBtn);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ListBtn);
            this.Controls.Add(this.LargeBtn);
            this.Controls.Add(this.SmallBtn);
            this.Controls.Add(this.DetailBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PathTb);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox PathTb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList SmallIconList;
        private System.Windows.Forms.ImageList LargeIconList;
        private System.Windows.Forms.Button DetailBtn;
        private System.Windows.Forms.Button SmallBtn;
        private System.Windows.Forms.Button LargeBtn;
        private System.Windows.Forms.Button ListBtn;
        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.TreeView FileTree;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Button ForwardBtn;
    }
}

