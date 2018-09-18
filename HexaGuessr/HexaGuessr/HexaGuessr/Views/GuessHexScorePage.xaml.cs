﻿using HexaGuessr.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HexaGuessr.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GuessHexScorePage : ContentPage
	{
		public GuessHexScorePage (Color actualColor, Color guessedColor)
		{
			InitializeComponent ();

            ActualStackLayout.BackgroundColor = actualColor;
            GuessStackLayout.BackgroundColor = guessedColor;

            double hAccuracy = 1 - Math.Abs(actualColor.Hue - guessedColor.Hue);
            double sAccuracy = 1 - Math.Abs(actualColor.Saturation - guessedColor.Saturation);
            double lAccuracy = 1 - Math.Abs(actualColor.Luminosity - guessedColor.Luminosity);

            int points = (int)(Math.Ceiling(10 * Math.Pow(hAccuracy, 7))) + (int)(Math.Ceiling(10 * Math.Pow(sAccuracy, 7))) + (int)(Math.Ceiling(10 * Math.Pow(lAccuracy, 7)));
            Debug.WriteLine($"You have scored {points} points.");
            PlayerInfo.CurrentScore += points;

            Color averageColor = new Color((actualColor.R + guessedColor.R) / 2, (actualColor.G + guessedColor.G) / 2, (actualColor.B + guessedColor.B) / 2);
            ScoreLabel.Text = points.ToString();
            ScoreStackLayout.BackgroundColor = averageColor;
		}
	}
}