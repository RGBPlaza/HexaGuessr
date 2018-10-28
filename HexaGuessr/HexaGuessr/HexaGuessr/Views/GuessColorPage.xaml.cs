using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexaGuessr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HexaGuessr.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuessColorPage : ContentPage
	{
		public GuessColorPage ()
		{
			InitializeComponent ();
            colorGenerator = new ColorUtility();
		}

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (hueSelected)
            {
                guessingColor = guessingColor
                .WithSaturation(Math.Min(1, Math.Max(0, (guessingColor.Saturation + (e.TotalX / (100 * ColorDisplayBoxView.Width)) % 1))))
                .WithLuminosity(Math.Min(1, Math.Max(0, (guessingColor.Luminosity - (e.TotalY / (100 * ColorDisplayBoxView.Height)) % 1))));
                SubmitButton.TextColor = guessingColor;
            }
            else
            {
                guessingColor = guessingColor.WithHue(Math.Abs(guessingColor.Hue + (e.TotalX / (200 * ColorDisplayBoxView.Width))) % 1);
                SelectHueButton.TextColor = guessingColor;
            }

            ColorDisplayBoxView.BackgroundColor = guessingColor;

        }

        ColorUtility colorGenerator;
        Color colorToGuess;
        Color guessingColor;
        bool hueSelected;

        private void SetupPage()
        {
            RoundLabel.Text = $"Round {PlayerInfo.CurrentRound}";
            colorToGuess = colorGenerator.NextColor(0.2, 0.7);
            ScoreLabel.Text = PlayerInfo.CurrentScore.ToString();
            HexLabel.Text = ColorUtility.ColorToHex(colorToGuess);
            guessingColor = new Color(0.6, 0, 0);
            ColorDisplayBoxView.BackgroundColor = guessingColor;

            hueSelected = false;

            SelectHueButton.IsEnabled = true;
            SelectHueButton.TextColor = guessingColor;
            SelectHueButton.BackgroundColor = Color.FromHex("#eeeeeeee");

            SubmitButton.IsEnabled = false;
            SubmitButton.TextColor = Color.FromHex("#66eeeeee");
            SubmitButton.BackgroundColor = Color.FromHex("#66eeeeee");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetupPage();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void SelectHueButton_Clicked(object sender, EventArgs e)
        {
            hueSelected = true;

            SelectHueButton.IsEnabled = false;
            SelectHueButton.TextColor = Color.FromHex("#66eeeeee");
            SelectHueButton.BackgroundColor = Color.FromHex("#66eeeeee");

            SubmitButton.IsEnabled = true;
            SubmitButton.TextColor = guessingColor;
            SubmitButton.BackgroundColor = Color.FromHex("#eeeeeeee");
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoundScorePage(colorToGuess, guessingColor));
        }

    }
}