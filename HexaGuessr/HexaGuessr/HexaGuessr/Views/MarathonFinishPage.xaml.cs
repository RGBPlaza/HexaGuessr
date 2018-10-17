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

            string[] comments = new string[8] { "Oops!", "Better luck next time!", "Not bad!", "Nice!", "Nifty!", "Superb!", "Rad!", "That's some fine craftsmanship my man!" };
            if (marathon.MeanScore <= 5)
                CommentLabel.Text = comments[0];
            else if (marathon.MeanScore <= 12)
                CommentLabel.Text = comments[1]; 
            else if (marathon.MeanScore <= 18)
                CommentLabel.Text = comments[2];
            else if (marathon.MeanScore <= 22)
                CommentLabel.Text = comments[3];
            else if (marathon.MeanScore <= 25)
                CommentLabel.Text = comments[4];
            else if (marathon.MeanScore <= 27)
                CommentLabel.Text = comments[5];
            else if (marathon.MeanScore <= 29)
                CommentLabel.Text = comments[6];
            else
                CommentLabel.Text = comments[7];

            PlayerInfo.Marathons.Add(marathon);
            HistoryListView.ItemsSource = PlayerInfo.Marathons.Where(o => o.GameMode == gameMode).OrderByDescending(o => o.MeanScore).Take(5);
            System.Diagnostics.Debug.WriteLine(marathon.MeanScore);
            PlayerInfo.SaveMarathons();

		}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return base.OnBackButtonPressed();
        }
    }
}