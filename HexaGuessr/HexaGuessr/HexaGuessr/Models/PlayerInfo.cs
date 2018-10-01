using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HexaGuessr.Models
{
    public static class PlayerInfo
    {
        private static int currentScore = 0;
        private static List<Marathon> marathons;

        public static int CurrentScore { get => currentScore; set => currentScore = value; }
        public static List<Marathon> Marathons { get => marathons; set => marathons = value; }

        const string fileName = "marathons.json";
        public static void LoadMarathons()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string marathonsJson = File.ReadAllText(filePath);
            marathons = JsonConvert.DeserializeObject<List<Marathon>>(marathonsJson);
        }
        public static void SaveMarathons()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
            string marathonsJson = JsonConvert.SerializeObject(marathons);
            File.WriteAllText(filePath, marathonsJson);
        }

    }
}