/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
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
                                                                                                    Cefform.anaform.folderInfo,
                                                                                                    Cefform.anaform.defaultFolderName)
            { Icon = Cefform.Icon, OK = Cefform.anaform.OK, SetToDefault = Cefform.anaform.SetToDefault, Cancel = Cefform.anaform.Cancel, BackColor = Cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = Cefform.Settings.Theme.ForeColor };
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
                Cefform.Settings.UpdateFavList();
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
            Text = Cefform.anaform.newFavorite;
            button2.Text = Cefform.anaform.add;
            button1.Text = Cefform.anaform.newFolder;
            label1.Text = Cefform.anaform.nametd;
            label2.Text = Cefform.anaform.urltd;
            textBox1.Text = favName;
            textBox2.Text = favUrl;
            BackColor = Cefform.Settings.Theme.BackColor;
            Color BackColor2 = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Cefform.Settings.Theme.BackColor, 20, false);
            ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            button1.BackColor = BackColor2;
            button1.ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            button2.BackColor = BackColor2;
            button2.ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            //button3.BackColor = BackColor2;
            //button3.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor :  Settings.Theme.ForeColor;
            textBox1.BackColor = BackColor2;
            textBox1.ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            textBox2.BackColor = BackColor2;
            textBox2.ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            treeView1.BackColor = BackColor2;
            treeView1.ForeColor = Cefform.Settings.NinjaMode ? Cefform.Settings.Theme.BackColor : Cefform.Settings.Theme.ForeColor;
            textBox1.Location = new Point(label1.Location.X + label1.Width, label1.Location.Y);
            textBox1.Width = Width - (label1.Width + 50);
            textBox2.Location = new Point(label2.Location.X + label2.Width, label2.Location.Y);
            textBox2.Width = Width - (label2.Width + 50);
        }
    }
}