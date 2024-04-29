using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatic9045.AtsEx.UpdateChecker
{
    internal static class ColorParser
    {
        public static Color Parse(string colorText)
        {
            if (colorText.StartsWith("#", StringComparison.OrdinalIgnoreCase))
            {
                string r, g, b;
                switch (colorText.Length - 1)
                {
                    case 3:
                    case 4:
                        r = new string(colorText[colorText.Length - 3], 2);
                        g = new string(colorText[colorText.Length - 2], 2);
                        b = new string(colorText[colorText.Length - 1], 2);
                        break;

                    case 6:
                    case 8:
                        r = colorText.Substring(colorText.Length - 6, 2);
                        g = colorText.Substring(colorText.Length - 4, 2);
                        b = colorText.Substring(colorText.Length - 2, 2);
                        break;

                    default:
                        return default;
                }

                try
                {
                    return Color.FromArgb(Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));
                }
                catch
                {
                    return default;
                }
            }
            else
            {
                return Color.FromName(colorText);
            }
        }
    }
}
