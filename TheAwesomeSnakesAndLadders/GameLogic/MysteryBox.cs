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
    internal class MysteryBox
    {
        public int Position;
        public int Destination;
        public MysteryBox(FormGame formgame, Board board) 
        {
            InitializePosition(formgame, board);
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

            } while (board.AvailableSpots[newPosition - 1] == false);

            board.AvailableSpots[newPosition - 1] = false;
            int newSize = formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0].Size.Width - 20;
            //PictureBox pb = new PictureBox()
            //{
            //    Image = Image.FromFile("../../Images/MysteryBox.jpg"),
            //    Size = new Size(newSize, newSize),
            //    SizeMode = PictureBoxSizeMode.Zoom   

            //};

            //formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0].Controls.Add(pb);
            //formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0].

            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0].BackgroundImage = Image.FromFile("../../Images/MysteryBox.jpg");
            formgame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell{newPosition}", false)[0].BackgroundImageLayout = ImageLayout.Zoom;

        }
    }
}
