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

        ColorUtility colorGenerator;
        Color colorToGuess;

        private void SetupPage()
        {
            RoundLabel.Text = $"Round {PlayerInfo.CurrentRound}";
            colorToGuess = colorGenerator.NextColor(0.2, 0.7);
            ScoreLabel.Text = PlayerInfo.CurrentScore.ToString();
            HexLabel.Text = ColorUtility.ColorToHex(colorToGuess);
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

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
        }

    }
}