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
            int[] playerRolls = [0, 0];
            bool multiplayer = false;
            int highscore;

            Console.WriteLine("Now playing... Three or More!");
            Statistics.threeOrMoreGamesPlayed++;
            Thread.Sleep(2000);
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Would you like to play against another player or the computer? P/C" +
                    "\nThe computer will always reroll the remaining" +
                    "\nLowest rolls to get to 20 points wins");
                var choice = Console.ReadLine();
                if (choice.ToUpper() == "P")
                {
                    multiplayer = true;
                    break;
                }
                else if (choice.ToUpper() == "C")
                {
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
                playerRolls[i] = WhichPlayer(turn, playerRolls, multiplayer);
                turn++;
            }

            if (playerRolls[0] == playerRolls[1])
            {
                Console.WriteLine("It's a tie!" +
                    $"\nPlayer 1 rolls: {playerRolls[0]}" +
                    $"\nPlayer 2 rolss: {playerRolls[1]}");
            }
            else
            {
                Console.WriteLine(playerRolls[0] > playerRolls[1] ? "Player 1 wins!" +
                                                                 $"\nPlayer 1 rolls: {playerRolls[0]}" +
                                                                 $"\nPlayer 2 rolls: {playerRolls[1]}"
                                                                 :
                                                                 "Player 2 wins!" +
                                                                $"\nPlayer 1 rolls: {playerRolls[0]}" +
                                                                $"\nPlayer 2 rolls: {playerRolls[1]}");
            }
            if (playerRolls[0] < playerRolls[1])
            {
                highscore =- playerRolls[0];
            }
            else
            {
                highscore =- playerRolls[1];
            }
            if (highscore < Statistics.threeOrMoreHighScore)
            {
                Console.WriteLine("New High Score!");
                Statistics.threeOrMoreHighScore = highscore;
            }
            
            
            Console.WriteLine("\nGame Over! Returning to menu!");
            Thread.Sleep(2000);
            Console.Clear();
            Game.Main();
        }

        private Die[] rerollRemaining(Die[] dice)
        {

            var mostCommon = dice.GroupBy(d => d.DieValue).OrderByDescending(g => g.Count()).First().Key;
            foreach (Die die in dice)
            {
                if (die.DieValue != mostCommon)
                {
                    die.DieRoll();
                    Statistics.totalDiceRolled++;
                }
            }
            return dice;
        }

        private int WhichPlayer(int turn, int[]playerRolls, bool multiplayer)
        {
            // UI using ANSI escape sequences
            string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
            string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
            string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green

            bool gameOver = false;
            int sum = 0;
            int i = 0;
            

            while (!gameOver)
            {
                Console.WriteLine("Press Enter to roll the dice...");

                if (turn == 1 && multiplayer == false)
                {
                    Console.WriteLine("Computer is rolling... \n");
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.ReadLine();
                }

                i++;
                Die[] dice = new Die[5];
                for (int j = 0; j < dice.Length; j++)
                {
                    dice[j] = new Die();
                }
                foreach (Die die in dice)
                {
                    die.DieRoll();
                    Statistics.totalDiceRolled++;
                }

                var ofAKind = dice.GroupBy(d => d.DieValue).Max(g => g.Count());

                string displayGame = $"Roll {i}: " +
                                     $"\nDie 1: {dice[0].DieValue} ¦" +
                                     $"\nDie 2: {dice[1].DieValue} ¦" +
                                     $"\nDie 3: {dice[2].DieValue} ¦" +
                                     $"\nDie 4: {dice[3].DieValue} ¦" +
                                     $"\nDie 5: {dice[4].DieValue} ¦ Total: {sum}";

                Console.WriteLine(displayGame);

                while (ofAKind == 2)
                {
                    Console.WriteLine($"You got a {R}Two of a kind!{N}" +
                                      "\nWould you like to reroll all of the remaining? (A/R)");
                    while (true)
                    {
                        if (turn == 1 && multiplayer == false)
                        {
                            Console.WriteLine("Computer is rerolling the remaining...\n");
                            Thread.Sleep(1500);
                            dice = rerollRemaining(dice);
                            ofAKind = dice.GroupBy(d => d.DieValue).Max(g => g.Count());
                            i++;

                            Console.WriteLine($"Roll {i}: ");
                            foreach (Die die in dice)
                            {
                                Console.WriteLine($"Die {Array.IndexOf(dice, die) + 1}: {die.DieValue}");
                            }

                            break;
                        }
                        else
                        {
                            var choice = Console.ReadLine();
                            if (choice.ToUpper() == "A")
                            {
                                foreach (Die die in dice)
                                {
                                    die.DieRoll();
                                }
                                
                                ofAKind = dice.GroupBy(d => d.DieValue).Max(g => g.Count());
                                i++;

                                Console.WriteLine($"Roll {i}: ");
                                foreach (Die die in dice)
                                {
                                    Console.WriteLine($"Die {Array.IndexOf(dice, die) + 1}: {die.DieValue}");
                                }

                                break;
                            }
                            else if (choice.ToUpper() == "R")
                            {
                                dice = rerollRemaining(dice);
                                ofAKind = dice.GroupBy(d => d.DieValue).Max(g => g.Count());
                                i++;

                                Console.WriteLine($"Roll {i}: ");
                                foreach (Die die in dice)
                                {
                                    Console.WriteLine($"Die {Array.IndexOf(dice, die) + 1}: {die.DieValue}");
                                }

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter A or R.");
                            }
                        }  
                    }
                }


                switch (ofAKind)
                {
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

                
                if (sum >= 20)
                {
                    Console.WriteLine($"You scored {sum}! in {i} rolls!");
                    playerRolls[turn] = i;
                    gameOver = true;
                }
            }
            return playerRolls[turn];
        }
    }
}
