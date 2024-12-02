using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Dice
    {
        public static readonly Random RandomGenerator = new Random();

        public int Sides { get; private set; }

        public Dice(int sides = 6)
        {
            Sides = sides;
        }

        public int Roll()
        {
            return RandomGenerator.Next(1, Sides + 1); // Returns a value between 1 and Sides (inclusive)
        }
    }
}