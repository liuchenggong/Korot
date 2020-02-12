using System;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmError : Form
    {
        Exception Error;
        public frmError(Exception error)
        {
            Error = error;
            InitializeComponent();
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            lbErrorCode.Text = Error.Message;
            textBox1.Text = Error.ToString();
        }
    }
}
