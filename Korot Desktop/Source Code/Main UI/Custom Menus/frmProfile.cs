/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmProfile : Form
    {
        private readonly frmCEF cefform;

        public frmProfile(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
            InitializeComponent();
            timer1_Tick(this, new EventArgs());
        }

        private void frmProfile_Leave(object sender, EventArgs e)
        {
            Hide();
        }

        private void RefreshProfiles()
        {
            contextMenuStrip1.Items.Clear();
            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\"))
            {
                if (x != cefform.profilePath)
                {
                    DirectoryInfo info = new DirectoryInfo(x);
                    if (info.Name != cefform.userName)
                    {
                        ToolStripMenuItem profileItem = new ToolStripMenuItem
                        {
                            Text = info.Name
                        };
                        profileItem.Click += Profile_Click;
                        contextMenuStrip1.Items.Add(profileItem);
                    }
                }
            }
            if (contextMenuStrip1.Items.Count == 0)
            {
                contextMenuStrip1.Items.Add(tsEmpty);
            }
            contextMenuStrip1.Items.Add(toolStripSeparator1);
            contextMenuStrip1.Items.Add(tsImport);
            contextMenuStrip1.Items.Add(tsNew);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(BackColor);
            contextMenuStrip1.BackColor = BackColor;
            contextMenuStrip1.ForeColor = ForeColor;
            lbChangePic.ActiveLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbChangePic.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbChangePic.DisabledLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbChangePic.LinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbChangePic.VisitedLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbChangePic.BackColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.BackColor;
            lbExport.ActiveLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbExport.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbExport.DisabledLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbExport.LinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbExport.VisitedLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbExport.BackColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.BackColor;
            lbSwitch.ActiveLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbSwitch.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbSwitch.DisabledLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbSwitch.LinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbSwitch.VisitedLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbSwitch.BackColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.BackColor;
            lbDelete.ActiveLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbDelete.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbDelete.DisabledLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbDelete.LinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbDelete.VisitedLinkColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor;
            lbDelete.BackColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.BackColor;
            if (cefform.Settings.NinjaMode) { pictureBox1.Image = null; } else { if (!cefform.noProfilePic) { pictureBox1.Image = cefform.profilePic; } else { pictureBox1.Image = HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.profiles : Properties.Resources.profiles_w; } }
            lbName.Text = cefform.anaform.ProfileNameTemp.Replace("[NAME]", cefform.userName);
            lbChangePic.Text = cefform.anaform.ChangePic;
            lbExport.Text = cefform.anaform.ExportProfile;
            lbSwitch.Text = cefform.anaform.switchTo;
            lbDelete.Text = cefform.anaform.deleteProfile;
            lbChangePic.Enabled = !cefform._Incognito;
            lbExport.Enabled = !cefform._Incognito;
            lbSwitch.Enabled = !cefform._Incognito;
            lbDelete.Enabled = !cefform._Incognito;
            tsNew.Text = cefform.anaform.newprofile;
            tsImport.Text = cefform.anaform.ImportProfile;
            tsEmpty.Text = cefform.anaform.empty;
        }

        private void lbDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProfileManagement.DeleteProfile(cefform.userName, cefform);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ProfileManagement.NewProfile(cefform);
        }

        private void lbExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog() { Title = cefform.anaform.exportProfileInfo, Filter = cefform.anaform.ProfileFileInfo + "|*.kpa", };
            DialogResult dialog = fileDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                ZipFile.CreateFromDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\", fileDialog.FileName, CompressionLevel.Optimal, true, Encoding.Unicode);
            }
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            ProfileManagement.SwitchProfile(((ToolStripMenuItem)sender).Text, cefform);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog() { Title = cefform.anaform.importProfileInfo, Filter = cefform.anaform.ProfileFileInfo + "|*.kpa", };
            DialogResult dialog = fileDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                ZipFile.ExtractToDirectory(fileDialog.FileName, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\", Encoding.Unicode);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            RefreshProfiles();
        }

        private void lbSwitch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            contextMenuStrip1.Show(lbSwitch, 0, 0);
        }

        private void NewProfilePic()
        {
            OpenFileDialog filedialog = new OpenFileDialog
            {
                Title = "Korot",
                Filter = cefform.anaform.imageFiles + "|*.png",
                Multiselect = false
            };
            if (filedialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(filedialog.FileName, cefform.profilePath + "img.png", true);
                cefform.RefreshProfilePic();
            }
        }

        private void lbChangePic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cefform.noProfilePic) { NewProfilePic(); }
            else
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", cefform.anaform.ChangePicInfo, new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.YesNoCancel))
                {
                    BackColor = cefform.Settings.Theme.BackColor,
                    AutoForeColor = false,
                    ForeColor = cefform.Settings.Theme.ForeColor,
                    Yes = cefform.anaform.ResetImage,
                    No = cefform.anaform.SelectNewImage,
                    Cancel = cefform.anaform.Cancel,
                    Icon = cefform.anaform.Icon,
                };
                DialogResult dialogResult = mesaj.ShowDialog();
                if (dialogResult == DialogResult.Yes) //Reset
                {
                    File.Delete(cefform.profilePath + "img.png");
                    cefform.RefreshProfilePic();
                }
                else if (dialogResult == DialogResult.No) //Select new
                {
                    NewProfilePic();
                }
            }
        }
    }
}