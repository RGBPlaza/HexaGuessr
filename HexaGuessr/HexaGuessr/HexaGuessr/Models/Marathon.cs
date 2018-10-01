using System;
using System.Collections.Generic;
using System.Text;

namespace HexaGuessr.Models
{
    public class Marathon
    {
        private int score;
        private int rounds;
        private DateTime date;
        private GameMode gameMode;

        public Marathon(int score, int rounds, GameMode gameMode)
        {
            date = DateTime.Now;
        }

        public int Score { get => score; set => score = value; }
        public int Rounds { get => rounds; set => rounds = value; }
        public int MeanScore { get => score / rounds; }

        public GameMode GameMode { get => gameMode; }
        public DateTime Date { get => date; }
    }

    public enum GameMode
    {
        GuessHex,
        GuessColour
    }

}
