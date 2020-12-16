using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot_Win32
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            pTitleBar.MouseDoubleClick += new MouseEventHandler((sender, e) => OnMouseDoubleClick(e));
            pTitleBar.MouseDown += new MouseEventHandler((sender, e) => OnMouseDown(e));
            flpTitles.MouseDoubleClick += new MouseEventHandler((sender, e) => OnMouseDoubleClick(e));
            flpTitles.MouseDown += new MouseEventHandler((sender, e) => OnMouseDown(e));
        }
        private bool RightMostClosing = false;



        #region Animator


        /// <summary>
        /// Animation directions.
        /// </summary>
        private enum AnimateDirection
        {
            /// <summary>
            /// No animation.
            /// </summary>
            Nothing,
            /// <summary>
            /// Back to normal
            /// </summary>
            Left,
            /// <summary>
            /// Expand a little
            /// </summary>
            Right,
            /// <summary>
            /// Expand to current screen
            /// </summary>
            RightMost,
            /// <summary>
            /// Hide completely
            /// </summary>
            LeftFullScreen,
        }

        private void AnimateTo(AnimateDirection animate)
        {
            AnimationContinue = true;
            Direction = animate;
            timer1.Start();
        }

        private AnimateDirection Direction = AnimateDirection.Nothing;
        private AnimateDirection PrevDirection = AnimateDirection.Left;
        private int AnimationSpeed = 60;
        private int panelMinSize = 60;
        private int panelMaxSize = 420;
        private bool AnimationContinue = false;
        private bool isAnimating => Direction != AnimateDirection.Nothing;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (AnimationContinue)
            {
                case true:
                    if(pAppDrawer.Dock != DockStyle.Left)
                    {
                        pAppDrawer.Dock = DockStyle.Left;
                    }
                    switch (Direction)
                    {
                        case AnimateDirection.Left:
                            switch (PrevDirection)
                            {
                                case AnimateDirection.RightMost:
                                case AnimateDirection.Right:
                                    pAppDrawer.Width = pAppDrawer.Width > panelMinSize ? pAppDrawer.Width - AnimationSpeed : panelMinSize;
                                    AnimationContinue = pAppDrawer.Width > panelMinSize;
                                    break;
                                case AnimateDirection.LeftFullScreen:
                                    pAppDrawer.Width = pAppDrawer.Width < panelMinSize ? pAppDrawer.Width + AnimationSpeed : panelMinSize;
                                    AnimationContinue = pAppDrawer.Width < panelMinSize;
                                    break;
                                case AnimateDirection.Left:
                                    pAppDrawer.Width = panelMinSize;
                                    AnimationContinue = false;
                                    break;
                            }
                            break;
                        case AnimateDirection.LeftFullScreen:
                            pAppDrawer.Width = pAppDrawer.Width > 10 ? pAppDrawer.Width - AnimationSpeed : pAppDrawer.Width;
                            AnimationContinue = pAppDrawer.Width > 10;
                            break;
                        case AnimateDirection.Right:
                            switch(PrevDirection)
                            {
                                case AnimateDirection.LeftFullScreen:
                                case AnimateDirection.Left:
                                    pAppDrawer.Width = pAppDrawer.Width < panelMaxSize ? pAppDrawer.Width + AnimationSpeed : panelMaxSize;
                                    AnimationContinue = pAppDrawer.Width < panelMaxSize;
                                    break;
                                case AnimateDirection.RightMost:
                                    pAppDrawer.Width = pAppDrawer.Width > panelMaxSize ? pAppDrawer.Width - AnimationSpeed : panelMaxSize;
                                    AnimationContinue = pAppDrawer.Width > panelMaxSize;
                                    break;
                                case AnimateDirection.Right:
                                    pAppDrawer.Width = panelMaxSize;
                                    AnimationContinue = false;
                                    break;
                            }
                            break;
                        case AnimateDirection.RightMost:
                            pAppDrawer.Width = pAppDrawer.Width < (Width - 15) ? pAppDrawer.Width + AnimationSpeed : (Width - 15);
                            AnimationContinue = pAppDrawer.Width < (Width - 15);
                            break;
                    }
                    break;
                case false:
                    switch (Direction)
                    {
                        case AnimateDirection.Nothing:
                        default:
                        case AnimateDirection.Left:
                            pAppDrawer.Width = panelMinSize;
                            break;
                        case AnimateDirection.LeftFullScreen:
                            pAppDrawer.Width = 10;
                            break;
                        case AnimateDirection.Right:
                            pAppDrawer.Width = panelMaxSize;
                            break;
                        case AnimateDirection.RightMost:
                            pAppDrawer.Width = Width - 15;
                            break;
                    }
                    pAppDrawer.Anchor = Direction == AnimateDirection.RightMost
                        ? AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
                        : AnchorStyles.Top | AnchorStyles.Left;
                    pAppDrawer.Dock = Direction == AnimateDirection.RightMost ? DockStyle.Fill : DockStyle.Left;
                    PrevDirection = Direction;
                    Direction = AnimateDirection.Nothing;
                    timer1.Stop();
                    break;
            }
        }
        #endregion Animator

        private bool allowResize = false;
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pAppDrawer.Dock != DockStyle.Left)
            {
                pAppDrawer.Dock = DockStyle.Left;
            }
            
            if (e.Clicks > 2) 
            {
                switch (PrevDirection)
                {
                    default:
                    case AnimateDirection.Left:
                    case AnimateDirection.Nothing:
                    case AnimateDirection.LeftFullScreen:
                        RightMostClosing = false;
                        AnimateTo(AnimateDirection.Right);
                        break;
                    case AnimateDirection.Right:
                        AnimateTo(RightMostClosing ? AnimateDirection.Left : AnimateDirection.RightMost);
                        RightMostClosing = false;
                        break;
                    case AnimateDirection.RightMost:
                        RightMostClosing = true;
                        AnimateTo(AnimateDirection.Right);
                        break;
                }
            }
            else
            {
                allowResize = true;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (allowResize)
            {
                if (pAppDrawer.Width > panelMaxSize)
                {
                    AnimateTo(PrevDirection == AnimateDirection.RightMost ? AnimateDirection.Right : AnimateDirection.RightMost);
                }
                else if (pAppDrawer.Width < panelMaxSize)
                {
                    AnimateTo(PrevDirection == AnimateDirection.Right ? AnimateDirection.Left : AnimateDirection.Right);
                }
            }
            allowResize = false;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            int w = label1.Left + e.X;
            pAppDrawer.Width = allowResize ? (w > 0 ? (w <= (Width - 15) ? w : (Width - 15)) : panelMinSize) : pAppDrawer.Width;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            allowResize = false;
        }

        private void pTitleBar_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        #region TabDragDrop

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            Control target = new Control();

            target.Parent = sender as Control;

            if (target != null)
            {
                int targetIndex = FindCSTIndex(target.Parent);
                if (targetIndex != -1)
                {
                    string cst_ctrl = typeof(TabLabel).FullName;
                    if (e.Data.GetDataPresent(cst_ctrl))

                    {
                        Button source = new Button();
                        source.Parent = e.Data.GetData(cst_ctrl) as TabLabel;

                        if (targetIndex != -1)
                            this.flpTitles.Controls.SetChildIndex(source.Parent, targetIndex);
                    }
                }
            }
        }

        private int FindCSTIndex(Control cst_ctr)
        {
            for (int i = 0; i < this.flpTitles.Controls.Count; i++)
            {
                TabLabel target = this.flpTitles.Controls[i] as TabLabel;

                if (cst_ctr.Parent == target)
                    return i;
            }
            return -1;
        }

        private void OnCstMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control cst = sender as Control;
                cst.DoDragDrop(cst.Parent, DragDropEffects.Move);
            }
        }
        #endregion TabDragDrop
    }
}
