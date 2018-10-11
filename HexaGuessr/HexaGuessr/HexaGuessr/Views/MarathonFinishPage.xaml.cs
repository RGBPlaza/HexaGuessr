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
	public partial class MarathonFinishPage : ContentPage
	{
        public MarathonFinishPage(GameMode gameMode, Color backgroundColor)
        {
            InitializeComponent();
            Marathon marathon = new Marathon(PlayerInfo.CurrentScore, PlayerInfo.CurrentRound + 1, gameMode);
            BackgroundColor = backgroundColor;

            TotalLabel.Text = PlayerInfo.CurrentScore.ToString();
            RoundsLabel.Text = (PlayerInfo.CurrentRound + 1).ToString();
            int averageScore = PlayerInfo.CurrentScore / (PlayerInfo.CurrentRound + 1);
            AverageLabel.Text = averageScore.ToString();

            //HistoryListView.ItemsSource = PlayerInfo.Marathons.Where(o => o.GameMode == gameMode);
            //PlayerInfo.Marathons.Add(marathon);
            PlayerInfo.SaveMarathons();

		}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return base.OnBackButtonPressed();
        }
    }
}