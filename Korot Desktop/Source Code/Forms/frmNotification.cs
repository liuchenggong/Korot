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
    public partial class frmNotification : Form
    {
        public bool isPreRelease = false;
        public int preVer = 0;
        frmCEF cefform;
        Notification notification;
        public frmNotification(frmCEF _frmCEF,Notification _n)
        {
            cefform = _frmCEF;
            notification = _n;
            InitializeComponent();
            Rectangle screenSize = Screen.GetWorkingArea(this);
            int pointX = screenSize.Width - (this.Width + 10);
            int pointY = screenSize.Height -( this.Height + 10);
            this.Location = new Point(pointX,pointY);
            lbSource.Text = notification.url;
            lbTitle.Text = notification.title;
            lbMessage.Text = notification.message;
            pbImage.ImageLocation = notification.imageUrl;

        }
        private void notification_Click(object sender, EventArgs e)
        {
            if (cefform == null) { Close(); return; }
            if (cefform.IsDisposed) { Close(); return; }
            if (cefform.closing) { Close(); return; }
            cefform.Invoke(new Action(() => cefform.NewTab(notification.url)));
            Close();
        }
        private void frmNotification_Load(object sender, EventArgs e)
        {
            lbKorot.Text = "Korot " +  Application.ProductVersion.ToString() + (isPreRelease ? "-pre" + preVer : "") + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
        }
    }
    public class Notification
    {
       public string url { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string imageUrl { get; set; }
    }
}
