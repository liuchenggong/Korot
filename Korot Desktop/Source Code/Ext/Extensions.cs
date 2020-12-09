/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace Korot
{
    public class Extensions
    {
        public Settings Settings { get; set; }
        public string ProfileName = "";

        public Extensions(string ExtList)
        {
            ExtensionList = new List<Extension>();
            ExtensionCodeNames = new List<string>();
            if (string.IsNullOrWhiteSpace(ExtList) || ExtList.ToLowerInvariant().Replace(Environment.NewLine, "") == "<extensions></extensions>")
            {
                return;
            }
            XmlDocument document = new XmlDocument();
            document.Load(ExtList);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLowerInvariant() == "extension")
                {
                    string codeName = node.InnerText.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    ExtensionCodeNames.Add(codeName);
                }
            }
            LoadExtensions();
        }

        public string ExtractList
        {
            get
            {
                string extList = "   <Extensions>" + Environment.NewLine;
                foreach (string x in ExtensionCodeNames)
                {
                    extList += "     <Extension>" + x + "</Extension>" + Environment.NewLine;
                }
                extList += "   </Extensions>" + Environment.NewLine;
                return extList;
            }
        }

        public string ExtensionDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + ProfileName + "\\Extensions";

        public List<Extension> ExtensionList { get; set; }
        public List<string> ExtensionCodeNames { get; set; }

        public void LoadExtensions()
        {
            ExtensionList.Clear();
            foreach (string x in ExtensionCodeNames)
            {
                ExtensionList.Add(new Extension(ExtensionDirectory + "\\" + x + "\\ext.kem") { LocalSettings = Settings });
            }
        }

        public void UpdateExtensions()
        {
            foreach (Extension x in ExtensionList)
            {
                x.Update();
            }
        }

        public Extension GetExtensionByCodeName(string CodeName)
        {
            List<Extension> foundext = ExtensionList.FindAll(i => i.CodeName == CodeName);
            if (foundext == null) { return null; }
            else
            {
                if (foundext.Count == 0)
                {
                    return null;
                }
                else
                {
                    return foundext[0];
                }
            }
        }

        public bool Exists(string CodeName)
        {
            List<Extension> foundext = ExtensionList.FindAll(i => i.CodeName == CodeName);
            if (foundext == null) { return false; }
            else
            {
                if (foundext.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void InstallExtension(string ExtFile, bool Silent = false)
        {
            frmInstallExt installExt = new frmInstallExt(Settings, ExtFile, Silent);
            installExt.Show();
        }
    }

    public class Extension
    {
        public string HTUpdate { get; set; }
        public Settings LocalSettings { get; set; }

        public Extension(string ManifestFileLocation)
        {
            ManifestFile = ManifestFileLocation;
            // Read the file
            string ManifestXML = HTAlt.Tools.ReadFile(ManifestFileLocation, Encoding.Unicode);
            string ExtFolder = new FileInfo(ManifestFileLocation).DirectoryName + "\\";
            Folder = ExtFolder;
            // Write XML to Stream so we don't need to load the same file again.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(ManifestXML); //Writes our XML file
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream); //Loads our XML Stream
            // Make sure that this is an extension manifest.
            // This is the part where my brain stopped and tried shutting down (aka sleep).
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                switch (node.Name.ToLowerInvariant())
                {
                    case "name":
                        Name = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "version":
                        Version = Convert.ToInt32(node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"));
                        break;
                    case "author":
                        Author = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "icon":
                        Icon = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "htupdate":
                        HTUpdate = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "startupfile":
                        Startup = node.InnerText.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "popupfile":
                        Popup = node.InnerText.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "backfile":
                        Background = node.InnerText.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "proxyfile":
                        Proxy = node.InnerText.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "menusize":
                        {
                            string w = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                            string h = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                            Size = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
                            break;
                        }

                    case "files":
                        {
                            Files = new List<string>();
                            foreach (XmlNode subnode in node.ChildNodes)
                            {
                                if (subnode.Name.ToLowerInvariant() == "file")
                                {
                                    if (subnode.Attributes["Location"] != null)
                                    {
                                        string loc = subnode.Attributes["Location"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                                        Files.Add(loc);
                                    }
                                }
                            }

                            break;
                        }

                    case "rightclick":
                        {
                            foreach (XmlNode subnode in node.ChildNodes)
                            {
                                switch (subnode.Name.ToLowerInvariant())
                                {
                                    case "none":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.None,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }

                                    case "link":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.Link,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }

                                    case "always":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.Always,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }

                                    case "edit":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.Edit,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }

                                    case "image":
                                    case "ımage":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.Image,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }

                                    case "text":
                                        {
                                            if (subnode.Attributes["Icon"] != null && subnode.Attributes["Text"] != null && subnode.Attributes["Script"] != null)
                                            {
                                                RightClickOption option = new RightClickOption()
                                                {
                                                    Icon = subnode.Attributes["Icon"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Text = subnode.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Script = subnode.Attributes["Script"].Value.Replace("[EXTFOLDER]", ExtFolder).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                                                    Option = RightClickOptionStyle.Text,
                                                };
                                                RightClickOptions.Add(option);
                                            }

                                            break;
                                        }
                                }
                            }

                            break;
                        }

                    case "settings":
                        {
                            Settings = new ExtensionSettings();
                            foreach (XmlNode subnode in node.ChildNodes)
                            {
                                switch (subnode.Name)
                                {
                                    case "autoLoad":
                                        Settings.autoLoad = subnode.InnerText == "true";
                                        break;
                                    case "onlineFiles":
                                        Settings.onlineFiles = subnode.InnerText == "true";
                                        break;
                                    case "showPopupMenu":
                                        Settings.showPopupMenu = subnode.InnerText == "true";
                                        break;
                                    case "activateScript":
                                        Settings.activateScript = subnode.InnerText == "true";
                                        break;
                                    case "hasProxy":
                                        Settings.hasProxy = subnode.InnerText == "true";
                                        break;
                                    case "useHaltroyUpdater":
                                        Settings.useHaltroyUpdater = subnode.InnerText == "true";
                                        break;
                                }
                            }

                            break;
                        }
                }
            }
        }

        public string Folder { get; set; }

        public bool FileExists(string filePath)
        {
            string fileLoc = Folder + filePath;
            string fileShortLoc = "[EXTFOLDER]" + filePath;
            if (Files.Contains(fileShortLoc))
            {
                if (File.Exists(fileLoc)) { return true; } else { return false; }
            }
            else { return false; }
        }

        public List<RightClickOption> RightClickOptions { get => _RCOptions; set => _RCOptions = value; }
        private List<RightClickOption> _RCOptions = new List<RightClickOption>();
        public string ManifestFile { get; set; }
        public string CodeName => Author + "." + Name;
        public string Name { get; set; }
        public string Author { get; set; }
        public int Version { get; set; }
        public string Icon { get; set; }
        public Size Size { get; set; }
        public string Popup { get; set; }
        public string Startup { get; set; }
        public string Background { get; set; }
        public string Proxy { get; set; }
        public List<string> Files { get; set; }
        public ExtensionSettings Settings { get; set; }

        public void Update()
        {
            if (Settings.useHaltroyUpdater)
            {
                frmUpdateExt frmUpdate = new frmUpdateExt(this, LocalSettings);
                frmUpdate.Show();
            }
        }
    }

    public class RightClickOption
    {
        public string Text { get; set; }
        public string Script { get; set; }
        public string Icon { get; set; }
        public RightClickOptionStyle Option { get; set; }
    }

    public enum RightClickOptionStyle
    {
        None,
        Link,
        Image,
        Text,
        Edit,
        Always
    }

    public class ExtensionSettings
    {
        private bool _autoLoad = false;
        private bool _onlineFiles = false;
        private bool _showPopupMenu = false;
        private bool _activateScript = false;
        private bool _hasProxy = false;
        private bool _useHaltroyUpdater = false;

        public bool autoLoad
        {
            get => _autoLoad;
            set => _autoLoad = value;
        }

        public bool onlineFiles
        {
            get => _onlineFiles;
            set => _onlineFiles = value;
        }

        public bool showPopupMenu
        {
            get => _showPopupMenu;
            set => _showPopupMenu = value;
        }

        public bool activateScript
        {
            get => _activateScript;
            set => _activateScript = value;
        }

        public bool hasProxy
        {
            get => _hasProxy;
            set => _hasProxy = value;
        }

        public bool useHaltroyUpdater
        {
            get => _useHaltroyUpdater;
            set => _useHaltroyUpdater = value;
        }
    }
    public class HTUpdateShim
    {
        public HTUpdateShim(string XMLText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLText);
            for (int i = 0; i < doc.FirstChild.ChildNodes.Count; i++)
            {
                XmlNode node = doc.FirstChild.ChildNodes[i];
                switch (node.Name.ToLowerInvariant())
                {
                    case "name":
                        Name = node.InnerXml.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
                        break;
                    case "latestversion":
                        LatestVersion = Convert.ToInt32(node.InnerXml);
                        break;
                    case "minkorotver":
                        MinKorotVer = Convert.ToInt32(node.InnerXml);
                        break;
                    case "file":
                        File = node.InnerXml.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
                        break;
                }
            }
        }
        public HTUpdateShim(XmlNode MainNode)
        {
            for(int i = 0;i < MainNode.ChildNodes.Count;i++)
            {
                XmlNode node = MainNode.ChildNodes[i];
                switch(node.Name.ToLowerInvariant())
                {
                    case "name":
                        Name = node.InnerXml.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
                        break;
                    case "latestversion":
                        LatestVersion = Convert.ToInt32(node.InnerXml);
                        break;
                    case "minkorotver":
                        MinKorotVer = Convert.ToInt32(node.InnerXml);
                        break;
                    case "file":
                        File = node.InnerXml.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
                        break;
                }
            }
        }
        public string Name { get; set; }
        public int LatestVersion { get; set; }
        public int MinKorotVer { get; set; }
        public string File { get; set; }
        public bool CanInstall => MinKorotVer <= VersionInfo.VersionNumber;
    }
}