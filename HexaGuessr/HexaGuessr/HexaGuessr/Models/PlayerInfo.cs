using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HexaGuessr.Models
{
    public static class PlayerInfo
    {
        private static int currentScore;
        private static int currentRound;
        private static GameMode currentGameMode;

        public static int CurrentScore { get => currentScore; set => currentScore = value; }
        public static int CurrentRound { get => currentRound; set => currentRound = value; }
        public static GameMode CurrentGameMode { get => currentGameMode; set => currentGameMode = value; }

        public static List<Marathon> Marathons { get; set; }
        const string fileName = "marathons.json";
        public static void LoadMarathons()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            if (File.Exists(filePath))
            {
                string marathonsJson = File.ReadAllText(filePath);
                Marathons = string.IsNullOrWhiteSpace(marathonsJson) ? new List<Marathon>() : JsonConvert.DeserializeObject<List<Marathon>>(marathonsJson);
                System.Diagnostics.Debug.WriteLine(Marathons.Count);
            }
            else
            {
                File.Create(filePath);
                Marathons = new List<Marathon>();
            }
        }
        public static void SaveMarathons()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string marathonsJson = JsonConvert.SerializeObject(Marathons);
            File.WriteAllText(filePath, marathonsJson);
        }

        public static int TotalMoons
        {
            get {
                int totalMoons = 0;
                foreach (Marathon m in Marathons)
                    totalMoons += m.Score;
                return totalMoons;
            }
        }

    }
}