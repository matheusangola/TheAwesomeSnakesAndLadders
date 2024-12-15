using System;
using System.Drawing;
using System.Windows.Forms;


namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Snake
    {
        public int Head;
        public int Tail;
        public string Color;
        public double SnakeLength;
        public double SnakeAngle;
        public int TailX;
        public int TailY;
        public int HeadX;
        public int HeadY;


        public Snake(string color, FormGame formgame, Board board)
        {
            Color = color;
            InitializeHead(formgame, board);
            InitializeTail(formgame, board);
            CalculateSnakeLength(formgame, board);
            Console.WriteLine(this);
        }


        private void InitializeHead(FormGame formgame, Board board)
        {
            //Calculate NewHead
            int minHead = board.Size + 1;
            int maxHead = board.Size * board.Size -3;

            Random r = new Random();

            int newHead;
            do
            {
                newHead = r.Next(minHead, maxHead);
            } while (board.CellList[newHead - 1].IsAvailable == false);

            Head = newHead;

            //Calculate HeadX and HeadY
            int headX = 0;
            int headY = 0;

            for (int i = 0; i < newHead-1; i++)
            {
                headX += board.CellList[i].NextCellDeltaX;
                headY += board.CellList[i].NextCellDeltaY;
            }

            HeadX = headX;
            HeadY = headY;

            board.CellList[newHead - 1].IsAvailable = false;

            //Create Image
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newHead}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 2 * paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile($"../../Images/Snakes/{Color}SnakeHead.png"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(paddingSize, paddingSize*2),
            };
            selectedCell.Controls.Add(pb);
            pb.BringToFront();

            
        }


        private void InitializeTail(FormGame formgame, Board board)
        {
            //Calculate Newtail
            int minTail = 1;
            int maxTail = board.Size * board.Size - board.Size + 1;

            Random r = new Random();

            int newTail;
            do
            {
                newTail = r.Next(minTail, maxTail);

                //Calculate BottomX and BottomY
                int tailX = 0;
                int tailY = 0;

                for (int i = 0; i < newTail-1; i++)
                {
                    tailX += board.CellList[i].NextCellDeltaX;
                    tailY += board.CellList[i].NextCellDeltaY;
                }

                TailX = tailX;
                TailY = tailY;
            } while (board.CellList[newTail - 1].IsAvailable == false || TailY >= HeadY);

            Tail = newTail;
            board.CellList[newTail - 1].IsAvailable = false;

            //Create Image
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newTail}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 2 * paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile($"../../Images/Snakes/{Color}SnakeTail.png"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(3*paddingSize, -5),
            };
            selectedCell.Controls.Add(pb);
            pb.BringToFront();

            //Create Destination Label at Head
            Label newLabel = new Label()
            {
                Text = $"Dest [TAIL]: {Tail}"
            };
            //formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Head}", false)[0].Controls.Add(newLabel);
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Head}", false)[0].Controls.Find($"label{Head}", false)[0].BringToFront();
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Tail}", false)[0].Controls.Find($"label{Tail}", false)[0].BringToFront();
            //newLabel.BringToFront();
        }


        


        private void CalculateSnakeLength(FormGame formgame, Board board)
        {
            double deltaX = (double)HeadX - (double)TailX;
            double deltaY = (double)HeadY - (double)TailY;

            SnakeLength = Math.Pow(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2), 0.5);

            SnakeAngle = Math.Atan2(deltaX, deltaY)*180/Math.PI;
        }


        public override string ToString()
        {
            return $"[Snake] Head: {Head}; Tail: {Tail}; Color: {Color}; SnakeLength: {SnakeLength}; SnakeAngle: {SnakeAngle}; TailX: {TailX}; tailY: {TailY}; HeadX: {HeadX}; HeadY: {HeadY}";
        } 
    }
}
