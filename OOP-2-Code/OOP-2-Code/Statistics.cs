using System;

namespace OOP_2_Code
{
    class Statistics
    {
        public static int sevensOutHighScore;
        public static int sevensOutGamesPlayed;
        public static int threeOrMoreHighScore;
        public static int threeOrMoreGamesPlayed;
        public static int totalDiceRolled;
        public static void ResetStats()
        {
            Console.WriteLine("Resetting statistics...");
            sevensOutHighScore = 0;
            sevensOutGamesPlayed = 0;
            threeOrMoreHighScore = 0;
            threeOrMoreGamesPlayed = 0;
            totalDiceRolled = 0;

        }

        public static void SavedStats()
        {
            threeOrMoreHighScore = threeOrMoreHighScore * -1;
            Console.WriteLine("Saved statistics..." +
                            "\nHere are the current statistics: " +
                            "\n-----------------------------------------------" +
                            $"\nSevens out high score: {sevensOutHighScore}" +
                            $"\nSevens out games played: {sevensOutGamesPlayed}" +
                            $"\nThree or more high score: {threeOrMoreHighScore}" +
                            $"\nThree or more games played: {threeOrMoreGamesPlayed}" +
                            $"\nTotal dice rolled: {totalDiceRolled}");
        }   
    }
}