using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Ladder
    {
        int Top;
        int Bottom;
        float LadderLength;

        public Ladder(FormGame formgame, Board board)
        {
            InitializeBottom(formgame, board);
            InitializeTop(formgame, board);
            CalculateLadderLength(formgame, board);
            GenerateImage(formgame, board);
            
        }

        private void InitializeBottom(FormGame formgame, Board board)
        {
            int minBottom = 2;
            int maxBottom = board.Size * board.Size - board.Size;

            Random r = new Random();
            int newBottom;
            do
            {
                newBottom = r.Next(minBottom, maxBottom);

            } while (board.AvailableSpots[newBottom - 1] == false);

            Bottom = newBottom;

            board.AvailableSpots[newBottom - 1] = false;
            
        }

        private void InitializeTop(FormGame formgame, Board board)
        {
            int minTop = Bottom + board.Size;
            int maxTop = board.Size * board.Size - 5;

            Random r = new Random();
            int newTop;
            do
            {
                newTop = r.Next(minTop, maxTop);

            } while (board.AvailableSpots[newTop - 1] == false);

            Top = newTop;

            board.AvailableSpots[newTop - 1] = false;
        }

        

        private void CalculateLadderLength(FormGame formgame, Board board)
        {

        }

        private void GenerateImage(FormGame formgame, Board board)
        {

        }


    }
}
