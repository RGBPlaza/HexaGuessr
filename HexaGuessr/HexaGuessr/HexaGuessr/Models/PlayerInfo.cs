using System;
using System.Collections.Generic;
using System.Text;

namespace HexaGuessr.Models
{
    public static class PlayerInfo
    {
        private static int currentScore = 0;

        public static int CurrentScore { get => currentScore; set => currentScore = value; }
    }
}