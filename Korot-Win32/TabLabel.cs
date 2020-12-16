using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot_Win32
{
    class TabLabel : Control
    {
        public Icon Icon
        {
            get
            {
                if (AssocForm == null)
                {
                    return null;
                }else
                {
                    if (AssocForm.IsDisposed || AssocForm.Disposing)
                    {
                        return null;
                    }else
                    {
                        return AssocForm.Icon;
                    }
                }
            }set
            {
                if (AssocForm != null && !AssocForm.Disposing && !AssocForm.IsDisposed)
                {
                    AssocForm.Icon = value;
                }
            }
        }

        public override string Text
        {
            get
            {
                if (AssocForm == null)
                {
                    return "DENEME";
                }
                else
                {
                    if (AssocForm.IsDisposed || AssocForm.Disposing)
                    {
                        return "DENEME";
                    }
                    else
                    {
                        return AssocForm.Text;
                    }
                }
            }
            set
            {
                if (AssocForm != null && !AssocForm.Disposing && !AssocForm.IsDisposed)
                {
                    AssocForm.Text = value;
                }
            }
        }

        public TabPage AssocTabPage { get; set; } = null;
        public Form AssocForm { get; set; } = null;
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), Bounds);
        }
    }
}
