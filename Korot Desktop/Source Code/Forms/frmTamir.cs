using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmTamir : Form
    {
        public frmTamir()
        {
            InitializeComponent();
        }

        private void frmTamir_Load(object sender, EventArgs e)
        {
            FixDefaultLanguage();
        }
        async void FixDefaultLanguage()
        {
            await Task.Run(() => {
                if (!Directory.Exists(Application.StartupPath + "\\Lang\\")) Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
                FileSystem2.WriteFile(Application.StartupPath + "\\Lang\\English.lang", Properties.Resources.English);
            });
            Application.Restart();
        }
    }
}
