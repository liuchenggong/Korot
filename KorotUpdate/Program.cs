using System;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace KorotInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Contains("--skip-folder-check"))
            {
                if (Application.StartupPath != (Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\"))
                {
                    Console.WriteLine(" [Startup] Moving to Program Files folder...");
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\");
                    }
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\KorotInstaller.exe"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\KorotInstaller.exe");
                    }
                    File.Move(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\KorotInstaller.exe");
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Settings settings = new Settings();
            Application.Run(new frmMain(settings));
            settings.Save();
        }

        static void oldMain(string[] args)
        {
            string title = Console.Title;
            Console.Title = "Korot Updater";
            string backupFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\UpdateBackup.hup";
            string newVer = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\UpdateTemp\\";
            Console.WriteLine("Korot Update Utility");
            Console.WriteLine("--------------------");
            string appPath = Directory.GetCurrentDirectory();
            Console.Title = "Korot Updater - 0%";

            Console.WriteLine("Creating backup from: " + appPath);
            try
            {
                if (File.Exists(backupFile))
                {
                    try
                    {
                        File.Delete(backupFile);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while deleting: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 25%";

                    Console.WriteLine("File already exists. Deleted.");
                }
                ZipFile.CreateFromDirectory(appPath, backupFile);
                Console.WriteLine("Successfully created backup in " + backupFile + " .");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating backup: " + ex.ToString());
                Console.WriteLine("Press S to skip job. Press Enter to close.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
            }
            if (args.Length > 0)
            {
                string upgradeFile = args[0];
                Console.Title = "Korot Updater - 50%";
                Console.WriteLine("Upgrading from " + upgradeFile + " ...");
                Console.WriteLine("Unzipping to " + newVer + " ...");
                try
                {
                    if (Directory.Exists(newVer))
                    {
                        try
                        {
                            Directory.Delete(newVer, true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error while deleting: " + ex.ToString());
                            Console.WriteLine("Press S to skip job. Press Enter to close.");
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                        }
                        Console.WriteLine("Directory already exists. Deleted.");
                    }
                    try
                    {
                        ZipFile.ExtractToDirectory(upgradeFile, newVer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while unzipping: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 75%";
                    Console.WriteLine("Successfully unzipped. Copying files...");
                    try
                    {
                        Copy(newVer, appPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while copying: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 100%";
                    Console.WriteLine("Successfully copied. Closing...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while unzipping: " + ex.ToString());
                    Console.WriteLine("Press S to skip job. Press Enter to close.");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                }
            }
            else
            {
                Console.Title = "Korot Updater - 100%";
                Console.WriteLine("No extra arguments found. Closing...");
            }
            Console.Title = title;
            return;
        }
        internal static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        internal static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                string fName = Path.Combine(target.FullName, fi.Name);
                if (File.Exists(fName)) { File.Delete(fName); Console.WriteLine("File exists. Deleted: "+ fName); }
                fi.CopyTo(fName, true);
                Console.WriteLine("File copied: " + fi.Name);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
    /// <summary>
    /// HTAlt.Standart.Tools Jr.
    /// </summary>
    public static class HTAltTools
    {
        /// <summary>
        /// Creates and writes a file without locking it.
        /// </summary>
        /// <param name="fileLocation">Location of the file.</param>
        /// <param name="input">Text to write on.</param>
        /// <param name="encode">Rules to follow while writing.</param>
        /// <returns><c>true</c> if successfully writes to file, otherwise throws an exception.</returns>
        public static bool WriteFile(string fileLocation, string input, Encoding encode)
        {
            if (!Directory.Exists(new FileInfo(fileLocation).DirectoryName)) { Directory.CreateDirectory(new FileInfo(fileLocation).DirectoryName); }
            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }
            File.Create(fileLocation).Dispose();
            FileStream writer = new FileStream(fileLocation, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            writer.Write(encode.GetBytes(input), 0, encode.GetBytes(input).Length);
            writer.Close();
            return true;
        }

        /// <summary>
        /// Reads a file without locking it.
        /// </summary>
        /// <param name="fileLocation">Location of the file.</param>
        /// <param name="encode">Rules for reading the file.</param>
        /// <returns>Text inside the file.</returns>
        public static string ReadFile(string fileLocation, Encoding encode)
        {
            FileStream fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, encode);
            string result = sr.ReadToEnd();
            sr.Close();
            return result;
        }
    }

    public class Settings
    {
        public static string FileLocation => WorkFolder + "installer.settings";
        public static string WorkFolder => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Korot\\Installer\\";

        public Settings()
        {
            if (File.Exists(FileLocation))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(HTAltTools.ReadFile(FileLocation, Encoding.Unicode));
                    XmlNode firstNode = doc.FirstChild.Name == "InstallerSettings" ? doc.FirstChild : doc.FirstChild.NextSibling;
                    int loadedSettings = 0;
                    foreach (XmlNode node in firstNode.ChildNodes)
                    {
                        if (node.Name == "LangFile")
                        {
                            loadedSettings++;
                            LanguageFile = string.IsNullOrWhiteSpace(node.InnerXml) ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Korot\\Installer\\English.language" : node.InnerXml.Replace("[APPDATA]", WorkFolder);
                        }
                        else if (node.Name == "InstalledVersion")
                        {
                            loadedSettings++;
                            InstalledVersion = string.IsNullOrWhiteSpace(node.InnerXml) ? "" : node.InnerXml;
                        }
                        else if (node.Name == "ExePath")
                        {
                            loadedSettings++;
                            KorotExePath = string.IsNullOrWhiteSpace(node.InnerXml) ? InstallPath + "Korot.exe" : node.InnerXml.Replace("[INSTALL]", InstallPath);
                        }
                        else if (node.Name == "DarkMode")
                        {
                            loadedSettings++;
                            isDarkMode = string.IsNullOrWhiteSpace(node.InnerXml) ? false : (node.InnerXml == "1");
                        }
                    }
                    LoadedDefaults = false;
                    Console.WriteLine(" [Settings] Successfully loaded " + loadedSettings + " setting(s).");
                }catch (Exception ex)
                {
                    Console.WriteLine(" [Settings] Loaded Defaults: Error: " + ex.ToString());
                    isDarkMode = false;
                    KorotExePath = InstallPath + "Korot.exe";
                    LanguageFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Korot\\Installer\\English.language";
                    LoadedDefaults = true;
                }
            }else
            {
                Console.WriteLine(" [Settings] Loaded Defaults: File not found (\"" + FileLocation + "\").");
                isDarkMode = false;
                KorotExePath = InstallPath + "Korot.exe";
                LanguageFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Korot\\Installer\\English.language";
                LoadedDefaults = true;
            }
        }

        public string XmlOut()
        {
            return "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + Environment.NewLine
                + "<InstallerSettings>" + Environment.NewLine
                + "  <LangFile>" + (string.IsNullOrWhiteSpace(LanguageFile) ? "" : LanguageFile.Replace(WorkFolder, "[APPDATA]")) + "</LangFile>" + Environment.NewLine
                + "  <InstalledVersion>" + (string.IsNullOrWhiteSpace(InstalledVersion) ? "" : InstalledVersion) + "</InstalledVersion>" + Environment.NewLine
                + "  <ExePath>" + (string.IsNullOrWhiteSpace(KorotExePath) ? "" : KorotExePath.Replace(InstallPath,"[INSTALL]")) + "</ExePath>" + Environment.NewLine
                + "  <DarkMode>" + (isDarkMode ? "1" : "0") + "</DarkMode>" + Environment.NewLine
                + "</InstallerSettings>";
        }

        public void LoadLang(string LangFile)
        {
            if (string.IsNullOrWhiteSpace(LangFile)) { LangFile = LanguageFile; }
            if (!File.Exists(LangFile)) { LangFile = LanguageFile; }
            Translations.Clear();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(HTAltTools.ReadFile(LangFile, Encoding.Unicode));
            XmlNode firstNode = doc.FirstChild.Name != "Language" ? doc.FirstChild.NextSibling : doc.FirstChild;
            for(int i = 0; i < firstNode.ChildNodes.Count;i++)
            {
                XmlNode node = firstNode.ChildNodes[i];
                if (node.Name == "Translate")
                {
                    if (node.Attributes["ID"] != null && node.Attributes["Text"] != null)
                    {
                        Translations.Add(new Translation(node.Attributes["ID"].Value, node.Attributes["Text"].Value));
                    }
                }
            }
        }

        public Color BackColor
        {
            get => isDarkMode ? Color.FromArgb(255, 0, 0, 0) : Color.FromArgb(255, 255, 255, 255);
        }

        public Color BackColor1
        {
            get => isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 235, 235, 235);
        }

        public Color BackColor2
        {
            get => isDarkMode ? Color.FromArgb(255, 40, 40, 40) : Color.FromArgb(255, 215, 215, 215);
        }

        public Color BackColor3
        {
            get => isDarkMode ? Color.FromArgb(255, 60, 60, 60) : Color.FromArgb(255, 195, 195, 195);
        }

        public Color MidColor
        {
            get => Color.FromArgb(255, 128, 128, 128);
        }

        public Color ForeColor
        {
            get => isDarkMode ? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(255, 0, 0, 0);
        }

        public void Save()
        {
            HTAltTools.WriteFile(FileLocation, XmlOut(), Encoding.Unicode);
        }

        public string InstalledVersion { get; set; } 

        public string LanguageFile { get; set; } 
        public static string InstallPath => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Haltroy\\Korot\\";
        public string KorotExePath { get; set; }
        public string GetKorotVersion()
        {
            if (InstalledVersion is null) 
            {
                if (!string.IsNullOrWhiteSpace(KorotExePath))
                {
                    if (File.Exists(KorotExePath))
                    {
                        return FileVersionInfo.GetVersionInfo(KorotExePath).ProductVersion;
                    }
                    return null;
                }
                return null;
            }
            return InstalledVersion;
        }
        public bool isDarkMode { get; set; }
        public bool LoadedDefaults = true;
        public List<Translation> Translations { get; set; } = new List<Translation>();
        public string GetItemText(string ID)
        {
            List<Translation> foundItems = new List<Translation>();
            for(int i = 0; i< Translations.Count;i++)
            {
                if (Translations[i].ID == ID) { foundItems.Add(Translations[i].CarbonCopy()); }
            }
            if (foundItems.Count > 0) { return foundItems[new Random().Next(0, foundItems.Count - 1)].Text; }else { return "[MI]" + ID; }
        }
    }

    public class Translation
    {
        public Translation(string id, string text)
        {
            ID = id;
            Text = text;
        }
        public Translation CarbonCopy()
        {
            return new Translation(ID, Text);
        }
        public void CarbonCopyTo(Translation copyTo)
        {
            copyTo.ID = ID;
            copyTo.Text = Text;
        }

        public override bool Equals(object obj)
        {
            return obj is Translation translation &&
                   ID == translation.ID;
        }

        public override int GetHashCode()
        {
            int hashCode = -2039955522;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            return hashCode;
        }

        public string ID { get; set; }
        public string Text { get; set; }
    }

    public class VersionManager
    {
        public string LatesVersion { get; set; }
        public int LatestVersionNumber { get; set; }
        public int LatestUpdateMinVer { get; set; }
        public int InstallVer { get; set; }
        public List<KorotVersion> Versions { get; set; } = new List<KorotVersion>();

        public List<KorotVersion> PreOutVersions { get => Versions.FindAll(i => i.isPreOut); }
        public enum UpdateType
        {
            /// <summary>
            /// Application is up to date.
            /// </summary>
            UpToDate,
            /// <summary>
            /// Application requires minimal update.
            /// </summary>
            Upgrade,
            /// <summary>
            /// Application requires complete reinstallation
            /// </summary>
            FullUpgrade,
            /// <summary>
            /// Application requşres update in PreOut mode.
            /// </summary>
            PreOutUpdate,
            /// <summary>
            /// A full release is available even for latest PreOut version.
            /// </summary>
            PreOutFull,
        }

       
    }
    public class KorotVersion
    {
        public KorotVersion(string _text, int _no, string _zipPath, string _flags)
        {
            if (!string.IsNullOrWhiteSpace(_flags))
            {
                Flags = _flags.Split(';');
            }
            VersionText = _text;
            VersionNo = _no;
            ZipPath = _zipPath;
        }
        public KorotVersion(string _text, int _no)
        {
            VersionText = _text;
            VersionNo = _no;
        }
        public KorotVersion(string _text, int _no, string _flags)
        {
            if (!string.IsNullOrWhiteSpace(_flags))
            {
                Flags = _flags.Split(';');
            }
            VersionText = _text;
            VersionNo = _no;
        }
        public bool isOnlyx86 => Flags.Contains("onlyx86");
        public bool isOnlyx64 => Flags.Contains("onlyx64");
        public bool isPreOut => Flags.Contains("preout");
        public bool isMissing => Flags.Contains("missing");
        /// <summary>
        /// .Net Framework 4.8
        /// </summary>
        public bool RequiresNet48 => Flags.Contains("reqnet48");
        /// <summary>
        /// .Net Framework 4.5.2
        /// </summary>
        public bool RequiresNet452 => Flags.Contains("reqnet452");
        /// <summary>
        /// Visual C++ 2015
        /// </summary>
        public bool RequiresVisualC2015 => Flags.Contains("reqvc2015");
        public string[] Flags { get; set; } = new string[] { };
        public string VersionText { get; set; } = "";
        public int VersionNo { get; set; } = 0;
        public string ZipPath { get; set; } = "";
    }
}
