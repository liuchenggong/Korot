/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    internal class ContextMenuHandler : IContextMenuHandler
    {
        private readonly frmCEF ActiveForm;
        private bool initComplete = false;

        public ContextMenuHandler(frmCEF activeform)
        {
            ActiveForm = activeform;
        }

        #region "CMS Designer"

        private void InitializeCMSComponent()
        {
            if (!initComplete)
            {
                initComplete = true;
                cmsCef = new System.Windows.Forms.ContextMenuStrip();
                backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                extensionsTSMI = new System.Windows.Forms.ToolStripMenuItem();
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
                addToDictTSMI = new System.Windows.Forms.ToolStripMenuItem();
                openLinkInBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                copyLinkAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                openImageInNewTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                saveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                tsSep2 = new System.Windows.Forms.ToolStripSeparator();
                missSpellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                createShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                shortcutDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
                cmsCef.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            backToolStripMenuItem,
            forwardToolStripMenuItem,
            refreshToolStripMenuItem,
            refreshNoCacheToolStripMenuItem,
            stopToolStripMenuItem,
            selectAllToolStripMenuItem,
            addToCollection,
            extensionsTSMI,
            tsSep1,
            openLinkInNewTabToolStripMenuItem,
            openLinkInBackToolStripMenuItem,
            openLinkINWTSMI,
            openLinkINAIWTSMI,
            saveLinkAsTSMI,
            copyLinkAddressToolStripMenuItem,
            copyImageTSMI,
            copyImageAddressTSMI,
            openImageInNewTabToolStripMenuItem,
            saveImageAsToolStripMenuItem,
            tsSep2,
            missSpellToolStripMenuItem,
            readToolStripMenuItem,
            createShortcutToolStripMenuItem,
            shortcutDesktopToolStripMenuItem,
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
                backToolStripMenuItem.Click += new System.EventHandler(backToolStripMenuItem_Click);
                forwardToolStripMenuItem.Click += new System.EventHandler(forwardToolStripMenuItem_Click);
                refreshToolStripMenuItem.Click += new System.EventHandler(refreshToolStripMenuItem_Click);
                refreshNoCacheToolStripMenuItem.Click += new System.EventHandler(refreshNoCacheToolStripMenuItem_Click);
                stopToolStripMenuItem.Click += new System.EventHandler(stopToolStripMenuItem_Click);
                selectAllToolStripMenuItem.Click += new System.EventHandler(selectAllToolStripMenuItem_Click);
                newCollection.Click += new System.EventHandler(newCollection_Click);
                addToDictTSMI.Click += new System.EventHandler(addToDict_Click);
                openLinkInBackToolStripMenuItem.Click += new System.EventHandler(openLinkInBackToolStripMenuItem_Click);
                openLinkInNewTabToolStripMenuItem.Click += new System.EventHandler(openLinkInNewTabToolStripMenuItem_Click);
                copyLinkAddressToolStripMenuItem.Click += new System.EventHandler(copyLinkAddressToolStripMenuItem_Click);
                openImageInNewTabToolStripMenuItem.Click += new System.EventHandler(openImageInNewTabToolStripMenuItem_Click);
                saveImageAsToolStripMenuItem.Click += new System.EventHandler(saveImageAsToolStripMenuItem_Click);
                pasteToolStripMenuItem.Click += new System.EventHandler(pasteToolStripMenuItem_Click);
                cutToolStripMenuItem.Click += new System.EventHandler(cutToolStripMenuItem_Click);
                addToCollection.DropDown.Items.AddRange(new ToolStripItem[] { tsSepCol, newCollection });
                readToolStripMenuItem.Click += new System.EventHandler(read_Click);
                saveLinkAsTSMI.Click += new System.EventHandler(saveLinkAs);
                copyImageTSMI.Click += new System.EventHandler(copyImage);
                copyImageAddressTSMI.Click += new System.EventHandler(copyImageAddress);
                openLinkINWTSMI.Click += new System.EventHandler(openLinkInANewWindow);
                openLinkINAIWTSMI.Click += new System.EventHandler(openLinkInANewIncognitoWindow);
                copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
                undoToolStripMenuItem.Click += new System.EventHandler(undoToolStripMenuItem_Click);
                redoToolStripMenuItem.Click += new System.EventHandler(redoToolStripMenuItem_Click);
                deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
                searchOrOpenSelectedInNewTabToolStripMenuItem.Click += new System.EventHandler(seacrhOrOpenSelectedInNewTabToolStripMenuItem_Click);
                printToolStripMenuItem.Click += new System.EventHandler(printToolStripMenuItem_Click);
                showDevToolsToolStripMenuItem.Click += new System.EventHandler(showDevToolsToolStripMenuItem_Click);
                viewSourceToolsToolStripMenuItem.Click += new System.EventHandler(viewSourceToolsToolStripMenuItem_Click);
                createShortcutToolStripMenuItem.Click += new System.EventHandler(createShortcut_Click);
                shortcutDesktopToolStripMenuItem.Click += new System.EventHandler(quickhortcut_Click);
            }
            cmsCef.SuspendLayout();
            //
            // cmsCef
            //
            cmsCef.AutoSize = true;
            cmsCef.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            cmsCef.ShowImageMargin = false;
            //
            // backToolStripMenuItem
            //
            backToolStripMenuItem.Text = ActiveForm.anaform.goBack;
            //
            // forwardToolStripMenuItem
            //
            forwardToolStripMenuItem.Text = ActiveForm.anaform.goForward;
            //
            // extensionsTSMI
            //
            extensionsTSMI.Text = ActiveForm.anaform.Extensions;
            extensionsTSMI.Image = HTAlt.Tools.IsBright(ActiveForm.anaform.Settings.Theme.BackColor) ? Properties.Resources.ext : Properties.Resources.ext_w;
            //
            // refreshToolStripMenuItem
            //
            refreshToolStripMenuItem.Text = ActiveForm.anaform.refresh;
            //
            // refreshNoCacheToolStripMenuItem
            //
            refreshNoCacheToolStripMenuItem.Text = ActiveForm.anaform.refreshNoCache;
            //
            // stopToolStripMenuItem
            //
            stopToolStripMenuItem.Text = ActiveForm.anaform.stop;
            //
            // selectAllToolStripMenuItem
            //
            selectAllToolStripMenuItem.Text = ActiveForm.anaform.selectAll;
            //
            // addToCollection
            //
            addToCollection.Text = ActiveForm.anaform.addToCollection;
            //
            // tsEmptyCol
            //
            tsEmptyCol.Enabled = false;
            tsEmptyCol.Text = ActiveForm.anaform.empty;
            //
            // newCollection
            //
            newCollection.Text = ActiveForm.anaform.newCollection;
            //
            // addToDictTSMI
            //
            addToDictTSMI.Text = ActiveForm.anaform.addToDict;
            //
            // openLinkInBackToolStripMenuItem
            //
            openLinkInBackToolStripMenuItem.Text = ActiveForm.anaform.openLinkInBack;
            //
            // openLinkInNewTabToolStripMenuItem
            //
            openLinkInNewTabToolStripMenuItem.Text = ActiveForm.anaform.openLinkInNewTab;
            //
            // copyLinkAddressToolStripMenuItem
            //
            copyLinkAddressToolStripMenuItem.Text = ActiveForm.anaform.copyLink;
            //
            // openImageInNewTabToolStripMenuItem
            //
            openImageInNewTabToolStripMenuItem.Text = ActiveForm.anaform.openImageInNewTab;
            //
            // saveImageAsToolStripMenuItem
            //
            saveImageAsToolStripMenuItem.Text = ActiveForm.anaform.saveImageAs;
            //
            // readToolStripMenuItem
            //
            readToolStripMenuItem.Text = ActiveForm.anaform.ReadTTS;
            //
            // createShortcutToolStripMenuItem
            //
            createShortcutToolStripMenuItem.Text = ActiveForm.anaform.CreateShortcut;
            //
            // shortcutDesktopToolStripMenuItem
            //
            shortcutDesktopToolStripMenuItem.Text = ActiveForm.anaform.CreateShortcutToDesktop;
            //
            // pasteToolStripMenuItem
            //
            pasteToolStripMenuItem.Text = ActiveForm.anaform.paste;
            //
            // cutToolStripMenuItem
            //
            cutToolStripMenuItem.Text = ActiveForm.anaform.cut;
            //
            // saveLinkAsTSMI
            //
            saveLinkAsTSMI.Text = ActiveForm.anaform.saveLinkAs;
            //
            // copyImageTSMI
            //
            copyImageTSMI.Text = ActiveForm.anaform.copyImage;
            //
            // copyImageAddressTSMI
            //
            copyImageAddressTSMI.Text = ActiveForm.anaform.copyImageAddress;
            //
            // openLinkINWTSMI
            //
            openLinkINWTSMI.Text = ActiveForm.anaform.openLinkInNewWindow;
            //
            // openLinkINWTSMI
            //
            openLinkINAIWTSMI.Text = ActiveForm.anaform.openLinkInNewIncWindow;
            //
            // copyToolStripMenuItem
            //
            copyToolStripMenuItem.Text = ActiveForm.anaform.copy;
            //
            // undoToolStripMenuItem
            //
            undoToolStripMenuItem.Text = ActiveForm.anaform.undo;
            //
            // redoToolStripMenuItem
            //
            redoToolStripMenuItem.Text = ActiveForm.anaform.redo;
            //
            // deleteToolStripMenuItem
            //
            deleteToolStripMenuItem.Text = ActiveForm.anaform.delete;
            //
            // searchOrOpenSelectedInNewTabToolStripMenuItem
            //
            searchOrOpenSelectedInNewTabToolStripMenuItem.Text = ActiveForm.anaform.SearchOrOpenSelectedInNewTab;
            //
            // printToolStripMenuItem
            //
            printToolStripMenuItem.Text = ActiveForm.anaform.print;
            //
            // showDevToolsToolStripMenuItem
            //
            showDevToolsToolStripMenuItem.Text = ActiveForm.anaform.developerTools;
            //
            // viewSourceToolsToolStripMenuItem
            //
            viewSourceToolsToolStripMenuItem.Text = ActiveForm.anaform.viewSource;
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
        public System.Windows.Forms.ToolStripMenuItem extensionsTSMI;
        public System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem addToDictTSMI;
        public System.Windows.Forms.ToolStripMenuItem openLinkInNewTabToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openLinkInBackToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyLinkAddressToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openImageInNewTabToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveImageAsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem missSpellToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem createShortcutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem shortcutDesktopToolStripMenuItem;
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

        #endregion "CMS Designer"

        #region "CMS"

        private void extitem_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Tag == null || !(item.Tag is RightClickOption)) { return; }
            RightClickOption rco = item.Tag as RightClickOption;
            chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(HTAlt.Tools.ReadFile(rco.Script, Encoding.Unicode), true);
        }

        private void RefreshDict()
        {
            missSpellToolStripMenuItem.DropDown.Items.Clear();
            foreach (string x in DictSuggestions)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Text = "\"" + x + "\"",
                    Name = HTAlt.Tools.GenerateRandomText(12),
                    Tag = x,
                };
                item.Click += dictWord_Click;
                item.BackColor = ActiveForm.Settings.Theme.BackColor;
                item.ForeColor = HTAlt.Tools.IsBright(ActiveForm.Settings.Theme.BackColor) ? Color.Black : Color.White;
                missSpellToolStripMenuItem.DropDown.Items.Add(item);
            }
            missSpellToolStripMenuItem.DropDown.Items.Add(addToDictTSMI);
        }

        private void RefreshRCO()
        {
            extensionsTSMI.DropDownItems.Clear();
            List<RightClickOption> options = new List<RightClickOption>();
            foreach (Extension ext in ActiveForm.Settings.Extensions.ExtensionList)
            {
                foreach (RightClickOption option in ext.RightClickOptions)
                {
                    if (option.Option == RightClickOptionStyle.Always)
                    {
                        options.Add(option);
                    }
                    else if (option.Option == RightClickOptionStyle.Edit)
                    {
                        if (IsEditable) { options.Add(option); }
                    }
                    else if (option.Option == RightClickOptionStyle.Image)
                    {
                        if (HasImageContents && !string.IsNullOrWhiteSpace(SourceUrl)) { options.Add(option); }
                    }
                    else if (option.Option == RightClickOptionStyle.Link)
                    {
                        if (!string.IsNullOrWhiteSpace(LinkUrl)) { options.Add(option); }
                    }
                    else if (option.Option == RightClickOptionStyle.None)
                    {
                        if (!IsEditable && !HasImageContents && string.IsNullOrWhiteSpace(SourceUrl) && string.IsNullOrWhiteSpace(LinkUrl) && string.IsNullOrWhiteSpace(SelectionText)) { options.Add(option); }
                    }
                    else if (option.Option == RightClickOptionStyle.Text)
                    {
                        if (!string.IsNullOrWhiteSpace(SelectionText)) { options.Add(option); }
                    }
                }
            }
            int i = 0;
            foreach (RightClickOption option in options)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Image = HTAlt.Tools.ReadFile(option.Icon, "ignored"),
                    BackColor = ActiveForm.Settings.Theme.BackColor
                };
                item.ForeColor = HTAlt.Tools.AutoWhiteBlack(item.BackColor);
                item.Tag = option;
                item.Text = option.Text;
                item.Click += extitem_Click;
                i++;
                extensionsTSMI.DropDown.Items.Add(item);
            }
            if (i == 0)
            {
                ToolStripMenuItem empty = new ToolStripMenuItem
                {
                    Text = ActiveForm.anaform.empty,
                    BackColor = ActiveForm.Settings.Theme.BackColor
                };
                empty.ForeColor = HTAlt.Tools.AutoWhiteBlack(empty.BackColor);
                empty.Enabled = false;
                extensionsTSMI.DropDown.Items.Add(empty);
            }
        }

        private void createShortcut_Click(object sender, EventArgs e)
        {
            string url = "";
            if (!string.IsNullOrWhiteSpace(SelectionText) && HTAlt.Tools.ValidUrl(SelectionText))
            {
                url = SelectionText;
            }
            if (!string.IsNullOrWhiteSpace(SourceUrl))
            {
                url = SourceUrl;
            }
            if (!string.IsNullOrWhiteSpace(LinkUrl))
            {
                url = LinkUrl;
            }
            if (!string.IsNullOrWhiteSpace(UnfilteredLinkUrl))
            {
                url = UnfilteredLinkUrl;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = ActiveForm.anaform.CreateShortcut;
            dialog.Filter = ActiveForm.anaform.ShortcutKorot + "|*.korot|" +ActiveForm.anaform.ShortcutNormal + "|*.url";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                bool isKorotShortcut = dialog.FileName.ToLowerInvariant().EndsWith("korot");
                if (isKorotShortcut)
                {
                    WshShell shell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(dialog.FileName.Substring(0,dialog.FileName.Length - 5) + "lnk");
                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.Arguments = url;
                    shortcut.Save();
                }
                else
                {
                    HTAlt.Tools.WriteFile(dialog.FileName, "[InternetShortcut]" + Environment.NewLine + "URL=" + url,Encoding.UTF8);
                }
            }
        }

        private void quickhortcut_Click(object sender, EventArgs e)
        {
            string url = "";
            if (!string.IsNullOrWhiteSpace(SelectionText) && HTAlt.Tools.ValidUrl(SelectionText))
            {
                url = SelectionText;
            }
            if (!string.IsNullOrWhiteSpace(SourceUrl))
            {
                url = SourceUrl;
            }
            if (!string.IsNullOrWhiteSpace(LinkUrl))
            {
                url = LinkUrl;
            }
            if (!string.IsNullOrWhiteSpace(UnfilteredLinkUrl))
            {
                url = UnfilteredLinkUrl;
            }
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + ActiveForm.anaform.KorotShortcut + "_" + HTAlt.Tools.GenerateRandomText() + ".lnk";
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.Arguments = url;
            shortcut.Save();
        }

        private void read_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Volume = ActiveForm.Settings.SynthVolume;
            synth.Rate = ActiveForm.Settings.SynthRate;
            synth.SpeakAsync(SelectionText);
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if (!string.IsNullOrWhiteSpace(SelectionText))
            {
                string newItem = "<text ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectionText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                return;
            }
            if (!string.IsNullOrWhiteSpace(SourceUrl))
            {
                string newItem = "<image ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Source=\"" + SourceUrl.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                return;
            }
            if (!string.IsNullOrWhiteSpace(LinkUrl))
            {
                if (!string.IsNullOrWhiteSpace(SelectionText))
                {
                    string newItem = "<link ID =\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectionText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + LinkUrl.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                    ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
                else
                {
                    string newItem = "<link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + LinkUrl.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + LinkUrl.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                    ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(SelectionText))
                {
                    string newItem = "<link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + SelectionText.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + chromiumWebBrowser1.Address.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                    ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
                else
                {
                    string newItem = "<link ID=\"" + HTAlt.Tools.GenerateRandomText(12) + "\" Text=\"" + ActiveForm.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Source=\"" + chromiumWebBrowser1.Address.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                    ActiveForm.Settings.CollectionManager.GetCollectionFromID(item.Name).NewItemFromCode(newItem);
                    return;
                }
            }
        }

        private void CollectionList()
        {
            addToCollection.DropDown.Items.Clear();
            foreach (Collection node in ActiveForm.Settings.CollectionManager.Collections)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Text = node.Text,
                    Name = node.ID,
                    Tag = node.outXML
                };
                item.Click += item_Click;
                item.BackColor = ActiveForm.Settings.Theme.BackColor;
                item.ForeColor = HTAlt.Tools.IsBright(ActiveForm.Settings.Theme.BackColor) ? Color.Black : Color.White;
                addToCollection.DropDown.Items.Add(item);
            }
            if (addToCollection.DropDown.Items.Count < 1)
            {
                addToCollection.DropDown.Items.Add(tsEmptyCol);
            }
            addToCollection.DropDown.Items.Add(tsSepCol);
            addToCollection.DropDown.Items.Add(newCollection);
        }

        private string MisspelledWord;
        private string LinkUrl;
        private string UnfilteredLinkUrl;
        private string SourceUrl;
        private bool HasImageContents;
        private bool IsEditable;
        private bool IsSpellCheckEnabled;
        private string SelectionText;
        private string[] DictSuggestions;

        public void showCMS(IContextMenuParams CMparams, IWebBrowser cwb)
        {
            MisspelledWord = CMparams.MisspelledWord;
            LinkUrl = CMparams.LinkUrl;
            UnfilteredLinkUrl = CMparams.UnfilteredLinkUrl;
            SourceUrl = CMparams.SourceUrl;
            HasImageContents = CMparams.HasImageContents;
            IsEditable = CMparams.IsEditable;
            SelectionText = CMparams.SelectionText;
            IsSpellCheckEnabled = CMparams.IsSpellCheckEnabled;
            DictSuggestions = CMparams.DictionarySuggestions.ToArray();
            InitializeCMSComponent();
            currentCMS = cmsCef;
            chromiumWebBrowser1 = cwb;
            cmsCef.BackColor = ActiveForm.Settings.Theme.BackColor;
            cmsCef.ForeColor = ActiveForm.Settings.Theme.ForeColor;
            addToCollection.DropDown.BackColor = ActiveForm.Settings.Theme.BackColor;
            addToCollection.DropDown.ForeColor = ActiveForm.Settings.Theme.ForeColor;
            extensionsTSMI.DropDown.BackColor = ActiveForm.Settings.Theme.BackColor;
            extensionsTSMI.DropDown.ForeColor = ActiveForm.Settings.Theme.ForeColor;
            missSpellToolStripMenuItem.DropDown.BackColor = ActiveForm.Settings.Theme.BackColor;
            missSpellToolStripMenuItem.DropDown.ForeColor = ActiveForm.Settings.Theme.ForeColor;
            RefreshRCO();
            CollectionList();
            if (!string.IsNullOrEmpty(MisspelledWord))
            {
                missSpellToolStripMenuItem.Text = "\"" + MisspelledWord + "\"";
                missSpellToolStripMenuItem.Enabled = true;
                missSpellToolStripMenuItem.Visible = true;
                RefreshDict();
            }
            else
            {
                missSpellToolStripMenuItem.Enabled = false;
                missSpellToolStripMenuItem.Visible = false;
            }
            createShortcutToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(SelectionText) || !string.IsNullOrWhiteSpace(LinkUrl) || !string.IsNullOrWhiteSpace(UnfilteredLinkUrl) || !string.IsNullOrWhiteSpace(SourceUrl);
            shortcutDesktopToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(SelectionText) || !string.IsNullOrWhiteSpace(LinkUrl) || !string.IsNullOrWhiteSpace(UnfilteredLinkUrl) || !string.IsNullOrWhiteSpace(SourceUrl);
            // Links
            openLinkInBackToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkInNewTabToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            copyLinkAddressToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkINAIWTSMI.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkINWTSMI.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            saveLinkAsTSMI.Visible = !string.IsNullOrWhiteSpace(LinkUrl);
            tsSep2.Visible = (!string.IsNullOrWhiteSpace(LinkUrl) || (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents));
            openLinkInBackToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkInNewTabToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            copyLinkAddressToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkINAIWTSMI.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            openLinkINWTSMI.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            saveLinkAsTSMI.Enabled = !string.IsNullOrWhiteSpace(LinkUrl);
            tsSep2.Enabled = (!string.IsNullOrWhiteSpace(LinkUrl) || (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents));
            // Images
            copyImageTSMI.Visible = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            copyImageAddressTSMI.Visible = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            saveImageAsToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            openImageInNewTabToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            copyImageTSMI.Enabled = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            copyImageAddressTSMI.Enabled = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            saveImageAsToolStripMenuItem.Enabled = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            openImageInNewTabToolStripMenuItem.Enabled = (!string.IsNullOrWhiteSpace(SourceUrl) && HasImageContents);
            // Text - Selection
            readToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(SelectionText);
            copyToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(SelectionText);
            searchOrOpenSelectedInNewTabToolStripMenuItem.Visible = !string.IsNullOrWhiteSpace(SelectionText);
            tsSep3.Visible = (!string.IsNullOrWhiteSpace(SelectionText) || IsEditable || (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable));
            readToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(SelectionText);
            copyToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(SelectionText);
            searchOrOpenSelectedInNewTabToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(SelectionText);
            tsSep3.Enabled = (!string.IsNullOrWhiteSpace(SelectionText) || IsEditable || (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable));
            // Text - Editable
            pasteToolStripMenuItem.Visible = IsEditable;
            undoToolStripMenuItem.Visible = IsEditable;
            redoToolStripMenuItem.Visible = IsEditable;
            pasteToolStripMenuItem.Enabled = IsEditable;
            undoToolStripMenuItem.Enabled = IsEditable;
            redoToolStripMenuItem.Enabled = IsEditable;
            // Text - Editable & Selection
            cutToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable);
            deleteToolStripMenuItem.Visible = (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable);
            cutToolStripMenuItem.Enabled = (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable);
            deleteToolStripMenuItem.Enabled = (!string.IsNullOrWhiteSpace(SelectionText) && IsEditable);
            // Final
            cmsCef.Show(Cursor.Position);
            cmsCef.BringToFront();
            ActiveForm.cmsCEF = cmsCef;
        }

        public IWebBrowser chromiumWebBrowser1;

        public ContextMenuStrip currentCMS;

        private void addToDict_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.AddWordToDictionary(MisspelledWord);
        }

        private void dictWord_Click(object sender, EventArgs e)
        {
            if (sender is null) { return; }
            var cntrl = sender as ToolStripMenuItem;
            chromiumWebBrowser1.ReplaceMisspelling(cntrl.Tag.ToString());
        }

        private void NewTab(string url, bool back = false)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(url, back)));
        }

        private void newCollection_Click(object sender, EventArgs e)
        {
            ActiveForm.Invoke(new Action(() => ActiveForm.CreateNewCollection()));
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
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { Process.Start(Application.ExecutablePath, LinkUrl); }
        }

        private void openLinkInANewIncognitoWindow(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { Process.Start(Application.ExecutablePath, "-incognito \"" + LinkUrl + "\""); }
        }

        private void copyImage(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceUrl)) { Clipboard.SetImage(HTAlt.Tools.GetImageFromUrl(SourceUrl)); }
        }

        private void copyImageAddress(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceUrl)) { Clipboard.SetText(SourceUrl); }
        }

        private void saveLinkAs(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { chromiumWebBrowser1.GetBrowserHost().StartDownload(LinkUrl); }
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
            if (!string.IsNullOrWhiteSpace(SelectionText)) { NewTab(SelectionText); }
        }

        private void openLinkInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { NewTab(LinkUrl); }
        }

        private void openLinkInBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { NewTab(LinkUrl, true); }
        }

        private void copyLinkAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(LinkUrl)) { Clipboard.SetText(LinkUrl); }
        }

        private void openImageInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceUrl)) { NewTab(SourceUrl); }
        }

        private void saveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SourceUrl)) { chromiumWebBrowser1.GetBrowserHost().StartDownload(SourceUrl); }
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

        #endregion "CMS"

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            if (!parameters.IsPepperMenu)
            {
                model.Clear();
                showCMS(parameters, browserControl);
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