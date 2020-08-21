//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNewFav : Form
    {
        private readonly string favName;
        private readonly string favUrl;
        private readonly frmCEF Cefform;
        public frmMain anaform()
        {
            return ((frmMain)Cefform.ParentTabs);
        }
        public frmNewFav(string name, string url, frmCEF _frmCef)
        {
            Cefform = _frmCef;
            favName = name;
            favUrl = url;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }
        public void LoadDynamicMenu()
        {
            treeView1.Nodes.Clear();
            TreeNode rootNode = new TreeNode
            {
                Name = "root",
                Text = "Root",
                ToolTipText = "korot://root"
            };
            treeView1.Nodes.Add(rootNode);

            foreach (Folder folder in Cefform.Settings.Favorites.Favorites)
            {
                if (folder is Favorite)
                {
                    TreeNode menuItem = new TreeNode
                    {
                        Name = folder.Name,
                        Text = folder.Text,
                        Tag = folder,
                        ToolTipText = (folder as Favorite).Url
                    };

                    rootNode.Nodes.Add(menuItem);

                }
                else
                {
                    TreeNode menuItem = new TreeNode
                    {
                        Name = folder.Name,
                        Text = folder.Text,
                        Tag = folder
                    };

                    rootNode.Nodes.Add(menuItem);
                    GenerateMenusFromXML(folder, menuItem);
                }
            }
            treeView1.ExpandAll();
        }
        private void GenerateMenusFromXML(Folder folder, TreeNode menuItem)
        {
            TreeNode item = null;

            foreach (Folder subfolder in folder.Favorites)
            {
                if (subfolder is Favorite)
                {
                    item = new TreeNode
                    {
                        Name = subfolder.Name,
                        Text = subfolder.Text,
                        Tag = subfolder,
                        ToolTipText = (subfolder as Favorite).Url,
                    };
                    menuItem.Nodes.Add(item);
                }
                else
                {
                    item = new TreeNode
                    {
                        Name = subfolder.Name,
                        Text = subfolder.Text,
                        ToolTipText = "korot://folder"
                    };

                    menuItem.Nodes.Add(item);
                    GenerateMenusFromXML(subfolder, item);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmNewFav_Load(object sender, EventArgs e)
        {

            LoadDynamicMenu();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox HTInputBox = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                                    Cefform.folderInfo,
                                                                                                    Cefform.defaultFolderName)
            { Icon = Cefform.Icon, OK = Cefform.OK, SetToDefault = Cefform.SetToDefault, Cancel = Cefform.Cancel, BackgroundColor = Cefform.Settings.Theme.BackColor };
            if (HTInputBox.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(HTInputBox.TextValue))
                {
                    TreeNode newFolder = new TreeNode
                    {
                        Text = HTInputBox.TextValue,
                        Name = HTInputBox.TextValue.Replace(" ", "").Replace(Environment.NewLine, ""),
                        Tag = new Folder() { Text = HTInputBox.TextValue, Name = HTInputBox.TextValue.Replace(" ", "").Replace(Environment.NewLine, ""), ParentFolder = (treeView1.SelectedNode.Tag != null ? (treeView1.SelectedNode.Tag as Folder) : null), }
                    };
                    treeView1.SelectedNode.Nodes.Add(newFolder);
                    treeView1.ExpandAll();
                }
                else
                {
                    button1_Click(sender, e);
                }
            }

        }

        private readonly string iconStorage = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\IconStorage\\";

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text)) { }
            else
            {
                if (textBox2.Text != "korot://folder")
                {
                    Favorite newFav = new Favorite()
                    {
                        Name = textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, ""),
                        Text = textBox1.Text,
                        Url = textBox2.Text,
                        IconPath = "{ICONSTORAGE}" + textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, "") + ".png",
                    };
                    if (!File.Exists(iconStorage + textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, "") + ".png"))
                    {
                        HTAlt.Tools.WriteFile(iconStorage + textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, "") + ".png", Cefform.Icon.ToBitmap(), ImageFormat.Png);
                    }
                    if (treeView1.SelectedNode.Name == "root" && treeView1.SelectedNode.ToolTipText == "korot://root")
                    {
                        newFav.ParentFolder = null;
                        Cefform.Settings.Favorites.Favorites.Add(newFav);
                    }
                    else
                    {
                        newFav.ParentFolder = (treeView1.SelectedNode.Tag as Folder);
                        (treeView1.SelectedNode.Tag as Folder).Favorites.Add(newFav);
                    }
                }
                else
                {
                    Folder newFav = new Folder()
                    {
                        Name = textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, ""),
                        Text = textBox1.Text,
                        Favorites = new System.Collections.Generic.List<Folder>(),

                    };
                    if (treeView1.SelectedNode.Name == "root" && treeView1.SelectedNode.ToolTipText == "korot://root")
                    {
                        newFav.ParentFolder = null;
                        Cefform.Settings.Favorites.Favorites.Add(newFav);
                    }
                    else
                    {
                        newFav.ParentFolder = (treeView1.SelectedNode.Tag as Folder);
                        (treeView1.SelectedNode.Tag as Folder).Favorites.Add(newFav);
                    }
                }
                Close();
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            treeView1_NodeMouseClick(sender, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = favName;
            textBox2.Text = favUrl;
            //button3.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text))
            {
                button2.Enabled = true;
                button1.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ToolTipText != null)
                {
                    if (treeView1.SelectedNode.ToolTipText.ToString() == "korot://folder" || treeView1.SelectedNode.ToolTipText.ToString() == "korot://root")
                    {
                        button2.Enabled = true;
                        button1.Enabled = true;
                    }
                }
                else
                {
                    button2.Enabled = false;
                    button1.Enabled = false;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView1_NodeMouseClick(sender, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = Cefform.newFavorite;
            button2.Text = Cefform.add;
            button1.Text = Cefform.newFolder;
            label1.Text = Cefform.nametd;
            label2.Text = Cefform.urltd;
            textBox1.Text = favName;
            textBox2.Text = favUrl;
            BackColor = Cefform.Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            button1.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            button1.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            button2.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            button2.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            //button3.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            //button3.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            textBox1.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            textBox1.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            textBox2.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            textBox2.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            treeView1.BackColor = HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 29, false);
            treeView1.ForeColor = HTAlt.Tools.IsBright(Cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            textBox1.Location = new Point(label1.Location.X + label1.Width, label1.Location.Y);
            textBox1.Width = Width - (label1.Width + 50);
            textBox2.Location = new Point(label2.Location.X + label2.Width, label2.Location.Y);
            textBox2.Width = Width - (label2.Width + 50);
        }
    }
}
