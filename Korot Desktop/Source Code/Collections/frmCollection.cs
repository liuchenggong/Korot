/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

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
            timer1_Tick(this, new EventArgs());
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
            newCollectionToolStripMenuItem.Text = cefform.anaform.newCollection;
            deleteThisCollectionsToolStripMenuItem.Text = cefform.anaform.deleteCollection;
            clearToolStripMenuItem.Text = cefform.anaform.clearCollection;
            exportToolStripMenuItem.Text = cefform.anaform.exportCollection;
            ımportToolStripMenuItem.Text = cefform.anaform.importCollection;
            deleteThisİtemToolStripMenuItem.Text = cefform.anaform.deleteItem;
            exportThisİtemToolStripMenuItem.Text = cefform.anaform.deleteItem;
            editThisİtemToolStripMenuItem.Text = cefform.anaform.editItem;
            ımportİtemToolStripMenuItem.Text = cefform.anaform.importColItem;
            changeCollectionIDToolStripMenuItem.Text = cefform.anaform.changeColID;
            changeCollectionTextToolStripMenuItem.Text = cefform.anaform.changeColText;
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
                    ıTEMToolStripMenuItem.Text = cefform.anaform.image;
                }
                else if (sender is CustomLinkLabel)
                {
                    ıTEMToolStripMenuItem.Text = cefform.anaform.link;
                }
                else if (sender is Label)
                {
                    ıTEMToolStripMenuItem.Text = cefform.anaform.text;
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
            generateCollectionList(cefform.Settings.CollectionManager);
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
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
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
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            foreach (TabPage x in tabControl1.TabPages)
            {
                x.BackColor = cefform.Settings.Theme.BackColor;
                x.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
            }
            foreach (Panel x in titlePanels)
            {
                x.BackColor = BackColor2;
                x.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
            }
            foreach (Control x in defaultBackColor)
            {
                x.BackColor = cefform.Settings.Theme.BackColor;
            }
            foreach (Control x in DefaultforeColor)
            {
                if (x is CustomLinkLabel)
                {
                    ((CustomLinkLabel)x).ActiveLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
                    ((CustomLinkLabel)x).DisabledLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
                    ((CustomLinkLabel)x).VisitedLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
                    ((CustomLinkLabel)x).LinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
                }
                x.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
            }
            foreach (PictureBox x in backButtons)
            {
                x.Image = cefform.Settings.NinjaMode ? null : (HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
            }
            listView1.BackColor = BackColor2;
            listView1.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
            cmsCollection.BackColor = BackColor2;
            cmsCollection.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
            cmsMain.BackColor = BackColor2;
            cmsMain.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor; ;
        }

        private void newCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                          cefform.anaform.newColInfo,
                                                                                          cefform.anaform.newColName)
            { Icon = cefform.anaform.Icon, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor };
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
                    cefform.Settings.CollectionManager.Collections.Add(newCol);
                    genColList();
                }
                else { newCollectionToolStripMenuItem_Click(sender, e); }
            }
        }

        private void ımportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                          cefform.anaform.importColInfo,
                                                                                          "<collection></collection>")
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    Collection newCol = new Collection(mesaj.TextValue);
                    cefform.Settings.CollectionManager.Collections.Add(newCol);
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
                    cefform.anaform.delColInfo.Replace("$", listView1.SelectedItems[0].Text),
                    new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.YesNoCancel))
                { Yes = cefform.anaform.Yes, No = cefform.anaform.No, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor, Icon = cefform.anaform.Icon };
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    cefform.Settings.CollectionManager.Collections.Remove(cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name));
                    genColList();
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(
                "Korot",
                cefform.anaform.clearColInfo,
                new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.YesNoCancel))
            { Yes = cefform.anaform.Yes, No = cefform.anaform.No, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor, Icon = cefform.anaform.Icon };
            if (mesaj.ShowDialog() == DialogResult.Yes)
            {
                cefform.Settings.CollectionManager.Collections.Clear();
                genColList();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                              cefform.anaform.okToClipboard,
                                                                              listView1.SelectedItems[0].ToolTipText)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor }; ;
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
            cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).GetItemFromID(((Control)ıTEMToolStripMenuItem.Tag).Name);
            genColList();
        }

        private void exportThisİtemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                      cefform.anaform.okToClipboard,
                                                      ((Control)ıTEMToolStripMenuItem.Tag).Tag.ToString())
            { MsgBoxButtons = new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.OKCancel, false, true), Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor }; ;
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
                                                                                         cefform.anaform.importColItemInfo,
                                                                                         "")
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor }; ;
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).NewItemFromCode(mesaj.TextValue);
                    genColList();
                }
                else { ımportİtemToolStripMenuItem_Click(sender, e); }
            }
        }

        private void changeCollectionIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                         cefform.anaform.changeColIDInfo,
                                                                                         cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).ID)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).ID = mesaj.TextValue;
                    genColList();
                }
                else { changeCollectionIDToolStripMenuItem_Click(sender, e); }
            }
        }

        private void changeCollectionTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                             cefform.anaform.changeColTextInfo,
                                                                             cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).Text)
            { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    cefform.Settings.CollectionManager.GetCollectionFromID(listView1.SelectedItems[0].Name).Text = mesaj.TextValue;
                    genColList();
                }
                else { changeCollectionTextToolStripMenuItem_Click(sender, e); }
            }
        }
    }
}