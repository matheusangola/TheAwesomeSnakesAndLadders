using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Board
    {
        
            public int Size { get; set; }

        public Board(string gameDificulty)
        {
            if (gameDificulty == "Easy")
            {
                Size = 7;
            } else if (gameDificulty == "Medium")
            {
                Size = 10;
            } else
            {
                Size = 12;
            }

        }

    }
}
