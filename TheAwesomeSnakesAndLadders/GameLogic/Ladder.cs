using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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

        }

        private void GenerateImage(FormGame formgame, Board board)
        {
            ///////criar novo painel OU renderizar painel de novo/////////
            PictureBox pb = new PictureBox()
            {
                //Image = Image.FromFile("../../Images/Ladder.jpg"),
                Size = new Size(500, 500),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(0, 0),
            };
            pb.Paint += OnPaint;
            formgame.Controls.Find("boardPanel", false)[0].Controls.Add(pb);
            pb.BringToFront();
        }

        protected void OnPaint(object sender, PaintEventArgs e)
        {
            var ladder = new Bitmap("../../Images/Ladder.jpg");
            e.Graphics.RotateTransform(20.0F);

            e.Graphics.DrawImage(ladder, 160, 160, 150, 150);
        }


    }
}
