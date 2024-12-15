using System;
using System.Drawing;
using System.Windows.Forms;


namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class MysteryBox
    {
        public int Position;
        public int Destination;
        public Random R;

        public MysteryBox(FormGame formgame, Board board) 
        {
            R = new Random();
            InitializePosition(formgame, board);
            GenerateRandomDestination(formgame);
            Console.WriteLine(this);
        }

        private void InitializePosition(FormGame formgame, Board board)
        {
            int minPosition = 5;
            int maxPosition = board.Size * board.Size - 4;
            
            int newPosition;
            do
            {
                newPosition = R.Next(minPosition, maxPosition);

            } while (board.CellList[newPosition - 1].IsAvailable == false);

            Position = newPosition;

            board.CellList[newPosition - 1].IsAvailable = false;
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 4*paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile("../../Images/MysteryBox.png"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(2*paddingSize, 3*paddingSize),
                BackColor = Color.FromArgb(30, 0, 0, 0)
            };

            selectedCell.Controls.Add(pb);
            pb.BringToFront();
        }
        private void GenerateRandomDestination(FormGame formgame)
        {
            int maxMovement = 4;
            Destination = R.Next(Position - maxMovement, Position + maxMovement + 1);
            Label newLabel = new Label()
            {
                Text = $"Dest: {Destination}"
            };
            //formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Position}", false)[0].Controls.Add(newLabel);
            //newLabel.BringToFront();
        }

        public override string ToString()
        {
            return $"[MysteryBox] Dest: {Destination} Position: {Position}";
        }


    }
}
