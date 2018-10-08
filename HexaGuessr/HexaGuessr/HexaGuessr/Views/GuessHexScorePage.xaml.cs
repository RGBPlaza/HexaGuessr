using HexaGuessr.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HexaGuessr.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuessHexScorePage : ContentPage
	{
        private Color _actualColor;
        public GuessHexScorePage(Color actualColor, Color guessedColor)
        {
            InitializeComponent();

            ActualBackground.BackgroundColor = actualColor;
            GuessBackground.BackgroundColor = guessedColor;

            _actualColor = actualColor;

            double hAccuracy = 1 - Math.Abs(actualColor.Hue - guessedColor.Hue);
            double sAccuracy = 1 - Math.Abs(actualColor.Saturation - guessedColor.Saturation);
            double lAccuracy = 1 - Math.Abs(actualColor.Luminosity - guessedColor.Luminosity);

            int points = (int)(Math.Ceiling(12 * Math.Pow(hAccuracy, 8))) + (int)(Math.Ceiling(9 * Math.Pow(sAccuracy, 8))) + (int)(Math.Ceiling(9 * Math.Pow(lAccuracy, 8)));
            Debug.WriteLine($"You have scored {points} points.");
            PlayerInfo.CurrentScore += points;
            TotalScoreLabel.Text = PlayerInfo.CurrentScore.ToString();

            string[] comments = new string[8] { "Oops!", "Better luck next time!", "Not bad!", "Nice!", "Nifty!", "Superb!", "Rad!", "That's some fine craftsmanship my man!" };
            ScoreLabel.Text = $"{points} Moons";
            if (points <= 5)
                CommentLabel.Text = comments[0];
            else if (points <= 12)
                CommentLabel.Text = comments[1];
            else if (points <= 18)
                CommentLabel.Text = comments[2];
            else if (points <= 22)
                CommentLabel.Text = comments[3];
            else if (points <= 25)
                CommentLabel.Text = comments[4];
            else if (points <= 27)
                CommentLabel.Text = comments[5];
            else if (points <= 29)
                CommentLabel.Text = comments[6];
            else
                CommentLabel.Text = comments[7];

            ActualLabel.Text = ColorUtility.ColorToHex(actualColor);
            GuessLabel.Text = ColorUtility.ColorToHex(guessedColor);

            ScoreLabel.TranslationX = -(Application.Current.MainPage.Width + ScoreLabel.Width) / 8;
            ScoreLabel.Opacity = 0;
            CommentLabel.TranslationX = Application.Current.MainPage.Width / 8;
            CommentLabel.Opacity = 0;

            NavStackLayout.Orientation = Device.Idiom == TargetIdiom.Phone ? StackOrientation.Vertical : StackOrientation.Horizontal;

		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ScoreLabel.FadeTo(1, 500, Easing.CubicOut);
            CommentLabel.FadeTo(1, 500, Easing.CubicOut);
            ScoreLabel.TranslateTo(0, 0, 500, Easing.SpringOut);
            CommentLabel.TranslateTo(0, 0, 500, Easing.SpringOut);
        }

        private async void NextButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfo.CurrentRound++;
            await Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return base.OnBackButtonPressed();
        }

        private async void FinishButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MarathonFinishPage(GameMode.GuessHex, _actualColor));
        }

	}
}