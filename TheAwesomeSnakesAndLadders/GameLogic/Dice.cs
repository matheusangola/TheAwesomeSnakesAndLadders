using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Dice
    {
        int MaxValue;
        public int Value;
        Random R;
        public Dice(int maxValue) 
        {
            MaxValue = maxValue;
            Value = 0;

            R = new Random();
        }

        public void GenerateRandomNumber()
        {
            Value = R.Next(1, MaxValue);
        }
    }
}
