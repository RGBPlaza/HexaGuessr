using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexaGuessr.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

    }
}