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
            label1.Text += kaynak;
            label2.Text += hedef; //2023
            Properties.Settings.Default.DowloadHistory += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + kaynak + ";" + hedef + ";"; checkBox2.Checked = Properties.Settings.Default.downloadClose;
            checkBox1.Checked = Properties.Settings.Default.downloadOpen;
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
            catch
            {
            }
        }
    }
}
