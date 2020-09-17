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
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmInstallExt : Form
    {
        public Settings Settings;
        private bool allowSwitch = false;
        private string ExtFile;
        private string noPermission = "This extension does not require any permissions but:";
        private string Initializing = "Initializing...";
        private string installed = "Installed.";
        private string installing = "Installing...";
        private string ext = "Extension";
        private string theme = "Theme";
        private readonly bool silentInstall = false;

        public frmInstallExt(Settings settings, string installFrom, bool silent = false)
        {
            Settings = settings;
            ExtFile = installFrom;
            silentInstall = silent;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private static int Brightness(System.Drawing.Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }

        private void frmInstallExt_Load(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage4));
            if (silentInstall) { Hide(); }
            if (ExtFile.ToLower().EndsWith(".kef")) { StartupEXT(); } else if (ExtFile.ToLower().EndsWith(".ktf")) { StartupTF(); }
        }

        private async void StartupEXT()
        {
            await Task.Run(() =>
            {
                if (!silentInstall)
                {
                    lbThemeExtension.Invoke(new Action(() => lbThemeExtension.Text = ext));
                    string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\" + HTAlt.Tools.GenerateRandomText(12) + "\\";
                    if (Directory.Exists(tempFolder))
                    {
                        Directory.Delete(tempFolder, true);
                    }
                    Directory.CreateDirectory(tempFolder);
                    ZipFile.ExtractToDirectory(ExtFile, tempFolder, Encoding.UTF8);
                    ExtFile = tempFolder + "ext.kem";
                    Invoke(new Action(() => ReadKEM(ExtFile)));
                }
                else
                {
                    lbThemeExtension.Invoke(new Action(() => lbThemeExtension.Text = ext));
                    string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\" + HTAlt.Tools.GenerateRandomText(12) + "\\";
                    if (Directory.Exists(tempFolder))
                    {
                        Directory.Delete(tempFolder, true);
                    }
                    Directory.CreateDirectory(tempFolder);
                    ZipFile.ExtractToDirectory(ExtFile, tempFolder, Encoding.UTF8);
                    ExtFile = tempFolder + "ext.kem";
                    if (new FileInfo(ExtFile).Length < 1048576)
                    {
                        Invoke(new Action(() => ReadKEM(ExtFile)));
                    }
                    else
                    {
                        Invoke(new Action(() => Close()));
                    }
                }
            });
        }

        private async void StartupTF()
        {
            await Task.Run(() =>
            {
                lbThemeExtension.Invoke(new Action(() => lbThemeExtension.Text = theme));
                Invoke(new Action(() => ReadKTF(ExtFile)));
            });
        }

        private void ReadKEM(string fileLocation)
        {
            Extension extension;
            try
            {
                extension = new Extension(fileLocation);
                Invoke(new Action(() =>
                {
                    lbName.Text = extension.Name;
                    lbVersion.Text = extension.Version.ToString();
                    lbAuthor.Text = extension.Author;
                    if (File.Exists(extension.Icon.Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\")))
                    {
                        pbLogo.Image = Image.FromFile(extension.Icon.Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\"));
                    }
                    panel7.Visible = extension.Settings.onlineFiles;
                    panel6.Visible = extension.Settings.autoLoad;
                    panel3.Visible = extension.Settings.onlineFiles;
                    panel4.Visible = false;
                    lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                    if (!silentInstall)
                    {
                        allowSwitch = true;
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        i = 5;
                        button1_Click(null, null);
                    }
                }));
            }
            catch (Exception ex)
            {
                if (silentInstall)
                {
                    Close();
                    return;
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        allowSwitch = true;
                        tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                        textBox1.Text = ex.Message;
                    }));
                    return;
                }
            }
        }

        private void ReadKTF(string fileLocation)
        {
            Theme extension;
            try
            {
                extension = new Theme(fileLocation);
                Invoke(new Action(() =>
                {
                    lbName.Text = extension.Name;
                    lbVersion.Text = extension.Version.ToString();
                    lbAuthor.Text = extension.Author;
                    panel4.Visible = true;
                    lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                    if (!silentInstall)
                    {
                        allowSwitch = true;
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        i = 5;
                        button1_Click(null, null);
                    }
                }));
            }
            catch (Exception ex)
            {
                if (silentInstall)
                {
                    Close();
                    return;
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        allowSwitch = true;
                        tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                        textBox1.Text = ex.Message;
                    }));
                    return;
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !allowSwitch;
            if (allowSwitch) { allowSwitch = false; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage3));
            timer1.Start();
        }

        private int i = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 1;
            lbStatus.Text = Initializing;
            if (i == 5)
            {
                if (lbThemeExtension.Text == ext)
                {
                    installKEM();
                }
                else if (lbThemeExtension.Text == theme)
                {
                    installKTF();
                }
                timer1.Stop();
            }
        }

        private void button3Mode(bool ev)
        {
            btClose.Visible = ev;
            btClose.Enabled = ev;
        }

        private readonly string korotExtDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\Extensions";
        private readonly string korotThemeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\Themes";
        private string extCodeName;

        private async void installKEM()
        {
            await Task.Run(() =>
            {
                extCodeName = (lbAuthor.Text + "." + lbName.Text).Replace("\\", "").Replace("/", "").Replace(":", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
                Invoke(new Action(() => button3Mode(false)));
                lbStatus.Invoke(new Action(() => lbStatus.Text = installing));
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 10));
                if (Directory.Exists(korotExtDirectory + "\\" + extCodeName))
                {
                    Directory.Delete(korotExtDirectory + "\\" + extCodeName, true);
                }
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 30));
                Directory.CreateDirectory(korotExtDirectory + "\\" + extCodeName);
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 60));
                foreach (string x in Directory.GetFiles(new FileInfo(ExtFile).DirectoryName + " \\"))
                {
                    File.Copy(x, korotExtDirectory + "\\" + extCodeName + "\\" + new FileInfo(x).Name);
                }
                foreach (string x in Directory.GetDirectories(new FileInfo(ExtFile).DirectoryName + " \\"))
                {
                    Directory.Move(x, korotExtDirectory + "\\" + extCodeName + "\\" + new DirectoryInfo(x).Name);
                }
                lbStatus.Invoke(new Action(() => lbStatus.Visible = false));
                lbInstallInfo.Invoke(new Action(() => lbInstallInfo.Text = installed));
                Invoke(new Action(() => button3Mode(true)));
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 100));
                Directory.Delete(new FileInfo(ExtFile).DirectoryName, true);
                Settings.Extensions.ExtensionCodeNames.Add(extCodeName);
                Extension ext = new Extension(korotExtDirectory + "\\" + extCodeName + "\\ext.kem");
                Settings.Extensions.ExtensionList.Add(ext);
                ext.Update();
                if (silentInstall)
                {
                    Invoke(new Action(() => Close()));
                }
            });
        }

        private async void installKTF()
        {
            await Task.Run(() =>
            {
                Invoke(new Action(() => button3Mode(false)));
                lbStatus.Invoke(new Action(() => lbStatus.Text = installing));
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 90));
                string fileName = new FileInfo(ExtFile).Name;
                File.Copy(ExtFile, korotThemeDirectory + fileName);
                lbStatus.Invoke(new Action(() => lbStatus.Visible = false));
                lbInstallInfo.Invoke(new Action(() => lbInstallInfo.Text = installed));
                Invoke(new Action(() => button3Mode(true)));
                htProgressBar1.Invoke(new Action(() => htProgressBar1.Value = 300));
                if (silentInstall)
                {
                    Invoke(new Action(() => Close()));
                }
            });
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            pictureBox1.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
            tabPage1.BackColor = Settings.Theme.BackColor;
            tabPage1.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            tabPage2.BackColor = Settings.Theme.BackColor;
            tabPage2.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            tabPage3.BackColor = Settings.Theme.BackColor;
            tabPage3.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            panel1.BackColor = Settings.Theme.BackColor;
            tabPage4.BackColor = Settings.Theme.BackColor;
            tabPage4.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            panel1.ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            pictureBox7.Image = Settings.NinjaMode ? null : (Brightness(Settings.Theme.BackColor) < 130 ? Properties.Resources._1_w : Properties.Resources._1);
            pictureBox2.Image = Settings.NinjaMode ? null : (Brightness(Settings.Theme.BackColor) < 130 ? Properties.Resources._2_w : Properties.Resources._2);
            pictureBox5.Image = Settings.NinjaMode ? null : (Brightness(Settings.Theme.BackColor) < 130 ? Properties.Resources._3_w : Properties.Resources._3);
            noPermission = Settings.LanguageSystem.GetItemText("ExtensionNoPermission");
            Initializing = Settings.LanguageSystem.GetItemText("Initializing");
            installed = Settings.LanguageSystem.GetItemText("Installed");
            installing = Settings.LanguageSystem.GetItemText("Installing");
            lbExtensionRequires.Text = Settings.LanguageSystem.GetItemText("ExtensionRequiresPermission");
            lbAutoLoad.Text = Settings.LanguageSystem.GetItemText("autoLoad");
            lbAutoLoadInfo.Text = Settings.LanguageSystem.GetItemText("autoLoadInfo");
            lbCanAccess.Text = Settings.LanguageSystem.GetItemText("canAccessWebContent");
            lbCanAccessInfo.Text = Settings.LanguageSystem.GetItemText("canAccessWebContentInfo");
            lbOnlineFiles.Text = Settings.LanguageSystem.GetItemText("onlineFiles");
            lbOnlineFilesInfo.Text = Settings.LanguageSystem.GetItemText("onlineFilesInfo");
            btInstall.Text = Settings.LanguageSystem.GetItemText("Install");
            btClose2.Text = Settings.LanguageSystem.GetItemText("Close");
            btClose.Text = Settings.LanguageSystem.GetItemText("Close");
            btClose1.Text = Settings.LanguageSystem.GetItemText("Close");
            lbInstallInfo.Text = Settings.LanguageSystem.GetItemText("");
            lbCannotInstall.Text = Settings.LanguageSystem.GetItemText("ExtensionInstallError");
            lbPleaseWait.Text = Settings.LanguageSystem.GetItemText("PleaseWait");
            lbInstallIt.Text = Settings.LanguageSystem.GetItemText("Installing");
            ext = Settings.LanguageSystem.GetItemText("Extension");
            theme = Settings.LanguageSystem.GetItemText("Theme");
            lbNRContent.Text = Settings.LanguageSystem.GetItemText("NotRatedContent");
            lbNRContentInfo.Text = Settings.LanguageSystem.GetItemText("NotRatedContentInfo");
        }
    }
}