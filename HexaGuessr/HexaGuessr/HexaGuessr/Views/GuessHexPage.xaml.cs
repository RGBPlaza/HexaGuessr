using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexaGuessr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace HexaGuessr.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuessHexPage : ContentPage
	{
		public GuessHexPage ()
		{
			InitializeComponent ();
            colorGenerator = new RandomColorGenerator();
        }
        
        RandomColorGenerator colorGenerator;
        Color colorToGuess;

        private void SetupPage()
        {
            colorToGuess = colorGenerator.NextColor(0.2, 0.7);
            BackgroundColor = colorToGuess;
            ScoreLabel.Text = PlayerInfo.CurrentScore.ToString();
            HexEntry.Text = string.Empty;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetupPage();

            System.Diagnostics.Debug.WriteLine(string.Format("{0:X},{1:X},{2:X}", (int)(colorToGuess.R * 255), (int)(colorToGuess.G * 255), (int)(colorToGuess.B * 255)));

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void HexEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (int.TryParse(e.NewTextValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                {
                    if (e.NewTextValue.Length == 6)
                    {
                        SubmitButton.BackgroundColor = Color.FromHex("#eeeeeeee");
                        SubmitButton.TextColor = colorToGuess;
                        SubmitButton.IsEnabled = true;
                        return;
                    }
                }
                else
                {
                    HexEntry.Text = e.OldTextValue;
                }

                if (e.NewTextValue.Length > 6)
                    HexEntry.Text = e.NewTextValue.Substring(0, 6);
            }

            SubmitButton.BackgroundColor = Color.FromHex("#44eeeeee");
            SubmitButton.TextColor = Color.Black;
            SubmitButton.IsEnabled = false;

        }

        private Color HexToColor(string hexString)
        {
            double red   = ((double)int.Parse(hexString.Substring(0,2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            double green = ((double)int.Parse(hexString.Substring(2,2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            double blue  = ((double)int.Parse(hexString.Substring(4,2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)) / 255;
            return new Color(red, green, blue);
        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            Color guessedColor = HexToColor(HexEntry.Text);

            double hAccuracy = 1 - Math.Abs(colorToGuess.Hue - guessedColor.Hue);
            double sAccuracy = 1 - Math.Abs(colorToGuess.Saturation - guessedColor.Saturation);
            double lAccuracy = 1 - Math.Abs(colorToGuess.Luminosity - guessedColor.Luminosity);

            int points = (int)(Math.Ceiling(10 * Math.Pow(hAccuracy,7))) + (int)(Math.Ceiling(10 * Math.Pow(sAccuracy, 7))) + (int)(Math.Ceiling(10 * Math.Pow(lAccuracy, 7)));
            Debug.WriteLine($"You have scored {points} points.");
            PlayerInfo.CurrentScore += points;

            ScoreLabel.Text = PlayerInfo.CurrentScore.ToString();
        }

    }
}