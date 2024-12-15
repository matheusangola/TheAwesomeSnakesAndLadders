using System;


namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Cell
    {
        public Boolean IsAvailable;
        public int NextCellDeltaX;
        public int NextCellDeltaY;
        public int X;
        public int Y;
        public MysteryBox MyMysteryBox;
        public Snake MySnake;
        public Ladder MyLadder;

        public Cell(Boolean isAvailable, int x, int y, int nextCellDeltaX, int nextCellDeltaY)
        {
            IsAvailable = isAvailable;
            X = x;
            Y = y;
            NextCellDeltaX = nextCellDeltaX;
            NextCellDeltaY = nextCellDeltaY;
            MyMysteryBox = null;
            MyLadder = null;
            MySnake = null;
        }
        public override string ToString()
        {
            return $"x = {X}, y = {Y}, Delta x {NextCellDeltaX}, delta y {NextCellDeltaY}";
        }
    }

}
