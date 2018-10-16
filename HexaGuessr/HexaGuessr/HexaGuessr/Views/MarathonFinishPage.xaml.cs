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

            PlayerInfo.Marathons.Add(marathon);
            HistoryListView.ItemsSource = PlayerInfo.Marathons.Where(o => o.GameMode == gameMode).Take(5);
            System.Diagnostics.Debug.WriteLine(marathon.Score);
            PlayerInfo.SaveMarathons();

		}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return base.OnBackButtonPressed();
        }
    }
}