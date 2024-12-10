using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Cell
    {
        public Boolean IsAvailable;
        public int NextCellDeltaX;
        public int NextCellDeltaY;
        public int X;
        public int Y;

        public Cell(Boolean isAvailable, int x, int y, int nextCellDeltaX, int nextCellDeltaY)
        {
            IsAvailable = isAvailable;
            X = x;
            Y = y;
            NextCellDeltaX = nextCellDeltaX;
            NextCellDeltaY = nextCellDeltaY;
        }
        public override string ToString()
        {
            return $"x = {X}, y = {Y}, Delta x {NextCellDeltaX}, delta y {NextCellDeltaY}";
        }
    }

}
