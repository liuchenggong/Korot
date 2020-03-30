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
using System.Xml;

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
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Favorites))
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(Properties.Settings.Default.Favorites.Replace("[", "<").Replace("]", ">"));
                writer.Flush();
                stream.Position = 0;
                XmlDocument document = new XmlDocument();
                document.Load(stream);

                XmlElement element = document.DocumentElement;
                TreeNode rootNode = new TreeNode
                {
                    Name = "root",
                    Text = "Root",
                    ToolTipText = "korot://root"
                };
                treeView1.Nodes.Add(rootNode);

                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    if (node.Name == "Folder")
                    {
                        TreeNode menuItem = new TreeNode
                        {
                            Name = node.Attributes["Name"].Value,
                            Text = node.Attributes["Text"].Value,
                            ToolTipText = "korot://folder"
                        };

                        rootNode.Nodes.Add(menuItem);
                        GenerateMenusFromXML(node, menuItem);

                    }
                    else if (node.Name == "Favorite")
                    {
                        TreeNode menuItem = new TreeNode
                        {
                            Name = node.Attributes["Name"].Value,
                            Text = node.Attributes["Text"].Value,
                            Tag = node.Attributes["Icon"] != null ? node.Attributes["Icon"].Value : "",
                            ToolTipText = node.Attributes["Url"] == null ? null : node.Attributes["Url"].Value
                        };

                        rootNode.Nodes.Add(menuItem);
                    }
                }
                treeView1.ExpandAll();
            }
            else
            {
                Properties.Settings.Default.Favorites = "[root][/root]";
                LoadDynamicMenu();
            }
        }
        private void GenerateMenusFromXML(XmlNode rootNode, TreeNode menuItem)
        {
            TreeNode item = null;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name == "Folder")
                {
                    item = new TreeNode
                    {
                        Name = node.Attributes["Name"].Value,
                        Text = node.Attributes["Text"].Value,
                        ToolTipText = "korot://folder"
                    };

                    menuItem.Nodes.Add(item);

                    if (node.Attributes["FormLocation"] != null)
                    {
                        item.ToolTipText = node.Attributes["FormLocation"].Value;
                    }

                    GenerateMenusFromXML(node, item);
                }
                else if (node.Name == "Favorite")
                {
                    item = new TreeNode
                    {
                        Name = node.Attributes["Name"].Value,
                        Text = node.Attributes["Text"].Value,
                        Tag = node.Attributes["Icon"] != null ? node.Attributes["Icon"].Value : "",
                        ToolTipText = node.Attributes["Url"] == null ? null : node.Attributes["Url"].Value
                    };

                    menuItem.Nodes.Add(item);

                    if (node.Attributes["FormLocation"] != null)
                    {
                        item.ToolTipText = node.Attributes["FormLocation"].Value;
                    }
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
            HaltroyFramework.HaltroyInputBox haltroyInputBox = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                                    Cefform.folderInfo,
                                                                                                    Cefform.Icon,
                                                                                                    Cefform.defaultFolderName,
                                                                                                    Properties.Settings.Default.BackColor,
                                                                                                    Cefform.OK,
                                                                                                    Cefform.Cancel,
                                                                                                    400,
                                                                                                    150);
            if (haltroyInputBox.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(haltroyInputBox.TextValue()))
                {
                    TreeNode newFolder = new TreeNode
                    {
                        Text = haltroyInputBox.TextValue(),
                        Name = haltroyInputBox.TextValue().Replace(" ", "").Replace(Environment.NewLine, ""),
                        ToolTipText = "korot://folder"
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
        public void TreeViewToXml(TreeView treeView)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.AppendChild(XmlRecursiveExport(xmlDocument.DocumentElement, treeView.Nodes, xmlDocument));
            Properties.Settings.Default.Favorites = xmlDocument.OuterXml.Replace("<", "[").Replace(">", "]");

        }
        private XmlNode XmlRecursiveExport(XmlNode nodeElement, TreeNodeCollection treeNodeCollection, XmlDocument xmlDocument)
        {
            XmlNode xmlNode = null;
            foreach (TreeNode treeNode in treeNodeCollection)
            {
                if (treeNode.ToolTipText != null)
                {
                    if (treeNode.ToolTipText.ToString() == "korot://root")
                    {
                        xmlNode = xmlDocument.CreateElement("root");
                        XmlRecursiveExport(xmlNode, treeNode.Nodes, xmlDocument);
                    }
                    else
                    {
                        xmlNode = xmlDocument.CreateElement(treeNode.ToolTipText == null ?
                            "" :
                            (treeNode.ToolTipText.ToString() == "korot://folder" ? "Folder" : "Favorite"));
                        xmlNode.Attributes.Append(xmlDocument.CreateAttribute("Name"));
                        xmlNode.Attributes["Name"].Value = treeNode.Name;
                        xmlNode.Attributes.Append(xmlDocument.CreateAttribute("Text"));
                        xmlNode.Attributes["Text"].Value = treeNode.Text;
                        xmlNode.Attributes.Append(xmlDocument.CreateAttribute("Url"));
                        xmlNode.Attributes["Url"].Value = treeNode.ToolTipText;
                        xmlNode.Attributes.Append(xmlDocument.CreateAttribute("Icon"));
                        xmlNode.Attributes["Icon"].Value = treeNode.Tag == null ? "" : treeNode.Tag.ToString();

                        if (nodeElement != null)
                        {
                            nodeElement.AppendChild(xmlNode);
                        }

                        if (treeNode.Nodes.Count > 0)
                        {
                            XmlRecursiveExport(xmlNode, treeNode.Nodes, xmlDocument);
                        }
                    }
                }
            }
            return xmlNode;
        }

        private readonly string iconStorage = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\";

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(textBox2.Text)) { }
            else
            {
                TreeNode newFolder = new TreeNode
                {
                    Name = textBox1.Text.Replace(" ", "").Replace(Environment.NewLine, ""),
                    Text = textBox1.Text,
                    ToolTipText = textBox2.Text
                };
                if (textBox2.Text != "korot://folder")
                {
                    FileSystem2.WriteFile(iconStorage + newFolder.Name + ".png", Cefform.Icon.ToBitmap(), ImageFormat.Png);
                    newFolder.Tag = "{ICONSTORAGE}" + newFolder.Name + ".png";
                }
                treeView1.SelectedNode.Nodes.Add(newFolder);
                TreeViewToXml(treeView1);
                Output.WriteLine(Properties.Settings.Default.Favorites);
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
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            button1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            button1.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            button2.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            button2.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            //button3.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            //button3.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            textBox1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            textBox1.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            textBox2.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            textBox2.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            treeView1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 29, false);
            treeView1.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            textBox1.Location = new Point(label1.Location.X + label1.Width, label1.Location.Y);
            textBox1.Width = Width - (label1.Width + 50);
            textBox2.Location = new Point(label2.Location.X + label2.Width, label2.Location.Y);
            textBox2.Width = Width - (label2.Width + 50);
        }
    }
}
