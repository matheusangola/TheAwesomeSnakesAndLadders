

using System.Collections.Generic;

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
        public int PinDisplayOffsetX;
        public List<int> PreviousDiceRollsList;

        public Player (string name, string color, int number, int pinDisplayOffsetX)
        {
            Name = name;
            Color = color;
            CellNumber = 0;
            Number = number;
            X = -1;
            Y = -1;
            PinDisplayOffsetX = pinDisplayOffsetX;
            PreviousDiceRollsList = new List<int>();
        }


        public string PrintPreviousDiceRollsList()
        {
            string output = "[";
            for (int i = 0; i<PreviousDiceRollsList.Count; i++)
            {
                if(i==0) output += $"{PreviousDiceRollsList[i]}";
                else {
                    output += $", {PreviousDiceRollsList[i]}";
                }
            }
            output += "]";

            return output;
        }


        public override string ToString()
        {
            return $"[Player] Name: {Name}; Number: {Number}; Color: {Color}; CellNumber: {CellNumber}; X: {X}; Y: {Y}; PinDisplayOffsetX: {PinDisplayOffsetX}; PreviousDiceRollsList: {PreviousDiceRollsList}";
        }
    }
}
