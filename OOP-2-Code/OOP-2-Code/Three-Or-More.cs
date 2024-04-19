using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_2_Code
{
    class Three_Or_More : Game
    {
        protected override void Play()
        {
            int turn = 0;
            int[] playerScores = [0, 0];

            Console.WriteLine("Now playing... Three or More!");
            Thread.Sleep(2000);
            Console.Clear();

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Player {turn + 1}:");
                playerScores[i] = WhichPlayer(turn, playerScores);
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
            Console.WriteLine("\nGame Over! Returning to menu!");
            Thread.Sleep(2000);
            Console.Clear();
            Game.Main();
        }
        private int WhichPlayer(int turn, int[]playerScore)
        {
            // UI using ANSI escape sequences
            string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
            string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
            string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green

            Die die1 = new Die();
            Die die2 = new Die();
            Die die3 = new Die();
            Die die4 = new Die();
            Die die5 = new Die();

            bool gameOver = false;
            int total = 0;
            int sum = 0;
            int i = 0;

            while (!gameOver)
            {
                Console.WriteLine("Press Enter to roll the dice...");
                Console.ReadLine();
                i++;
                die1.DieRoll();
                die2.DieRoll();
                die3.DieRoll();
                die4.DieRoll();
                die5.DieRoll();

                int[] dice = new int[] { die1.DieValue, die2.DieValue, die3.DieValue, die4.DieValue, die5.DieValue };
                var mostCommon = dice.GroupBy(x => x).OrderByDescending(x => x.Count()).First();

                Console.WriteLine($"Roll {i}: " +
                    $"\nDie 1: {die1.DieValue} ¦" +
                    $"\nDie 2: {die2.DieValue} ¦" +
                    $"\nDie 3: {die3.DieValue} ¦" +
                    $"\nDie 4: {die4.DieValue} ¦" +
                    $"\nDie 5: {die5.DieValue} ¦ Total: {sum}");


                switch (mostCommon.Count())
                {
                    case 2:
                        while (true)
                        {
                            Console.WriteLine($"You got a {R}Two of a kind!{N}" +
                                          $"\nWould you like to re-roll all the dice or just the other 3? " +
                                          $"\nA for all, R for remainder: ");
                            var choice = Console.ReadLine();
                            if (choice.ToUpper() == "A") { break; }
                            else if (choice.ToUpper() == "R")
                            {
                                for (int j = 2; j < 3; j++)
                                {
                                    Console.WriteLine($"Re-rolling die {j + 1}...");
                                    dice[dice[j]] = die1.DieRoll();
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter A or R.");
                            }
                        }
                        
                    break;
                    case 3:
                        Console.WriteLine($"You got a {G}Three of a kind!" +
                                          $"\n+3 Points!{N}");
                        sum += 3;
                        break;
                    case 4:
                        Console.WriteLine($"You got a {G}Four of a kind!" +
                                          $"\n+6 Points!{N}");
                        sum += 6;
                        break;
                    case 5:
                        Console.WriteLine($"You got a {G}Five of a kind!" +
                                          $"\n+12 Points!{N}");
                        sum += 12;
                        break;
                    default:
                        Console.WriteLine("No matches. No points.");
                        break;
                }

                
                if (sum >= 21)
                {
                    Console.WriteLine($"You scored {sum}! in {i} rolls!");
                    playerScore[turn] = sum;
                    gameOver = true;
                }
            }
            return playerScore[turn];
        }
    }
}
