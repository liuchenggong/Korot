using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmError : Form
    {
        private readonly Exception Error;
        public frmError(Exception error)
        {
            Error = error;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            lbErrorCode.Text = Error.Message;
            textBox1.Text = Error.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.KorotErrorTitle;
            label2.Text = Properties.Settings.Default.KorotErrorDesc.Replace("[NEWLINE]", Environment.NewLine);
            label3.Text = Properties.Settings.Default.KorotErrorTI;
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            textBox1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            lbErrorCode.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            textBox1.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false)) ? Color.Black : Color.White;
            lbErrorCode.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false)) ? Color.Black : Color.White;
        }
    }
}
