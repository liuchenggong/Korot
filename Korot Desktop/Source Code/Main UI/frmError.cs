/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmError : Form
    {
        public Settings Settings;

        public frmError(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void frmError_Load(object sender, EventArgs e)
        {
        }

        bool loadedError = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!loadedError)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(SafeFileSettingOrganizedClass.ErrorMenu);
                foreach(XmlNode node in doc.FirstChild.ChildNodes)
                {
                    if (node.Name == "Translations")
                    {
                        foreach (XmlNode subnode in node.ChildNodes)
                        {
                            if (subnode.Name == "Restart")
                            {
                                btRestart.Text = subnode.InnerXml;
                            }
                            else if (subnode.Name == "Message1")
                            {
                                label1.Text = subnode.InnerXml;

                            }
                            else if (subnode.Name == "Message2")
                            {
                                label2.Text = subnode.InnerXml.Replace("[NEWLINE]", Environment.NewLine);
                            }else if (subnode.Name == "Technical")
                            {
                                label3.Text = subnode.InnerXml;
                            }
                        }
                    }
                    else if (node.Name == "Error")
                    {
                        lbErrorCode.Text = node.Attributes["Message"].Value;
                        textBox1.Text = node.InnerXml;
                    }
                }
            }
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            Color BackColor2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
            btRestart.BackColor = BackColor2;
            textBox1.BackColor = BackColor2;
            lbErrorCode.BackColor = BackColor2;
            textBox1.ForeColor = ForeColor;
            lbErrorCode.ForeColor = ForeColor;
        }

        private void btRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}