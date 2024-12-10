using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Controller
    {
        string GameDificulty;
        Board GameBoard;
        Player CurrentPlayer;
        List<Dice> DiceList;
        List<Player> PlayersList;
        FormGame MyFormGame;


        public Controller(string gameDificulty, List<Player> playersList, FormGame formgame) 
        {
            MyFormGame = formgame;
            GameDificulty = gameDificulty;
            PlayersList = playersList;
            CurrentPlayer = PlayersList[0];
            CreateDicePanel();
            CreatePlayerPanel();
            GameBoard = new Board(GameDificulty, PlayersList, MyFormGame);
            
        }

        public void CreatePlayerPanel()
        {
            //Create Player Panel
            int verticalPosition = 10;
            int subPanelHeight = 100;
            int subPanelWidth = 280;
            Panel playerPanel = new System.Windows.Forms.Panel()
            {
                Location = new Point(10, 100),
                Size = new System.Drawing.Size(300, 800),
                Name = "PlayerPanel"
            };
            MyFormGame.Controls.Add(playerPanel);

            for (int i = 0; i < PlayersList.Count; i++)
            {
                Player player = PlayersList[i];
                Panel playerSubPanel = new System.Windows.Forms.Panel()
                {
                    Location = new Point(10, verticalPosition),
                    Size = new System.Drawing.Size(subPanelWidth, subPanelHeight),
                    Name = $"PlayerSubPanel{i}"
                };
                verticalPosition += subPanelHeight + 10;

                playerPanel.Controls.Add(playerSubPanel);

                // Create PlayerTurn Label
                int labelHeight = 40;
                Label newLabel = new Label()
                {
                    Name = $"labelPlayerName{i}",
                    Text = $"{player}",
                    Font = new Font("Arial", 24),
                    Size = new Size(subPanelWidth, labelHeight),
                    //SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(10, 10),
                };
                verticalPosition += labelHeight + 10;
                playerSubPanel.Controls.Add(newLabel);

            }
        }

        public void CreateDicePanel()
        {
            //Create Dice Panel
            Panel dicePanel = new System.Windows.Forms.Panel()
            {
                Location = new Point(1200, 100),
                Size = new System.Drawing.Size(300, 800),
                Name = "dicePanel"
            };
            MyFormGame.Controls.Add(dicePanel);

            // Create Dice Objects and Dice PictureBox
            DiceList = new List<Dice>();

            int verticalPosition = 10;
            int pictureBoxSize = 280;
            for (int i = 1; i <= 2; i++) 
            {
                Dice newDice = new Dice(6);
                DiceList.Add(newDice);

                PictureBox pb = new PictureBox()
                {
                    Name = $"pictureBoxDice{i}",
                    Image = Image.FromFile($"../../Images/Dice1.png"),
                    Size = new Size(pictureBoxSize, pictureBoxSize),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(10, verticalPosition),
                };
                verticalPosition += pictureBoxSize + 10;
                dicePanel.Controls.Add(pb);
            }

            // Create PlayerTurn Label
            int labelHeight = 40;
            Label newLabel = new Label()
            {
                Name = "labelPlayerTurn",
                Text = $"{CurrentPlayer.Name}'s turn",
                Font = new Font("Arial", 24),
                Size = new Size(pictureBoxSize, labelHeight),
                //SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, verticalPosition),
            };
            verticalPosition += labelHeight + 10;
            dicePanel.Controls.Add(newLabel);


            // Create ButtonRollDice
            int buttonHeight = 80;
            Button newButton = new Button()
            {
                Name = "buttonRollDice",
                Text = "Roll",
                Font = new Font("Arial", 24),
                Size = new Size(pictureBoxSize, buttonHeight),
                //SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(10, verticalPosition),
            };
            verticalPosition += buttonHeight + 10;
            dicePanel.Controls.Add(newButton);
            newButton.Click += new EventHandler(this.rollDice);

        }

        public async void rollDice(object sender, EventArgs e) 
        {
            for (int i = 0; i < DiceList.Count; i++)
            {
                DiceList[i].GenerateRandomNumber();
            }
            PictureBox selectedPictureBox1 = (PictureBox)MyFormGame.Controls.Find("dicePanel", false)[0].Controls.Find($"pictureBoxDice1", false)[0];
            PictureBox selectedPictureBox2 = (PictureBox)MyFormGame.Controls.Find("dicePanel", false)[0].Controls.Find($"pictureBoxDice2", false)[0];

            for (int j = 0; j < 15; j++)
            {
                Random r = new Random();
                int randomValue1 = r.Next(1, 7);
                int randomValue2 = r.Next(1, 7);
                selectedPictureBox1.Image = Image.FromFile($"../../Images/Dice{randomValue1}.png");
                selectedPictureBox2.Image = Image.FromFile($"../../Images/Dice{randomValue2}.png");
                await Task.Delay(50);
            }
            selectedPictureBox1.Image = Image.FromFile($"../../Images/Dice{DiceList[0].Value}.png");
            Console.WriteLine(DiceList[0].Value);
            Console.WriteLine(DiceList[1].Value);
            selectedPictureBox2.Image = Image.FromFile($"../../Images/Dice{DiceList[1].Value}.png");
            await Task.Delay(100);
            movePlayer();
        }

        public async void movePlayer() 
        {
            //Sum Dice
            int remainingMovements = DiceList[0].Value + DiceList[1].Value;

            //Loop player (move + check destination)
            int currentPlayerPosition;
            int nextPlayerPosition;
            do
            {
                //Move Player Pin
                PictureBox selectedPlayerPin = (PictureBox)MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"playerPin{CurrentPlayer.Number}", false)[0];
                int cellSize = MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell1", false)[0].Size.Width;
                int boardSize = MyFormGame.Controls.Find("boardPanel", false)[0].Size.Width;
                if (CurrentPlayer.CellNumber == 0)
                {
                    nextPlayerPosition = 1;
                    CurrentPlayer.X = 0;
                    CurrentPlayer.Y = 0;
                    remainingMovements--;
                    selectedPlayerPin.Location = new Point(CurrentPlayer.X * cellSize, (boardSize - CurrentPlayer.Y * cellSize) - selectedPlayerPin.Size.Height);
                    selectedPlayerPin.BringToFront();
                    await Task.Delay(500);

                }
                else
                {
                    currentPlayerPosition = CurrentPlayer.CellNumber;

                    nextPlayerPosition = currentPlayerPosition + 1;
                }
                CurrentPlayer.CellNumber = nextPlayerPosition;
                CurrentPlayer.X += GameBoard.CellList[CurrentPlayer.CellNumber - 1].NextCellDeltaX;
                CurrentPlayer.Y += GameBoard.CellList[CurrentPlayer.CellNumber - 1].NextCellDeltaY;
                
                await Task.Delay(500);

                selectedPlayerPin.Location = new Point(CurrentPlayer.X*cellSize, (boardSize-CurrentPlayer.Y*cellSize)-selectedPlayerPin.Size.Height);
                selectedPlayerPin.BringToFront();


                remainingMovements--;

                //Check if gameover
                if (CheckGameOver())
                {
                    break;
                }



            }
            while (remainingMovements > 0);
            //Check destination cell


            // Snake or Ladder movement

            //if not gameover next player's turn
            CurrentPlayer = GameBoard.PlayerList[CurrentPlayer.Number % GameBoard.PlayerList.Count];
        }

        public Boolean CheckGameOver()
        {
            if (CurrentPlayer.CellNumber == GameBoard.Size * GameBoard.Size)
            {
                
                MessageBox.Show($"{CurrentPlayer.Name} won the game!");
                //eerror n, returning null
                MyFormGame.Parent.Parent.Show();
                MyFormGame.Close();

                return true;
            } return false;
        }
    }
}
