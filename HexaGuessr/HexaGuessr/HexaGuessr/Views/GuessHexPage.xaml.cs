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
            colorGenerator = new ColorUtility();
        }
        
        ColorUtility colorGenerator;
        Color colorToGuess;

        private void SetupPage()
        {
            RoundLabel.Text = $"Round {PlayerInfo.CurrentRound}";
            colorToGuess = colorGenerator.NextColor(0.2, 0.7);
            BackgroundColor = colorToGuess;
            ScoreLabel.Text = PlayerInfo.CurrentScore.ToString();
            HexEntry.Text = string.Empty;
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

        private void HexEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (int.TryParse(e.NewTextValue.Replace("#", ""), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hex))
                {
                    if (e.NewTextValue.Length == 7)
                    {
                        SubmitButton.BackgroundColor = Color.FromHex("#eeeeeeee");
                        SubmitButton.TextColor = colorToGuess;
                        SubmitButton.IsEnabled = true;
                        return;
                    }
                }
                else
                {
                    if (e.NewTextValue != "#")
                        HexEntry.Text = e.OldTextValue;
                }

                if (e.NewTextValue[0] != '#')
                    HexEntry.Text = "#" + e.NewTextValue;

                if (e.NewTextValue.Length > 7)
                    HexEntry.Text = e.NewTextValue.Substring(0, 7);
            }

            SubmitButton.BackgroundColor = Color.FromHex("#44eeeeee");
            SubmitButton.TextColor = Color.Black;
            SubmitButton.IsEnabled = false;

        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            Color guessedColor = ColorUtility.HexToColor(HexEntry.Text.Replace("#", ""));
            await Navigation.PushAsync(new GuessHexScorePage(colorToGuess, guessedColor));
        }

        private async void HexEntry_Enter(object sender, EventArgs e)
        {
            Color guessedColor = ColorUtility.HexToColor(HexEntry.Text.Replace("#", ""));
            await Navigation.PushAsync(new GuessHexScorePage(colorToGuess, guessedColor));
        }

    }
}