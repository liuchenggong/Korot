using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot_Installer
{
    public partial class frmFrame : Form
    {
        bool isMouseDown = false;
        Point mouseposition;
        public frmFrame()
        {
            InitializeComponent();

        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseposition = new Point(-e.X, -e.Y);
                isMouseDown = true;
            }
        }
        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousepad = Control.MousePosition;
                mousepad.Offset(mouseposition.X, mouseposition.Y);
                this.Location = mousepad;
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //Dark Mode
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
            }else
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
            isDarkMode = checkBox1.Checked;
        }
        public bool isDarkMode = false;
        public bool doNotClose = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFrame(new frame0(this));
        }
        public void ShowFrame(Form newframe)
        {
            panel1.Controls.Clear();
            newframe.TopMost = false;
            newframe.TopLevel = false;
            newframe.Dock = DockStyle.Fill;
            newframe.WindowState = FormWindowState.Maximized;
            newframe.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(newframe);
            newframe.Show();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (doNotClose)
            {
                label3.Enabled = false;
                label3.Visible = false;
            }else
            {
                label3.Enabled = true;
                label3.Visible = true;
            }
        }

        private void FrmFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (doNotClose)
            {
                e.Cancel = true;
            }
        }
    }
}
