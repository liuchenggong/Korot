using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmTamirLang : Form
    {
        string[] argus;
        public frmTamirLang(string[] argmnt)
        {
            InitializeComponent();
            argus = argmnt;
        }
        void WriteToConsole(string text)
        {
            lbConsole.Invoke(new Action(() => lbConsole.Text += text + Environment.NewLine));
        }

        private void frmTamir_Load(object sender, EventArgs e)
        {
            WriteToConsole("Starting Self-Repair...");
            FixDefaultLanguage();
            WriteToConsole("Self-Repair done.");
            string args = argus.ToString().Replace(Application.ExecutablePath,"");
            Process.Start(Application.ExecutablePath, args);
            Application.Exit();
        }
        async void FixDefaultLanguage()
        {
            WriteToConsole("Starting Language Repair...");
            await Task.Run(() =>
            {
                WriteToConsole("Checking Default Language...");
                if (!Directory.Exists(Application.StartupPath + "\\Lang\\"))
                {
                    WriteToConsole("Language folder didn't exist. Creating...");
                    Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
                    WriteToConsole("Created Language folder.");
                }
                WriteToConsole("Creating Default Language...");
                FileSystem2.WriteFile(Application.StartupPath + "\\Lang\\English.lang", Properties.Resources.English);
                WriteToConsole("Created.");
            });
            WriteToConsole("End of Language Repair.");
        }
    }
}
