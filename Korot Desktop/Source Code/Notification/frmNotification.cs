using CefSharp;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNotification : Form
    {
        private readonly frmCEF cefform;
        public Notification notification;
        public frmNotification(frmCEF _frmCEF, Notification _n)
        {
            cefform = _frmCEF;
            notification = _n;
            InitializeComponent();
            lbSource.Text = notification.url;
            lbTitle.Text = notification.title;
            lbMessage.Text = notification.message;
            ilkImage = notification.imageUrl;
            if (!HTAlt.Tools.ValidUrl(ilkImage, new string[] { "korot:" }))
            {
                ilkImage = notification.url + ilkImage;
            }
            try
            {
                pbImage.Image = HTAlt.Tools.GetImageFromUrl(ilkImage);
            }
            catch (Exception)
            {
                pbImage.Image = Properties.Resources.error;

            }
        }

        private bool playedSound = false;
        public void PlayNotificationSound()
        {
            if (playedSound) { return; }
            bool isw7 = KorotTools.getOSInfo() == "NT 6.1";
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
            if (string.IsNullOrWhiteSpace(notification.action))
            {
                cefform.Invoke(new Action(() => cefform.NewTab(notification.url)));
            }
            else
            {
                cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsync(@" " + notification.action)));
            }
            cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this)));
            Close();
        }
        private void frmNotification_Load(object sender, EventArgs e)
        {
            bool n = cefform.Settings.IsQuietTime;
            if (cefform.Settings.DoNotPlaySound) { PlayNotificationSound(); }
            if (!cefform.Settings.QuietMode) { Hide(); }
            lbKorot.Text = "Korot " + Application.ProductVersion.ToString() + (VersionInfo.IsPreRelease ? "-pre" + VersionInfo.PreReleaseNumber : "") + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)") + " (" + VersionInfo.CodeName + ")";
        }


        private void lbClose_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.anaform.notifications.Remove(this)));
            Close();
        }

        private string ilkImage = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ilkImage != notification.imageUrl)
            {
                ilkImage = notification.imageUrl;
                pbImage.Image = HTAlt.Tools.GetImageFromUrl(ilkImage);
            }
            lbSource.Text = notification.url;
            lbTitle.Text = notification.title;
            lbMessage.Text = notification.message;
            bool n = cefform.Settings.IsQuietTime;
            if (cefform.Settings.QuietMode) { Hide(); } else { Show(); }
            if (!cefform.Settings.QuietMode) { PlayNotificationSound(); }
            Rectangle screenSize = Screen.GetWorkingArea(this);
            int pointX = screenSize.Width - (Width + 10);
            int pointY = screenSize.Height - ((cefform.anaform.notifications.IndexOf(this) + 1)
                *
                (Height + 10));
            Location = new Point(pointX, pointY);
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pUp.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pDown.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pLeft.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pRight.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
        }
    }
    public class Notification
    {
        public string id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public string imageUrl { get; set; }
        public string action { get; set; }
    }
}
