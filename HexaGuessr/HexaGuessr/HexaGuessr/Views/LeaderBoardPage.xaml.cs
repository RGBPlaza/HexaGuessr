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
            BackgroundColor = backColor;
            List<string> gameModeOptions = new List<string> { "All", "Guess Hex" };
            if (PlayerInfo.TotalMoons >= 200)
                gameModeOptions.Add("Guess Color");
            if (PlayerInfo.TotalMoons >= 500)
                gameModeOptions.Add("Mixed");
            MarathonTypePicker.ItemsSource = gameModeOptions;
            MarathonTypePicker.SelectedIndex = 0;
            ScoreLabel.Text = PlayerInfo.TotalMoons.ToString();
		}

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HistoryListView.SelectedItem = null;
        }

        private void MarathonTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(MarathonTypePicker.SelectedIndex == 0)
                HistoryListView.ItemsSource = PlayerInfo.Marathons.OrderByDescending(o => o.Score).Take(10);
            else
                HistoryListView.ItemsSource = PlayerInfo.Marathons.Where(o => o.GameMode == (GameMode)(MarathonTypePicker.SelectedIndex - 1)).OrderByDescending(o => o.Score).Take(10);
        }

    }
}