using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Image = System.Drawing.Image;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    public class Board
    {
        
        public int Size { get; set; }

        public int LadderQuantity { get; set; }
        public int SnakeQuantity { get; set; }
        public int MysteryBoxQuantity { get; set; }
        public List<Snake> SnakeList { get; set; }
        public List<Ladder> LadderList { get; set; }
        public List<MysteryBox> MysteryBoxList { get; set; }

        public List<Cell> CellList { get; set; }

        public FormGame MyFormGame { get; set; }

        public List<Player> PlayerList { get; set; }
        public int FontSize;



        public Board(string gameDificulty, List<Player> playerList, FormGame formgame)
        {
            MyFormGame = formgame;
            PlayerList = playerList;
            if (gameDificulty == "Easy")
            {
                Size = 6;
                SnakeQuantity = 4;
                LadderQuantity = 4;
                MysteryBoxQuantity = 4;
                FontSize = 25;
            } else if (gameDificulty == "Medium")
            {
                Size = 8;
                SnakeQuantity = 7;
                LadderQuantity = 7;
                MysteryBoxQuantity = 7;
                FontSize = 20;
            }
            else
            {
                Size = 10;
                SnakeQuantity = 11;
                LadderQuantity = 11;
                MysteryBoxQuantity = 11;
                FontSize = 15;
            }

            CreateCells();
            CreateBoardGrid();
            CreateMysteryBoxes();
            CreateLadders();
            CreateSnakes();
            CreatePlayerPin();
        }

        private void CreatePlayerPin()
        {
            int pinSize = 3 * FontSize;
            for (int i = 0; i < PlayerList.Count; i++)
            {
                PictureBox pb = new PictureBox()
                {
                    Name = $"playerPin{i+1}",
                    Image = Image.FromFile($"../../Images/Pin{PlayerList[i].Color}.png"),
                    Size = new Size(pinSize, pinSize),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(10+pinSize*i, 810),
                    BackColor = Color.Transparent,
                };
                MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Add(pb);
                pb.BackColor = Color.Transparent;
            }
           
        }

        private void CreateBoardGrid()
        {
            
            Panel boardPanel = new System.Windows.Forms.Panel()
            {
                    Location = new Point(350, 30),
                    Size = new System.Drawing.Size(800, 900),
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

                    // Create a new panel with number for each cell
                    Panel newPanel = new Panel()
                    {
                        Name = $"cell{cellNumber}",
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(10,10,10,10)
                    };

                    

                    // Alternating colors
                    //// Maybe we can add a choice for the user? ////
                    if ((row + col) % 2 == 0)
                    {
                        newPanel.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        newPanel.BackColor = Color.LightGreen;
                    }

                    // Set location within boardPanel
                    newPanel.Location = new Point(col * cellSize, row * cellSize);

                    // Add the cell to the boardPanel
                    boardPanel.Controls.Add(newPanel);

                    // Create a new label with number for each cell

                    Label newLabel = new Label
                    {
                        Text = cellNumber.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", FontSize),
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.Transparent,
                        Name = $"label{cellNumber}"
                    };
                   
                    newPanel.Controls.Add(newLabel);



                }
            }
            MyFormGame.Controls.Add(boardPanel);
        }

        private void CreateSnakes()
        {

        }



        private void CreateLadders()
        {
            Ladder newLadder = new Ladder(MyFormGame, this);
        }

        private void CreateMysteryBoxes()
        {
            int mysteryBoxPosition;
            MysteryBoxList = new List<MysteryBox>();
            for (int i = 1; i <= MysteryBoxQuantity; i++)
            {
                MysteryBox newMysteryBox = new MysteryBox(MyFormGame, this);
                MysteryBoxList.Add(newMysteryBox);
                mysteryBoxPosition = newMysteryBox.Position;
                CellList[mysteryBoxPosition - 1].MyMysteryBox = newMysteryBox;
            }
        }

        private void CreateCells()
        {
            int newDeltaX = 1;
            int newDeltaY = 0;
            int newX = 0;
            int newY = 0;
            int row = 1;
            CellList = new List<Cell>();
            for (int i = 1; i <= Size*Size; i++)
            {
                if (i % Size == 0)
                {
                    newDeltaX = 0;
                    newDeltaY = 1;
                    row++;
                }
                else if (row % 2 == 1)
                {
                    newDeltaX = 1;
                    newDeltaY = 0;
                } else if (row % 2 == 0)
                {
                    newDeltaX = -1;
                    newDeltaY = 0;
                }
                Cell newCell = new Cell(true, newX, newY, newDeltaX, newDeltaY);
                CellList.Add(newCell);
                newX += newDeltaX;
                newY += newDeltaY;
                Console.WriteLine($"cellnumber = {i}, {newCell}");
            }
        }

        
    }
}
