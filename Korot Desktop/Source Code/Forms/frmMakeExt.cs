using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmMakeExt : Form
    {
        public frmMakeExt()
        {
            InitializeComponent();
        }
        private void anything_MouseEnter(object sender,EventArgs e)
        {
            var cntrl = sender as Control;
            if (cooldownMode) { return; }
            if (cntrl != null) 
            {
                if (cntrl.Tag != null) 
                {
                    textBox4.Text = cntrl.Tag.ToString();
                    return;
                }else
                {
                    textBox4.Text = textBox4.Tag.ToString();
                    return;
                }
            }
            else
            {
                textBox4.Text = textBox4.Tag.ToString();
                return;
            }
        }
        private void anything_MouseLeave(object sender, EventArgs e)
        {
            if (cooldownMode) { return; }
            textBox4.Text = textBox4.Tag.ToString();
        }
        private bool isEmptyWorkSpace
        {
            get
            {
                return (string.IsNullOrWhiteSpace(tbName.Text) &&
                string.IsNullOrWhiteSpace(tbVersion.Text) &&
                string.IsNullOrWhiteSpace(tbAuthor.Text) &&
                string.IsNullOrWhiteSpace(cbIcon.Text) &&
                cbIcon.Items.Count == 0 &&
                string.IsNullOrWhiteSpace(cbStartup.Text) &&
                cbStartup.Items.Count == 0 &&
                string.IsNullOrWhiteSpace(cbMenu.Text) &&
                cbMenu.Items.Count == 0 &&
                string.IsNullOrWhiteSpace(cbBackground.Text) &&
                cbBackground.Items.Count == 0 &&
                nudW.Value == 0 &&
                nudH.Value == 0 &&
                string.IsNullOrWhiteSpace(cbProxy.Text) &&
                cbProxy.Items.Count == 0 &&
                autoLoad.Checked == false &&
                onlineFiles.Checked == false &&
                showPopupMenu.Checked == false &&
                activateScript.Checked == false &&
                hasProxy.Checked == false &&
                useHaltroyUpdater.Checked == false &&
                lbLocation.Items.Count == 0);
            }
        }
        private string isIncompleteWorkSpace
        {
            get
            {
                string incompleteError = "";

                incompleteError = incompleteError + (string.IsNullOrWhiteSpace(tbName.Text) ? "\"Name\" is empty." + Environment.NewLine : "");

                incompleteError = incompleteError + (string.IsNullOrWhiteSpace(tbVersion.Text) ? "\"Version\" is empty." + Environment.NewLine : "");

                incompleteError = incompleteError + (string.IsNullOrWhiteSpace(tbAuthor.Text) ? "\"Author\" is empty." + Environment.NewLine : "");

                incompleteError = incompleteError + (string.IsNullOrWhiteSpace(cbIcon.Text) ? "\"Icon\" is empty." + Environment.NewLine : "");

                bool autoLoadError = ((autoLoad.Checked ||activateScript.Checked) ? (!string.IsNullOrWhiteSpace(cbStartup.Text) ? (lbSafeName.Items.Contains(cbStartup.Text.Replace("[EXTFOLDER]", "")) ? false : true) : true) : false);
                incompleteError = incompleteError + (autoLoadError ? "\"StartupFile\" is empty." + Environment.NewLine : "");

                bool popupError = (showPopupMenu.Checked ? (!string.IsNullOrWhiteSpace(cbMenu.Text) ? (lbSafeName.Items.Contains(cbMenu.Text.Replace("[EXTFOLDER]", "")) ? false : true) : true) : false);
                incompleteError = incompleteError + (popupError ? "\"PopupFile\" is empty." + Environment.NewLine : "");

                bool backError = (autoLoad.Checked ? (!string.IsNullOrWhiteSpace(cbBackground.Text) ? (lbSafeName.Items.Contains(cbBackground.Text.Replace("[EXTFOLDER]", "")) ? false : true) : true) : false);
                incompleteError = incompleteError + (backError ? "\"BackFile\" is empty." + Environment.NewLine : "");

                bool proxyError = (hasProxy.Checked ? (!string.IsNullOrWhiteSpace(cbProxy.Text) ? (lbSafeName.Items.Contains(cbProxy.Text.Replace("[EXTFOLDER]", "")) ? false : true) : true) : false);
                incompleteError = incompleteError + (proxyError ? "\"ProxyFile\" is empty." + Environment.NewLine : "");

                incompleteError = incompleteError + ((lbLocation.Items.Count > 0 && lbSafeName.Items.Count > 0) ? "" : "\"Files\" is empty." + Environment.NewLine);

                incompleteError = incompleteError + ((!lbSafeName.Items.Contains(cbIcon.Text.Replace("[EXTFOLDER]", ""))) ? "\"Icon\" is not found in \"Files\"." + Environment.NewLine : "");

                return incompleteError;
            }
        }
        private string BuildExtManifest()
        {
            string extManifest = "<KorotExtension>" + Environment.NewLine +
                         "<!-- Auto-generated by Korot Extension Maker. -->" + Environment.NewLine +
                         "<name>" + tbName.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</name>" + Environment.NewLine +
                         "<author>" + tbAuthor.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</author>" + Environment.NewLine +
                         "<version>" + tbVersion.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</version>" + Environment.NewLine +
                         "<icon>" + cbIcon.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</icon>" + Environment.NewLine +
                         "<MenuSize>" + nudW.Value + " ; " + nudH.Value + "</MenuSize>" + Environment.NewLine +
                         "<PopupFile>" + cbMenu.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</PopupFile>" + Environment.NewLine +
                         "<StartupFile>" + cbStartup.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</StartupFile>" + Environment.NewLine +
                         "<BackFile>" + cbBackground.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</BackFile>" + Environment.NewLine +
                         "<ProxyFile>" + cbProxy.Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</ProxyFile>" + Environment.NewLine +
                         "<Settings>" + Environment.NewLine +
                         "<Setting ID=\"autoLoad\">" + (autoLoad.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "<Setting ID=\"onlineFİles\">" + (onlineFiles.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "<Setting ID=\"showPopupMenu\">" + (showPopupMenu.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "<Setting ID=\"activateScript\">" + (activateScript.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "<Setting ID=\"hasProxy\">" + (hasProxy.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "<Setting ID=\"useHaltroyUpdater\">" + (useHaltroyUpdater.Checked ? "true" : "false") + "</Setting>" + Environment.NewLine +
                         "</Settings>" + Environment.NewLine +
                         "<Files>" + Environment.NewLine;
            foreach (var i in lbSafeName.Items) 
            { 
              extManifest += "<File Location=\"" + i.ToString().Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine;
            }
            extManifest += "</Files>" + Environment.NewLine;
            extManifest += "</KorotExtension>" + Environment.NewLine;
            return extManifest;
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEmptyWorkSpace)
            {
                return;
            }
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Do you really want to clear current workspace?", new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true, })
            {
                Icon = Icon,
                BackgroundColor = BackColor,
                Yes = "Yes",
                No = "No",
                Cancel = "cancel",
            };
            DialogResult res = mesaj.ShowDialog();
            if (res == DialogResult.Yes)
            {
                clear();
            }
        }
        private void clear()
        {
            tbName.Text = "";
            tbVersion.Text = "";
            tbAuthor.Text = "";
            cbIcon.Text = "";
            cbIcon.Items.Clear();
            cbStartup.Text = "";
            cbStartup.Items.Clear();
            cbMenu.Text = "";
            cbMenu.Items.Clear();
            cbBackground.Text = "";
            cbBackground.Items.Clear();
            cbProxy.Text = "";
            cbProxy.Items.Clear();
            autoLoad.Checked = false;
            onlineFiles.Checked = false;
            showPopupMenu.Checked = false;
            activateScript.Checked = false;
            hasProxy.Checked = false;
            useHaltroyUpdater.Checked = false;
            lbLocation.Items.Clear();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isEmptyWorkSpace)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "This action clears current workspace. Do you wish to continue?", new HTAlt.WinForms.HTDialogBoxContext() { Yes= true,No = true,Cancel = true, })
                {
                    Icon = Icon,
                    BackgroundColor = BackColor,
                    Yes = "Yes",
                    No = "No",
                    Cancel = "Cancel",
                };
                DialogResult res = mesaj.ShowDialog();
                if (res != DialogResult.Yes)
                {
                    return;
                }
            }
            clear();
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Korot Extension File|*.kef|Korot Extension Manifest|*.kem",
                Title = "Load extension...",
                Multiselect = false,
            };
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) { return; }
            if (dialog.FileName.ToLower().EndsWith(".kef")) //Extension File
            {
                string workspace = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\MakeExt\\load\\" + HTAlt.Tools.GenerateRandomText(12) + "\\";
                TemporaryFiles.Add(workspace);
                if (Directory.Exists(workspace)) { Directory.Delete(workspace, true); }
                Directory.CreateDirectory(workspace);
                ZipFile.ExtractToDirectory(dialog.FileName, workspace,Encoding.UTF8);
                foreach(string x in Directory.GetFiles(workspace))
                {
                    if(x != workspace + "ext.kem") 
                    {
                        lbLocation.Items.Add(x);
                    }
                }
                LoadKEM(workspace + "ext.kem");
            }
            else if (dialog.FileName.ToLower().EndsWith(".kem")) //Extension Manifest
            {
                LoadKEM(dialog.FileName);
                FileInfo info = new FileInfo(dialog.FileName);
                foreach(var item in lbSafeName.Items)
                {
                    lbLocation.Items.Add(info.DirectoryName + "\\" + item.ToString());
                }
            }
        }
        private List<string> TemporaryFiles = new List<string>();
        private void LoadKEM(string kemloc)
        {
            // Read the file
            string ManifestXML = HTAlt.Tools.ReadFile(kemloc, Encoding.UTF8);
            // Write XML to Stream so we don't need to load the same file again.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(ManifestXML); //Writes our XML file
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream); //Loads our XML Stream
            // Make sure that this is an extension manifest.
            if (document.FirstChild.Name.ToLower() != "korotextension") { return; }
            // This is the part where hell starts. Looking at this code for a small amount
            // of time might cause turning skin to red, puking blood and hair loss. 
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower() == "name")
                {
                    if (!string.IsNullOrWhiteSpace(tbName.Text)) 
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }else
                    {
                        tbName.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "version")
                {
                    if (!string.IsNullOrWhiteSpace(tbVersion.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        tbVersion.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "author")
                {
                    if (!string.IsNullOrWhiteSpace(tbAuthor.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() +"\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        tbAuthor.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "icon")
                {
                    if (!string.IsNullOrWhiteSpace(cbIcon.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbIcon.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "icon")
                {
                    if (!string.IsNullOrWhiteSpace(cbIcon.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbIcon.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "startupfile")
                {
                    if (!string.IsNullOrWhiteSpace(cbStartup.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbStartup.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "popupfile")
                {
                    if (!string.IsNullOrWhiteSpace(cbMenu.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbMenu.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "backfile")
                {
                    if (!string.IsNullOrWhiteSpace(cbBackground.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbBackground.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "proxyfile")
                {
                    if (!string.IsNullOrWhiteSpace(cbProxy.Text))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "There are more than 1 \"" + node.Name.ToLower() + "\" nodes.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    else
                    {
                        cbProxy.Text = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    }
                }
                else if (node.Name.ToLower() == "menusize")
                {
                    if (!node.InnerText.Contains(";"))
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Invalid inner text for \"" + node.Name.ToLower() + "\" node.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    string w = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                    string h = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                    try
                    {
                        nudW.Value = Convert.ToInt32(w.Replace(";", "").Trim());
                        nudH.Value = Convert.ToInt32(h.Replace(";", "").Trim());
                    }
                    catch
                    {
                        HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Invalid inner text for \"" + node.Name.ToLower() + "\" node.", new HTDialogBoxContext() { OK = true, })
                        {
                            BackgroundColor = BackColor,
                            OK = "OK",
                            Icon = Icon,
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                }
                else if (node.Name.ToLower() == "files")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "file")
                        {
                            if (subnode.Attributes["Location"] != null)
                            {
                                string loc = subnode.Attributes["Location"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                                lbSafeName.Items.Add(loc);
                            }
                        }
                    }
                }
                else if (node.Name.ToLower() == "settings")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name == "autoLoad")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            autoLoad.Checked = subnode.InnerText == "true";
                        }else if (subnode.Name == "onlineFiles")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            onlineFiles.Checked = subnode.InnerText == "true";
                        }
                        else if (subnode.Name == "showPopupMenu")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            showPopupMenu.Checked = subnode.InnerText == "true";
                        }
                        else if (subnode.Name == "activateScript")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            activateScript.Checked = subnode.InnerText == "true";
                        }
                        else if (subnode.Name == "hasProxy")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            hasProxy.Checked = subnode.InnerText == "true";
                        }
                        else if (subnode.Name == "useHaltroyUpdater")
                        {
                            if (subnode.InnerText == "true" || subnode.InnerText == "false")
                            {
                                HTMsgBox mesaj = new HTMsgBox(Text, "Error while reading manifest file." + Environment.NewLine + "Setting \"" + subnode.Name + "\" can only get value of \"true\" or \"false\".", new HTDialogBoxContext() { OK = true, })
                                {
                                    BackgroundColor = BackColor,
                                    OK = "OK",
                                    Icon = Icon,
                                };
                                mesaj.ShowDialog(); clear();
                                return;
                            }
                            useHaltroyUpdater.Checked = subnode.InnerText == "true";
                        }
                    }
                }
            }
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string incompleteError = isIncompleteWorkSpace;
            if (!string.IsNullOrWhiteSpace(incompleteError))
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Cannot create an extension. Workspace is incomplete. Please fill all informations correctly." + Environment.NewLine + incompleteError, new HTAlt.WinForms.HTDialogBoxContext() { OK = true,})
                {
                    Icon = Icon,
                    BackgroundColor = BackColor,
                    OK = "OK",
                };
                mesaj.ShowDialog();
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog() 
            {
               Filter = "Korot Extension File|*.kef|Korot Extension Manifest|*.kem",
               Title = "Build package to...",
               FileName = tbAuthor.Text + "." + tbName.Text + ".kef",
            };
            DialogResult dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK) 
            {
                try
                {
                    if (dialog.FileName.ToLower().EndsWith(".kef"))
                    {
                        // Create new work folder
                        string workspace = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\MakeExt\\generate\\" + HTAlt.Tools.GenerateRandomText(12) + "\\";
                        if (Directory.Exists(workspace))
                        {
                            Directory.Delete(workspace, true);
                        }
                        Directory.CreateDirectory(workspace);
                        HTAlt.Tools.WriteFile(workspace + "ext.kem", BuildExtManifest(), Encoding.UTF8);
                        foreach (var i in lbSafeName.Items)
                        {
                            int ii = lbSafeName.Items.IndexOf(i);
                            File.Copy(lbLocation.Items[ii].ToString(), workspace + i.ToString());

                        }
                        ZipFile.CreateFromDirectory(workspace, dialog.FileName, CompressionLevel.Optimal,false,Encoding.UTF8);
                        Directory.Delete(workspace, true);
                    }
                    else if (dialog.FileName.ToLower().EndsWith(".kem"))
                    {
                        HTAlt.Tools.WriteFile(dialog.FileName, BuildExtManifest(), Encoding.UTF8);
                    }
                    // Retareded guy making easter eggs dating back to probably 2014.
                    textBox4.Text = ((tbAuthor.Text.ToLower() == "zone" || tbAuthor.Text.ToLower() == "zone-tan" || tbAuthor.Text.ToLower() == "zone-sama") ? "Built package with 1 issue(s): " + Environment.NewLine +  "NOT IN BETA" + Environment.NewLine + "https://haltroy.com/Korot.html" : "Successfully built package.");
                    cooldownMode = true;
                    tmrCooldown.Start();
                }
                catch(Exception ex)
                {
                    textBox4.Text = "Error while building package." + Environment.NewLine + ex.ToString();
                    cooldownMode = true;
                    tmrCooldown.Start();
                }
            }
        }

        private void addFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Add files...",
                Multiselect = true,
            };
            DialogResult res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                foreach (string x in dialog.FileNames)
                {
                    lbLocation.Items.Add(x);
                }
                foreach (string x in dialog.SafeFileNames)
                {
                    lbSafeName.Items.Add(x);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbLocation.SelectedIndex = lbSafeName.SelectedIndex;
            if (lbLocation.SelectedIndex != -1)
            {
                if (lbLocation.SelectedItem != null)
                {
                    lbLocation.Items.Remove(lbLocation.SelectedItem);
                    lbSafeName.Items.Remove(lbSafeName.SelectedItem);
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbLocation.Items.Clear();
            lbSafeName.Items.Clear();
        }
        bool cooldownMode = false;
        int cooldown = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            cooldown += 1;
            if (cooldown == 5)
            {
                cooldownMode = false;
                cooldown = 0;
                tmrCooldown.Stop();
            }
        }

        private void cbIcon_DropDown(object sender, EventArgs e)
        {
            cbIcon.Items.Clear();
            cbMenu.Items.Clear();
            cbBackground.Items.Clear();
            cbStartup.Items.Clear();
            cbProxy.Items.Clear();
            foreach (var item in lbSafeName.Items)
            {
                // javascript
                if (item.ToString().ToLower().EndsWith(".js"))
                {
                    cbBackground.Items.Add("[EXTFOLDER]" + item);
                    cbStartup.Items.Add("[EXTFOLDER]" + item);
                }
                // html
                if (item.ToString().ToLower().EndsWith(".html"))
                {
                    cbMenu.Items.Add("[EXTFOLDER]" + item);
                }
                // icon
                if (item.ToString().ToLower().EndsWith(".png")
                    || item.ToString().ToLower().EndsWith(".bmp")
                    || item.ToString().ToLower().EndsWith(".ico")
                    || item.ToString().ToLower().EndsWith(".gif")
                    || item.ToString().ToLower().EndsWith(".jpeg")
                    || item.ToString().ToLower().EndsWith(".jpg")
                    || item.ToString().ToLower().EndsWith(".exif")
                    || item.ToString().ToLower().EndsWith(".tiff")
                    || item.ToString().ToLower().EndsWith(".tif")
                    || item.ToString().ToLower().EndsWith(".wmf")
                    || item.ToString().ToLower().EndsWith(".emf")
                    || item.ToString().ToLower().EndsWith(".gif")
                    || item.ToString().ToLower().EndsWith(".jfif"))
                {
                    cbIcon.Items.Add("[EXTFOLDER]" + item);
                }
                // proxy
                if (item.ToString().ToLower().EndsWith(".kpf"))
                {
                    cbProxy.Items.Add("[EXTFOLDER]" + item);
                }
            }
        }

        private void frmMakeExt_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (string x in TemporaryFiles)
            {
                Directory.Delete(x, true);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbLocation.SelectedIndex = lbSafeName.SelectedIndex;
            if (lbLocation.SelectedIndex != -1)
            {
                if (lbLocation.SelectedItem != null)
                {
                    System.Diagnostics.Process.Start(lbLocation.SelectedItem.ToString());
                }
            }
        }

        private void editSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbLocation.SelectedIndex = lbSafeName.SelectedIndex;
            if (lbLocation.SelectedIndex != -1)
            {
                if (lbLocation.SelectedItem != null)
                {
                    string defaultValue = lbLocation.SelectedItem.ToString();
                    int index = lbLocation.SelectedIndex;
                    HTInputBox input = new HTInputBox(Text, "Please enter a valid address for source.", new HTDialogBoxContext() { OK = true, Cancel = true, SetToDefault = true,}, defaultValue) 
                    {
                        SetToDefault = "Revert",
                        OK = "OK",
                        Cancel = "Cancel",
                        BackgroundColor = BackColor,
                        Icon = Icon,
                    };
                    DialogResult res = input.ShowDialog();
                    if(res == DialogResult.OK)
                    {
                        lbLocation.Items.RemoveAt(index);
                        lbLocation.Items.Insert(index, input.TextValue);
                    }
                }
            }
        }

        private void editTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbLocation.SelectedIndex = lbSafeName.SelectedIndex;
            if (lbSafeName.SelectedIndex != -1)
            {
                if (lbSafeName.SelectedItem != null)
                {
                    string defaultValue = lbSafeName.SelectedItem.ToString();
                    int index = lbSafeName.SelectedIndex;
                    HTInputBox input = new HTInputBox(Text, "Please enter a valid address for target.", new HTDialogBoxContext() { OK = true, Cancel = true, SetToDefault = true, }, defaultValue)
                    {
                        SetToDefault = "Revert",
                        OK = "OK",
                        Cancel = "Cancel",
                        BackgroundColor = BackColor,
                        Icon = Icon,
                    };
                    DialogResult res = input.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        lbSafeName.Items.RemoveAt(index);
                        lbSafeName.Items.Insert(index, input.TextValue);
                    }
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            lbLocation.SelectedIndex = lbSafeName.SelectedIndex;
            clearToolStripMenuItem.Enabled = (lbSafeName.Items.Count > 0 && lbLocation.Items.Count > 0);
            if (lbSafeName.SelectedIndex != -1)
            {
                if (lbSafeName.SelectedItem != null)
                {
                    openToolStripMenuItem.Enabled = true;
                    removeToolStripMenuItem.Enabled = true;
                    editSourceToolStripMenuItem.Enabled = true;
                    editTargetToolStripMenuItem.Enabled = true;
                    return;
                }
            }
            openToolStripMenuItem.Enabled = false;
            removeToolStripMenuItem.Enabled = false;
            editSourceToolStripMenuItem.Enabled = false;
            editTargetToolStripMenuItem.Enabled = false;
        }

        private void convertFrom06ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isEmptyWorkSpace)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "This action clears current workspace. Do you wish to continue?", new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true, })
                {
                    Icon = Icon,
                    BackgroundColor = BackColor,
                    Yes = "Yes",
                    No = "No",
                    Cancel = "Cancel",
                };
                DialogResult res = mesaj.ShowDialog();
                if (res != DialogResult.Yes)
                {
                    return;
                }
            }
            clear();
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Korot Extension File|*.kef|Korot Extension Manifest|*.kem",
                Title = "Load extension...",
                Multiselect = false,
            };
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK) { return; }
            if (dialog.FileName.ToLower().EndsWith(".kef")) //Extension File
            {
                string workspace = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\MakeExt\\load\\" + HTAlt.Tools.GenerateRandomText(12) + "\\";
                TemporaryFiles.Add(workspace);
                if (Directory.Exists(workspace)) { Directory.Delete(workspace, true); }
                Directory.CreateDirectory(workspace);
                ZipFile.ExtractToDirectory(dialog.FileName, workspace, Encoding.UTF8);
                ReadOldKEM(workspace + "ext.kem");
            }
            else if (dialog.FileName.ToLower().EndsWith(".kem")) //Extension Manifest
            {
                ReadOldKEM(dialog.FileName);
            }
        }
        private void ReadOldKEM(string oldKemLoc)
        {
            string Playlist = HTAlt.Tools.ReadFile(oldKemLoc, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase.Length >= 8)
            {
                //ExtName
                tbName.Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "");
                //ExtVersion
                tbVersion.Text = SplittedFase[1].Substring(1).Replace(Environment.NewLine, "");
                //ExtAuthor
                tbAuthor.Text = SplittedFase[2].Substring(1).Replace(Environment.NewLine, "");
                //ExtIcon
                cbIcon.Text = SplittedFase[3].Substring(1).Replace(Environment.NewLine, "");
                //ExtReq - autoLoad
                if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Length >= 5)
                {
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1")
                    {
                        autoLoad.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "0")
                    {
                        autoLoad.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"autoLoad\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    //ExtReq - onlineFiles
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "1")
                    {
                        onlineFiles.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "0")
                    {
                        onlineFiles.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"onlineFiles\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    //ExtReq - showPopupMenu
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "1")
                    {
                        showPopupMenu.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "0")
                    {
                        showPopupMenu.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"showPopupMenu\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    //ExtReq - activateScript
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "1")
                    {
                        activateScript.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "0")
                    {
                        activateScript.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"activateScript\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    //ExtReq - hasProxy
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "1")
                    {
                        hasProxy.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "0")
                    {
                        hasProxy.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"hasProxy\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                    //ExtReq - useHaltroyUpdater
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(5, 1) == "1")
                    {
                        useHaltroyUpdater.Checked = true;
                    }
                    else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(5, 1) == "0")
                    {
                        useHaltroyUpdater.Checked = false;
                    }
                    else
                    {
                        HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Setting \"useHaltroyUpdater\" can only get \"1\" or \"0\".", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                        {
                            Icon = Icon,
                            BackgroundColor = BackColor,
                            OK = "OK",
                        };
                        mesaj.ShowDialog(); clear();
                        return;
                    }
                }
                else
                {
                    HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Some required lines are empty.", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                    {
                        Icon = Icon,
                        BackgroundColor = BackColor,
                        OK = "OK",
                    };
                    mesaj.ShowDialog(); clear();
                    return;
                }
            }
            else
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(Text, "Error while loading old-style manifest file." + Environment.NewLine + "Some required lines are empty.", new HTAlt.WinForms.HTDialogBoxContext() { OK = true, })
                {
                    Icon = Icon,
                    BackgroundColor = BackColor,
                    OK = "OK",
                };
                mesaj.ShowDialog(); clear();
                return;
            }
        }

        private void proxyFİleCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProxyGen kpfGen = new frmProxyGen();
            DialogResult res = kpfGen.ShowDialog();
            if (res == DialogResult.OK)
            {
                lbLocation.Items.Add(kpfGen.savelocation);
                lbSafeName.Items.Add(kpfGen.safelocation);
            }
        }
    }
}