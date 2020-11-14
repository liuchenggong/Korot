﻿using System;
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
using System.Management;
using System.Diagnostics;

namespace KorotInstaller
{
    public partial class frmMain : Form
    {
        Settings Settings;
        public frmMain(Settings settings)
        {
            Settings = settings;
            VersionManager = new VersionManager();
            InitializeComponent();
            GPWC.DownloadStringCompleted += GPWC_DownloadStringComplete;
            GPWC.DownloadProgressChanged += GPWC_ProgressChanged;
            GPWC.DownloadFileCompleted += GPWC_DownloadFileComplete;
            string[] langFiles = Directory.GetFiles(Settings.WorkFolder, "*.language");
            cbLang.Items.Clear();
            for (int i = 0; i < langFiles.Length; i++)
            {
                cbLang.Items.Add(Path.GetFileNameWithoutExtension(langFiles[i]));
            }
            cbLang.Text = Path.GetFileNameWithoutExtension(Settings.LanguageFile);
            LoadLang();
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
                    GPWC_AllJobsDone(); 
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
                    GPWC_AllJobsDone(); 
                    if (!isPreparing)
                    {
                        lbDownloadInfo.Text = DownloadsComplete;
                    }
                }
            }
        }
        private void GPWC_AllJobsDone()
        {
            if (downloadStrings.Count > 0 && !GPWC.IsBusy && workOn == null)
            {
                if(downloadStrings[0].Type == StringEventhHybrid.StringType.File)
                {
                    DoFileWork(downloadStrings[0]);
                }else
                {
                    DoStringWork(downloadStrings[0]);
                }
                return;
            }
            if (isPreparing)
            {
                isPreparing = false;
                allowSwitch = true;
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot.exe") && !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot Beta.exe"))
                {
                    allowSwitch = true;
                    tabControl1.SelectedTab = tpFirst;
                    if(supportsThisVer(VersionManager.GetVersionFromVersionNo(VersionManager.LatestVersionNumber)))
                    {
                        lbReady.Text = UIReadyDesc;
                        btInstall.Enabled = true;
                        btInstall.Visible = true;
                    }else
                    {
                        lbReady.Text = UINotReadyDesc;
                        btInstall.Enabled = false;
                        btInstall.Visible = false;
                    }
                }
                else
                {
                    allowSwitch = true;
                    tabControl1.SelectedTab = tpModify;
                    if (VersionManager.Versions.Count > 0)
                    {
                        string korotPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot.exe";
                        string korotBetaPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot Beta.exe";
                        bool korotExists = File.Exists(korotPath);
                        var current = VersionManager.GetVersionFromVersionName(FileVersionInfo.GetVersionInfo(korotExists ? korotPath : korotBetaPath).ProductVersion);
                        btRepair.Text = current != null
                            ? VersionManager.LatestVersionNumber != current.VersionNo || VersionManager.PreOutVerNumber != current.VersionNo
                                ? UIUpdateButton
                                : UIRepairButton
                            : UIRepairButton;
                    }
                }
            }
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
        string UIRepairButton = "Repair";
        string UIUpdateButton = "Update";
        string UIInstallVer = "Install [VER]";
        string UIGatherInfo = "Gathering Information...";
        string UICheckUpdate = "Checking for updates...";
        string UIReadyDesc = "Your Korot is ready to be installed.";
        string UINotReadyDesc ="You don't meet the requirements for installing Korot.";
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
            lbChangeVerDesc.Text = Settings.GetItemText("UIChangeVerDesc");
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
            UIReadyDesc = Settings.GetItemText("UIReadyDesc");
            UINotReadyDesc = Settings.GetItemText("UINotReadyDesc");
            lbModifyDesc.Text = Settings.GetItemText("UIModifyDesc");
            btInstall.Text = Settings.GetItemText("UIReadyButton");
            UIRepairButton = Settings.GetItemText("UIRepairButton");
            UIUpdateButton = Settings.GetItemText("UIUpdateButton");
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
            if (VersionManager.PreOutVerNumber != 0 && VersionManager.LatestVersionNumber != 0)
            {
                rbPreOut.Text = UIChangeVerO1.Replace("[PREOUT]", VersionManager.GetVersionFromVersionNo(VersionManager.PreOutVerNumber).VersionText);
                rbStable.Text = UIChangeVerO2.Replace("[LATEST]", VersionManager.GetVersionFromVersionNo(VersionManager.LatestVersionNumber).VersionText);
                if (supportsLatestPreOut())
                {
                    lbPerOutReq.Text = UIPreOutAvailable;
                    rbPreOut.Enabled = true;
                }
                else
                {
                    lbPerOutReq.Text = UIPreOutDisable;
                    rbPreOut.Enabled = false;
                }
            }
            if (VersionManager.Versions.Count > 0)
            {
                string korotPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot.exe";
                string korotBetaPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot Beta.exe";
                bool korotExists = File.Exists(korotPath);
                var current = VersionManager.GetVersionFromVersionName(FileVersionInfo.GetVersionInfo(korotExists ? korotPath : korotBetaPath).ProductVersion);
                btRepair.Text = current != null
                    ? VersionManager.LatestVersionNumber != current.VersionNo || VersionManager.PreOutVerNumber != current.VersionNo
                        ? UIUpdateButton
                        : UIRepairButton
                    : UIRepairButton;
            }
            cbOld.Location = new Point(lbVersionToInstall.Location.X + lbVersionToInstall.Width, cbOld.Location.Y);

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
            downloadStrings.Add(seh);
            if (!GPWC.IsBusy) { workOn = seh; GPWC.DownloadFileAsync(new Uri(seh.String),seh.String2); }
        }

        #region "HTUPDATE"
        private VersionManager VersionManager;
        #endregion "HTUPDATE"

        private void htupdateDownloaded(object sender, EventArgs E)
        {
            if(E is DownloadStringCompletedEventArgs)
            {
                var e = E as DownloadStringCompletedEventArgs;
                if (e.Error == null && !e.Cancelled)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(e.Result);
                    XmlNode firstnode = doc.FirstChild.Name != "HaltroyUpdate" ? doc.FirstChild.NextSibling : doc.FirstChild;
                    int workCount = 0;
                    foreach (XmlNode node in firstnode.ChildNodes)
                    {
                        if (node.Name == "PreOutVer")
                        {
                            VersionManager.LatesPreOut = node.InnerXml;
                            workCount++;
                        }
                        else if (node.Name == "PreOutNo")
                        {
                            VersionManager.PreOutVerNumber = Convert.ToInt32(node.InnerXml);
                            workCount++;
                        }
                        else if (node.Name == "PreOutLow")
                        {
                            VersionManager.PreOutMinVer = Convert.ToInt32(node.InnerXml);
                            workCount++;
                        }
                        else if (node.Name == "AppVersionNo")
                        {
                            VersionManager.LatestVersionNumber = Convert.ToInt32(node.InnerXml);
                            workCount++;
                        }
                        else if (node.Name == "MinimumNo")
                        {
                            VersionManager.LatestUpdateMinVer = Convert.ToInt32(node.InnerXml);
                            workCount++;
                        }
                        else if (node.Name == "InstallerVer")
                        {
                            VersionManager.LatestInstallerVer = Convert.ToInt32(node.InnerXml);
                            workCount++;
                        }
                        else if (node.Name == "AppVersion")
                        {
                            VersionManager.LatesVersion = node.InnerXml;
                            workCount++;
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
                                            KorotVersion ver = new KorotVersion(subnode.Attributes["Text"].Value, Convert.ToInt32(subnode.Attributes["VersionNo"].Value), subnode.Attributes["ZipPath"].Value, subnode.Attributes["Flags"].Value,subnode.Attributes["Script"].Value);
                                            VersionManager.Versions.Add(ver);
                                        }
                                    }
                                }
                            }
                            workCount++;
                        }
                    }
                }
                else
                {
                    StringEventhHybrid htupdate = new StringEventhHybrid() { String = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate", Type = StringEventhHybrid.StringType.String, };
                    htupdate.Event += htupdateDownloaded;
                }
            }else
            {
                Console.WriteLine(" [HTUPDATE] Error: EventArgs is not suitable.");
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
            string[] langFiles = Directory.GetFiles(Settings.WorkFolder, "*.language");
            cbLang.Items.Clear();
            for (int i = 0; i < langFiles.Length; i++)
            {
                cbLang.Items.Add(Path.GetFileNameWithoutExtension(langFiles[i]));
            }
        }

        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.LoadLang(Settings.WorkFolder + cbLang.SelectedItem.ToString() + ".language");
            LoadLang();
        }
        private bool supportsLatestPreOut()
        {
            string korotPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot.exe";
            string korotBetaPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot Beta.exe";
            bool korotExists = File.Exists(korotPath);
            bool betaExists = File.Exists(korotBetaPath);
            if (!korotExists && !betaExists)
            {
                return false;
            }
            var vPreOut = VersionManager.GetVersionFromVersionNo(VersionManager.PreOutVerNumber);
            if (vPreOut.isOnlyx64 & !Environment.Is64BitProcess) { return false; }
            var currentVer = VersionManager.GetVersionFromVersionName(FileVersionInfo.GetVersionInfo(korotExists ? korotPath : korotBetaPath).ProductVersion);
            if (currentVer.VersionNo < VersionManager.LatestVersionNumber || currentVer.VersionNo < VersionManager.PreOutMinVer) { return false; }
            return true;
        }

        private bool supportsThisVer(KorotVersion ver)
        {
            string korotPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot.exe";
            string korotBetaPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\Korot Beta.exe";
            bool korotExists = File.Exists(korotPath);
            bool betaExists = File.Exists(korotBetaPath);
            if (!korotExists && !betaExists)
            {
                return false;
            }
            if (ver.isOnlyx64 && !(Environment.Is64BitProcess || Environment.Is64BitOperatingSystem)) { return false; }
            if (ver.RequiresNet452 && !PreResqs.SystemSupportsNet452) { return false; }
            if (ver.RequiresNet461 && !PreResqs.SystemSupportsNet461) { return false; }
            if (ver.RequiresNet48 && !PreResqs.SystemSupportsNet48) { return false; }
            if (ver.RequiresVisualC2015 && !PreResqs.SystemSupportsVisualC2015x86) { return false; }
            return true;
        }

        private void btChangeVer_Click(object sender, EventArgs e)
        {
            lbModifyDesc.Enabled = false;
            btRepair.Enabled = false;
            btUninstall.Enabled = false;
            btChangeVer.Enabled = false;
            pChangeVer.Visible = true;
            pChangeVer.Enabled = true;
            rbPreOut.Text = UIChangeVerO1.Replace("[PREOUT]", VersionManager.GetVersionFromVersionNo(VersionManager.PreOutVerNumber).VersionText);
            rbStable.Text = UIChangeVerO2.Replace("[LATEST]", VersionManager.GetVersionFromVersionNo(VersionManager.LatestVersionNumber).VersionText);
            Console.WriteLine(" [PreOut] N: " + VersionManager.PreOutVerNumber + " T: " + VersionManager.GetVersionFromVersionNo(VersionManager.PreOutVerNumber).VersionText);
            Console.WriteLine(" [Latest] N: " + VersionManager.LatestVersionNumber + " T: " + VersionManager.GetVersionFromVersionNo(VersionManager.LatestVersionNumber).VersionText);
            if(supportsLatestPreOut())
            {
                lbPerOutReq.Text = UIPreOutAvailable;
                rbPreOut.Enabled = true;
            }else
            {
                lbPerOutReq.Text = UIPreOutDisable;
                rbPreOut.Enabled = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            lbModifyDesc.Enabled = true;
            btRepair.Enabled = true;
            btUninstall.Enabled = true;
            btChangeVer.Enabled = true;
            pChangeVer.Visible = false;
            pChangeVer.Enabled = false;
        }

        private void rbPerOut_CheckedChanged(object sender, EventArgs e)
        {
            if(rbPreOut.Checked)
            {
                rbStable.Checked = false;
                rbOld.Checked = false;
                cbOld.Enabled = false;
                lbVersionToInstall.Enabled = false;
            }
        }

        private void rbStable_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStable.Checked)
            {
                rbPreOut.Checked = false;
                rbOld.Checked = false;
                cbOld.Enabled = false;
                lbVersionToInstall.Enabled = false;
            }
        }

        private void rbOld_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOld.Checked)
            {
                rbPreOut.Checked = false;
                rbPreOut.Checked = false;
                cbOld.Enabled = true;
                lbVersionToInstall.Enabled = true;
                cbOld.Items.Clear();
                for(int i = VersionManager.PreOutVersions.Count + 1;i < VersionManager.Versions.Count; i++)
                {
                    if (i != VersionManager.LatestVersionNumber && i != VersionManager.PreOutVerNumber)
                    {
                        var ver = VersionManager.Versions[i];
                        cbOld.Items.Add(ver.VersionText + " (" + ver.VersionNo + ")");
                    }
                }
                cbOld.SelectedIndex = 0;
            }
        }

        private void cbOld_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexOfO = cbOld.Text.IndexOf("(") + 1;
            int indexOfC = cbOld.Text.IndexOf(")");
            int getVerNoLenght = indexOfC - indexOfO;
            int verNo = Convert.ToInt32(cbOld.Text.Substring(indexOfO, getVerNoLenght));
            var ver = VersionManager.GetVersionFromVersionNo(verNo);
            btInstall1.Text = UIInstallVer.Replace("[VER]", ver.VersionText);
            if (ver.isMissing)
            {
                lbInstallError.Text = UIChangeVerMissing;
                btInstall1.Enabled = false;
                return;
            }
            if (!supportsThisVer(ver))
            {
                lbInstallError.Text = UIChangeVerArchNotSupported;
                btInstall1.Enabled = false;
                return;
            }
            lbInstallError.Text = "";
            btInstall1.Enabled = true;
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
    /// <summary>
    /// Stole-*couch* copied from StackOverflow Community
    /// https://stackoverflow.com/a/42733327
    /// TODO: Test this out
    /// </summary>
    public static class RestorePoint
    {
        /// <summary>
        ///     The type of event. For more information, see <see cref="CreateRestorePoint"/>.
        /// </summary>
        public enum EventType
        {
            /// <summary>
            ///     A system change has begun. A subsequent nested call does not create a new restore
            ///     point.
            ///     <para>
            ///         Subsequent calls must use <see cref="EventType.EndNestedSystemChange"/>, not
            ///         <see cref="EventType.EndSystemChange"/>.
            ///     </para>
            /// </summary>
            BeginNestedSystemChange = 0x66,

            /// <summary>
            ///     A system change has begun.
            /// </summary>
            BeginSystemChange = 0x64,

            /// <summary>
            ///     A system change has ended.
            /// </summary>
            EndNestedSystemChange = 0x67,

            /// <summary>
            ///     A system change has ended.
            /// </summary>
            EndSystemChange = 0x65
        }

        /// <summary>
        ///     The type of restore point. For more information, see <see cref="CreateRestorePoint"/>.
        /// </summary>
        public enum RestorePointType
        {
            /// <summary>
            ///     An application has been installed.
            /// </summary>
            ApplicationInstall = 0x0,

            /// <summary>
            ///     An application has been uninstalled.
            /// </summary>
            ApplicationUninstall = 0x1,

            /// <summary>
            ///     An application needs to delete the restore point it created. For example, an
            ///     application would use this flag when a user cancels an installation. 
            /// </summary>
            CancelledOperation = 0xd,

            /// <summary>
            ///     A device driver has been installed.
            /// </summary>
            DeviceDriverInstall = 0xa,

            /// <summary>
            ///     An application has had features added or removed.
            /// </summary>
            ModifySettings = 0xc
        }

        /// <summary>
        ///     Creates a restore point on the local system.
        /// </summary>
        /// <param name="description">
        ///     The description to be displayed so the user can easily identify a restore point.
        /// </param>
        /// <param name="eventType">
        ///     The type of event.
        /// </param>
        /// <param name="restorePointType">
        ///     The type of restore point. 
        /// </param>
        /// <exception cref="ManagementException">
        ///     Access denied.
        /// </exception>
        public static void CreateRestorePoint(string description, EventType eventType, RestorePointType restorePointType)
        {
            var mScope = new ManagementScope("\\\\localhost\\root\\default");
            var mPath = new ManagementPath("SystemRestore");
            var options = new ObjectGetOptions();
            using (var mClass = new ManagementClass(mScope, mPath, options))
            using (var parameters = mClass.GetMethodParameters("CreateRestorePoint"))
            {
                parameters["Description"] = description;
                parameters["EventType"] = (int)eventType;
                parameters["RestorePointType"] = (int)restorePointType;
                mClass.InvokeMethod("CreateRestorePoint", parameters, null);
            }
        }
    }
}