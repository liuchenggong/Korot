using System;
using System.Drawing;

namespace HaltroyTabs
{
	/// <summary>Renderer that produces tabs that mimic the appearance of the Korot browser.</summary>
	public class KorotTabRenderer : BaseTabRenderer
	{
        /// <summary>
        /// A Korot-specific right-side tab image that allows the separation between inactive tabs to be more clearly defined.
        /// </summary>
	    protected Image _inactiveRightSideShadowImage = Resources.KorotRight;

		/// <summary>Constructor that initializes the various resources that we use in rendering.</summary>
		/// <param name="parentWindow">Parent window that this renderer belongs to.</param>
		public KorotTabRenderer(TitleBarTabs parentWindow,Color BackColor,Color ForeColor,Color OverlayColor)
			: base(parentWindow)
		{
            BackgroundColor = BackColor;
            ForegroundColor = ForeColor;
            OverlayLayerColor = OverlayColor;
            this._inactiveRightSideShadowImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));
            // Initialize the various images to use during rendering
            _activeLeftSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotLeft,50,Color.White,Color.FromArgb(255,MoreBrightnessIfNeeded(BackColor.R,30,255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
			_activeRightSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 30, 255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
            _activeCenterImage = IsBright(BackColor) ? ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 30, 255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
            _closeButtonImage = ColorReplace(ColorReplace(Resources.KorotClose,50,Color.White,ForeColor), 50, Color.Red, OverlayColor);
			_closeButtonHoverImage = IsBright(OverlayColor) ? ColorReplace(ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor), 50, Color.Red, Color.FromArgb(255, LowerBrightnessIfNeeded(OverlayColor.R, 20), LowerBrightnessIfNeeded(OverlayColor.G, 20), LowerBrightnessIfNeeded(OverlayColor.B, 20))) : ColorReplace(ColorReplace(Resources.KorotClose,50,Color.White,ForeColor), 50, Color.Red, Color.FromArgb(255,MoreBrightnessIfNeeded(OverlayColor.R,20,255),MoreBrightnessIfNeeded(OverlayColor.G, 20, 255), MoreBrightnessIfNeeded(OverlayColor.B, 20, 255)));
            _background = ColorReplace(Resources.KorotBackground,50,Color.White,BackColor);
			_addButtonImage = new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, OverlayColor));
            _addButtonHoverImage = IsBright(OverlayColor) ? new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(OverlayColor.R, 20), LowerBrightnessIfNeeded(OverlayColor.G, 20), LowerBrightnessIfNeeded(OverlayColor.B, 20)))) : new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(OverlayColor.R, 20, 255), MoreBrightnessIfNeeded(OverlayColor.G, 20, 255), MoreBrightnessIfNeeded(OverlayColor.B, 20, 255))));
            _inactiveLeftSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));
            _inactiveRightSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255))); 
            _inactiveCenterImage = IsBright(BackColor) ? ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255))); 
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
        public void ChangeColors(Color BackColor, Color ForeColor, Color OverlayColor){
            BackgroundColor = BackColor;
            ForegroundColor = ForeColor;
            OverlayLayerColor = OverlayColor;
            this._inactiveRightSideShadowImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));
            // Initialize the various images to use during rendering
            _activeLeftSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 30, 255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
            _activeRightSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 30, 255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
            _activeCenterImage = IsBright(BackColor) ? ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 30), LowerBrightnessIfNeeded(BackColor.G, 30), LowerBrightnessIfNeeded(BackColor.B, 30))) : ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 30, 255), MoreBrightnessIfNeeded(BackColor.G, 30, 255), MoreBrightnessIfNeeded(BackColor.B, 30, 255)));
            _closeButtonImage = ColorReplace(ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor), 50, Color.Red, OverlayColor);
            _closeButtonHoverImage = IsBright(OverlayColor) ? ColorReplace(ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor), 50, Color.Red, Color.FromArgb(255, LowerBrightnessIfNeeded(OverlayColor.R, 20), LowerBrightnessIfNeeded(OverlayColor.G, 20), LowerBrightnessIfNeeded(OverlayColor.B, 20))) : ColorReplace(ColorReplace(Resources.KorotClose, 50, Color.White, ForeColor), 50, Color.Red, Color.FromArgb(255, MoreBrightnessIfNeeded(OverlayColor.R, 20, 255), MoreBrightnessIfNeeded(OverlayColor.G, 20, 255), MoreBrightnessIfNeeded(OverlayColor.B, 20, 255)));
            _background = ColorReplace(Resources.KorotBackground, 50, Color.White, BackColor);
            _addButtonImage = new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, OverlayColor));
            _addButtonHoverImage = IsBright(OverlayColor) ? new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(OverlayColor.R, 20), LowerBrightnessIfNeeded(OverlayColor.G, 20), LowerBrightnessIfNeeded(OverlayColor.B, 20)))) : new Bitmap(ColorReplace(Resources.KorotAdd, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(OverlayColor.R, 20, 255), MoreBrightnessIfNeeded(OverlayColor.G, 20, 255), MoreBrightnessIfNeeded(OverlayColor.B, 20, 255))));
            _inactiveLeftSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotLeft, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));
            _inactiveRightSideImage = IsBright(BackColor) ? ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotRight, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));
            _inactiveCenterImage = IsBright(BackColor) ? ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, LowerBrightnessIfNeeded(BackColor.R, 20), LowerBrightnessIfNeeded(BackColor.G, 20), LowerBrightnessIfNeeded(BackColor.B, 20))) : ColorReplace(Resources.KorotCenter, 50, Color.White, Color.FromArgb(255, MoreBrightnessIfNeeded(BackColor.R, 20, 255), MoreBrightnessIfNeeded(BackColor.G, 20, 255), MoreBrightnessIfNeeded(BackColor.B, 20, 255)));

        }
        private static int LowerBrightnessIfNeeded(int defaultint, int lower) => defaultint > lower ? defaultint - lower : defaultint;
        private static int MoreBrightnessIfNeeded(int defaultint, int add, int limit) => defaultint + add > limit ? defaultint : defaultint + add;
        private static bool IsBright(Color c){return (int)Math.Sqrt(c.R * c.R * .241 +c.G * c.G * .691 +c.B * c.B * .068) > 130 ? true : false;}
        
        public static Image ColorReplace(Image inputImage, int tolerance, Color oldColor, Color NewColor)
        {
            Bitmap outputImage = new Bitmap(inputImage.Width, inputImage.Height);
            Graphics G = Graphics.FromImage(outputImage);
            G.DrawImage(inputImage, 0, 0);
            for (Int32 y = 0; y < outputImage.Height; y++)
                for (Int32 x = 0; x < outputImage.Width; x++)
                {
                    Color PixelColor = outputImage.GetPixel(x, y);
                    if (PixelColor.R > oldColor.R - tolerance && PixelColor.R < oldColor.R + tolerance && PixelColor.G > oldColor.G - tolerance && PixelColor.G < oldColor.G + tolerance && PixelColor.B > oldColor.B - tolerance && PixelColor.B < oldColor.B + tolerance)
                    {
                        int RColorDiff = oldColor.R - PixelColor.R;
                        int GColorDiff = oldColor.G - PixelColor.G;
                        int BColorDiff = oldColor.B - PixelColor.B;

                        if (PixelColor.R > oldColor.R) RColorDiff = NewColor.R + RColorDiff;
                        else RColorDiff = NewColor.R - RColorDiff;
                        if (RColorDiff > 255) RColorDiff = 255;
                        if (RColorDiff < 0) RColorDiff = 0;
                        if (PixelColor.G > oldColor.G) GColorDiff = NewColor.G + GColorDiff;
                        else GColorDiff = NewColor.G - GColorDiff;
                        if (GColorDiff > 255) GColorDiff = 255;
                        if (GColorDiff < 0) GColorDiff = 0;
                        if (PixelColor.B > oldColor.B) BColorDiff = NewColor.B + BColorDiff;
                        else BColorDiff = NewColor.B - BColorDiff;
                        if (BColorDiff > 255) BColorDiff = 255;
                        if (BColorDiff < 0) BColorDiff = 0;

                        outputImage.SetPixel(x, y, Color.FromArgb(RColorDiff, GColorDiff, BColorDiff));
                    }
                }
            return outputImage;
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
	            return base.GetTabRightImage(tab);

	        return _inactiveRightSideShadowImage;
	    }

	    /// <summary>Since Korot tabs overlap, we set this property to the amount that they overlap by.</summary>
		public override int OverlapWidth
		{
			get
			{
				return 15;
			}
		}
	}
}