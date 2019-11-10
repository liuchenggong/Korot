using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using EasyTabs;

namespace TestApp
{
    public partial class TabWindow : Form
    {
        TestApp _TestApp;
		//change colors with this code:
		// _TestApp.Invoke(new Action(() => _TestApp.tabRenderer.ChangeColors(BackColor,ForeColor,OverlayColor)));
        protected TitleBarTabs ParentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }
        }

        public TabWindow(TestApp mainform)
        {
            _TestApp = mainform;
            InitializeComponent();
        }

        private void TabWindow_Load(object sender, EventArgs e)
        {
        }
    }
}
