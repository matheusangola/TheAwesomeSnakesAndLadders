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

        public Cell(Boolean isAvailable, int nextCellDeltaX, int nextCellDeltaY)
        {
            IsAvailable = isAvailable;
            NextCellDeltaX = nextCellDeltaX;
            NextCellDeltaY = nextCellDeltaY;
        }
        public override string ToString()
        {
            return $"Delta x {NextCellDeltaX}, delta y {NextCellDeltaY}";
        }
    }

}
