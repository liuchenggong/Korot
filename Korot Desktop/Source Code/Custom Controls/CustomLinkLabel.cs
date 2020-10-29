/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using System.ComponentModel;
using System.Windows.Forms;

namespace Korot
{
    internal class CustomLinkLabel : LinkLabel
    {
        [Bindable(false)]
        [DefaultValue(typeof(string), "")]
        [Category("Misc")]
        [Description("Address of link.")]
        public string Url { get; set; }
    }
}