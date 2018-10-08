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
            Marathon marathon = new Marathon(PlayerInfo.CurrentRound, PlayerInfo.CurrentRound + 1, gameMode);
            BackgroundColor = backgroundColor;
		}
	}
}