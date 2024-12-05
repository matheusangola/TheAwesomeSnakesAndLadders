using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Board
    {
        
        public int Size { get; set; }

        public int LadderQuantity { get; set; }
        public int SnakeQuantity { get; set; }
        public int MysteryBoxQuantity { get; set; }
        public List<Snake> SnakeList { get; set; }
        public List<Ladder> LadderList { get; set; }
        public List<MysteryBox> MysteryBoxList { get; set; }

        public List<Boolean> AvailableSpots { get; set; }



        public Board(string gameDificulty, FormGame formgame)
        {
            if (gameDificulty == "Easy")
            {
                Size = 5;
                SnakeQuantity = 4;
                LadderQuantity = 4;
                MysteryBoxQuantity = 4;
            } else if (gameDificulty == "Medium")
            {
                Size = 8;
                SnakeQuantity = 7;
                LadderQuantity = 7;
                MysteryBoxQuantity = 7;
            } else
            {
                Size = 12;
                SnakeQuantity = 11;
                LadderQuantity = 11;
                MysteryBoxQuantity = 11;
            }

            CreateAvailableSpots();
            CreateBoardGrid(formgame);
            CreateLadders(formgame);
            CreateSnakes(formgame);
            CreateMysteryBoxes(formgame);

        }

        private void CreateBoardGrid(FormGame formgame)
        {
            
            Panel boardPanel = new System.Windows.Forms.Panel()
            {
                    Location = new Point(350, 100),
                    Size = new System.Drawing.Size(800, 800),
                    Name = "boardPanel"
            };
            boardPanel.Controls.Clear();
            int cellSize = boardPanel.Width / Size;
            int totalCells = Size * Size;

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    int cellNumber;

                    if (row % 2 == 0)
                    {
                        cellNumber = totalCells - (row * Size + col);
                    }
                    // Not using this, but if needed we can adjust it here. Order of rows
                    else
                    {
                        cellNumber = totalCells - (row * Size + (Size - col - 1));
                    }

                    // Create a new label with number for each cell
                    Label cell = new Label
                    {
                        Text = cellNumber.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        Name = $"cell{cellNumber}"
                    };

                    // Alternating colors
                    //// Maybe we can add a choice for the user? ////
                    if ((row + col) % 2 == 0)
                    {
                        cell.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        cell.BackColor = Color.LightGreen;
                    }

                    // Set location within boardPanel
                    cell.Location = new Point(col * cellSize, row * cellSize);

                    // Add the cell to the boardPanel
                    boardPanel.Controls.Add(cell);
                }
            }
            formgame.Controls.Add(boardPanel);
        }

        private void CreateSnakes(FormGame formgame)
        {

        }

        private void CreateLadders(FormGame formgame)
        {

        }

        private void CreateMysteryBoxes(FormGame formgame)
        {
            MysteryBoxList = new List<MysteryBox>();
            for (int i = 1; i <= MysteryBoxQuantity; i++)
            {
                MysteryBox newMysteryBox = new MysteryBox(formgame, this);
                MysteryBoxList.Add(newMysteryBox);
            }
        }

        private void CreateAvailableSpots()
        {
            AvailableSpots = new List<bool>();
            for (int i = 0; i < Size*Size; i++)
            {
                AvailableSpots.Add(true);
            }
        }
    }
}
