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
using CefSharp.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    internal class ContextMenuHandler : IContextMenuHandler
    {

        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;
        private const int SaveImageAs = 26503;
        private const int SaveLinkAs = 26505;
        private const int CopyLinkAddress = 26506;
        private const int OpenLinkInNewTab = 26507;
        private const int SeacrhOrOpenSelectedInNewTab = 40007;
        private const int RefreshTab = 40008;
        private const int OpenImageInNewTab = 26508;
        frmCEF ActiveForm;
        frmMain anafrm;

        //private string lastSelText = "";

        public ContextMenuHandler(frmCEF activeform, frmMain aNaform) { ActiveForm = activeform; anafrm = aNaform; }
        #region "CMS Designer"
        private void InitializeCMSComponent()
        {
            this.cmsCef = new System.Windows.Forms.ContextMenuStrip();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshNoCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.openLinkInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLinkAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOrOpenSelectedInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDevToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSourceToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImageTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openLinkINWTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openLinkINAIWTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImageAddressTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLinkAsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCef.SuspendLayout();
            // 
            // cmsCef
            // 
            this.cmsCef.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.refreshNoCacheToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.tsSep1,
            this.openLinkInNewTabToolStripMenuItem,
            this.openLinkINWTSMI,
            this.openLinkINAIWTSMI,
            this.saveLinkAsTSMI,
            this.copyLinkAddressToolStripMenuItem,
            this.copyImageTSMI,
            this.copyImageAddressTSMI,
            this.openImageInNewTabToolStripMenuItem,
            this.saveImageAsToolStripMenuItem,
            this.tsSep2,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.searchOrOpenSelectedInNewTabToolStripMenuItem,
            this.tsSep3,
            this.printToolStripMenuItem,
            this.showDevToolsToolStripMenuItem,
            this.viewSourceToolsToolStripMenuItem});
            this.cmsCef.Name = "cmsCef";
            this.cmsCef.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsCef.ShowImageMargin = false;
            this.cmsCef.Size = new System.Drawing.Size(241, 484);
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.backToolStripMenuItem.Text = ActiveForm.goBack;
            this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.forwardToolStripMenuItem.Text = ActiveForm.goForward;
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.forwardToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.refreshToolStripMenuItem.Text = ActiveForm.refresh;
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // refreshNoCacheToolStripMenuItem
            // 
            this.refreshNoCacheToolStripMenuItem.Name = "refreshNoCacheToolStripMenuItem";
            this.refreshNoCacheToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.refreshNoCacheToolStripMenuItem.Text = ActiveForm.refreshNoCache;
            this.refreshNoCacheToolStripMenuItem.Click += new System.EventHandler(this.refreshNoCacheToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.stopToolStripMenuItem.Text = ActiveForm.stop;
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.selectAllToolStripMenuItem.Text = ActiveForm.selectAll;
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(237, 6);
            // 
            // openLinkInNewTabToolStripMenuItem
            // 
            this.openLinkInNewTabToolStripMenuItem.Name = "openLinkInNewTabToolStripMenuItem";
            this.openLinkInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openLinkInNewTabToolStripMenuItem.Text = ActiveForm.openLinkInNewTab;
            this.openLinkInNewTabToolStripMenuItem.Click += new System.EventHandler(this.openLinkInNewTabToolStripMenuItem_Click);
            // 
            // copyLinkAddressToolStripMenuItem
            // 
            this.copyLinkAddressToolStripMenuItem.Name = "copyLinkAddressToolStripMenuItem";
            this.copyLinkAddressToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyLinkAddressToolStripMenuItem.Text = ActiveForm.copyLink;
            this.copyLinkAddressToolStripMenuItem.Click += new System.EventHandler(this.copyLinkAddressToolStripMenuItem_Click);
            // 
            // openImageInNewTabToolStripMenuItem
            // 
            this.openImageInNewTabToolStripMenuItem.Name = "openImageInNewTabToolStripMenuItem";
            this.openImageInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openImageInNewTabToolStripMenuItem.Text = ActiveForm.openImageInNewTab;
            this.openImageInNewTabToolStripMenuItem.Click += new System.EventHandler(this.openImageInNewTabToolStripMenuItem_Click);
            // 
            // saveImageAsToolStripMenuItem
            // 
            this.saveImageAsToolStripMenuItem.Name = "saveImageAsToolStripMenuItem";
            this.saveImageAsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.saveImageAsToolStripMenuItem.Text = ActiveForm.saveImageAs;
            this.saveImageAsToolStripMenuItem.Click += new System.EventHandler(this.saveImageAsToolStripMenuItem_Click);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(237, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.pasteToolStripMenuItem.Text = ActiveForm.paste;
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.cutToolStripMenuItem.Text = ActiveForm.cut;
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // saveLinkAsTSMI
            // 
            this.saveLinkAsTSMI.Name = "saveLinkAsTSMI";
            this.saveLinkAsTSMI.Size = new System.Drawing.Size(240, 22);
            this.saveLinkAsTSMI.Text = ActiveForm.saveLinkAs;
            this.saveLinkAsTSMI.Click += new System.EventHandler(this.saveLinkAs);
            //
            // copyImageTSMI
            // 
            this.copyImageTSMI.Name = "copyImageTSMI";
            this.copyImageTSMI.Size = new System.Drawing.Size(240, 22);
            this.copyImageTSMI.Text = ActiveForm.copyImage;
            this.copyImageTSMI.Click += new System.EventHandler(this.copyImage);
            //
            // copyImageAddressTSMI
            //
            this.copyImageAddressTSMI.Name = "copyImageAddressTSMI";
            this.copyImageAddressTSMI.Size = new System.Drawing.Size(240, 22);
            this.copyImageAddressTSMI.Text = ActiveForm.copyImageAddress;
            this.copyImageAddressTSMI.Click += new System.EventHandler(this.copyImageAddress);
            //
            // openLinkINWTSMI
            //
            this.openLinkINWTSMI.Name = "openLinkINWTSMI";
            this.openLinkINWTSMI.Size = new System.Drawing.Size(240, 22);
            this.openLinkINWTSMI.Text = ActiveForm.openLinkInNewWindow;
            this.openLinkINWTSMI.Click += new System.EventHandler(this.openLinkInANewWindow);
            //
            // openLinkINWTSMI
            //
            this.openLinkINAIWTSMI.Name = "openLinkINAIWTSMI";
            this.openLinkINAIWTSMI.Size = new System.Drawing.Size(240, 22);
            this.openLinkINAIWTSMI.Text = ActiveForm.openLinkInNewIncWindow;
            this.openLinkINAIWTSMI.Click += new System.EventHandler(this.openLinkInANewIncognitoWindow);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyToolStripMenuItem.Text = ActiveForm.copy;
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.undoToolStripMenuItem.Text = ActiveForm.undo;
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.redoToolStripMenuItem.Text = ActiveForm.redo;
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.deleteToolStripMenuItem.Text = ActiveForm.delete;
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // searchOrOpenSelectedInNewTabToolStripMenuItem
            // 
            this.searchOrOpenSelectedInNewTabToolStripMenuItem.Name = "searchOrOpenSelectedInNewTabToolStripMenuItem";
            this.searchOrOpenSelectedInNewTabToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.searchOrOpenSelectedInNewTabToolStripMenuItem.Text = ActiveForm.SearchOrOpenSelectedInNewTab;
            this.searchOrOpenSelectedInNewTabToolStripMenuItem.Click += new System.EventHandler(this.seacrhOrOpenSelectedInNewTabToolStripMenuItem_Click);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(237, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.printToolStripMenuItem.Text = ActiveForm.print;
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // showDevToolsToolStripMenuItem
            // 
            this.showDevToolsToolStripMenuItem.Name = "showDevToolsToolStripMenuItem";
            this.showDevToolsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.showDevToolsToolStripMenuItem.Text = ActiveForm.developerTools;
            this.showDevToolsToolStripMenuItem.Click += new System.EventHandler(this.showDevToolsToolStripMenuItem_Click);
            // 
            // viewSourceToolsToolStripMenuItem
            // 
            this.viewSourceToolsToolStripMenuItem.Name = "viewSourceToolsToolStripMenuItem";
            this.viewSourceToolsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.viewSourceToolsToolStripMenuItem.Text = ActiveForm.viewSource;
            this.viewSourceToolsToolStripMenuItem.Click += new System.EventHandler(this.viewSourceToolsToolStripMenuItem_Click);
            //
            this.cmsCef.ResumeLayout(false);
            }
        public System.Windows.Forms.ContextMenuStrip cmsCef;
        public System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem refreshNoCacheToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
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
        public System.Windows.Forms.ToolStripSeparator tsSep1;
        public System.Windows.Forms.ToolStripSeparator tsSep2;
        public System.Windows.Forms.ToolStripSeparator tsSep3;
        #endregion
        #region "CMS"
        public void showCMS(string link, string source, string selected, bool hasimage, bool editable, IWebBrowser cwb)
        {
            InitializeCMSComponent();
            chromiumWebBrowser1 = cwb;
            cmsCef.BackColor = Properties.Settings.Default.BackColor;
            cmsCef.ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            LinkURL = link;
            hasImageContents = hasimage;
            SourceURL = source;
            isEditable = editable;
            SelectedText = selected;
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
        }
        public IWebBrowser chromiumWebBrowser1;
        public string LinkURL = "";
        public bool hasImageContents = false;
        public string SourceURL = "";
        public bool isEditable = false;
        public string SelectedText = "";
        void NewTab(string url)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(url)));
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

        private void copyImage(object sender,EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceURL)) { Clipboard.SetImage(Tools.getImageFromUrl(SourceURL)); }
        }
        private void copyImageAddress(object sender, EventArgs e)
        {
                if (!string.IsNullOrWhiteSpace(SourceURL)) { Clipboard.SetText(SourceURL); }
        }
        private void saveLinkAs(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkURL)){ chromiumWebBrowser1.GetBrowserHost().StartDownload(LinkURL);}
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
            chromiumWebBrowser1.ShowDevTools();
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
            model.Clear();
            showCMS(parameters.LinkUrl,
                                    parameters.SourceUrl,
                                    parameters.SelectionText,
                                    parameters.HasImageContents,
                                    parameters.IsEditable,
                                    browserControl);
        }
        #region "Backup"
        //            lastSelText = parameters.SelectionText;
        //            model.AddItem(CefMenuCommand.Back, ActiveForm.goBack);
        //            model.AddItem(CefMenuCommand.Forward, ActiveForm.goForward);
        //            model.AddItem((CefMenuCommand)RefreshTab, ActiveForm.refresh);
        //            model.AddItem(CefMenuCommand.ReloadNoCache, ActiveForm.refreshNoCache);
        //            model.AddItem(CefMenuCommand.StopLoad, ActiveForm.stop);
        //            model.AddItem(CefMenuCommand.SelectAll, ActiveForm.selectAll);
        //            model.AddSeparator();
        //            if (parameters.LinkUrl != "")
        //            {
        //                model.AddItem((CefMenuCommand)OpenLinkInNewTab, ActiveForm.openLinkInNewTab);
        //                model.AddItem((CefMenuCommand)CopyLinkAddress, ActiveForm.copyLink);
        //            }
        //            if (parameters.HasImageContents && parameters.SourceUrl.Length > 0)
        //            {
        //                model.AddItem((CefMenuCommand)OpenImageInNewTab, ActiveForm.openImageInNewTab);
        //                model.AddItem((CefMenuCommand)SaveImageAs, ActiveForm.saveImageAs);
        //                model.AddSeparator();
        //            }
        //            if (parameters.IsEditable)
        //            {
        //                model.AddItem(CefMenuCommand.Paste, ActiveForm.paste);
        //            }
        //            if (parameters.SelectionText != "")
        //            {
        //                model.AddItem(CefMenuCommand.Copy, ActiveForm.copy);
        //                if (parameters.IsEditable)
        //                {
        //                    model.AddItem(CefMenuCommand.Cut, ActiveForm.cut); //requires both
        //                    model.AddItem(CefMenuCommand.Undo, ActiveForm.undo);
        //                    model.AddItem(CefMenuCommand.Redo, ActiveForm.redo);
        //                    model.AddItem(CefMenuCommand.Delete, ActiveForm.delete); //requires both
        //                }
        //                model.AddItem((CefMenuCommand)SeacrhOrOpenSelectedInNewTab, ActiveForm.SearchOrOpenSelectedInNewTab);
        //                model.AddSeparator();
        //            }
        //            model.AddItem(CefMenuCommand.Print, ActiveForm.print);
        //            model.AddItem((CefMenuCommand)ShowDevTools, ActiveForm.developerTools);
        //            model.AddItem(CefMenuCommand.ViewSource, ActiveForm.viewSource);
        #endregion

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            return false;
        }
        #region "Backup"
        //int id = (int)commandId;
        //    if (id == ShowDevTools)
        //    {
        //browser.ShowDevTools();
        //}
        //   if (id == CloseDevTools)
        //   {
        //browser.CloseDevTools();
        //}
        //  if (id == SaveImageAs)
        //   {
        // browser.GetHost().StartDownload(parameters.SourceUrl);
        // }
        //  if (id == SaveLinkAs)
        //   {
        // browser.GetHost().StartDownload(parameters.LinkUrl);
        // }
        //if (id == OpenLinkInNewTab)
        // {
        // browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.LinkUrl + "')");
        // }
        // if (id == CopyLinkAddress)
        //   {
        // Clipboard.SetText(parameters.LinkUrl);
        // }
        //    if (id == OpenImageInNewTab)
        //    {
        //browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SourceUrl + "')");
        // }
        //   if (id == SeacrhOrOpenSelectedInNewTab)
        //    {
        //browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SelectionText + "')");
        //}
        #endregion
        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) { }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) 
        {
            return false; 
        }
    }
}