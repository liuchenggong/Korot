using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace Korot
{
    public partial class frmInstallExt : Form
    {
        bool requires1;
        bool requires2;
        bool requires3;
        bool allowSwitch = false;
        string ExtFile;
        string noPermission = "This extension does not require any permissions.";
        string Initializing = "Initializing...";
        string installed = "Installed.";
        string dc = "Directory created. Moving files and folders...";
        string cd = "Creating directory...";
        string rof2 = "Removed old files.";
        string rof1 = "Removing old files...";
        string installing = "Installing...";
        public frmInstallExt(string installFrom)
        {
            ExtFile = installFrom;
            InitializeComponent();
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

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\", true);
                }
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\");
                ZipFile.ExtractToDirectory(ExtFile, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\",Encoding.UTF8);
                ExtFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\Korot\\newExt\\ext.kem";
                ReadKEM(ExtFile);
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            pictureBox1.BackColor = Properties.Settings.Default.OverlayColor;
            tabPage1.BackColor = Properties.Settings.Default.BackColor;
            tabPage1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabPage2.BackColor = Properties.Settings.Default.BackColor;
            tabPage2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabPage3.BackColor = Properties.Settings.Default.BackColor;
            tabPage3.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            panel1.BackColor = Properties.Settings.Default.BackColor;
            panel1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            pictureBox7.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._1_w : Properties.Resources._1;
            pictureBox2.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._2_w : Properties.Resources._2;
            pictureBox5.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources._3_w : Properties.Resources._3;
            StreamReader ReadFile = new StreamReader(Properties.Settings.Default.LangFile, Encoding.UTF8, false);
            string Playlist = ReadFile.ReadToEnd();
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            noPermission = SplittedFase[116].Substring(1).Replace(Environment.NewLine, "");
            Initializing = SplittedFase[126].Substring(1).Replace(Environment.NewLine, "");
            installed = SplittedFase[123].Substring(1).Replace(Environment.NewLine, "");
            dc = SplittedFase[122].Substring(1).Replace(Environment.NewLine, "");
            cd = SplittedFase[121].Substring(1).Replace(Environment.NewLine, "");
            rof2 = SplittedFase[120].Substring(1).Replace(Environment.NewLine, "");
            rof1 = SplittedFase[119].Substring(1).Replace(Environment.NewLine, "");
            installing = SplittedFase[118].Substring(1).Replace(Environment.NewLine, "");
            label9.Text = SplittedFase[115].Substring(1).Replace(Environment.NewLine, "");
            label15.Text = SplittedFase[127].Substring(1).Replace(Environment.NewLine, "");
        label16.Text = SplittedFase[128].Substring(1).Replace(Environment.NewLine, "");
        label5.Text = SplittedFase[129].Substring(1).Replace(Environment.NewLine, "");
            label10.Text = SplittedFase[130].Substring(1).Replace(Environment.NewLine, "");
            label7.Text = SplittedFase[131].Substring(1).Replace(Environment.NewLine, "");
            label14.Text = SplittedFase[132].Substring(1).Replace(Environment.NewLine, "");
            button1.Text = SplittedFase[125].Substring(1).Replace(Environment.NewLine, "");
            button2.Text = SplittedFase[87].Substring(1).Replace(Environment.NewLine, "");
        button3.Text = SplittedFase[124].Substring(1).Replace(Environment.NewLine, "");
            button4.Text = SplittedFase[124].Substring(1).Replace(Environment.NewLine, "");
            label8.Text = SplittedFase[118].Substring(1).Replace(Environment.NewLine, "");
            label6.Text = SplittedFase[117].Substring(1).Replace(Environment.NewLine, "");
        }
        void ReadKEM(string fileLocation)
        {
            StreamReader ReadFile = new StreamReader(fileLocation, Encoding.UTF8, false);
            string Playlist = ReadFile.ReadToEnd();
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            //ExtName
                lbName.Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "");
            //ExtVersion
            lbVersion.Text = SplittedFase[1].Substring(1).Replace(Environment.NewLine, "");
            //ExtAuthor
            lbAuthor.Text = SplittedFase[2].Substring(1).Replace(Environment.NewLine, "");
            //ExtIcon
            pbLogo.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).DirectoryName + " \\" ));
            //ExtReq - autoLoad
            if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "") == "1")
            {
                requires1 = true;
            }
            else if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "") == "0")
            {
                requires1 = false;
            }else
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tabPage1;
                textBox1.Text = "Requirement (autoLoad) can only get \"1\" or \"0\" values." + Environment.NewLine + " at " + fileLocation + " line 5";
            }
            //ExtReq - canAccessWebContent
            if (SplittedFase[5].Substring(1).Replace(Environment.NewLine, "") == "1")
            {
                requires2 = true;
            }
            else if (SplittedFase[5].Substring(1).Replace(Environment.NewLine, "") == "0")
            {
                requires2 = false;
            }
            else
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tabPage1;
                textBox1.Text = "Requirement (canAccessWebContent) can only get \"1\" or \"0\" values." + Environment.NewLine + " at " + fileLocation + " line 6";
            }
            //ExtReq - onlineFiles
            if (SplittedFase[6].Substring(1).Replace(Environment.NewLine, "") == "1")
            {
                requires3 = true;
            }
            else if (SplittedFase[6].Substring(1).Replace(Environment.NewLine, "") == "0")
            {
                requires3 = false;
            }
            else
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tabPage1;
                textBox1.Text = "Requirement (onlineFiles) can only get \"1\" or \"0\" values." + Environment.NewLine + " at " + fileLocation + " line 7";
            }

            ReadFile.Close();
            panel6.Visible = requires1;
            panel7.Visible = requires2;
            panel3.Visible = requires3;
            if (!requires1 && !requires2 && !requires3)
            {
                label9.Text = noPermission;
            }
            lbVersion.Location = new Point(lbName.Location.X + lbName.Width + 5,lbName.Location.Y);
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !allowSwitch;
            if (allowSwitch) { allowSwitch = false; }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage3;
            timer1.Start();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 1;
            lbStatus.Text = Initializing;
            if (i == 5)
            {
                installKEM();
                timer1.Stop();
            }
        }
        string korotExtDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions";
        string extCodeName;
#pragma warning disable CS1998 // Zaman uyumsuz yöntemde 'await' işleçleri yok ve zaman uyumlu çalışacak
        private async void installKEM()
#pragma warning restore CS1998 // Zaman uyumsuz yöntemde 'await' işleçleri yok ve zaman uyumlu çalışacak
        {
            extCodeName = (lbAuthor.Text + "." + lbName.Text).Replace("\\", "").Replace("/", "").Replace(":", "").Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");
            button3.Visible = false;
            button3.Enabled = false;
            lbStatus.Text = installing;
            pbStatus.Width = 45;
            if (Directory.Exists(korotExtDirectory + "\\" + extCodeName))
            {
                lbStatus.Text = rof1;
                Directory.Delete(korotExtDirectory + "\\" + extCodeName, true);
                lbStatus.Text = rof2;
            }
            pbStatus.Width = 90;
            lbStatus.Text = cd;
            Directory.CreateDirectory(korotExtDirectory + "\\" + extCodeName);
            pbStatus.Width = 180;
            lbStatus.Text = dc;
            foreach (String x in Directory.GetFiles(new FileInfo(ExtFile).DirectoryName + " \\"))
            {
                File.Copy(x, korotExtDirectory + "\\" + extCodeName + "\\" + new FileInfo(x).Name);
            }
            foreach (String x in Directory.GetDirectories(new FileInfo(ExtFile).DirectoryName + " \\"))
            {
                Directory.Move(x, korotExtDirectory + "\\" + extCodeName + "\\" + new DirectoryInfo(x).Name);
            }
            lbStatus.Visible = false;
            label8.Text = installed;
            button3.Visible = true;
            button3.Enabled = true;
            panel2.Visible = false;
            pbStatus.Width = 300;
        }
    }
}
