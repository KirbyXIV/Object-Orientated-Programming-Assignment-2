using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_2_Code
{
    class Sevens_Out : Game
    {
        // sets name
        public Sevens_Out() { name = "Sevens Out"; }

        protected override void Play()
        {
            // variables
            int turn = 0;
            int[] playerScores = [0, 0];
            bool multiplayer = false;

            Console.WriteLine("Now playing... Sevens Out!");
            // loads the stats
            Statistics.LoadStats(this);
            Thread.Sleep(2000);
            Console.Clear();

            // player can only input p or c
            while (true)
            {
                Console.WriteLine("Would you like to play against another player or the computer? P/C");
                var choice = Console.ReadLine();
                if (choice.ToUpper() == "P")
                {
                    Console.WriteLine("You have chosen to play against another player.");
                    multiplayer = true;
                    break;
                }
                else if (choice.ToUpper() == "C")
                {
                    Console.WriteLine("You have chosen to play against the computer.");
                    multiplayer = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter P or C.");
                }
            }

            // runs the game for each player and saves their score seperately in the array
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Player {turn + 1}:");
                playerScores[i] = WhichPlayer(turn, playerScores, multiplayer);
                turn++;
            }

            // checks for winner
            if (playerScores[0] > playerScores[1])
            {
                Console.WriteLine("Player 1 wins!" +
                                 $"\nPlayer 1 scored: {playerScores[0]}" +
                                 $"\nPlayer 2 scored: {playerScores[1]}");
            }
            else if (playerScores[0] < playerScores[1])
            {
                Console.WriteLine("Player 2 wins!" +
                                 $"\nPlayer 1 scored: {playerScores[0]}" +
                                 $"\nPlayer 2 scored: {playerScores[1]}");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }

            // sets new highscore
            if (playerScores[0] > highscore)
            {
                highscore = playerScores[0];
            }
            if (playerScores[1] > highscore)
            {
                highscore = playerScores[1];
            }

            Console.WriteLine("\nGame Over! Returning to menu!");
            // saves the stats
            Statistics.SaveStats(this);
            Thread.Sleep(2000);
            Console.Clear();
            // restarts program
            Game.Main();
        }
        private int WhichPlayer(int turn, int[] playerScore, bool multiplayer)
        {
            // UI using ANSI escape sequences
            string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
            string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
            string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green

            // variables, i = player rolls
            bool gameOver = false;
            int total = 0;
            int sum = 0;
            int i = 0;

            // loops game until player scores a 7
            while (!gameOver)
            {
                Console.WriteLine("Press Enter to roll the dice...");
                
                // if player a computer, changes the "input"
                if (turn == 1 && multiplayer == false)
                {
                    Console.WriteLine("Computer is rolling...");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.ReadLine();
                }

                // checks total player rolls
                i++;

                // instansiates 2 die
                Die[] dice = new Die[2];
                for (int j = 0; j < dice.Length; j++)
                {
                    dice[j] = new Die();
                }

                // rolls die, adds to dice rolled
                foreach (Die die in dice)
                {
                    die.DieRoll();
                    DiceRolled++;
                }

                // checks for pair, if true, double points
                if (dice[0].DieValue == dice[1].DieValue)
                {
                    Console.WriteLine($"{G}You rolled a pair! Double Points!{N}");
                    total = (dice[0].DieValue + dice[1].DieValue) * 2;

                }
                else { total = dice[0].DieValue + dice[1].DieValue; }

                // displays rolls
                Console.WriteLine($"Roll {i}: " +
                    $"\nDie 1: {dice[0].DieValue} ¦ Die 2: {dice[1].DieValue} ¦ Total: {total}");

                // adds total to players sum
                sum += total;
                
                // checks for a 7, if true, end turn
                if (total == 7)
                {
                    Console.WriteLine($"\n{R}You rolled a 7! Turn over!{N}" +
                                      $"\nYou had a total score of {G}{sum}{N}! \n");
                    playerScore[turn] = sum;
                    gameOver = true;


                }
            }
            return playerScore[turn];
        }

        public void Test()
        {
            // variables
            int total = 0;
            int sum = 0;
            bool testOver = false;
            
            // keeps test running until a 7 is found
            while (!testOver)
            {
                // instantiates 2 die
                Die[] dice = new Die[2];
                for (int j = 0; j < dice.Length; j++)
                {
                    dice[j] = new Die();
                }

                // rolls die
                foreach (Die die in dice)
                {
                    die.DieRoll();
                }


                if (dice[0].DieValue == dice[1].DieValue)
                {
                    total = (dice[0].DieValue + dice[1].DieValue) * 2;

                }
                else { total = dice[0].DieValue + dice[1].DieValue; }

                sum += total;
                if (total == 7)
                {
                    Debug.Assert(total == 7, "game ended correctly");
                    testOver = true;
                }
            }
            
        }
    }
}
