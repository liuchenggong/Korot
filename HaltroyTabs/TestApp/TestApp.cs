using EasyTabs;
using System.Drawing;

namespace TestApp
{
	public partial class TestApp : TitleBarTabs
    {
       public KorotTabRenderer tabRenderer;
        public TestApp()
        {
            InitializeComponent();

            AeroPeekEnabled = true;
            tabRenderer = new KorotTabRenderer(this,Color.Black,Color.White,Color.DodgerBlue);
            TabRenderer = tabRenderer;
            Icon = Resources.DefaultIcon;
        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                Content = new TabWindow(this)
                {
                    Text = "New Tab"
                }
            };
        }
    }
}
