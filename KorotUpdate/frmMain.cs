using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace KorotInstaller
{
    public partial class frmMain : Form
    {
        Settings Settings;
        public frmMain(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            GPWC.DownloadStringCompleted += GPWC_DownloadStringComplete;
            GPWC.DownloadProgressChanged += GPWC_ProgressChanged;
            GPWC.DownloadFileCompleted += GPWC_DownloadFileComplete;
            updateTheme();
        }
        StringEventhHybrid workOn;
        List<StringEventhHybrid> downloadStrings = new List<StringEventhHybrid>();

        /// <summary>
        /// General purpose WebClient
        /// </summary>
        WebClient GPWC = new WebClient();
        private void GPWC_DownloadStringComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (GPWC.IsBusy) { GPWC.CancelAsync(); }
            if (workOn.Type == StringEventhHybrid.StringType.String)
            {
                workOn.RunEvent(this, e);
                downloadStrings.Remove(workOn);
                workOn = downloadStrings.Find(i => i.Type == StringEventhHybrid.StringType.String);
                if (workOn != null)
                {
                    GPWC.DownloadStringAsync(new Uri(workOn.String));
                }else
                {
                    if (downloadStrings.Count == 0 && !stillWork) { GPWC_AllJobsDone(); }
                }
            }
        }

        bool isPreparing = true;
        private void GPWC_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (workOn.Type == StringEventhHybrid.StringType.File)
            {
                if (!isPreparing)
                {
                    lbDownloadInfo.Text = DownloadProgress.Replace("[NAME]", workOn.String3).Replace("[CURRENT]", (e.BytesReceived / 1024) + "KiB").Replace("[TOTAL]", (e.TotalBytesToReceive / 1024) + "KiB");
                    pbDownload.Width = e.ProgressPercentage * (pDownload.Width / 100);
                }
            }
        }

        private void GPWC_DownloadFileComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (GPWC.IsBusy) { GPWC.CancelAsync(); }
            if (workOn.Type == StringEventhHybrid.StringType.File)
            {
                workOn.RunEvent(this, e);
                downloadStrings.Remove(workOn);
                workOn = downloadStrings.Find(i => i.Type == StringEventhHybrid.StringType.File);
                if (workOn != null)
                {
                    GPWC.DownloadStringAsync(new Uri(workOn.String));
                }
                else
                {
                    if(downloadStrings.Count == 0 && !stillWork) { GPWC_AllJobsDone(); }
                    if (!isPreparing)
                    {
                        lbDownloadInfo.Text = DownloadsComplete;
                    }
                }
            }
        }
        bool stillWork = true;
        private void GPWC_AllJobsDone()
        {
            Console.WriteLine(" [General Purpose WebClient] All Jobs Done!");
        }

        #region "Translations"
        string UIChangeVerMissing = "We couldn't find this version at our archives.";
        string UIChangeVerArchNotSupported = "Your platform is not supported in this version.";
        string DownloadProgress = "Downloading [NAME]... [CURRENT]/[TOTAL]";
        string DownloadKorotDesktop = "Korot Desktop";
        string DownloadsComplete = "All downloads are finished.";
        string InstallComplete = "All installations are finished.";
        string RegistryComplete = "All registries are registered.";
        string RegistryStart = "Registering...";
        string UIYes = "Yes";
        string UINo = "No";
        string UIOK = "OK";
        string UICancel = "Cancel";
        string UIInstallVer = "Install [VER]";
        string UIGatherInfo = "Gathering Information...";
        string UICheckUpdate = "Checking for updates...";
        string UIChangeVerO1 = "Latest PreOut Version ([PREOUT])";
        string UIChangeVerO2 = "Latest Stable Version ([LATEST])";
        string UICreateRecovery = "Creating a restore point...";
        string UIDoneUninstall = "Korot successfully uninstalled." + Environment.NewLine + "" + Environment.NewLine + "It's improtant for us to listen to your reason why you decided to uninstall Korot so please open an issue in GitHub by clickng &quot;Send Feedback&quot; button below." + Environment.NewLine + "" + Environment.NewLine + "Farewell, old firend." + Environment.NewLine + "" + Environment.NewLine + "";
        string UIDoneInstall = "Korot installed successfully." + Environment.NewLine + "" + Environment.NewLine + "Closing this program will start the application.";
        string UIDoneUpdate = "Korot updated successfully.";
        string UIDoneRepair = "Korot repaired successfully.";
        string UIDoneError = "An error occured while doing your request. Don't worry, we restored your Korot installation. Please create an issue on GitHub by clicking &quot;Send Feedback&quot; and copy-paste this information below:";
        string UIPreOutAvailable = "You meet the requirements.";
        string UIPreOutDisable = "You don't meet the requirements. Update or Repair your Korot first.";
        string CreateShortcut = "Creating shortcuts...";
        string UIUpdating = "Updating Installer... Please wait..." + Environment.NewLine + "[PERC]% | [CURRENT] KiB downloaded out of [TOTAL] KiB.";

        public void LoadLang()
        {
            UIInstallVer = Settings.GetItemText("UIInstallVer");
            UIChangeVerMissing = Settings.GetItemText("UIChangeVerMissing");
            UIChangeVerArchNotSupported = Settings.GetItemText("UIChangeVerArchNotSupported");
            DownloadProgress = Settings.GetItemText("DownloadProgress");
            DownloadKorotDesktop = Settings.GetItemText("DownloadKorotDesktop");
            DownloadsComplete = Settings.GetItemText("DownloadsComplete");
            InstallComplete = Settings.GetItemText("InstallComplete");
            RegistryComplete = Settings.GetItemText("RegistryComplete");
            RegistryStart = Settings.GetItemText("RegistryStart");
            UIYes = Settings.GetItemText("UIYes");
            UINo = Settings.GetItemText("UINo");
            UIOK = Settings.GetItemText("UIOK");
            UICancel = Settings.GetItemText("UICancel");
            btClose.Text = Settings.GetItemText("UIClose");
            UIGatherInfo = Settings.GetItemText("UIGatherInfo");
            UICheckUpdate = Settings.GetItemText("UICheckUpdate");
            lbReady.Text = Settings.GetItemText("UIReadyDesc");
            lbModifyDesc.Text = Settings.GetItemText("UIModifyDesc");
            btInstall.Text = Settings.GetItemText("UIReadyButton");
            btRepair.Text = Settings.GetItemText("UIRepairButton");
            btUninstall.Text = Settings.GetItemText("UIUninstallButton");
            btChangeVer.Text = Settings.GetItemText("UIChangeVerButton");
            UIChangeVerO1 = Settings.GetItemText("UIChangeVerO1");
            UIChangeVerO2 = Settings.GetItemText("UIChangeVerO2");
            rbOld.Text = Settings.GetItemText("UIChangeVerO3");
            lbDownloading.Text = Settings.GetItemText("UIDownloading");
            lbInstalling.Text = Settings.GetItemText("UIInstalling");
            UICreateRecovery = Settings.GetItemText("UICreateRecovery");
            UIDoneUninstall = Settings.GetItemText("UIDoneUninstall");
            UIDoneInstall = Settings.GetItemText("UIDoneInstall");
            UIDoneUpdate = Settings.GetItemText("UIDoneUpdate");
            UIDoneRepair = Settings.GetItemText("UIDoneRepair");
            UIDoneError = Settings.GetItemText("UIDoneError");
            btSendFeedback.Text = Settings.GetItemText("UISendFeedback");
            UIPreOutAvailable = Settings.GetItemText("UIPreOutAvailable");
            UIPreOutDisable = Settings.GetItemText("UIPreOutDisable");
            lbVersionToInstall.Text = Settings.GetItemText("UIVersionToInstall");
            CreateShortcut = Settings.GetItemText("CreateShortcut");
            UIUpdating = Settings.GetItemText("UIUpdating");
        }

        #endregion "Translations"

        private void frmMain_Load(object sender, EventArgs e)
        {
            StringEventhHybrid htupdate = new StringEventhHybrid() { String = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate", Type = StringEventhHybrid.StringType.String, };
            htupdate.Event += htupdateDownloaded;
            DoStringWork(htupdate);
        }

        private void DoStringWork(StringEventhHybrid seh)
        {
            downloadStrings.Add(seh);
            if (!GPWC.IsBusy) { workOn = seh; GPWC.DownloadStringAsync(new Uri(seh.String)); }
        }

        private void DoFileWork(StringEventhHybrid seh)
        {
            Console.WriteLine(" [DoFileWork] string: " + seh.String + " string2: " + seh.String2 + " string3: " + seh.String3);
            downloadStrings.Add(seh);
            if (!GPWC.IsBusy) { workOn = seh; GPWC.DownloadFileAsync(new Uri(seh.String),seh.String2); }
        }

        #region "HTUPDATE"
        private VersionManager VersionManager = new VersionManager();
        #endregion "HTUPDATE"

        private void htupdateDownloaded(object sender, EventArgs E)
        {
            Console.WriteLine(" [HTUPDATE] received.");
            if(E is DownloadStringCompletedEventArgs)
            {
                var e = E as DownloadStringCompletedEventArgs;
                if (e.Error != null && !e.Cancelled)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(e.Result);
                    XmlNode firstnode = doc.FirstChild.Name != "HaltroyUpdate" ? doc.FirstChild.NextSibling : doc.FirstChild;
                    foreach (XmlNode node in firstnode.ChildNodes)
                    {
                        if (node.Name == "PreOutVer")
                        {
                            VersionManager.LatesPreOut = node.InnerXml;
                        }
                        else if (node.Name == "PreOutNo")
                        {
                            VersionManager.PreOutVerNumber = Convert.ToInt32(node.InnerXml);
                        }
                        else if (node.Name == "PreOutLow")
                        {
                            VersionManager.PreOutMinVer = Convert.ToInt32(node.InnerXml);
                        }
                        else if (node.Name == "AppVersionNo")
                        {
                            VersionManager.LatestVersionNumber = Convert.ToInt32(node.InnerXml);
                        }
                        else if (node.Name == "MinimumNo")
                        {
                            VersionManager.LatestUpdateMinVer = Convert.ToInt32(node.InnerXml);
                        }
                        else if (node.Name == "InstallerVer")
                        {
                            VersionManager.LatestInstallerVer = Convert.ToInt32(node.InnerXml);
                        }
                        else if (node.Name == "AppVersion")
                        {
                            VersionManager.LatesVersion = node.InnerXml;
                        }
                        else if(node.Name == "UpdateLang")
                        {
                            foreach(XmlNode subnode in node.ChildNodes)
                            {
                                if (node.Name == "Lang")
                                {
                                    if (subnode.Attributes["Name"] != null && subnode.Attributes["Location"] != null)
                                    {
                                        if (File.Exists(Settings.WorkFolder + subnode.Attributes["Name"].Value + ".language"))
                                        {
                                            File.Delete(Settings.WorkFolder + subnode.Attributes["Name"].Value + ".language");
                                        }
                                        StringEventhHybrid seh = new StringEventhHybrid()
                                        {
                                            String = subnode.Attributes["Location"].Value,
                                            String2 = Settings.WorkFolder + subnode.Attributes["Name"].Value + ".language",
                                            String3 = subnode.Attributes["Name"].Value,
                                            Type = StringEventhHybrid.StringType.File
                                        };
                                        DoFileWork(seh);
                                    }
                                }
                            }
                        }
                        else if (node.Name == "Versions")
                        {
                            foreach(XmlNode subnode in node.ChildNodes)
                            {
                                if (subnode.Name == "Version")
                                {
                                    if (subnode.Attributes["VersionNo"] != null && subnode.Attributes["Flags"] != null && subnode.Attributes["Text"] != null)
                                    {
                                        if (subnode.Attributes["Flags"].Value.Contains("missing"))
                                        {
                                            KorotVersion ver = new KorotVersion(subnode.Attributes["Text"].Value, Convert.ToInt32(subnode.Attributes["VersionNo"].Value),subnode.Attributes["Flags"].Value);
                                            VersionManager.Versions.Add(ver);
                                        }else
                                        {
                                            KorotVersion ver = new KorotVersion(subnode.Attributes["Text"].Value, Convert.ToInt32(subnode.Attributes["VersionNo"].Value), subnode.Attributes["ZipPath"].Value, subnode.Attributes["Flags"].Value);
                                            VersionManager.Versions.Add(ver);
                                        }

                                    }
                                }
                            }
                        }
                    }
                    stillWork = false;
                }
                else
                {
                    StringEventhHybrid htupdate = new StringEventhHybrid() { String = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate", Type = StringEventhHybrid.StringType.String, };
                    htupdate.Event += htupdateDownloaded;
                }
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            if (allowClose)
            {
                Settings.Save();
                Close();
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }


        private void updateTheme()
        {
            BackColor = Settings.BackColor;
            ForeColor = Settings.ForeColor;
            pictureBox2.Image = Settings.isDarkMode ? Properties.Resources.dark : Properties.Resources.light;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                TabPage page = tabControl1.TabPages[i];
                page.BackColor = Settings.BackColor1;
                page.ForeColor = Settings.ForeColor;
            }
            foreach (Control cntrl in Controls)
            {
                RecursiveControlPaint(cntrl);
            }
            cbLang.BackColor = Settings.BackColor1;
            cbLang.ForeColor = Settings.MidColor;
        }
        private void RecursiveControlPaint(Control cntrl)
        {
            if (cntrl is null) { return; }
            if (cntrl is Button)
            {
                var button = cntrl as Button;
                button.BackColor = Settings.BackColor2;
                button.ForeColor = Settings.ForeColor;
            }
            else if (cntrl is ComboBox)
            {
                var button = cntrl as ComboBox;
                button.BackColor = Settings.BackColor2;
                button.ForeColor = Settings.ForeColor;
            }else
            {
                foreach (Control cntrl1 in cntrl.Controls)
                {
                    RecursiveControlPaint(cntrl1);
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Settings.isDarkMode = !Settings.isDarkMode;
            updateTheme();
        }

        bool allowClose = true;
        bool allowSwitch = false;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitch) { allowSwitch = false; } else { e.Cancel = true; }
        }

        private void cbLang_DropDown(object sender, EventArgs e)
        {
            cbLang.Items.Clear();
            String[] langFiles = Directory.GetFiles(Settings.WorkFolder, "*.language");
            for (int i = 0; i< langFiles.Length;i++)
            {
                cbLang.Items.Add(Path.GetFileNameWithoutExtension(langFiles[i]));
            }
        }

        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.LoadLang(Settings.WorkFolder + cbLang.SelectedItem.ToString() + ".language");
            LoadLang();
        }
    }
    public class StringEventhHybrid
    {
        public string String { get; set; }
        public string String2 { get; set; }
        public string String3 { get; set; }
        public StringType Type { get; set; }
        public delegate void EventDelegate(object sender, EventArgs e);
        public event EventDelegate Event;
        public void RunEvent(object sender, EventArgs e)
        {
            if (Event != null)
            {
                Event(sender, e);
            }
        }

        public enum StringType
        {
            String,
            File
        }
    }
}
