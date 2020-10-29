/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
namespace Korot
{
    partial class frmCollection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCollection));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteThisCollectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCollectionIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCollectionTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ımportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCollection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ıTEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteThisİtemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportThisİtemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editThisİtemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ımportİtemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.cmsMain.SuspendLayout();
            this.cmsCollection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpMain);
            this.tabControl1.Location = new System.Drawing.Point(-8, -32);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(791, 375);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.listView1);
            this.tpMain.Location = new System.Drawing.Point(4, 25);
            this.tpMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpMain.Size = new System.Drawing.Size(783, 346);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Collections";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.cmsMain;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 4);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(777, 338);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // cmsMain
            // 
            this.cmsMain.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCollectionToolStripMenuItem,
            this.deleteThisCollectionsToolStripMenuItem,
            this.changeCollectionIDToolStripMenuItem,
            this.changeCollectionTextToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem,
            this.ımportToolStripMenuItem});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsMain.ShowImageMargin = false;
            this.cmsMain.Size = new System.Drawing.Size(186, 164);
            this.cmsMain.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMain_Opening);
            // 
            // newCollectionToolStripMenuItem
            // 
            this.newCollectionToolStripMenuItem.Name = "newCollectionToolStripMenuItem";
            this.newCollectionToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.newCollectionToolStripMenuItem.Text = "New Collection";
            this.newCollectionToolStripMenuItem.Click += new System.EventHandler(this.newCollectionToolStripMenuItem_Click);
            // 
            // deleteThisCollectionsToolStripMenuItem
            // 
            this.deleteThisCollectionsToolStripMenuItem.Name = "deleteThisCollectionsToolStripMenuItem";
            this.deleteThisCollectionsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.deleteThisCollectionsToolStripMenuItem.Text = "Delete this Collection";
            this.deleteThisCollectionsToolStripMenuItem.Click += new System.EventHandler(this.deleteThisCollectionsToolStripMenuItem_Click);
            // 
            // changeCollectionIDToolStripMenuItem
            // 
            this.changeCollectionIDToolStripMenuItem.Name = "changeCollectionIDToolStripMenuItem";
            this.changeCollectionIDToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.changeCollectionIDToolStripMenuItem.Text = "Change Collection ID";
            this.changeCollectionIDToolStripMenuItem.Click += new System.EventHandler(this.changeCollectionIDToolStripMenuItem_Click);
            // 
            // changeCollectionTextToolStripMenuItem
            // 
            this.changeCollectionTextToolStripMenuItem.Name = "changeCollectionTextToolStripMenuItem";
            this.changeCollectionTextToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.changeCollectionTextToolStripMenuItem.Text = "Change Collection Text";
            this.changeCollectionTextToolStripMenuItem.Click += new System.EventHandler(this.changeCollectionTextToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // ımportToolStripMenuItem
            // 
            this.ımportToolStripMenuItem.Name = "ımportToolStripMenuItem";
            this.ımportToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.ımportToolStripMenuItem.Text = "Import";
            this.ımportToolStripMenuItem.Click += new System.EventHandler(this.ımportToolStripMenuItem_Click);
            // 
            // cmsCollection
            // 
            this.cmsCollection.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.cmsCollection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ıTEMToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteThisİtemToolStripMenuItem,
            this.exportThisİtemToolStripMenuItem,
            this.editThisİtemToolStripMenuItem,
            this.ımportİtemToolStripMenuItem});
            this.cmsCollection.Name = "cmsCollection";
            this.cmsCollection.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsCollection.ShowImageMargin = false;
            this.cmsCollection.Size = new System.Drawing.Size(149, 120);
            // 
            // ıTEMToolStripMenuItem
            // 
            this.ıTEMToolStripMenuItem.Enabled = false;
            this.ıTEMToolStripMenuItem.Name = "ıTEMToolStripMenuItem";
            this.ıTEMToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ıTEMToolStripMenuItem.Text = "ITEM";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // deleteThisİtemToolStripMenuItem
            // 
            this.deleteThisİtemToolStripMenuItem.Name = "deleteThisİtemToolStripMenuItem";
            this.deleteThisİtemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteThisİtemToolStripMenuItem.Text = "Delete this item";
            this.deleteThisİtemToolStripMenuItem.Click += new System.EventHandler(this.deleteThisİtemToolStripMenuItem_Click);
            // 
            // exportThisİtemToolStripMenuItem
            // 
            this.exportThisİtemToolStripMenuItem.Name = "exportThisİtemToolStripMenuItem";
            this.exportThisİtemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exportThisİtemToolStripMenuItem.Text = "Export this item";
            this.exportThisİtemToolStripMenuItem.Click += new System.EventHandler(this.exportThisİtemToolStripMenuItem_Click);
            // 
            // editThisİtemToolStripMenuItem
            // 
            this.editThisİtemToolStripMenuItem.Name = "editThisİtemToolStripMenuItem";
            this.editThisİtemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.editThisİtemToolStripMenuItem.Text = "Edit this item";
            this.editThisİtemToolStripMenuItem.Click += new System.EventHandler(this.editThisİtemToolStripMenuItem_Click);
            // 
            // ımportİtemToolStripMenuItem
            // 
            this.ımportİtemToolStripMenuItem.Name = "ımportİtemToolStripMenuItem";
            this.ımportİtemToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ımportİtemToolStripMenuItem.Text = "Import item";
            this.ımportİtemToolStripMenuItem.Click += new System.EventHandler(this.ımportİtemToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 2500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 336);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Ubuntu", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCollection";
            this.Text = "frmCollection";
            this.Load += new System.EventHandler(this.frmCollection_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.cmsMain.ResumeLayout(false);
            this.cmsCollection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.ContextMenuStrip cmsMain;
        private System.Windows.Forms.ToolStripMenuItem deleteThisCollectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ımportToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsCollection;
        private System.Windows.Forms.ToolStripMenuItem ıTEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteThisİtemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportThisİtemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editThisİtemToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem newCollectionToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem ımportİtemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCollectionIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeCollectionTextToolStripMenuItem;
    }
}