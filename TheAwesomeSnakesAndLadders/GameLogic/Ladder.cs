using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Ladder
    {
        public int Top;
        public int Bottom;
        public string Color;
        public double LadderLength;
        public double LadderAngle;
        public int BottomX;
        public int BottomY;
        public int TopX;
        public int TopY;


        public Ladder(string color, FormGame formgame, Board board)
        {
            Color = color;
            InitializeBottom(formgame, board);
            InitializeTop(formgame, board);
            CalculateLadderLength(formgame, board);
            Console.WriteLine(this);
        }


        private void InitializeBottom(FormGame formgame, Board board)
        {
            //Calculate NewBottom
            int minBottom = 2;
            int maxBottom = board.Size * board.Size - board.Size + 1;

            Random r = new Random();

            int newBottom;
            do
            {
                newBottom = r.Next(minBottom, maxBottom);
            } while (board.CellList[newBottom - 1].IsAvailable == false);

            Bottom = newBottom;

            board.CellList[newBottom - 1].IsAvailable = false;
            
            //Calculate BottomX and BottomY
            int bottomX = 0;
            int bottomY = 0;

            for (int i = 0; i < Bottom-1; i++)
            {
                bottomX += board.CellList[i].NextCellDeltaX;
                bottomY += board.CellList[i].NextCellDeltaY;
            }

            BottomX = bottomX;
            BottomY = bottomY;

            //Create Image
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newBottom}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 2 * paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile($"../../Images/Ladders/{Color}LadderBottom.png"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(3*paddingSize, 0),
            };
            selectedCell.Controls.Add(pb);
            pb.BringToFront();
        }


        private void InitializeTop(FormGame formgame, Board board)
        {
            //Calculate NewTop
            int minTop = Bottom + 1;
            int maxTop = board.Size * board.Size -3;

            Random r = new Random();

            int newTop;
            do
            {
                newTop = r.Next(minTop, maxTop);

                //Calculate TopX and TopY
                int topX = 0;
                int topY = 0;

                for (int i = 0; i < newTop-1; i++)
                {
                    topX += board.CellList[i].NextCellDeltaX;
                    topY += board.CellList[i].NextCellDeltaY;
                }

                TopX = topX;
                TopY = topY;

            } while (board.CellList[newTop - 1].IsAvailable == false || TopY <= BottomY);

            Top = newTop;

            board.CellList[newTop - 1].IsAvailable = false;

            //Create Image
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newTop}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 2 * paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile($"../../Images/Ladders/{Color}LadderTop.png"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(paddingSize, paddingSize*2),
            };
            selectedCell.Controls.Add(pb);
            pb.BringToFront();

            //Create Destination Label at Bottom
            Label newLabel = new Label()
            {
                Text = $"Dest [TOP]: {Top}"
            };
            //formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Bottom}", false)[0].Controls.Add(newLabel);
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Bottom}", false)[0].Controls.Find($"label{Bottom}", false)[0].BringToFront();
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Top}", false)[0].Controls.Find($"label{Top}", false)[0].BringToFront();
            //newLabel.BringToFront();
        }


        private void CalculateLadderLength(FormGame formgame, Board board)
        {
            double deltaX = (double)TopX - (double)BottomX;
            double deltaY = (double)TopY - (double)BottomY;

            LadderLength = Math.Pow(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2), 0.5);

            LadderAngle = Math.Atan2(deltaX, deltaY)*180/Math.PI;
        }


        public override string ToString()
        {
            return $"[Ladder] Top: {Top}; Bottom: {Bottom}; Color: {Color}; LadderLength: {LadderLength}; LadderAngle: {LadderAngle}; BottomX: {BottomX}; BottomY: {BottomY}; TopX: {TopX}; TopY: {TopY}";
        } 
    }
}
