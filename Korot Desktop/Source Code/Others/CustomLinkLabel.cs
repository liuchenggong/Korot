using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    class CustomLinkLabel : LinkLabel
    {
        [Bindable(false)]
        [DefaultValue(typeof(String), "")]
        [Category("Misc")]
        [Description("Address of link.")]
        public string Url { get; set; }
    }
}
