using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HexaGuessr.Models;
using Xamarin.Forms;

namespace HexaGuessr
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new Views.MenuPage());
        }

		protected override void OnStart ()
		{
            PlayerInfo.LoadMarathons();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
