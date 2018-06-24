using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HexaGuessr.Models
{
    public class RandomColorGenerator
    {
        private Random rand;

        public RandomColorGenerator()
        {
            rand = new Random();
        }

        public Color NextColor(double minimumLuminosity = 0)
        {
            double red = rand.NextDouble();
            double green = rand.NextDouble();
            double blue = rand.NextDouble();
            
            Color newColor = new Color(red, green, blue);
            if (newColor.Luminosity < minimumLuminosity)
                newColor = newColor.WithLuminosity(minimumLuminosity);
            return newColor;
        }
    }
}
