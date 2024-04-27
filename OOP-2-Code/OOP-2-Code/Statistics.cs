using System;
using System.IO;
using Newtonsoft.Json;

namespace OOP_2_Code
{
    class Statistics
    {
        public static void SaveStats(Game game)
        {
            // checks if json file exists
            if (File.Exists("../stats.json"))
            {
                // loads the stats from json file
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));

                // gets the amount of dice rolled
                stats["Total Dice Rolled"] += game.DiceRolled;
                
                // gets highscore and times played for each game
                if (game is Three_Or_More)
                {
                    stats["Three or More Games Played"]++;
                    stats["Three or More High Score"] = game.highscore;

                }
                else if (game is Sevens_Out)
                {
                    stats["Sevens Out Games Played"]++;
                    stats["Sevens Out High Score"] = game.highscore;
                }

                // saves the stats to the json file
                var json = JsonConvert.SerializeObject(stats);
                File.WriteAllText("../stats.json", json);
            }
        }

        public static void LoadStats(Game game)
        {
            if (File.Exists("../stats.json"))
            {
                // loads the stats from the json file
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));

                // sets the stats for the game
                if (game is Three_Or_More)
                {
                    game.timesPlayed = stats["Three or More Games Played"];
                    game.highscore = stats["Three or More High Score"];

                }
                else if (game is Sevens_Out)
                {
                    game.timesPlayed = stats["Sevens Out Games Played"];
                    game.highscore = stats["Sevens Out High Score"];
                }
                game.DiceRolled = stats["Total Dice Rolled"];
                
            }
        }

        public static void ResetStats()
        {
            // resets stats
            var stats = new Dictionary<string, int>
            {
                {"Sevens Out Games Played", 0},
                {"Three or More Games Played", 0},
                {"Sevens Out High Score", 0},
                {"Three or More High Score", 0},
                {"Total Dice Rolled", 0 }
            };

            // saves stats to file
            var json = JsonConvert.SerializeObject(stats);
            File.WriteAllText("../stats.json", json);
        }

        public static void ViewStats()
        {
            if (File.Exists("../stats.json"))
            {
                // load stats
                var stats = JsonConvert.DeserializeObject<Dictionary<string, int>>(File.ReadAllText("../stats.json"));
                // display stats
                foreach (var stat in stats)
                {
                    Console.WriteLine($"{stat.Key}: {stat.Value}");
                }
            }
        }
    }
}