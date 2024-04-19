using System;

namespace OOP_2_Code
{
    internal class Statistics
    {
        public static void ResetStats()
        {
            Console.WriteLine("Resetting statistics...");
        }

        public void SavedStats()
        {
            Console.WriteLine("Saving statistics...");
            Console.WriteLine("Sevens out high score: " +
                            "\nSevens out games played: " +
                            "\nThree or more high score: " +
                            "\nThree or more games played: " +
                            "Total dice rolled: ");
        }   
    }
}