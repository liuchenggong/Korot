using System;
using System.Drawing;
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
            foreach (Control x in this.Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            lbErrorCode.Text = Error.Message;
            textBox1.Text = Error.ToString();
        }
    }
}
