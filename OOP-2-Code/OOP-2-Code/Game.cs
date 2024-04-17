using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
namespace OOP_2_Code;

public abstract class Game
{
    /// <summary>
    /// Main Program, runs the game
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
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
        // Main game loop
        while (true)
        {
            Console.WriteLine("-----------------------------------------------" +
                          $"\n{G}Games:" +
                          $"\n{G}[1]Three or More" +
                          $"\n{G}[2]Sevens Out" +
                          $"\n{N}-------------------" +
                          $"\n{R}Admin Choices:" +
                          $"\n{R}[R]Reset Statistics" +
                          $"\n{R}[T]Run Tests" +
                          $"\n{N}-----------------------------------------------" +
                           "\nEnter the number of the game you want to play"+
                           "\nOr choose from any of the admin choices above"+
                           "\nChoice: ");

            var choice = Console.ReadLine();
            if (int.TryParse(choice, out int gameChoice))
            {
                if (gameChoice > 0 && gameChoice <= Games.Length)
                {
                    Games[gameChoice - 1].Play();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and " + Games.Length);
                }
            }
            else if (choice.ToUpper() == "R")
            {
                Console.WriteLine($"{R}Stats reset... Clearing Console \n");
                Statistics.ResetStats();
                Thread.Sleep(2500);
                Console.Clear();
            }
            else if (choice.ToUpper() == "T")
            {
                Console.WriteLine($"{R}Tests run... Clearing Console \n");
                Testing.RunTests();
                Thread.Sleep(2500);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number or R or T. \n");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    protected abstract void Play();
    public int timesPlayed { get; set; } = 0;
    public int highScore { get; set; } = 0;
}
