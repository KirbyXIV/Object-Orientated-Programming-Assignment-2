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

        protected override void Play()
        {
            int turn = 0;
            int[] playerScores = [0, 0];
            bool multiplayer = false;

            Console.WriteLine("Now playing... Sevens Out!");
            Statistics.sevensOutGamesPlayed++;
            Thread.Sleep(2000);
            Console.Clear();

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

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Player {turn + 1}:");
                playerScores[i] = WhichPlayer(turn, playerScores, multiplayer);
                turn++;
            }

            if (playerScores[0] == playerScores[1])
            {
                Console.WriteLine("It's a tie!" +
                    $"\nPlayer 1 score: {playerScores[0]}" +
                    $"\nPlayer 2 score: {playerScores[1]}");
                
            }
            else
            {
                Console.WriteLine(playerScores[0] > playerScores[1] ? "Player 1 wins!" +
                                                                 $"\nPlayer 1 score: {playerScores[0]}" +
                                                                 $"\nPlayer 2 score: {playerScores[1]}"
                                                                 :
                                                                 "Player 2 wins!" +
                                                                $"\nPlayer 1 score: {playerScores[0]}" +
                                                                $"\nPlayer 2 score: {playerScores[1]}");
            }

            if (playerScores[0] > playerScores[1] && playerScores[0] > Statistics.sevensOutHighScore)
            {
                Console.WriteLine("New High Score!");
                Statistics.sevensOutHighScore = playerScores[0];
            }
            else if (playerScores[1] > playerScores[0] && playerScores[1] > Statistics.sevensOutHighScore)
            {
                Console.WriteLine("New High Score!");
                Statistics.sevensOutHighScore = playerScores[1];
            }

            Console.WriteLine("\nGame Over! Returning to menu!");
            Thread.Sleep(2000);
            Console.Clear();
            Game.Main();
        }
        private int WhichPlayer(int turn, int[] playerScore, bool multiplayer)
        {
            // UI using ANSI escape sequences
            string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
            string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
            string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green

            bool gameOver = false;
            int total = 0;
            int sum = 0;
            int i = 0;

            while (!gameOver)
            {
                Console.WriteLine("Press Enter to roll the dice...");
                
                if (turn == 1 && multiplayer == false)
                {
                    Console.WriteLine("Computer is rolling...");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.ReadLine();
                }

                i++;


                Die[] dice = new Die[2];
                for (int j = 0; j < dice.Length; j++)
                {
                    dice[j] = new Die();
                }

                foreach (Die die in dice)
                {
                    die.DieRoll();
                    Statistics.totalDiceRolled++;
                }


                if (dice[0].DieValue == dice[1].DieValue)
                {
                    Console.WriteLine($"{G}You rolled a pair! Double Points!{N}");
                    total = (dice[0].DieValue + dice[1].DieValue) * 2;

                }
                else { total = dice[0].DieValue + dice[1].DieValue; }


                Console.WriteLine($"Roll {i}: " +
                    $"\nDie 1: {dice[0].DieValue} ¦ Die 2: {dice[1].DieValue} ¦ Total: {total}");

                sum += total;
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
    }
}
