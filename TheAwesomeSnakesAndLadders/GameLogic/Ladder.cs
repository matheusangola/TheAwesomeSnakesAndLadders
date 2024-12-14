using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Ladder
    {
        public int Top;
        public int Bottom;
        public double LadderLength;
        public double LadderAngle;
        public int BottomX;
        public int BottomY;
        public int TopX;
        public int TopY;

        public Ladder(FormGame formgame, Board board)
        {
            InitializeBottom(formgame, board);
            InitializeTop(formgame, board);
            CalculateLadderLength(formgame, board);
            Console.WriteLine(this);
            
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

            } while (board.CellList[newBottom - 1].IsAvailable == false);

            Bottom = newBottom;

            board.CellList[newBottom - 1].IsAvailable = false;
            
        }

        private void InitializeTop(FormGame formgame, Board board)
        {
            int minTop = Bottom + board.Size -3;
            int maxTop = board.Size * board.Size -3;

            Random r = new Random();
            int newTop;
            do
            {
                newTop = r.Next(minTop, maxTop);

            } while (board.CellList[newTop - 1].IsAvailable == false);

            Top = newTop;

            board.CellList[newTop - 1].IsAvailable = false;
        }

        

        private void CalculateLadderLength(FormGame formgame, Board board)
        {
            int bottomX = 0;
            int bottomY = 0;
            int topX = 0;
            int topY = 0;
            for (int i = 0; i < Bottom-1; i++) {
                bottomX += board.CellList[i].NextCellDeltaX;
                bottomY += board.CellList[i].NextCellDeltaY;
            }
            for (int i = 0; i < Top-1; i++) {
                topX += board.CellList[i].NextCellDeltaX;
                topY += board.CellList[i].NextCellDeltaY;
            }

            BottomX = bottomX;
            BottomY = bottomY;
            TopX = topX;
            TopY = topY;

            double deltaX = (double)TopX - (double)BottomX;
            double deltaY = (double)TopY - (double)BottomY;

            LadderLength = Math.Pow(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2), 0.5);

            LadderAngle = Math.Atan2(deltaX, deltaY)*180/Math.PI;
        }





        public override string ToString()
        {
            return $"[Ladder] Top: {Top}; Bottom: {Bottom}; LadderLength: {LadderLength}; LadderAngle: {LadderAngle}; BottomX: {BottomX}; BottomY: {BottomY}; TopX: {TopX}; TopY: {TopY}";
        } 
    }
}
