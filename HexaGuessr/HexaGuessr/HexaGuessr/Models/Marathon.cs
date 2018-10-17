using System;
using System.Collections.Generic;
using System.Text;

namespace HexaGuessr.Models
{
    public class Marathon
    {
        private int _score;
        private int _rounds;
        private DateTime _date;
        private GameMode _gameMode;

        public Marathon(int score, int rounds, GameMode gameMode)
        {
            _score = score;
            _rounds = rounds;
            _date = DateTime.Now;
            _gameMode = GameMode;
        }

        public int Score { get => _score; set => _score = value; }
        public int Rounds { get => _rounds; set => _rounds = value; }
        public int MeanScore { get => _score / _rounds; }

        public GameMode GameMode { get => _gameMode; }
        public string DateString { get => _date.ToShortDateString(); }
    }

    public enum GameMode
    {
        GuessHex,
        GuessColour
    }

}
