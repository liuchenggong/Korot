using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
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
            lbSource.Text = notification.url;
            lbTitle.Text = notification.title;
            lbMessage.Text = notification.message;
            pbImage.ImageLocation = notification.imageUrl;
            if (!Properties.Settings.Default.quietMode) { PlayNotificationSound(); }
        }
        public void PlayNotificationSound()
        {
            bool isw7 = Tools.getOSInfo() == "NT 6.1";
            if (!isw7)
            {
                bool found = false;
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\Notification.Default\.Current"))
                    {
                        if (key != null)
                        {
                            Object o = key.GetValue(null); // pass null to get (Default)
                            if (o != null)
                            {
                                SoundPlayer theSound = new SoundPlayer((String)o);
                                theSound.Play();
                                found = true;
                            }
                        }
                    }
                }
                catch
                { }
                if (!found)
                    SystemSounds.Beep.Play(); // consolation prize
            }
            else
            {
                bool found = false;
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\SystemNotification\.Current"))
                    {
                        if (key != null)
                        {
                            Object o = key.GetValue(null); // pass null to get (Default)
                            if (o != null)
                            {
                                SoundPlayer theSound = new SoundPlayer((String)o);
                                theSound.Play();
                                found = true;
                            }
                        }
                    }
                }
                catch
                { }
                if (!found)
                    SystemSounds.Beep.Play(); // consolation prize
            }
        }
        private void notification_Click(object sender, EventArgs e)
        {
            if (cefform == null) { cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this))); Close(); return; }
            if (cefform.IsDisposed) { cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this))); Close(); return; }
            if (cefform.closing) { cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this))); Close(); return; }
            cefform.Invoke(new Action(() => cefform.NewTab(notification.url)));
            cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this)));
            Close();
        }
        private void frmNotification_Load(object sender, EventArgs e)
        {
            lbKorot.Text = "Korot " +  Application.ProductVersion.ToString() + (isPreRelease ? "-pre" + preVer : "") + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this)));
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle screenSize = Screen.GetWorkingArea(this);
            int pointX = screenSize.Width - (this.Width + 10);
            int pointY = screenSize.Height - ((cefform.anaform.notifications.IndexOf(this) + 1) 
                *
                (this.Height + 10));
            this.Location = new Point(pointX, pointY);
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
