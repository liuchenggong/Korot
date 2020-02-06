using System;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmError : Form
    {
        string Error = "";
        public frmError(string error)
        {
            Error = error;
            InitializeComponent();
        }

        private void frmError_Load(object sender, EventArgs e)
        {

        }
    }
}
