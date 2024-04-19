using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP_2_Code
{
    internal class Three_Or_More : Game
    {
        protected override void Play()
        {
            // UI using ANSI escape sequences
            string N = Console.IsOutputRedirected ? "" : "\x1b[39m"; //reset color
            string R = Console.IsOutputRedirected ? "" : "\x1b[91m"; //red
            string G = Console.IsOutputRedirected ? "" : "\x1b[92m"; //green

            Console.WriteLine("Now playing... Three or More!");
            Thread.Sleep(2000);
            Console.Clear();

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

                if (die1.DieValue == die2.DieValue && die1.DieValue == die3.DieValue && die1.DieValue == die4.DieValue && die1.DieValue == die5.DieValue)
                {
                    Console.WriteLine($"{G}You rolled a five of a kind! Double Points!{N}");
                    total = (die1.DieValue + die2.DieValue + die3.DieValue + die4.DieValue + die5.DieValue) * 2;

                }
                else { total = die1.DieValue + die2.DieValue + die3.DieValue + die4.DieValue + die5.DieValue; }

                Console.WriteLine($"Roll {i}: " +
                    $"\nDie 1: {die1.DieValue} ¦" +
                    $"\nDie 2: {die2.DieValue} ¦" +
                    $"\nDie 3: {die3.DieValue} ¦" +
                    $"\nDie 4: {die4.DieValue} ¦" +
                    $"\nDie 5: {die5.DieValue} ¦ Total: {total}");
            }

        }
        private void WhichPlayer(int turn)
        {

        }
    }
}
