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
	public partial class LeaderBoardPage : ContentPage
	{
		public LeaderBoardPage (Color backColor)
		{
            InitializeComponent();
            BackGroundColor = backColor;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}