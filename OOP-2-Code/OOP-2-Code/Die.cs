using System;

namespace OOP_2_Code
{
    /// <summary>
    /// This class should contain one property to hold the current die value,
    /// and one method that rolls the die, returns and integer and takes no parameters.
    /// </summary>
    /// <returns></returns>
    class Die
    {

        public int DieValue { get; set; }
        static Random rnd = new Random();

        //Method
        public int DieRoll()
        {
            DieValue = rnd.Next(1, 7);
            return DieValue;
        }
    }
}
