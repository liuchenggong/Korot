//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using Korot.Properties;
using System.Drawing;

namespace Korot
{
    /// <summary>Renderer that produces tabs that mimic the appearance of the Korot browser.</summary>
    public class KorotTabRenderer : BaseTabRenderer
    {
        /// <summary>
        /// A Korot-specific right-side tab image that allows the separation between inactive tabs to be more clearly defined.
        /// </summary>
        /// 
        protected Color _BackColor;
        protected Color _ForeColor;
        protected Color _OverlayColor;
        protected Image _inactiveRightSideShadowImage = Resources.KorotRight;

        /// <summary>Constructor that initializes the various resources that we use in rendering.</summary>
        /// <param name="parentWindow">Parent window that this renderer belongs to.</param>
        public KorotTabRenderer(TitleBarTabs parentWindow, Color BackColor, Color ForeColor, Color OverlayColor, Image _back, bool drawback = false)
            : base(parentWindow)
        {
            _BackColor = BackColor;
            _ForeColor = ForeColor;
            _OverlayColor = OverlayColor;
            BackgroundColor = BackColor;
            ForegroundColor = ForeColor;
            OverlayLayerColor = OverlayColor;
            drawBackgroundColor = drawback;
            switch (Properties.Settings.Default.newTabColor)
            {
                case 0:
                    AddButtonColor = BackColor;
                    break;
                case 1:
                    AddButtonColor = ForeColor;
                    break;
                case 2:
                    AddButtonColor = OverlayColor;
                    break;
            }
            switch (Properties.Settings.Default.closeColor)
            {
                case 0:
                    CloseButtonColor = BackColor;
                    break;
                case 1:
                    CloseButtonColor = ForeColor;
                    break;
                case 2:
                    CloseButtonColor = OverlayColor;
                    break;
            }
            _inactiveRightSideShadowImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
            // Initialize the various images to use during rendering
            _activeLeftSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));
            _activeRightSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));
            _activeCenterImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));
            if (Properties.Settings.Default.closeColor == 0)
            {
                _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, BackColor);
                _closeButtonHoverImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
            }
            else if (Properties.Settings.Default.closeColor == 1)
            {
                _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor);
                _closeButtonHoverImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255)));
            }
            else if (Properties.Settings.Default.closeColor == 3)
            {
                _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, OverlayColor);
                _closeButtonHoverImage = Tools.isBright(OverlayColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(OverlayColor.R, 20), Tools.GerekiyorsaAzalt(OverlayColor.G, 20), Tools.GerekiyorsaAzalt(OverlayColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(OverlayColor.R, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.G, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.B, 20, 255)));
            }
            else
            {
                Properties.Settings.Default.closeColor = 1;
                _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor);
                _closeButtonHoverImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255)));
            }

            _background = _back;

            if (Properties.Settings.Default.newTabColor == 0)
            {
                _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, BackColor));
                _addButtonHoverImage = Tools.isBright(BackColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255))));
            }
            else if (Properties.Settings.Default.newTabColor == 1)
            {
                _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, ForeColor));
                _addButtonHoverImage = Tools.isBright(ForeColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255))));
            }
            else if (Properties.Settings.Default.newTabColor == 2)
            {
                _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, OverlayColor));
                _addButtonHoverImage = Tools.isBright(OverlayColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(OverlayColor.R, 20), Tools.GerekiyorsaAzalt(OverlayColor.G, 20), Tools.GerekiyorsaAzalt(OverlayColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(OverlayColor.R, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.G, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.B, 20, 255))));
            }
            else
            {
                Properties.Settings.Default.newTabColor = 1;
                _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, ForeColor));
                _addButtonHoverImage = Tools.isBright(ForeColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255))));
            }

            _inactiveLeftSideImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255)));
            _inactiveRightSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
            _inactiveCenterImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
            // Set the various positioning properties
            CloseButtonMarginTop = 6;
            CloseButtonMarginLeft = 2;
            AddButtonMarginTop = 7;
            AddButtonMarginLeft = 5;
            CaptionMarginTop = 6;
            IconMarginTop = 7;
            IconMarginRight = 5;
            AddButtonMarginRight = 11;
        }
        public void ChangeColors(Color BackColor, Color ForeColor, Color OverlayColor)
        {
            switch (Properties.Settings.Default.newTabColor)
            {
                case 0:
                    AddButtonColor = BackColor;
                    break;
                case 1:
                    AddButtonColor = ForeColor;
                    break;
                case 2:
                    AddButtonColor = OverlayColor;
                    break;
            }
            switch (Properties.Settings.Default.closeColor)
            {
                case 0:
                    CloseButtonColor = BackColor;
                    break;
                case 1:
                    CloseButtonColor = ForeColor;
                    break;
                case 2:
                    CloseButtonColor = OverlayColor;
                    break;
            }
            if (BackColor != _BackColor)
            {
                _BackColor = BackColor;
                BackgroundColor = BackColor;
                _inactiveLeftSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                _inactiveRightSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                _inactiveCenterImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                _inactiveRightSideShadowImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                _activeLeftSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));
                _activeRightSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));
                _activeCenterImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 30), Tools.GerekiyorsaAzalt(BackColor.G, 30), Tools.GerekiyorsaAzalt(BackColor.B, 30))) : Tools.ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 30, 255), Tools.GerekiyorsaArttır(BackColor.G, 30, 255), Tools.GerekiyorsaArttır(BackColor.B, 30, 255)));

                //_background = Tools.ColorReplace(Resources.KorotBackground, 50, Color.White, BackColor);
                if (Properties.Settings.Default.closeColor == 0)
                {
                    _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, BackColor);
                    _closeButtonHoverImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                }
                if (Properties.Settings.Default.newTabColor == 0)
                {
                    _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, BackColor));
                    _addButtonHoverImage = Tools.isBright(BackColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255))));
                    _inactiveLeftSideImage = Tools.isBright(BackColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(BackColor.R, 20), Tools.GerekiyorsaAzalt(BackColor.G, 20), Tools.GerekiyorsaAzalt(BackColor.B, 20))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(BackColor.R, 20, 255), Tools.GerekiyorsaArttır(BackColor.G, 20, 255), Tools.GerekiyorsaArttır(BackColor.B, 20, 255)));
                }
                if (_parentWindow._overlay != null)
                {
                    _parentWindow._overlay.Render(true);
                }
            }
            if (ForeColor != _ForeColor)
            {
                _ForeColor = ForeColor;
                ForegroundColor = ForeColor;
                if (Properties.Settings.Default.closeColor == 1)
                {
                    _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor);
                    _closeButtonHoverImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255)));
                }
                if (Properties.Settings.Default.newTabColor == 1)
                {
                    _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, ForeColor));
                    _addButtonHoverImage = Tools.isBright(ForeColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255))));
                    _inactiveLeftSideImage = Tools.isBright(ForeColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(ForeColor.R, 20), Tools.GerekiyorsaAzalt(ForeColor.G, 20), Tools.GerekiyorsaAzalt(ForeColor.B, 20))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(ForeColor.R, 20, 255), Tools.GerekiyorsaArttır(ForeColor.G, 20, 255), Tools.GerekiyorsaArttır(ForeColor.B, 20, 255)));
                }
                if (_parentWindow._overlay != null)
                {
                    _parentWindow._overlay.Render(true);
                }
            }
            if (OverlayColor != _OverlayColor)
            {
                _OverlayColor = OverlayColor;
                OverlayLayerColor = OverlayColor;
                if (Properties.Settings.Default.closeColor == 3)
                {
                    _closeButtonImage = Tools.ColorReplace(Resources.KorotClose, 50, Color.White, OverlayColor);
                    _closeButtonHoverImage = Tools.isBright(OverlayColor) ? Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(OverlayColor.R, 20), Tools.GerekiyorsaAzalt(OverlayColor.G, 20), Tools.GerekiyorsaAzalt(OverlayColor.B, 20))) : Tools.ColorReplace(Resources.KorotClose, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(OverlayColor.R, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.G, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.B, 20, 255)));
                }
                if (Properties.Settings.Default.newTabColor == 2)
                {
                    _addButtonImage = new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, OverlayColor));
                    _addButtonHoverImage = Tools.isBright(OverlayColor) ? new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(OverlayColor.R, 20), Tools.GerekiyorsaAzalt(OverlayColor.G, 20), Tools.GerekiyorsaAzalt(OverlayColor.B, 20)))) : new Bitmap(Tools.ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(OverlayColor.R, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.G, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.B, 20, 255))));
                    _inactiveLeftSideImage = Tools.isBright(OverlayColor) ? Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaAzalt(OverlayColor.R, 20), Tools.GerekiyorsaAzalt(OverlayColor.G, 20), Tools.GerekiyorsaAzalt(OverlayColor.B, 20))) : Tools.ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, Tools.GerekiyorsaArttır(OverlayColor.R, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.G, 20, 255), Tools.GerekiyorsaArttır(OverlayColor.B, 20, 255)));
                }
                if (_parentWindow._overlay != null)
                {
                    _parentWindow._overlay.Render(true);
                }
            }

        }
        public void ChangeBackImage(Image BackImage)
        {
            if (BackImage != _background)
            {
                _background = BackImage;
            }
        }
        public void ChangeDrawMode(bool drawBack)
        {
            drawBackgroundColor = drawBack;
        }


        /// <summary>
        /// Gets the image to use for the right side of the tab.  For Korot, we pick a specific image for inactive tabs that aren't at
        /// the end of the list to allow for the separation between inactive tabs to be more clearly defined.
        /// </summary>
        /// <param name="tab">Tab that we are retrieving the image for.</param>
        /// <returns>Right-side image for <paramref name="tab"/>.</returns>
        /// 
        protected override Image GetTabRightImage(TitleBarTab tab)
        {
            ListWithEvents<TitleBarTab> allTabs = tab.Parent.Tabs;

            if (tab.Active || allTabs.IndexOf(tab) == allTabs.Count - 1)
            {
                return base.GetTabRightImage(tab);
            }

            return _inactiveRightSideShadowImage;
        }

        /// <summary>Since Korot tabs overlap, we set this property to the amount that they overlap by.</summary>
        public override int OverlapWidth => 10;
    }
}