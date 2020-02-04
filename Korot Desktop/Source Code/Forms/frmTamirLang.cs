using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmTamirLang : Form
    {
        public frmTamirLang()
        {
            InitializeComponent();
        }

        private void frmTamir_Load(object sender, EventArgs e)
        {
            FixDefaultLanguage();
        }
        async void FixDefaultLanguage()
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(Application.StartupPath + "\\Lang\\")) Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
                FileSystem2.WriteFile(Application.StartupPath + "\\Lang\\English.lang", Properties.Resources.English);
            });
        }
    }
}
