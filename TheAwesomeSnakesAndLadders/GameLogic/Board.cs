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
using System.Drawing.Imaging;

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
                SnakeQuantity = 5;
                LadderQuantity = 5;
                MysteryBoxQuantity = 5;
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

            CreateListCells();
            CreateBoardGrid();
            CreateMysteryBoxes();
            CreateSnakes();
            CreatePlayerPin();
            CreateLadders();

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
                        //lightblue
                        newPanel.BackColor = Color.FromArgb(80,0,0,100);
                    }
                    else
                    {
                        //lightgreen
                        newPanel.BackColor = Color.FromArgb(80,0,100,0);
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
            int ladderBottom;
            LadderList = new List<Ladder>();
            for (int i = 1; i <= LadderQuantity; i++)
            {
                Ladder newLadder = new Ladder(MyFormGame, this);
                LadderList.Add(newLadder);
                ladderBottom = newLadder.Bottom;
                CellList[ladderBottom-1].MyLadder = newLadder;
            }

            Panel selectedBoardPanel = (Panel)MyFormGame.Controls.Find("boardPanel", false)[0];
            selectedBoardPanel.Paint += PaintLadder;
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
                CellList[mysteryBoxPosition-1].MyMysteryBox = newMysteryBox;

            }
        }

        private void CreateListCells()
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
        
        
        protected void PaintLadder(object sender, PaintEventArgs e)
        {
            //for(int i = 0; i < LadderList.Count; i++) {
                
            //    double ladderLength = LadderList[i].LadderLength;
            //    Bitmap ladder;

            //    if (ladderLength > 0 && ladderLength <= 1)
            //    {
            //        ladder = new Bitmap("../../Images/Ladder1.png");
            //    }
            //    else if (ladderLength > 1 && ladderLength <= 2)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder2.png");
            //    }
            //    else if (ladderLength > 2 && ladderLength <= 3)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder3.png");
            //    }
            //    else if (ladderLength > 3 && ladderLength <= 4)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder4.png");
            //    }
            //    else if (ladderLength > 4 && ladderLength <= 5)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder5.png");
            //    }
            //    else if (ladderLength > 5 && ladderLength <= 6)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder6.png");
            //    }
            //    else if (ladderLength > 6 && ladderLength <= 7)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder7.png");
            //    }
            //    else if (ladderLength > 7 && ladderLength <= 8)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder8.png");
            //    }
            //    else if (ladderLength > 8)
            //    {
            //         ladder = new Bitmap("../../Images/Ladder9.png");
            //    }
            //    else
            //    {
            //         throw new Exception("[ERROR] Invalid ladderLength");
            //    }

                
            //    int cellSize = MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell1", false)[0].Size.Width;
            //    int boardSize = MyFormGame.Controls.Find("boardPanel", false)[0].Size.Width;

            //    int ladderBottomX = LadderList[i].BottomX * cellSize + cellSize/2;
            //    int ladderBottomY = LadderList[i].BottomY * cellSize + cellSize/2;
                
            //    e.Graphics.RotateTransform((float)LadderList[i].LadderAngle);
            //    e.Graphics.TranslateTransform(-ladderBottomX, +(boardSize-ladderBottomY));
            //    //e.Graphics.DrawImage(ladder, ladderBottomX, boardSize-ladderBottomY, 300, 300);
            //    e.Graphics.DrawImage(ladder, -600, -600, 600, 600);
                
            //    ladder.Dispose();
            //    e.Graphics.TranslateTransform(ladderBottomX, -(boardSize-ladderBottomY));
            //    e.Graphics.RotateTransform( - (float)LadderList[i].LadderAngle);

                
            double ladderLength = 3;
            Bitmap ladder;

            ladder = new Bitmap("../../Images/Ladder4.png");
                
            int cellSize = MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell1", false)[0].Size.Width;
            int boardSize = MyFormGame.Controls.Find("boardPanel", false)[0].Size.Width;

            int ladderBottomX = 3/2 * cellSize;
            int ladderBottomY = 3/2 * cellSize;
                
            e.Graphics.TranslateTransform(+ladderBottomX, -(ladderBottomY));
            e.Graphics.RotateTransform((float)33.69);
            //e.Graphics.DrawImage(ladder, ladderBottomX, boardSize-ladderBottomY, 300, 300);
            e.Graphics.DrawImage(ladder, 0, 0);
                
            ladder.Dispose();
            e.Graphics.RotateTransform( - (float)33.69);
            e.Graphics.TranslateTransform(-ladderBottomX, +(ladderBottomY));





            Console.WriteLine("PaintLadder Triggered");
        }

    }
}
