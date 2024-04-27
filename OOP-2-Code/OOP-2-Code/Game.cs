using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
namespace OOP_2_Code;

public abstract class Game
{
    public int highscore { get; set; } = 0;
    public int timesPlayed { get; set; } = 0;
    public string name { get; init; } = string.Empty;

    /// <summary>
    /// Main Program, runs the game
    /// </summary>
    public static void Main()
    {
        // Game array
        Game[] Games =
        [
            new Three_Or_More(),
            new Sevens_Out()
        ];

        ChooseGame(Games);
    }
        
    /// <summary>
    /// Lets the player choose a game to play, or admin possibilities
    /// </summary>
    /// <param name="Games"></param>
    private static void ChooseGame(Game[] Games)
    {
        // UI using ANSI escape sequences
        string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
        string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
        string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green
        // Main game loop, only allows chosen inputs
        while (true)
        {
            Console.WriteLine($"{N}-----------------------------------------------" +
                          $"\n{G}Games:" +
                          $"\n{G}[1]Three or More" +
                          $"\n{G}[2]Sevens Out" +
                          $"\n{N}-------------------" +
                          $"\n{R}Admin Choices:" +
                          $"\n{R}[V]View Statistics" +
                          $"\n{R}[R]Reset Statistics" +
                          $"\n{R}[T]Run Tests" +
                          $"\n{N}-------------------" +
                          $"\n[E]Exit Program" +
                          $"\n-----------------------------------------------" +
                           "\nEnter the number of the game you want to play"+
                           "\nOr choose from any of the admin choices above"+
                           "\nChoice: ");

            var choice = Console.ReadLine();
            // if user inputs a number, go into if loop
            if (int.TryParse(choice, out int gameChoice))
            {
                if (gameChoice > 0 && gameChoice <= Games.Length)
                {
                    // player selects game
                    Games[gameChoice - 1].Play();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and " + Games.Length);
                }
            }
            // if player does not enter a number, go into else if loop
            else if (choice.ToUpper() == "R")
            {
                // reset stats
                Console.WriteLine($"{R}Stats reset... Clearing Console \n");
                Statistics.ResetStats();
                Thread.Sleep(2500);
                Console.Clear();
            }
            else if (choice.ToUpper() == "V")
            {
                // view stats
                Statistics.ViewStats();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (choice.ToUpper() == "T")
            {
                // run tests
                Console.WriteLine($"{R}Tests run... Clearing Console \n");
                Testing.RunTests();
                Thread.Sleep(2500);
                Console.Clear();
            }
            else if (choice.ToUpper() == "E")
            {
                // exit program
                Console.WriteLine("Exiting program...");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else
            {
                // handles any incorrect input
                Console.WriteLine("Invalid input. Please enter a number or R, V, T or E. \n");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    // methods
    protected abstract void Play();
    //properties
    public int highScore { get; set; } = 0;
    internal int DiceRolled;
}
