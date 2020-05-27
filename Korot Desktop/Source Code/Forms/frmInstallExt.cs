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
        private bool requires1;
        private bool requires3;
        private bool allowSwitch = false;
        private string ExtFile;
        private string noPermission = "This extension does not require any permissions but:";
        private string Initializing = "Initializing...";
        private string installed = "Installed.";
        private string dc = "Directory created. Moving files and folders...";
        private string cd = "Creating directory...";
        private string rof2 = "Removed old files.";
        private string rof1 = "Removing old files...";
        private string installing = "Installing...";
        private string reqError = "Requirement ([REQ]) can only get \"1\" or \"0\" values." + Environment.NewLine + " at [FILE] line [LINE]";
        private string reqEmpty = "Some requirements are missing." + Environment.NewLine + " at [FILE] line [LINE]";
        private string fileSizeError = "Some files are above the file size limits. Please go to https://github.com/Haltroy/Korot/issues/27 for more info.";
        private string ext = "Extension";
        private string theme = "Theme";
        private readonly bool silentInstall = false;
        public frmInstallExt(string installFrom, bool silent = false)
        {
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
                    label3.Invoke(new Action(() => label3.Text = ext));
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
                        allowSwitch = true;
                        tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                        textBox1.Invoke(new Action(() => textBox1.Text = fileSizeError));
                    }
                }
                else
                {
                    label3.Invoke(new Action(() => label3.Text = ext));
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
                label3.Invoke(new Action(() => label3.Text = theme));
                Invoke(new Action(() => ReadKTF(ExtFile)));
            });
        }

        private void ReadKEM(string fileLocation)
        {
            if (!silentInstall)
            {
                string Playlist = HTAlt.Tools.ReadFile(fileLocation, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                if (SplittedFase.Length >= 8)
                {
                    //ExtName
                    lbName.Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "");
                    //ExtVersion
                    lbVersion.Text = SplittedFase[1].Substring(1).Replace(Environment.NewLine, "");
                    //ExtAuthor
                    lbAuthor.Text = SplittedFase[2].Substring(1).Replace(Environment.NewLine, "");
                    //ExtIcon
                    if (File.Exists(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\")))
                    {
                        pbLogo.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\"));
                    }
                    else
                    {
                        pbLogo.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.ext_w : Properties.Resources.ext;
                    }
                    //ExtReq - autoLoad
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Length >= 5)
                    {
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1")
                        {
                            requires1 = true;
                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "0")
                        {
                            requires1 = false;
                        }
                        else
                        {
                            allowSwitch = true;
                            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                            textBox1.Text = reqError.Replace("[REQ]", "(autoLoad)").Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                            return;
                        }
                        //ExtReq - onlineFiles
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "1")
                        {
                            requires3 = true;
                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "0")
                        {
                            requires3 = false;
                        }
                        else
                        {
                            allowSwitch = true;
                            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                            textBox1.Text = reqError.Replace("[REQ]", "(onlineFiles)").Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                            return;
                        }
                        //ExtReq - showPopupMenu
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "0")
                        {

                        }
                        else
                        {
                            allowSwitch = true;
                            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                            textBox1.Text = reqError.Replace("[REQ]", "(showPopupMenu)").Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                            return;
                        }
                        //ExtReq - activateScript
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "0")
                        {

                        }
                        else
                        {
                            allowSwitch = true;
                            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                            textBox1.Text = reqError.Replace("[REQ]", "(activateScript)").Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                            return;
                        }
                        //ExtReq - hasProxy
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "0")
                        {

                        }
                        else
                        {
                            allowSwitch = true;
                            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                            textBox1.Text = reqError.Replace("[REQ]", "(hasProxy)").Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                            return;
                        }
                        panel6.Visible = requires1;
                        panel3.Visible = requires3;
                        if (!requires1 && !requires3)
                        {
                            label9.Text = noPermission;
                        }
                        lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                        allowSwitch = true;
                        tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage2));
                    }
                    else
                    {
                        allowSwitch = true;
                        tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                        textBox1.Text = reqEmpty.Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "5");
                        return;
                    }
                }
                else
                {
                    allowSwitch = true;
                    tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                    textBox1.Text = reqEmpty.Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", SplittedFase.Length.ToString());
                    return;
                }
            }
            else
            {
                string Playlist = HTAlt.Tools.ReadFile(fileLocation, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                if (SplittedFase.Length >= 8)
                {
                    //ExtName
                    lbName.Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "");
                    //ExtVersion
                    lbVersion.Text = SplittedFase[1].Substring(1).Replace(Environment.NewLine, "");
                    //ExtAuthor
                    lbAuthor.Text = SplittedFase[2].Substring(1).Replace(Environment.NewLine, "");
                    //ExtIcon
                    if (File.Exists(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\")))
                    {
                        pbLogo.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\"));
                    }
                    else
                    {
                        pbLogo.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.ext_w : Properties.Resources.ext;
                    }
                    //ExtReq - autoLoad
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Length >= 5)
                    {
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1")
                        {
                            requires1 = true;
                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "0")
                        {
                            requires1 = false;
                        }
                        else
                        {
                            Close();
                            return;
                        }
                        //ExtReq - onlineFiles
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "1")
                        {
                            requires3 = true;
                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "0")
                        {
                            requires3 = false;
                        }
                        else
                        {
                            Close();
                            return;
                        }
                        //ExtReq - showPopupMenu
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "0")
                        {

                        }
                        else
                        {
                            Close();
                            return;
                        }
                        //ExtReq - activateScript
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "0")
                        {

                        }
                        else
                        {
                            Close();
                            return;
                        }
                        //ExtReq - hasProxy
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "1")
                        {

                        }
                        else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "0")
                        {

                        }
                        else
                        {
                            Close();
                            return;
                        }
                        panel6.Visible = requires1;
                        panel3.Visible = requires3;
                        if (!requires1 && !requires3)
                        {
                            label9.Text = noPermission;
                        }
                        lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                        i = 5;
                        Invoke(new Action(() => button1_Click(null, null)));
                    }
                    else
                    {
                        Close();
                        return;
                    }
                }
                else
                {
                    Close();
                    return;
                }
            }
        }

        private void ReadKTF(string fileLocation)
        {
            if (!silentInstall)
            {
                string Playlist = HTAlt.Tools.ReadFile(fileLocation, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                //ExtName
                lbName.Text = SplittedFase[8].Substring(0).Replace(Environment.NewLine, "");
                //ExtVersion
                lbVersion.Visible = false;
                //ExtAuthor
                lbAuthor.Text = SplittedFase[9].Substring(1).Replace(Environment.NewLine, "");
                panel6.Visible = false;
                pbLogo.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.theme_w : Properties.Resources.theme;
                panel7.Visible = false;
                panel4.Visible = true;
                label9.Visible = false;
                panel3.Visible = false;
                if (SplittedFase[6].Substring(0).Replace(Environment.NewLine, "").Length > 131072)
                {
                    allowSwitch = true;
                    tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage1));
                    textBox1.Text = fileSizeError.Replace("[NEWLINE]", Environment.NewLine).Replace("[FILE]", fileLocation.Replace(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", "...\\")).Replace("[LINE]", "7");
                    return;
                }
                lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                allowSwitch = true;
                tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tabPage2));
            }
            else
            {
                string Playlist = HTAlt.Tools.ReadFile(fileLocation, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                //ExtName
                lbName.Text = SplittedFase[8].Substring(0).Replace(Environment.NewLine, "");
                //ExtVersion
                lbVersion.Visible = false;
                //ExtAuthor
                lbAuthor.Text = SplittedFase[9].Substring(1).Replace(Environment.NewLine, "");
                panel6.Visible = false;
                pbLogo.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.theme_w : Properties.Resources.theme;
                panel7.Visible = false;
                panel4.Visible = true;
                label9.Visible = false;
                panel3.Visible = false;
                if (SplittedFase[6].Substring(0).Replace(Environment.NewLine, "").Length > 131072)
                {
                    Close();
                    return;
                }
                lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5, lbName.Location.Y);
                i = 5;
                button1_Click(null, null);
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
                if (label3.Text == ext)
                {
                    installKEM();
                }
                else if (label3.Text == theme)
                {
                    installKTF();
                }
                timer1.Stop();
            }
        }

        private void button3Mode(bool ev)
        {
            button3.Visible = ev;
            button3.Enabled = ev;
        }

        private readonly string korotExtDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions";
        private readonly string korotThemeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes";
        private string extCodeName;
        private async void installKEM()
        {
            await Task.Run(() =>
            {
                extCodeName = (lbAuthor.Text + "." + lbName.Text).Replace("\\", "").Replace("/", "").Replace(":", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
                Invoke(new Action(() => button3Mode(false)));
                lbStatus.Invoke(new Action(() => lbStatus.Text = installing));
                pbStatus.Invoke(new Action(() => pbStatus.Width = 45));
                if (Directory.Exists(korotExtDirectory + "\\" + extCodeName))
                {
                    lbStatus.Invoke(new Action(() => lbStatus.Text = rof1));
                    Directory.Delete(korotExtDirectory + "\\" + extCodeName, true);
                    lbStatus.Invoke(new Action(() => lbStatus.Text = rof2));
                }
                pbStatus.Invoke(new Action(() => pbStatus.Width = 90));
                lbStatus.Invoke(new Action(() => lbStatus.Text = cd));
                Directory.CreateDirectory(korotExtDirectory + "\\" + extCodeName);
                pbStatus.Invoke(new Action(() => pbStatus.Width = 180));
                lbStatus.Invoke(new Action(() => lbStatus.Text = dc));
                foreach (string x in Directory.GetFiles(new FileInfo(ExtFile).DirectoryName + " \\"))
                {
                    File.Copy(x, korotExtDirectory + "\\" + extCodeName + "\\" + new FileInfo(x).Name);
                }
                foreach (string x in Directory.GetDirectories(new FileInfo(ExtFile).DirectoryName + " \\"))
                {
                    Directory.Move(x, korotExtDirectory + "\\" + extCodeName + "\\" + new DirectoryInfo(x).Name);
                }
                lbStatus.Invoke(new Action(() => lbStatus.Visible = false));
                label8.Invoke(new Action(() => label8.Text = installed));
                Invoke(new Action(() => button3Mode(true)));
                panel2.Invoke(new Action(() => panel2.Visible = false));
                pbStatus.Invoke(new Action(() => pbStatus.Width = 300));
                Directory.Delete(new FileInfo(ExtFile).DirectoryName, true);
                Properties.Settings.Default.registeredExtensions.Add(extCodeName);
                Properties.Settings.Default.Save();
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
                pbStatus.Invoke(new Action(() => pbStatus.Width = 90));
                string fileName = new FileInfo(ExtFile).Name;
                File.Copy(ExtFile, korotThemeDirectory + fileName);
                lbStatus.Invoke(new Action(() => lbStatus.Visible = false));
                label8.Invoke(new Action(() => label8.Text = installed));
                Invoke(new Action(() => button3Mode(true)));
                panel2.Invoke(new Action(() => panel2.Visible = false));
                pbStatus.Invoke(new Action(() => pbStatus.Width = 300));
                if (silentInstall)
                {
                    Invoke(new Action(() => Close()));
                }
            });
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            pictureBox1.BackColor = Properties.Settings.Default.OverlayColor;
            tabPage1.BackColor = Properties.Settings.Default.BackColor;
            tabPage1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabPage2.BackColor = Properties.Settings.Default.BackColor;
            tabPage2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabPage3.BackColor = Properties.Settings.Default.BackColor;
            tabPage3.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            panel1.BackColor = Properties.Settings.Default.BackColor;
            tabPage4.BackColor = Properties.Settings.Default.BackColor;
            tabPage4.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            panel1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            pictureBox7.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._1_w : Properties.Resources._1;
            pictureBox2.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._2_w : Properties.Resources._2;
            pictureBox5.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._3_w : Properties.Resources._3;
            string Playlist = HTAlt.Tools.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            noPermission = SplittedFase[112].Substring(1).Replace(Environment.NewLine, "");
            Initializing = SplittedFase[122].Substring(1).Replace(Environment.NewLine, "");
            installed = SplittedFase[119].Substring(1).Replace(Environment.NewLine, "");
            dc = SplittedFase[118].Substring(1).Replace(Environment.NewLine, "");
            cd = SplittedFase[117].Substring(1).Replace(Environment.NewLine, "");
            rof2 = SplittedFase[116].Substring(1).Replace(Environment.NewLine, "");
            rof1 = SplittedFase[115].Substring(1).Replace(Environment.NewLine, "");
            installing = SplittedFase[113].Substring(1).Replace(Environment.NewLine, "");
            label9.Text = SplittedFase[111].Substring(1).Replace(Environment.NewLine, "");
            label15.Text = SplittedFase[123].Substring(1).Replace(Environment.NewLine, "");
            label16.Text = SplittedFase[124].Substring(1).Replace(Environment.NewLine, "");
            label5.Text = SplittedFase[125].Substring(1).Replace(Environment.NewLine, "");
            label10.Text = SplittedFase[126].Substring(1).Replace(Environment.NewLine, "");
            label7.Text = SplittedFase[127].Substring(1).Replace(Environment.NewLine, "");
            label14.Text = SplittedFase[128].Substring(1).Replace(Environment.NewLine, "");
            button1.Text = SplittedFase[121].Substring(1).Replace(Environment.NewLine, "");
            button2.Text = SplittedFase[87].Substring(1).Replace(Environment.NewLine, "");
            button3.Text = SplittedFase[120].Substring(1).Replace(Environment.NewLine, "");
            button4.Text = SplittedFase[120].Substring(1).Replace(Environment.NewLine, "");
            label8.Text = SplittedFase[114].Substring(1).Replace(Environment.NewLine, "");
            label6.Text = SplittedFase[113].Substring(1).Replace(Environment.NewLine, "");
            reqError = SplittedFase[129].Substring(1).Replace(Environment.NewLine, "");
            fileSizeError = SplittedFase[131].Substring(1).Replace(Environment.NewLine, "");
            label1.Text = SplittedFase[132].Substring(1).Replace(Environment.NewLine, "");
            label2.Text = SplittedFase[141].Substring(1).Replace(Environment.NewLine, "");
            ext = SplittedFase[142].Substring(1).Replace(Environment.NewLine, "");
            theme = SplittedFase[14].Substring(1).Replace(Environment.NewLine, "");
            label4.Text = SplittedFase[180].Substring(1).Replace(Environment.NewLine, "");
            label12.Text = SplittedFase[181].Substring(1).Replace(Environment.NewLine, "");
            reqEmpty = SplittedFase[192].Substring(1).Replace(Environment.NewLine, "");
        }
    }
}
