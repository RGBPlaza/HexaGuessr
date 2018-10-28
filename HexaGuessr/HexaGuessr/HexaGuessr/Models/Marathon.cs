using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HexaGuessr.Models
{
    public class Marathon
    {
        [JsonProperty("Score")]
        private int _score;

        [JsonProperty("Rounds")]
        private int _rounds;

        [JsonProperty("Date")]
        private DateTime _date;

        [JsonProperty("GameMode")]
        private GameMode _gameMode;

        public Marathon(int score, int rounds, GameMode gameMode)
        {
            _score = score;
            _rounds = rounds;
            _date = DateTime.Now;
            _gameMode = gameMode;
        }

        [JsonIgnore]
        public int Score { get => _score; set => _score = value; }

        [JsonIgnore]
        public int Rounds { get => _rounds; set => _rounds = value; }

        [JsonIgnore]
        public int MeanScore { get => _score / _rounds; }

        [JsonIgnore]
        public GameMode GameMode { get => _gameMode; }

        [JsonIgnore]
        public string DateString { get => _date.ToShortDateString(); }
    }

    public enum GameMode
    {
        GuessHex,
        GuessColor,
        Mixed
    }

}
