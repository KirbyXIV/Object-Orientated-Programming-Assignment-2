using System;
using System.IO;
using Newtonsoft.Json;

namespace OOP_2_Code
{
    class Statistics
    {
        public static void SaveStats(Game game)
        {
            if (File.Exists("../stats.json"))
            {
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));

                stats["Total Dice Rolled"] += game.DiceRolled;
                

                if (game is Three_Or_More)
                {
                    stats["Three or More Games Played"]++;
                    stats["Three or More High Score"] = game.threeOrMoreHighScore;

                }
                else if (game is Sevens_Out)
                {
                    stats["Sevens Out Games Played"]++;
                    stats["Sevens Out High Score"] = game.sevensOutHighScore;
                }

                var json = JsonConvert.SerializeObject(stats);

                File.WriteAllText("../stats.json", json);
            }
        }

        public static void LoadStats(Game game)
        {
            if (File.Exists("../stats.json"))
            {
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));

                game.threeOrMoreHighScore = stats["Three or More High Score"];
                game.sevensOutHighScore = stats["Sevens Out High Score"];
                game.DiceRolled = stats["Total Dice Rolled"];
                game.sevensOutGamesPlayed = stats["Sevens Out Games Played"];
                game.threeOrMoreGamesPlayed = stats["Three or More Games Played"];
            }
        }

        public static void ResetStats()
        {
            var stats = new Dictionary<string, int>
            {
                {"Three or More Games Played", 0},
                {"Sevens Out Games Played", 0},
                {"Sevens Out High Score", 0},
                {"Three or More High Score", 0},
                {"Total Dice Rolled", 0 }
            };

            var json = JsonConvert.SerializeObject(stats);

            File.WriteAllText("../stats.json", json);
        }

        public static void ViewStats()
        {
            if (File.Exists("../stats.json"))
            {
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));
                foreach (var stat in stats)
                {
                    Console.WriteLine($"{stat.Key}: {stat.Value}");
                }
            }
        }
    }
}