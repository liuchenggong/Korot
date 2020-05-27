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
using CefSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    internal class ContextMenuHandler : IContextMenuHandler
    {
        private readonly frmCEF ActiveForm;
        public ContextMenuHandler(frmCEF activeform) { ActiveForm = activeform; }
        #region "CMS Designer"
        private void InitializeCMSComponent()
        {
            cmsCef = new System.Windows.Forms.ContextMenuStrip();
            backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            refreshNoCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            addToCollection = new System.Windows.Forms.ToolStripMenuItem();
            selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newCollection = new System.Windows.Forms.ToolStripMenuItem();
            tsSepCol = new System.Windows.Forms.ToolStripSeparator();
            tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            openLinkInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyLinkAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openImageInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            searchOrOpenSelectedInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            showDevToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewSourceToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tsEmptyCol = new System.Windows.Forms.ToolStripMenuItem();
            copyImageTSMI = new System.Windows.Forms.ToolStripMenuItem();
            openLinkINWTSMI = new System.Windows.Forms.ToolStripMenuItem();
            openLinkINAIWTSMI = new System.Windows.Forms.ToolStripMenuItem();
            copyImageAddressTSMI = new System.Windows.Forms.ToolStripMenuItem();
            saveLinkAsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            cmsCef.SuspendLayout();
            // 
            // cmsCef
            // 
            cmsCef.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            backToolStripMenuItem,
            forwardToolStripMenuItem,
            refreshToolStripMenuItem,
            refreshNoCacheToolStripMenuItem,
            stopToolStripMenuItem,
            selectAllToolStripMenuItem,
            addToCollection,
            tsSep1,
            openLinkInNewTabToolStripMenuItem,
            openLinkINWTSMI,
            openLinkINAIWTSMI,
            saveLinkAsTSMI,
            copyLinkAddressToolStripMenuItem,
            copyImageTSMI,
            copyImageAddressTSMI,
            openImageInNewTabToolStripMenuItem,
            saveImageAsToolStripMenuItem,
            tsSep2,
            copyToolStripMenuItem,
            cutToolStripMenuItem,
            pasteToolStripMenuItem,
            undoToolStripMenuItem,
            redoToolStripMenuItem,
            deleteToolStripMenuItem,
            searchOrOpenSelectedInNewTabToolStripMenuItem,
            tsSep3,
            printToolStripMenuItem,
            showDevToolsToolStripMenuItem,
            viewSourceToolsToolStripMenuItem});
            cmsCef.Name = "cmsCef";
            cmsCef.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            cmsCef.ShowImageMargin = false;
            cmsCef.Size = new System.Drawing.Size(241, 484);
            // 
            // backToolStripMenuItem
            // 
            backToolStripMenuItem.Name = "backToolStripMenuItem";
            backToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            backToolStripMenuItem.Text = ActiveForm.goBack;
            backToolStripMenuItem.Click += new System.EventHandler(backToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            forwardToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            forwardToolStripMenuItem.Text = ActiveForm.goForward;
            forwardToolStripMenuItem.Click += new System.EventHandler(forwardToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            refreshToolStripMenuItem.Text = ActiveForm.refresh;
            refreshToolStripMenuItem.Click += new System.EventHandler(refreshToolStripMenuItem_Click);
            // 
            // refreshNoCacheToolStripMenuItem
            // 
            refreshNoCacheToolStripMenuItem.Name = "refreshNoCacheToolStripMenuItem";
            refreshNoCacheToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            refreshNoCacheToolStripMenuItem.Text = ActiveForm.refreshNoCache;
            refreshNoCacheToolStripMenuItem.Click += new System.EventHandler(refreshNoCacheToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            stopToolStripMenuItem.Text = ActiveForm.stop;
            stopToolStripMenuItem.Click += new System.EventHandler(stopToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            selectAllToolStripMenuItem.Text = ActiveForm.selectAll;
            selectAllToolStripMenuItem.Click += new System.EventHandler(selectAllToolStripMenuItem_Click);
            // 
            // addToCollection
            // 
            addToCollection.Name = "addToCollection";
            addToCollection.DropDown.Items.AddRange(new ToolStripItem[] { tsSepCol, newCollection });
            addToCollection.Size = new System.Drawing.Size(240, 22);
            addToCollection.Text = ActiveForm.addToCollection;
            // 
            // tsEmptyCol
            // 
            tsEmptyCol.Enabled = false;
            tsEmptyCol.Name = "tsEmptyCol";
            tsEmptyCol.Size = new System.Drawing.Size(240, 22);
            tsEmptyCol.Text = ActiveForm.empty;
            // 
            // newCollection
            // 
            newCollection.Name = "newCollection";
            newCollection.Size = new System.Drawing.Size(240, 22);
            newCollection.Text = ActiveForm.newCollection;
            newCollection.Click += new System.EventHandler(newCollection_Click);
            // 
            // tsSep1
            // 
            tsSep1.Name = "tsSep1";
            tsSep1.Size = new System.Drawing.Size(237, 6);
            // 
            // tsSepCol
            // 
            tsSepCol.Name = "tsSepCol";
            tsSepCol.Size = new System.Drawing.Size(237, 6);
            // 
            // openLinkInNewTabToolStripMenuItem
            // 
            openLinkInNewTabToolStripMenuItem.Name = "openLinkInNewTabToolStripMenuItem";
            openLinkInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            openLinkInNewTabToolStripMenuItem.Text = ActiveForm.openLinkInNewTab;
            openLinkInNewTabToolStripMenuItem.Click += new System.EventHandler(openLinkInNewTabToolStripMenuItem_Click);
            // 
            // copyLinkAddressToolStripMenuItem
            // 
            copyLinkAddressToolStripMenuItem.Name = "copyLinkAddressToolStripMenuItem";
            copyLinkAddressToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            copyLinkAddressToolStripMenuItem.Text = ActiveForm.copyLink;
            copyLinkAddressToolStripMenuItem.Click += new System.EventHandler(copyLinkAddressToolStripMenuItem_Click);
            // 
            // openImageInNewTabToolStripMenuItem
            // 
            openImageInNewTabToolStripMenuItem.Name = "openImageInNewTabToolStripMenuItem";
            openImageInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            openImageInNewTabToolStripMenuItem.Text = ActiveForm.openImageInNewTab;
            openImageInNewTabToolStripMenuItem.Click += new System.EventHandler(openImageInNewTabToolStripMenuItem_Click);
            // 
            // saveImageAsToolStripMenuItem
            // 
            saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            saveImageAsToolStripMenuItem.Text = ActiveForm.saveImageAs;
            saveImageAsToolStripMenuItem.Click += new System.EventHandler(saveImageAsToolStripMenuItem_Click);
            // 
            // tsSep2
            // 
            tsSep2.Name = "tsSep2";
            tsSep2.Size = new System.Drawing.Size(237, 6);
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            pasteToolStripMenuItem.Text = ActiveForm.paste;
            pasteToolStripMenuItem.Click += new System.EventHandler(pasteToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            cutToolStripMenuItem.Text = ActiveForm.cut;
            cutToolStripMenuItem.Click += new System.EventHandler(cutToolStripMenuItem_Click);
            // 
            // saveLinkAsTSMI
            // 
            saveLinkAsTSMI.Name = "saveLinkAsTSMI";
            saveLinkAsTSMI.Size = new System.Drawing.Size(240, 22);
            saveLinkAsTSMI.Text = ActiveForm.saveLinkAs;
            saveLinkAsTSMI.Click += new System.EventHandler(saveLinkAs);
            //
            // copyImageTSMI
            // 
            copyImageTSMI.Name = "copyImageTSMI";
            copyImageTSMI.Size = new System.Drawing.Size(240, 22);
            copyImageTSMI.Text = ActiveForm.copyImage;
            copyImageTSMI.Click += new System.EventHandler(copyImage);
            //
            // copyImageAddressTSMI
            //
            copyImageAddressTSMI.Name = "copyImageAddressTSMI";
            copyImageAddressTSMI.Size = new System.Drawing.Size(240, 22);
            copyImageAddressTSMI.Text = ActiveForm.copyImageAddress;
            copyImageAddressTSMI.Click += new System.EventHandler(copyImageAddress);
            //
            // openLinkINWTSMI
            //
            openLinkINWTSMI.Name = "openLinkINWTSMI";
            openLinkINWTSMI.Size = new System.Drawing.Size(240, 22);
            openLinkINWTSMI.Text = ActiveForm.openLinkInNewWindow;
            openLinkINWTSMI.Click += new System.EventHandler(openLinkInANewWindow);
            //
            // openLinkINWTSMI
            //
            openLinkINAIWTSMI.Name = "openLinkINAIWTSMI";
            openLinkINAIWTSMI.Size = new System.Drawing.Size(240, 22);
            openLinkINAIWTSMI.Text = ActiveForm.openLinkInNewIncWindow;
            openLinkINAIWTSMI.Click += new System.EventHandler(openLinkInANewIncognitoWindow);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            copyToolStripMenuItem.Text = ActiveForm.copy;
            copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            undoToolStripMenuItem.Text = ActiveForm.undo;
            undoToolStripMenuItem.Click += new System.EventHandler(undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            redoToolStripMenuItem.Text = ActiveForm.redo;
            redoToolStripMenuItem.Click += new System.EventHandler(redoToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            deleteToolStripMenuItem.Text = ActiveForm.delete;
            deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
            // 
            // searchOrOpenSelectedInNewTabToolStripMenuItem
            // 
            searchOrOpenSelectedInNewTabToolStripMenuItem.Name = "searchOrOpenSelectedInNewTabToolStripMenuItem";
            searchOrOpenSelectedInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            searchOrOpenSelectedInNewTabToolStripMenuItem.Text = ActiveForm.SearchOrOpenSelectedInNewTab;
            searchOrOpenSelectedInNewTabToolStripMenuItem.Click += new System.EventHandler(seacrhOrOpenSelectedInNewTabToolStripMenuItem_Click);
            // 
            // tsSep3
            // 
            tsSep3.Name = "tsSep3";
            tsSep3.Size = new System.Drawing.Size(237, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            printToolStripMenuItem.Text = ActiveForm.print;
            printToolStripMenuItem.Click += new System.EventHandler(printToolStripMenuItem_Click);
            // 
            // showDevToolsToolStripMenuItem
            // 
            showDevToolsToolStripMenuItem.Name = "showDevToolsToolStripMenuItem";
            showDevToolsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            showDevToolsToolStripMenuItem.Text = ActiveForm.developerTools;
            showDevToolsToolStripMenuItem.Click += new System.EventHandler(showDevToolsToolStripMenuItem_Click);
            // 
            // viewSourceToolsToolStripMenuItem
            // 
            viewSourceToolsToolStripMenuItem.Name = "viewSourceToolsToolStripMenuItem";
            viewSourceToolsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            viewSourceToolsToolStripMenuItem.Text = ActiveForm.viewSource;
            viewSourceToolsToolStripMenuItem.Click += new System.EventHandler(viewSourceToolsToolStripMenuItem_Click);
            //
            cmsCef.ResumeLayout(false);
            foreach (ToolStripItem x in cmsCef.Items)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }
        public System.Windows.Forms.ContextMenuStrip cmsCef;
        public System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem refreshNoCacheToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem addToCollection;
        public System.Windows.Forms.ToolStripMenuItem newCollection;
        public System.Windows.Forms.ToolStripSeparator tsSepCol;
        public System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openLinkInNewTabToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyLinkAddressToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openImageInNewTabToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem searchOrOpenSelectedInNewTabToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem showDevToolsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem viewSourceToolsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyImageTSMI;
        public System.Windows.Forms.ToolStripMenuItem openLinkINWTSMI;
        public System.Windows.Forms.ToolStripMenuItem openLinkINAIWTSMI;
        public System.Windows.Forms.ToolStripMenuItem copyImageAddressTSMI;
        public System.Windows.Forms.ToolStripMenuItem saveLinkAsTSMI;
        public System.Windows.Forms.ToolStripMenuItem tsEmptyCol;
        public System.Windows.Forms.ToolStripSeparator tsSep1;
        public System.Windows.Forms.ToolStripSeparator tsSep2;
        public System.Windows.Forms.ToolStripSeparator tsSep3;
        #endregion
        #region "CMS"
        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (!string.IsNullOrWhiteSpace(SelectedText))
            {
                string newItem = "[text ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectedText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                return;
            }
            if (!string.IsNullOrWhiteSpace(SourceURL))
            {
                string newItem = "[image ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Source=\"" + SourceURL.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                return;
            }
            if (!string.IsNullOrWhiteSpace(LinkURL))
            {
                if (!string.IsNullOrWhiteSpace(SelectedText))
                {
                    string newItem = "[link ID =\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectedText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + LinkURL.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                    ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
                else
                {
                    string newItem = "[link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + LinkURL.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + LinkURL.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                    ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SelectedText))
                {
                    string newItem = "[link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectedText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + chromiumWebBrowser1.Address.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                    ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
                else
                {
                    string newItem = "[link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + ActiveForm.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + chromiumWebBrowser1.Address.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" /]";
                    ActiveForm.colManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
            }
        }

        private void CollectionList()
        {
            addToCollection.DropDown.Items.Clear();
            foreach (Collection node in ActiveForm.colManager.Collections)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Text = node.Text,
                    Name = node.ID,
                    Tag = node.outXML
                };
                item.Click += item_Click;
                item.BackColor = Properties.Settings.Default.BackColor;
                item.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
                addToCollection.DropDown.Items.Add(item);
            }
            if (addToCollection.DropDown.Items.Count < 1)
            {
                addToCollection.DropDown.Items.Add(tsEmptyCol);
            }
            addToCollection.DropDown.Items.Add(tsSepCol);
            addToCollection.DropDown.Items.Add(newCollection);
        }
        public void showCMS(string link, string source, string selected, bool hasimage, bool editable, IWebBrowser cwb)
        {
            InitializeCMSComponent();
            currentCMS = cmsCef;
            chromiumWebBrowser1 = cwb;
            cmsCef.BackColor = Properties.Settings.Default.BackColor;
            cmsCef.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            addToCollection.DropDown.BackColor = Properties.Settings.Default.BackColor;
            addToCollection.DropDown.ForeColor = HTAlt.Tools.IsBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            LinkURL = link;
            hasImageContents = hasimage;
            SourceURL = source;
            isEditable = editable;
            SelectedText = selected;
            CollectionList();
            // Links
            openLinkInNewTabToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(LinkURL);
            copyLinkAddressToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(LinkURL);
            openLinkINAIWTSMI.Visible = !string.IsNullOrWhiteSpace(LinkURL);
            openLinkINWTSMI.Visible = !string.IsNullOrWhiteSpace(LinkURL);
            saveLinkAsTSMI.Visible = !string.IsNullOrWhiteSpace(LinkURL);
            tsSep2.Visible = (!string.IsNullOrWhiteSpace(LinkURL) || (!string.IsNullOrWhiteSpace(SourceURL) && hasImageContents));
            // Images
            copyImageTSMI.Visible = (!string.IsNullOrWhiteSpace(SourceURL) && hasImageContents);
            copyImageAddressTSMI.Visible = (!string.IsNullOrWhiteSpace(SourceURL) && hasImageContents);
            saveImageAsToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SourceURL) && hasImageContents);
            openImageInNewTabToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SourceURL) && hasImageContents);
            // Text - Selection
            copyToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(SelectedText);
            searchOrOpenSelectedInNewTabToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(SelectedText);
            tsSep3.Visible = (!string.IsNullOrWhiteSpace(SelectedText) || isEditable || (!string.IsNullOrWhiteSpace(SelectedText) && isEditable));
            // Text - Editable
            pasteToolStripMenuItem.Visible = isEditable;
            undoToolStripMenuItem.Visible = isEditable;
            redoToolStripMenuItem.Visible = isEditable;
            // Text - Editable & Selection
            cutToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SelectedText) && isEditable);
            deleteToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SelectedText) && isEditable);
            // Final
            cmsCef.Show(Cursor.Position);
            cmsCef.BringToFront();
            ActiveForm.cmsCEF = cmsCef;
        }
        public IWebBrowser chromiumWebBrowser1;
        public string LinkURL = "";
        public bool hasImageContents = false;
        public string SourceURL = "";
        public bool isEditable = false;
        public string SelectedText = "";
        public ContextMenuStrip currentCMS;
        private void NewTab(string url)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(url)));
        }

        private void newCollection_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                         ActiveForm.newColInfo,
                                                                                         ActiveForm.newColName)
            { Icon = ActiveForm.Icon, OK = ActiveForm.OK, SetToDefault = ActiveForm.SetToDefault, Cancel = ActiveForm.Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
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
                    ActiveForm.colManager.Collections.Add(newCol);
                }
                else { newCollection_Click(sender, e); }
            }
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Cut();
        }
        private void openLinkInANewWindow(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)) { Process.Start(Application.ExecutablePath, LinkURL); }
        }
        private void openLinkInANewIncognitoWindow(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)) { Process.Start(Application.ExecutablePath, "-incognito \"" + LinkURL + "\""); }
        }

        private void copyImage(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceURL)) { Clipboard.SetImage(HTAlt.Tools.GetImageFromUrl(SourceURL)); }
        }
        private void copyImageAddress(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceURL)) { Clipboard.SetText(SourceURL); }
        }
        private void saveLinkAs(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)) { chromiumWebBrowser1.GetBrowserHost().StartDownload(LinkURL); }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Copy();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Redo();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Delete();
        }

        private void seacrhOrOpenSelectedInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SelectedText)) { NewTab(SelectedText); }
        }

        private void openLinkInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)) { NewTab(LinkURL); }
        }

        private void copyLinkAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)) { Clipboard.SetText(LinkURL); }
        }

        private void openImageInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceURL)) { NewTab(SourceURL); }
        }

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceURL)) { chromiumWebBrowser1.GetBrowserHost().StartDownload(SourceURL); }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Print();
        }

        private void showDevToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.showDevTools()));
        }

        private void viewSourceToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.ViewSource();
        }

        private void refreshNoCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload(true);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Reload();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Stop();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.SelectAll();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.button1_Click(sender, e)));
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.button3_Click(sender, e)));
        }
        #endregion
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {

            if (!parameters.IsPepperMenu)
            {
                model.Clear();
                showCMS(parameters.LinkUrl,
                        parameters.SourceUrl,
                        parameters.SelectionText,
                        parameters.HasImageContents,
                        parameters.IsEditable,
                        browserControl);
            }
        }


        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            //currentCMS.Dispose();
        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}