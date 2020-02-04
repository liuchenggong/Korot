using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
