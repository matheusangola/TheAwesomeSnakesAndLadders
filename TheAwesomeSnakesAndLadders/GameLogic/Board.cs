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
        public Dictionary<int, int> Snakes { get; set; }
        public Dictionary<int, int> Ladders { get; set; }

        public Board(int size)
        {
            Size = size;
            Snakes = new Dictionary<int, int>();
            Ladders = new Dictionary<int, int>();
            InitializeSnakesAndLadders();
        }

        private void InitializeSnakesAndLadders()
        {
            // initialization for a 5x5 board
            if (Size == 5)
            {
                Snakes.Add(14, 7); // Snake from position 14 to 7
                Snakes.Add(23, 5); // Snake from position 23 to 5

                Ladders.Add(3, 12); // Ladder from position 3 to 12
                Ladders.Add(8, 19); // Ladder from position 8 to 19
            }
            else if (Size == 8)
            {
                Snakes.Add(45, 23);
                Snakes.Add(64, 49);

                Ladders.Add(10, 27);
                Ladders.Add(36, 58);
            }
            else if (Size == 10)
            {
                Snakes.Add(99, 77);
                Snakes.Add(88, 66);

                Ladders.Add(2, 38);
                Ladders.Add(41, 73);
            }
        }
    }
}
