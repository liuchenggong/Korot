using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNotificationPermission : Form
    {
        private readonly bool alreadyAddedAllow = false;
        private readonly bool alreadyAddedBlock = false;
        private readonly string baseUrl;
        private readonly frmCEF cefform;
        public frmNotificationPermission(frmCEF _frmCEF, string url)
        {
            baseUrl = url;
            cefform = _frmCEF;
            InitializeComponent();
            label1.Text = cefform.notificationPermission.Replace("[URL]", baseUrl);
            button1.Text = cefform.allow;
            button2.Text = cefform.deny;
            alreadyAddedAllow = Properties.Settings.Default.notificationAllow.Contains(baseUrl);
            alreadyAddedBlock = Properties.Settings.Default.notificationBlock.Contains(baseUrl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (alreadyAddedBlock)
            {
                Properties.Settings.Default.notificationBlock.Remove(baseUrl);
            }
            if (!alreadyAddedAllow)
            {
                Properties.Settings.Default.notificationAllow.Add(baseUrl);
            }
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (alreadyAddedAllow)
            {
                Properties.Settings.Default.notificationAllow.Remove(baseUrl);
            }
            if (!alreadyAddedBlock)
            {
                Properties.Settings.Default.notificationBlock.Add(baseUrl);
            }
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = cefform.notificationPermission.Replace("[URL]", baseUrl);
            button1.Text = cefform.allow;
            button2.Text = cefform.deny;
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            button1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            button2.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.Refresh()));
        }

        private void frmNotificationPermission_Leave(object sender, EventArgs e)
        {
            Close();
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.Refresh()));
        }

        private void frmNotificationPermission_Load(object sender, EventArgs e)
        {

        }
    }
}
