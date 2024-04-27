using System;
using System.Diagnostics;

namespace OOP_2_Code
{
	class Testing
	{

		public static void RunTests()
		{
            Console.WriteLine("Running tests...");
			Console.WriteLine("Testing DieRoll() method in Die class...");

            // tests the die
            Die Die = new Die();
            int TestRollSum = 0;
            for (int i = 1; i < 1000; i++)
            {
                int TestRollValue = Die.DieRoll();
                TestRollSum += TestRollValue;
                Debug.Assert(TestRollValue >= 1 && TestRollValue <= 6, "Die roll is out of range");
                Debug.Assert(TestRollSum >= i && TestRollSum <= (6 * i), $"Die roll sum is out of range, roll; {TestRollValue} sum: {TestRollSum}");

            }
            Console.WriteLine("Die Tests Complete...");
            Thread.Sleep(1000);

            // tests sevens out
            Sevens_Out sevensOut = new Sevens_Out();
			sevensOut.Test();
            Console.WriteLine("Sevens Out Tests Complete...");
            Thread.Sleep(1000);

            // tests three or more
			Three_Or_More threeOrMore = new Three_Or_More();
			threeOrMore.Test();
            Console.WriteLine("Three or More Tests Complete...");
            Thread.Sleep(1000);
        }
	}
}