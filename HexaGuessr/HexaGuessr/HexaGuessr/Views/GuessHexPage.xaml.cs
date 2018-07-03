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
	public partial class GuessHexPage : ContentPage
	{
		public GuessHexPage ()
		{
			InitializeComponent ();
            colorGenerator = new RandomColorGenerator();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Color colorToGuess = colorGenerator.NextColor(0.2);
            SubmitButton.TextColor = colorToGuess;
            BackgroundColor = colorToGuess;
        }

        RandomColorGenerator colorGenerator;

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private char[] hexDigits = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };

        private void HexEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach(char c in e.NewTextValue.ToLower())
            {
                System.Diagnostics.Debug.WriteLine(c);
                if (!(char.IsNumber(c) || hexDigits.Contains(c)))
                {
                    HexEntry.Text = e.OldTextValue;
                }
            }

            if (e.NewTextValue.Length > 6)
                HexEntry.Text = e.NewTextValue.Substring(0, 6);
        }

    }
}