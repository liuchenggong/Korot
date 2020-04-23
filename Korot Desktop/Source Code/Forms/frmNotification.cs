using Microsoft.Win32;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNotification : Form
    {
        public bool isPreRelease = false;
        public int preVer = 0;
        private readonly frmCEF cefform;
        private readonly Notification notification;
        public frmNotification(frmCEF _frmCEF, Notification _n)
        {
            cefform = _frmCEF;
            notification = _n;
            InitializeComponent();
            lbSource.Text = notification.url;
            lbTitle.Text = notification.title;
            lbMessage.Text = notification.message;
            pbImage.ImageLocation = notification.imageUrl;
        }

        private bool playedSound = false;
        public void PlayNotificationSound()
        {
            if (playedSound) { return; }
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
                            object o = key.GetValue(null);
                            if (o != null)
                            {
                                SoundPlayer theSound = new SoundPlayer((string)o);
                                theSound.Play();
                                found = true;
                            }
                        }
                    }
                }
                catch
                { }
                if (!found)
                {
                    SystemSounds.Beep.Play();
                }
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
                            object o = key.GetValue(null);
                            if (o != null)
                            {
                                SoundPlayer theSound = new SoundPlayer((string)o);
                                theSound.Play();
                                found = true;
                            }
                        }
                    }
                }
                catch
                { }
                if (!found)
                {
                    SystemSounds.Beep.Play();
                }
            }
            playedSound = true;
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
            checkSilentMode();
            if (Properties.Settings.Default.silentAllNotifications) { Hide(); }
            if (!Properties.Settings.Default.quietMode) { PlayNotificationSound(); }
            lbKorot.Text = "Korot " + Application.ProductVersion.ToString() + (isPreRelease ? "-pre" + preVer : "") + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
        }

        private void checkSilentMode()
        {
            if (Properties.Settings.Default.autoSilent)
            {
                DayOfWeek wk = DateTime.Today.DayOfWeek;
                if ((cefform.Nsunday && wk == DayOfWeek.Sunday)
                    || (cefform.Nmonday && wk == DayOfWeek.Monday)
                    || (cefform.Ntuesday && wk == DayOfWeek.Tuesday)
                    || (cefform.Nwednesday && wk == DayOfWeek.Wednesday)
                    || (cefform.Nthursday && wk == DayOfWeek.Thursday)
                    || (cefform.Nfriday && wk == DayOfWeek.Friday)
                    || (cefform.Nsaturday && wk == DayOfWeek.Saturday))
                {
                    //it passed the first test to be silent.
                    DateTime date = DateTime.Now;
                    int h = date.Hour;
                    int m = date.Minute;
                    if (cefform.fromH < h)
                    {
                        if (cefform.toH > h)
                        {
                            Properties.Settings.Default.silentAllNotifications = true;
                        }
                        else if (cefform.toH == h)
                        {
                            if (m >= cefform.toM)
                            {
                                Properties.Settings.Default.silentAllNotifications = true;
                            }
                            else
                            {
                                Properties.Settings.Default.silentAllNotifications = false;
                            }
                        }
                        else
                        {
                            Properties.Settings.Default.silentAllNotifications = false;
                        }
                    }
                    else if (cefform.fromH == h)
                    {
                        if (m >= cefform.fromM)
                        {
                            Properties.Settings.Default.silentAllNotifications = true;
                        }
                        else
                        {
                            Properties.Settings.Default.silentAllNotifications = false;
                        }
                    }
                    else
                    {
                        Properties.Settings.Default.silentAllNotifications = false;
                    }
                }
                else
                {
                    Properties.Settings.Default.silentAllNotifications = false;
                }
            }
            if (Properties.Settings.Default.silentAllNotifications) { Properties.Settings.Default.quietMode = true; }
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this)));
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkSilentMode();
            if (Properties.Settings.Default.silentAllNotifications) { Hide(); } else { Show(); }
            if (!Properties.Settings.Default.quietMode) { PlayNotificationSound(); }
            Rectangle screenSize = Screen.GetWorkingArea(this);
            int pointX = screenSize.Width - (Width + 10);
            int pointY = screenSize.Height - ((cefform.anaform.notifications.IndexOf(this) + 1)
                *
                (Height + 10));
            Location = new Point(pointX, pointY);
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
