using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Net;

namespace Korot
{
    public partial class frmCollection : Form
    {
        public frmCEF cefform;
        public frmCollection(frmCEF _cefform,int skipTo = -1)
        {
            cefform = _cefform;
            InitializeComponent();
            titlePanels = new List<Panel>();
            backButtons = new List<PictureBox>();
            defaultBackColor = new List<Control>();
            DefaultforeColor = new List<Control>();
            if (skipTo == -1)
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tpMain;
            }
            else
            {
                allowSwitch = true;
                tabControl1.SelectedIndex = skipTo;
            }
        }
        void RefreshTranslation()
        {
            newCollectionToolStripMenuItem.Text = cefform.newCollection;
            deleteThisCollectionsToolStripMenuItem.Text = cefform.deleteCollection;
            clearToolStripMenuItem.Text = cefform.clearCollection;
            exportToolStripMenuItem.Text = cefform.exportCollection;
            ımportToolStripMenuItem.Text = cefform.importCollection;
            deleteThisİtemToolStripMenuItem.Text = cefform.deleteItem;
            exportThisİtemToolStripMenuItem.Text = cefform.deleteItem;
            editThisİtemToolStripMenuItem.Text = cefform.editItem;
        }
        List<Panel> titlePanels;
        List<PictureBox> backButtons;
        List<Control> defaultBackColor;
        List<Control> DefaultforeColor;
        private void back_Click(object sender,EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpMain;
        }
        private void item_MouseClick(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is PictureBox)
                {
                    Clipboard.SetImage(((PictureBox)sender).Image);
                }
                else if (sender is CustomLinkLabel)
                {
                    cefform.Invoke(new Action(() => cefform.NewTab(((CustomLinkLabel)sender).Url)));
                }
                else if (sender is Label)
                {
                    Clipboard.SetText(((Label)sender).Text);
                }
            }else if (e.Button == MouseButtons.Right)
            {
                ıTEMToolStripMenuItem.Tag = sender;
                if (sender is PictureBox)
                {
                    ıTEMToolStripMenuItem.Text = cefform.image;
                }
                else if (sender is CustomLinkLabel)
                {
                    ıTEMToolStripMenuItem.Text = cefform.link;
                }
                else if (sender is Label)
                {
                    ıTEMToolStripMenuItem.Text = cefform.text;
                }
                cmsCollection.Show(MousePosition);
            }
        }
        public void genColList()
        {
            listView1.Items.Clear();
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(tpMain);
            titlePanels.Clear();
            backButtons.Clear();
            defaultBackColor.Clear();
            DefaultforeColor.Clear();
            generateCollectionList(cefform.colManager.Collections);
        }
        void generateCollectionList(string collections)
        {
            listView1.Items.Clear();
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(tpMain);
            titlePanels.Clear();
            backButtons.Clear();
            defaultBackColor.Clear();
            DefaultforeColor.Clear();
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(collections.Replace("[", "<").Replace("]", ">"));
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name == "collection")
                {
                    ListViewItem item = new ListViewItem(node.Attributes["Text"].Value);
                    item.ToolTipText = node.OuterXml.Replace("<", "[").Replace(">", "]");
                    item.Tag = tabControl1.TabPages.Count;
                    listView1.Items.Add(item);
                    TabPage tab = new TabPage();
                    PictureBox pbBack = new PictureBox()
                    {
                        Image = Properties.Resources.leftarrow,
                        Size = new System.Drawing.Size(30, 30),
                        Location = new System.Drawing.Point(9, 7),
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage,
                        Visible = true,
                    };
                    pbBack.Click += back_Click;
                    backButtons.Add(pbBack);
                    Label lbTitle = new Label()
                    {
                        AutoSize = true,
                        Font = new Font("Ubuntu", 15F),
                        Location = new System.Drawing.Point(pbBack.Location.X + pbBack.Width + 1, pbBack.Location.Y),
                        Text = node.Attributes["Text"].Value,
                        Visible = true,
                    };
                    Panel pnlTop = new Panel()
                    {
                        Dock = DockStyle.Top,
                        Height = 50,
                        Visible = true,
                    };
                    pnlTop.Controls.Add(pbBack);
                    pnlTop.Controls.Add(lbTitle);
                    titlePanels.Add(pnlTop);
                    FlowLayoutPanel flowPanel = new FlowLayoutPanel()
                    {

                        Location = new Point(0, pnlTop.Location.Y + pnlTop.Height),
                        Anchor = (((AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right),
                        FlowDirection = FlowDirection.TopDown,
                        Visible = true,
                    };
                    flowPanel.HorizontalScroll.Visible = true;
                    flowPanel.VerticalScroll.Visible = true;
                    tab.Controls.Add(pnlTop);
                    tab.Controls.Add(flowPanel);
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name == "label") //Label
                        {
                            try
                            {
                                Label newItem = new Label()
                                {
                                    AutoSize = true,
                                    Tag = subnode.OuterXml.Replace("<", "[").Replace(">", "]"),
                                    Name = subnode.Attributes["ID"].Value.Trim(),
                                    Text = subnode.Attributes["Text"].Value,
                                    BackColor = (subnode.Attributes["BackColor"] != null ? (subnode.Attributes["BackColor"].Value == "$" ? this.BackColor : Tools.HexToColor(subnode.Attributes["BackColor"].Value)) : this.BackColor),
                                    ForeColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    Font = new Font((subnode.Attributes["Font"] != null ? subnode.Attributes["Font"].Value : "Ubuntu"), (subnode.Attributes["FontSize"] != null ? Convert.ToInt32(subnode.Attributes["FontSize"].Value) : 10F)),
                                    Visible = true,
                                    TabIndex = flowPanel.Controls.Count,
                                };
                                if (subnode.Attributes["BackColor"] != null)
                                {
                                    if (subnode.Attributes["BackColor"].Value == "$")
                                    {
                                        defaultBackColor.Add(newItem);
                                    }
                                }
                                else
                                {
                                    defaultBackColor.Add(newItem);
                                }
                                if (subnode.Attributes["ForeColor"] != null)
                                {
                                    if (subnode.Attributes["ForeColor"].Value == "$")
                                    {
                                        DefaultforeColor.Add(newItem);
                                    }
                                }
                                else
                                {
                                    DefaultforeColor.Add(newItem);
                                }
                                if (subnode.Attributes["FontProperties"] != null)
                                {
                                    if (subnode.Attributes["FontProperties"].Value == "Bold")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Bold);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Italic")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Italic);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Regular")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Regular);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Underline")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Underline);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Strikeout);
                                    }
                                }
                                newItem.MouseClick += item_MouseClick;
                                flowPanel.Controls.Add(newItem);
                            }
                            catch (Exception ex)
                            {
                                Output.WriteLine("[Korot.Collections] Cannot add item" + subnode.OuterXml.Replace("<", "[").Replace(">", "]") + " : " + ex.ToString());
                            }
                        }
                        else if (subnode.Name == "picture") //Picturebox 
                        {
                            try
                            {
                                PictureBox newItem = new PictureBox()
                                {
                                    Tag = subnode.OuterXml.Replace("<", "[").Replace(">", "]"),
                                    Name = subnode.Attributes["ID"].Value.Trim(),
                                    ImageLocation = subnode.Attributes["Source"].Value,
                                    BackColor = (subnode.Attributes["BackColor"] != null ? (subnode.Attributes["BackColor"].Value == "$" ? this.BackColor : Tools.HexToColor(subnode.Attributes["BackColor"].Value)) : this.BackColor),
                                    ForeColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    Size = new Size(Convert.ToInt32(subnode.Attributes["Width"].Value), Convert.ToInt32(subnode.Attributes["Height"].Value)),
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Visible = true,
                                    TabIndex = flowPanel.Controls.Count,
                                };
                                if (subnode.Attributes["BackColor"] != null)
                                {
                                    if (subnode.Attributes["BackColor"].Value == "$")
                                    {
                                        defaultBackColor.Add(newItem);
                                    }
                                }
                                else
                                {
                                    defaultBackColor.Add(newItem);
                                }
                                newItem.MouseClick += item_MouseClick;
                                flowPanel.Controls.Add(newItem);
                            }
                            catch (Exception ex)
                            {
                                Output.WriteLine("[Korot.Collections] Cannot add item" + subnode.OuterXml.Replace("<", "[").Replace(">", "]") + " : " + ex.ToString());
                            }
                        }
                        else if (subnode.Name == "link") //Link
                        {
                            try
                            {
                                CustomLinkLabel newItem = new CustomLinkLabel()
                                {
                                    Tag = subnode.OuterXml.Replace("<", "[").Replace(">", "]"),
                                    Name = subnode.Attributes["ID"].Value.Trim(),
                                    Text = subnode.Attributes["Text"].Value,
                                    Url = subnode.Attributes["Source"].Value,
                                    LinkArea = new LinkArea(0, subnode.Attributes["Text"].Value.Length),
                                    AutoSize = true,
                                    BackColor = (subnode.Attributes["BackColor"] != null ? (subnode.Attributes["BackColor"].Value == "$" ? this.BackColor : Tools.HexToColor(subnode.Attributes["BackColor"].Value)) : this.BackColor),
                                    ActiveLinkColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    DisabledLinkColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    VisitedLinkColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    LinkColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    ForeColor = (subnode.Attributes["ForeColor"] != null ? (subnode.Attributes["ForeColor"].Value == "$" ? this.ForeColor : Tools.HexToColor(subnode.Attributes["ForeColor"].Value)) : this.ForeColor),
                                    Font = new Font((subnode.Attributes["Font"] != null ? subnode.Attributes["Font"].Value : "Ubuntu"), (subnode.Attributes["FontSize"] != null ? Convert.ToInt32(subnode.Attributes["FontSize"].Value) : 10F)),
                                    Visible = true,
                                    TabIndex = flowPanel.Controls.Count,
                                };
                                if (subnode.Attributes["BackColor"] != null)
                                {
                                    if (subnode.Attributes["BackColor"].Value == "$")
                                    {
                                        defaultBackColor.Add(newItem);
                                    }
                                }
                                else
                                {
                                    defaultBackColor.Add(newItem);
                                }
                                if (subnode.Attributes["ForeColor"] != null)
                                {
                                    if (subnode.Attributes["ForeColor"].Value == "$")
                                    {
                                        DefaultforeColor.Add(newItem);
                                    }
                                }
                                else
                                {
                                    DefaultforeColor.Add(newItem);
                                }
                                if (subnode.Attributes["FontProperties"] != null)
                                {
                                    if (subnode.Attributes["FontProperties"].Value == "Bold")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Bold);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Italic")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Italic);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Regular")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Regular);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Underline")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Underline);
                                    }
                                    else if (subnode.Attributes["FontProperties"].Value == "Strikeout")
                                    {
                                        newItem.Font = new Font(newItem.Font, FontStyle.Strikeout);
                                    }
                                }
                                newItem.MouseClick += item_MouseClick;
                                flowPanel.Controls.Add(newItem);
                            }
                            catch (Exception ex)
                            {
                                Output.WriteLine("[Korot.Collections] Cannot add item" + subnode.OuterXml.Replace("<", "[").Replace(">", "]") + " : " + ex.ToString());
                            }
                        }
                    }
                    tabControl1.TabPages.Add(tab);
                }
            }

        }
        bool allowSwitch = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitch) { allowSwitch = false; }else { e.Cancel = true; }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                allowSwitch = true;
                tabControl1.SelectedIndex = Convert.ToInt32(listView1.SelectedItems[0].Tag);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (TabPage x in tabControl1.TabPages)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
                x.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (Panel x in titlePanels)
            {
                x.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor,20,false);
                x.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (Control x in defaultBackColor)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
            }
            foreach (Control x in DefaultforeColor)
            {
                if (x is CustomLinkLabel)
                {
                    ((CustomLinkLabel)x).ActiveLinkColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).DisabledLinkColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).VisitedLinkColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).LinkColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;

                }
                x.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (PictureBox x in backButtons)
            {
                x.Image = Tools.isBright(Properties.Settings.Default.BackColor) ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
            }
            listView1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            listView1.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            cmsCollection.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            cmsCollection.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            cmsMain.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            cmsMain.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
        }


        private void newCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                          cefform.newColInfo,
                                                                                          cefform.Icon,
                                                                                          cefform.newColName,
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          cefform.OK,
                                                                                          cefform.Cancel);
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
               cefform.colManager.Collections = cefform.colManager.Collections.Replace("[/root]", "") + "[collection ID=\"" + Tools.generateRandomText() + "\" Text=\"" + mesaj.TextValue() + "\"]" +
                        "[/collection]" +
                        "[/root]";
                generateCollectionList(cefform.colManager.Collections);
            }
        }

        private void ımportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                          cefform.importColInfo,
                                                                                          cefform.Icon,
                                                                                          "[collection][/collection]",
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          cefform.OK,
                                                                                          cefform.Cancel);
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                cefform.colManager.Collections = cefform.colManager.Collections.Replace("[/root]", "") + 
                    mesaj.TextValue() + "[/root]";
                generateCollectionList(cefform.colManager.Collections);
            }
        }

        private void deleteThisCollectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(
                    "Korot",
                    cefform.delColInfo.Replace("$", listView1.SelectedItems[0].Text),
                    cefform.Icon,
                    MessageBoxButtons.YesNoCancel,
                    Properties.Settings.Default.BackColor,
                    cefform.Yes,
                    cefform.No,
                    cefform.OK,
                    cefform.Cancel);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    cefform.colManager.Collections = cefform.colManager.Collections.Replace(listView1.SelectedItems[0].ToolTipText, "");
                    generateCollectionList(cefform.colManager.Collections);
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(
                    "Korot",
                    cefform.clearColInfo,
                    cefform.Icon,
                    MessageBoxButtons.YesNoCancel,
                    Properties.Settings.Default.BackColor,
                    cefform.Yes,
                    cefform.No,
                    cefform.OK,
                    cefform.Cancel);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    cefform.colManager.Collections = "[root][/root]";
                    generateCollectionList(cefform.colManager.Collections);
                }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                              cefform.okToClipboard,
                                                                              cefform.Icon,
                                                                              listView1.SelectedItems[0].ToolTipText,
                                                                              Properties.Settings.Default.BackColor,
                                                                              cefform.OK,
                                                                              cefform.Cancel);
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                Clipboard.SetText(listView1.SelectedItems[0].ToolTipText);
            }
        }

        private void cmsMain_Opening(object sender, CancelEventArgs e)
        {
                exportToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
                deleteThisCollectionsToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
        }

        private void deleteThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cefform.colManager.Collections = cefform.colManager.Collections.Replace(((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString(),"");
            generateCollectionList(cefform.colManager.Collections);
        }
        private void exportThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox("Korot",
                                                      cefform.okToClipboard,
                                                      cefform.Icon,
                                                      ((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString(),
                                                      Properties.Settings.Default.BackColor,
                                                      cefform.OK,
                                                      cefform.Cancel);
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                Clipboard.SetText(((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString());
            }
        }

        private void copySourceOfThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control pborcll = (Control)ıTEMToolStripMenuItem.Tag;
            switch (pborcll)
            {
                case CustomLinkLabel _:
                    Clipboard.SetText(((CustomLinkLabel)pborcll).Url);
                    break;
                case PictureBox _:
                    Clipboard.SetText(((PictureBox)pborcll).ImageLocation);
                    break;
            }
        }

        private void editThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control cntrl = (Control)ıTEMToolStripMenuItem.Tag;
            frmEditCollection edit = new frmEditCollection(cefform,this, cntrl);
            edit.ShowDialog();
        }

        private void frmCollection_Load(object sender, EventArgs e)
        {
            RefreshTranslation();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            RefreshTranslation();
        }
    }
}
