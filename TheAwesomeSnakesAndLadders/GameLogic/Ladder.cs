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
        public float LadderLength;

        public Ladder(FormGame formgame, Board board)
        {
            InitializeBottom(formgame, board);
            InitializeTop(formgame, board);
            CalculateLadderLength(formgame, board);
            GenerateImage(formgame, board);
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

        }

        private void GenerateImage(FormGame formgame, Board board)
        {

            /////////criar novo painel OU renderizar painel de novo/////////
            //PictureBox pb = new PictureBox() {
            //    Image = new Bitmap("../../Images/Ladder1.png"),
            //    Size = new Size(500, 500),
            //    SizeMode = PictureBoxSizeMode.Zoom,
            //    Location = new Point(0, 0),
            //    BackColor = Color.Transparent
            //};
            //pb.Paint += OnPaint;
            //formgame.Controls.Find("boardPanel", false)[0].Controls.Add(pb);
            //pb.BringToFront();
            //pb.Invalidate();
            //pb.Refresh();



            Panel selectedBoardPanel = (Panel)formgame.Controls.Find("boardPanel", false)[0];
            selectedBoardPanel.Paint += OnPaint;

            selectedBoardPanel.Invalidate();
            selectedBoardPanel.Refresh();
        }


        protected void OnPaint(object sender, PaintEventArgs e)
        {
            var ladder = new Bitmap("../../Images/Ladder1.png");
            e.Graphics.RotateTransform(20.0F);
            e.Graphics.DrawImage(ladder, 800, 400, 150, 150);
            e.Graphics.Save();
            Console.WriteLine("OnPaint Triggered");
        }


        public override string ToString()
        {
            return $"[Ladder] Top: {Top}; Bottom: {Bottom}; LadderLength: {LadderLength}";
        } 
    }
}
