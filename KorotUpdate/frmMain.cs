using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KorotInstaller
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            bc1 = Color.FromArgb(255, 255, 255, 255);
            bc2 = Color.FromArgb(255, 235, 235, 235);
            bc3 = Color.FromArgb(255, 215, 215, 215);
            fc = Color.FromArgb(255, 0, 0, 0);
            updateTheme();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (allowClose)
            {
                Close();
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
        Color bc1;
        Color bc2;
        Color bc3;
        Color fc;
        bool isDark = false;
        private void updateTheme()
        {
            BackColor = bc1;
            ForeColor = fc;
            comboBox1.BackColor = bc2;
            comboBox1.ForeColor = fc;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                TabPage page = tabControl1.TabPages[i];
                page.BackColor = bc3;
                page.ForeColor = fc;
            }
            foreach (Control cntrl in Controls)
            {
                if (cntrl is Button)
                {
                    var button = cntrl as Button;
                    button.BackColor = bc3;
                    button.ForeColor = fc;
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            isDark = !isDark;

            if (isDark)
            {
                pictureBox2.Image = Properties.Resources.dark;
                bc1 = Color.FromArgb(255, 0, 0, 0);
                bc2 = Color.FromArgb(255 ,20, 20, 20);
                bc3 = Color.FromArgb(255 ,40, 40, 40);
                fc = Color.FromArgb(255, 255, 255, 255);
            }else
            {
                pictureBox2.Image = Properties.Resources.light;
                bc1 = Color.FromArgb(255, 255,255,255);
                bc2 = Color.FromArgb(255 ,235,235,235);
                bc3 = Color.FromArgb(255 ,215,215,215);
                fc = Color.FromArgb(255, 0,0,0);
            }
            updateTheme();
        }

        bool allowClose = true;
        bool allowSwitch = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitch) { allowSwitch = false; } else { e.Cancel = true; }
        }
    }
}
