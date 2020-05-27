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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmCollection : Form
    {
        public frmCEF cefform;
        public frmCollection(frmCEF _cefform, int skipTo = -1)
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

        private void RefreshTranslation()
        {
            newCollectionToolStripMenuItem.Text = cefform.newCollection;
            deleteThisCollectionsToolStripMenuItem.Text = cefform.deleteCollection;
            clearToolStripMenuItem.Text = cefform.clearCollection;
            exportToolStripMenuItem.Text = cefform.exportCollection;
            ımportToolStripMenuItem.Text = cefform.importCollection;
            deleteThisİtemToolStripMenuItem.Text = cefform.deleteItem;
            exportThisİtemToolStripMenuItem.Text = cefform.deleteItem;
            editThisİtemToolStripMenuItem.Text = cefform.editItem;
            ımportİtemToolStripMenuItem.Text = cefform.importColItem;
            changeCollectionIDToolStripMenuItem.Text = cefform.changeColID;
            changeCollectionTextToolStripMenuItem.Text = cefform.changeColText;
        }

        private readonly List<Panel> titlePanels;
        private readonly List<PictureBox> backButtons;
        private readonly List<Control> defaultBackColor;
        private readonly List<Control> DefaultforeColor;
        private void back_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpMain;
        }
        private void item_MouseClick(object sender, MouseEventArgs e)
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
            }
            else if (e.Button == MouseButtons.Right)
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
            generateCollectionList(cefform.colManager);
        }

        private void generateCollectionList(CollectionManager collections)
        {
            listView1.Items.Clear();
            tabControl1.TabPages.Clear();
            tabControl1.TabPages.Add(tpMain);
            titlePanels.Clear();
            backButtons.Clear();
            defaultBackColor.Clear();
            DefaultforeColor.Clear();
            foreach (Collection col in collections.Collections)
            {
                ListViewItem lwitem = new ListViewItem
                {
                    Text = col.Text,
                    Name = col.ID,
                    ToolTipText = col.outXML,
                    Tag = tabControl1.TabPages.Count
                };
                listView1.Items.Add(lwitem);
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
                    Text = col.Text,
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
                foreach (CollectionItem item in col.CollectionItems)
                {
                    if (item is TextItem) //Label
                    {
                        Label newItem = new Label()
                        {
                            AutoSize = true,
                            Tag = ((TextItem)item).outXML,
                            Name = item.ID,
                            Text = ((TextItem)item).Text,
                            BackColor = item.BackColor,
                            ForeColor = ((TextItem)item).ForeColor,
                            Font = new Font(((TextItem)item).Font, ((TextItem)item).FontSize),
                            Visible = true,
                            TabIndex = flowPanel.Controls.Count,
                        };
                        if (item.BackColor == Color.Empty || item.BackColor == Color.Transparent)
                        {
                            defaultBackColor.Add(newItem);
                        }
                        if (((TextItem)item).ForeColor == Color.Empty || ((TextItem)item).ForeColor == Color.Transparent)
                        {
                            DefaultforeColor.Add(newItem);
                        }
                        if (((TextItem)item).FontProperties == FontType.Bold)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Bold);
                        }
                        else if (((TextItem)item).FontProperties == FontType.Italic)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Italic);
                        }
                        else if (((TextItem)item).FontProperties == FontType.Regular)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Regular);
                        }
                        else if (((TextItem)item).FontProperties == FontType.Underline)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Underline);
                        }
                        else if (((TextItem)item).FontProperties == FontType.Strikeout)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Strikeout);
                        }
                        newItem.MouseClick += item_MouseClick;
                        flowPanel.Controls.Add(newItem);
                    }
                    else if (item is ImageItem) //Picturebox 
                    {
                        PictureBox newItem = new PictureBox()
                        {
                            Tag = ((ImageItem)item).outXML,
                            Name = item.ID,
                            ImageLocation = ((ImageItem)item).Source,
                            BackColor = item.BackColor,
                            Size = new Size(((ImageItem)item).Width, ((ImageItem)item).Height),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Visible = true,
                            TabIndex = flowPanel.Controls.Count,
                        };
                        if (item.BackColor == Color.Empty || item.BackColor == Color.Transparent)
                        {
                            defaultBackColor.Add(newItem);
                        }
                        newItem.MouseClick += item_MouseClick;
                        flowPanel.Controls.Add(newItem);
                    }
                    else if (item is LinkItem) //Link
                    {
                        CustomLinkLabel newItem = new CustomLinkLabel()
                        {
                            AutoSize = true,
                            Tag = ((LinkItem)item).outXML,
                            Name = item.ID,
                            Url = ((LinkItem)item).Source,
                            Text = ((LinkItem)item).Text,
                            BackColor = item.BackColor,
                            ForeColor = ((LinkItem)item).ForeColor,
                            Font = new Font(((LinkItem)item).Font, ((LinkItem)item).FontSize),
                            Visible = true,
                            TabIndex = flowPanel.Controls.Count,
                        };
                        if (item.BackColor == Color.Empty || item.BackColor == Color.Transparent)
                        {
                            defaultBackColor.Add(newItem);
                        }
                        if (((LinkItem)item).ForeColor == Color.Empty || ((LinkItem)item).ForeColor == Color.Transparent)
                        {
                            DefaultforeColor.Add(newItem);
                        }
                        if (((LinkItem)item).FontProperties == FontType.Bold)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Bold);
                        }
                        else if (((LinkItem)item).FontProperties == FontType.Italic)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Italic);
                        }
                        else if (((LinkItem)item).FontProperties == FontType.Regular)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Regular);
                        }
                        else if (((LinkItem)item).FontProperties == FontType.Underline)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Underline);
                        }
                        else if (((LinkItem)item).FontProperties == FontType.Strikeout)
                        {
                            newItem.Font = new Font(newItem.Font, FontStyle.Strikeout);
                        }
                        newItem.MouseClick += item_MouseClick;
                        flowPanel.Controls.Add(newItem);
                    }
                }
                tabControl1.TabPages.Add(tab);
            }
        }

        private bool allowSwitch = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitch) { allowSwitch = false; } else { e.Cancel = true; }
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
                x.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (Panel x in titlePanels)
            {
                x.BackColor = HTAlt.Tools.ShiftBrightness(Properties.Settings.Default.BackColor, 20, false);
                x.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (Control x in defaultBackColor)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
            }
            foreach (Control x in DefaultforeColor)
            {
                if (x is CustomLinkLabel)
                {
                    ((CustomLinkLabel)x).ActiveLinkColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).DisabledLinkColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).VisitedLinkColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                    ((CustomLinkLabel)x).LinkColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;

                }
                x.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            }
            foreach (PictureBox x in backButtons)
            {
                x.Image = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
            }
            listView1.BackColor = HTAlt.Tools.ShiftBrightness(Properties.Settings.Default.BackColor, 20, false);
            listView1.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            cmsCollection.BackColor = HTAlt.Tools.ShiftBrightness(Properties.Settings.Default.BackColor, 20, false);
            cmsCollection.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            cmsMain.BackColor = HTAlt.Tools.ShiftBrightness(Properties.Settings.Default.BackColor, 20, false);
            cmsMain.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
        }


        private void newCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                          cefform.newColInfo,
                                                                                          cefform.newColName)
            { Icon = cefform.anaform.Icon, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    Collection newCol = new Collection
                    {
                        ID = HTAlt.Tools.GenerateRandomText(12),
                        Text = mesaj.TextValue
                    };
                    cefform.colManager.Collections.Add(newCol);
                    genColList();
                }
                else { newCollectionToolStripMenuItem_Click(sender, e); }
            }
        }

        private void ımportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                          cefform.importColInfo,
                                                                                          "[collection][/collection]")
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    Collection newCol = new Collection(mesaj.TextValue);
                    cefform.colManager.Collections.Add(newCol);
                    genColList();
                }
                else { ımportToolStripMenuItem_Click(sender, e); }
            }
        }

        private void deleteThisCollectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(
                    "Korot",
                    cefform.delColInfo.Replace("$", listView1.SelectedItems[0].Text),
                    new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
                { Yes = cefform.Yes, No = cefform.No, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor, Icon = cefform.anaform.Icon };
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    cefform.colManager.Collections.Remove(cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name));
                    genColList();
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(
                "Korot",
                cefform.clearColInfo,
                new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { Yes = cefform.Yes, No = cefform.No, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor, Icon = cefform.anaform.Icon };
            if (mesaj.ShowDialog() == DialogResult.Yes)
            {
                cefform.colManager.Collections.Clear();
                genColList();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                              cefform.okToClipboard,
                                                                              listView1.SelectedItems[0].ToolTipText)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };;
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                Clipboard.SetText(string.IsNullOrWhiteSpace(mesaj.TextValue) ? listView1.SelectedItems[0].ToolTipText : mesaj.TextValue);
            }
        }

        private void cmsMain_Opening(object sender, CancelEventArgs e)
        {
            exportToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
            deleteThisCollectionsToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
            changeCollectionTextToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
            changeCollectionIDToolStripMenuItem.Enabled = (listView1.SelectedItems.Count > 0);
        }

        private void deleteThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).GetItemFromID(((Control)ıTEMToolStripMenuItem.Tag).Name);
            genColList();
        }
        private void exportThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                      cefform.okToClipboard,
                                                      ((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString())
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };;
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                Clipboard.SetText(string.IsNullOrWhiteSpace(mesaj.TextValue) ? ((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString() : mesaj.TextValue);
            }
        }

        private void editThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control cntrl = (Control)ıTEMToolStripMenuItem.Tag;
            frmEditCollection edit = new frmEditCollection(cefform, this, cntrl);
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

        private void ımportİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                         cefform.importColItemInfo,
                                                                                         "")
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };;
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).NewItemFromCode(mesaj.TextValue);
                    genColList();
                }
                else { ımportİtemToolStripMenuItem_Click(sender, e); }
            }
        }

        private void changeCollectionIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                         cefform.changeColIDInfo,
                                                                                         cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).ID)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).ID = mesaj.TextValue;
                    genColList();
                }
                else { changeCollectionIDToolStripMenuItem_Click(sender, e); }
            }
        }

        private void changeCollectionTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                             cefform.changeColTextInfo,
                                                                             cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).Text)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.SetToDefault, OK = cefform.OK, Cancel = cefform.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.colManager.GetCollectionFromID(listView1.SelectedItems[0].Name).Text = mesaj.TextValue;
                    genColList();
                }
                else { changeCollectionTextToolStripMenuItem_Click(sender, e); }
            }
        }
    }
}
