using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmDownloader : Form
    {
        string kaynak = null;
        string hedef = null; //2023
        public frmDownloader()
        {
            InitializeComponent();
        }
        WebClient webc = new WebClient();
        private static int Brightness(Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        private void frmDownloader_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.DowloadHistory += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + kaynak + ";" + hedef + ";";
            label1.Text += kaynak;
            label2.Text += hedef; //2023
            checkBox2.Checked = Properties.Settings.Default.downloadClose;
            checkBox1.Checked = Properties.Settings.Default.downloadOpen;
            //  webc.DownloadFileAsync(new Uri(kaynak), hedef);
            if (Brightness(Properties.Settings.Default.BackColor) < 130)
            { this.BackColor = Properties.Settings.Default.BackColor; this.ForeColor = Color.White; button1.BackColor = Color.FromArgb(Properties.Settings.Default.BackColor.R + 20, Properties.Settings.Default.BackColor.G + 20, Properties.Settings.Default.BackColor.B + 20); button1.ForeColor = Color.White; }
            else
            { this.BackColor = Properties.Settings.Default.BackColor; this.ForeColor = Color.Black; button1.BackColor = Color.FromArgb(Properties.Settings.Default.BackColor.R - 20, Properties.Settings.Default.BackColor.G - 20, Properties.Settings.Default.BackColor.B - 20); button1.ForeColor = Color.Black; }
        }

        public void downloaddone()
        {
            if (checkBox1.Checked) { button1_Click(null, null); }
            if (checkBox2.Checked) { this.Close(); }
            button1.Enabled = true;
            button1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.downloadOpen = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.downloadClose = checkBox2.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(hedef);
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", ex.Message, this.Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor);
                mesaj.ShowDialog();
            }
        }
    }
}
