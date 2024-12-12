using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Player
    {
        public string Name;
        public int Number;
        public string Color;
        public int CellNumber;
        public int X;
        public int Y;

        public Player (string name, string color, int number)
        {
            Name = name;
            Color = color;
            CellNumber = 0;
            Number = number;
            X = -1;
            Y = -1;
        }

        public override string ToString()
        {
            return $"{Name} ({Color})";
        }
    }
}
