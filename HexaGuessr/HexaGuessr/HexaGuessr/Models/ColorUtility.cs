using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HexaGuessr.Models
{
    public class ColorUtility
    {
        private Random rand;

        public ColorUtility()
        {
            rand = new Random();
        }

        public Color NextColor(double minimumLuminosity = 0, double maximumLuminosity = 1)
        {
            double red = rand.NextDouble();
            double green = rand.NextDouble();
            double blue = rand.NextDouble();
            
            Color newColor = new Color(red, green, blue);

            if (newColor.Luminosity < minimumLuminosity)
                newColor = newColor.WithLuminosity(minimumLuminosity);
            else if (newColor.Luminosity > maximumLuminosity)
                newColor = newColor.WithLuminosity(maximumLuminosity);

            return newColor; 
        }

        public static Color HexToColor(string hexString)
        {
            double red = ((double)int.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            double green = ((double)int.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            double blue = ((double)int.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            return new Color(red, green, blue);
        }

        public static string ColorToHex(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", (int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
        }

    }
}
