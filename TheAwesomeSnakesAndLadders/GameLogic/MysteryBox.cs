using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class MysteryBox
    {
        public int Position;
        public int Destination;
        public MysteryBox(FormGame formgame, Board board) 
        {
            InitializePosition(formgame, board);
            GenerateRandomDestination(formgame);
            Console.WriteLine(this);
        }

        private void InitializePosition(FormGame formgame, Board board)
        {
            int minPosition = 5;
            int maxPosition = board.Size * board.Size - 4;

            Random r = new Random();
            int newPosition;
            do
            {
                newPosition = r.Next(minPosition, maxPosition);

            } while (board.CellList[newPosition - 1].IsAvailable == false);

            Position = newPosition;

            board.CellList[newPosition - 1].IsAvailable = false;
            Panel selectedCell = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0] as Panel;
            int paddingSize = 10;
            int newSize = selectedCell.Size.Width - 2*paddingSize;
            PictureBox pb = new PictureBox()
            {
                Image = Image.FromFile("../../Images/MysteryBox.jpg"),
                Size = new Size(newSize, newSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(paddingSize, paddingSize),


            };

            selectedCell.Controls.Add(pb);
            pb.BringToFront();
            //selectedCell.Controls.Find($"label{newPosition}", false)[0].BringToFront();
            
        }
        private void GenerateRandomDestination(FormGame formgame)
        {
            Random r = new Random();
            int maxMovement = 4;
            Destination = r.Next(Position - maxMovement, Position + maxMovement + 1);
            Label newLabel = new Label()
            {
                Text = $"Dest: {Destination}"
            };
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{Position}", false)[0].Controls.Add(newLabel);
            newLabel.BringToFront();
        }

        public override string ToString()
        {
            return $"Dest: {Destination} Position: {Position}";
        }


    }
}
