using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmChangeTBTBack : Form
    {
        frmCEF cefform;
        public frmChangeTBTBack(frmCEF frm)
        {
            cefform = frm;
            InitializeComponent();
            pictureBox1.BackColor = cefform.ParentTab.BackColor;
            DialogResult = DialogResult.Cancel;
            label1.Text = cefform.titleBackInfo;
            btDefault.Text = cefform.setToDefault;
            btOK.Text = cefform.OK;
            btCancel.Text = cefform.Cancel;
        }
        public Color Color
        {
            get
            {
                return pictureBox1.BackColor;
            }
            set
            {
                pictureBox1.BackColor = value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog() { Color = pictureBox1.BackColor, AnyColor = true, AllowFullOpen = true, FullOpen = true, };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = dialog.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            this.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false)) ? Color.Black : Color.White;
            btDefault.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false);
            btDefault.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false)) ? Color.Black : Color.White;
            btOK.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false);
            btOK.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false)) ? Color.Black : Color.White;
            btCancel.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false);
            btCancel.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false)) ? Color.Black : Color.White;
        }
    }
}
