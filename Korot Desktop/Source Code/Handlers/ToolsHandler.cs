using System;
using System.Drawing;

namespace Korot
{
    static class ToolsHandler
    {
        public static string ToHexString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

        public static Color ToRgbString(this string c)
        {
            Color color = ColorTranslator.FromHtml(c);
            int r = Convert.ToInt16(color.R);
            int g = Convert.ToInt16(color.G);
            int b = Convert.ToInt16(color.B);
            return Color.FromArgb(255, r, g, b);
        }
    }
}
