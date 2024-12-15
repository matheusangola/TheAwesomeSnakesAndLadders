using System;
using System.Collections.Generic;
using System.Drawing;
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
        Random R;


        public Controller(string gameDificulty, List<Player> playersList, FormGame formgame) 
        {
            MyFormGame = formgame;
            GameDificulty = gameDificulty;
            PlayersList = playersList;
            CurrentPlayer = PlayersList[0];
            CreateDicePanel();
            CreatePlayerPanel();
            GameBoard = new Board(GameDificulty, PlayersList, MyFormGame);
            R = new Random();
            
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
                int labelHeight = 100;
                Label newLabel = new Label()
                {
                    Name = $"LabelPlayerName{i}",
                    Text = $"Player {player.Color}:\n{player.Name}\nPrevious Dice Rolls: {player.PrintPreviousDiceRollsList()}",
                    Font = new Font("Arial", 14),
                    Size = new Size(subPanelWidth, labelHeight),
                    //SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(10, 10),
                };
                verticalPosition += labelHeight + 10;
                playerSubPanel.Controls.Add(newLabel);

            }
        }

        public async void CreateDicePanel()
        {
            //Create Dice Panel
            Panel dicePanel = new System.Windows.Forms.Panel()
            {
                // to do (1200, 100)
                Location = new Point(1200, 100),
                // to do (300, 800)
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
                await Task.Delay(100);
                Dice newDice = new Dice(6);
                DiceList.Add(newDice);

                PictureBox pb = new PictureBox()
                {
                    Name = $"pictureBoxDice{i}",
                    Image = Image.FromFile($"../../Images/Dice/Dice1.png"),
                    Size = new Size(pictureBoxSize, pictureBoxSize),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(10, verticalPosition),
                };
                verticalPosition += pictureBoxSize + 10;
                dicePanel.Controls.Add(pb);
            }

            // Create PlayerTurn Label
            int labelHeight = 60;
            Label newLabel = new Label()
            {
                Name = "labelPlayerTurn",
                Text = $"{CurrentPlayer.Color}'s turn\n({CurrentPlayer.Name})",
                Font = new Font("Arial", 14),
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
            (sender as Button).Enabled = false;

            //Roll Dice
            for (int i = 0; i < DiceList.Count; i++)
            {
                DiceList[i].GenerateRandomNumber();
            }
            PictureBox selectedPictureBox1 = (PictureBox)MyFormGame.Controls.Find("dicePanel", false)[0].Controls.Find($"pictureBoxDice1", false)[0];
            PictureBox selectedPictureBox2 = (PictureBox)MyFormGame.Controls.Find("dicePanel", false)[0].Controls.Find($"pictureBoxDice2", false)[0];

            //Generate Shuffled dice images
            for (int j = 0; j < 15; j++)
            {
                int randomValue1 = R.Next(1, 7);
                int randomValue2 = R.Next(1, 7);
                selectedPictureBox1.Image = Image.FromFile($"../../Images/Dice/Dice{randomValue1}.png");
                selectedPictureBox2.Image = Image.FromFile($"../../Images/Dice/Dice{randomValue2}.png");
                await Task.Delay(50);
            }
            selectedPictureBox1.Image = Image.FromFile($"../../Images/Dice/Dice{DiceList[0].Value}.png");
            Console.WriteLine($"Dice 1 value: {DiceList[0].Value}");
            Console.WriteLine($"Dice 2 value: {DiceList[1].Value}");
            selectedPictureBox2.Image = Image.FromFile($"../../Images/Dice/Dice{DiceList[1].Value}.png");


            //Move Player
            await Task.Delay(100);
            await movePlayer();
            
            (sender as Button).Enabled = true;
        }

        public async Task movePlayer() 
        {
            //Sum Dice
            int remainingMovements = DiceList[0].Value + DiceList[1].Value;

            //Record DiceRoll in Player's PreviousDiceRollsList
            CurrentPlayer.PreviousDiceRollsList.Add(remainingMovements);

            //Update previous DiceRolls in Player information SubPanel
            MyFormGame.Controls.Find("PlayerPanel", false)[0].Controls.Find($"PlayerSubPanel{CurrentPlayer.Number-1}", false)[0].Controls.Find($"LabelPlayerName{CurrentPlayer.Number-1}", false)[0].Text = $"Player {CurrentPlayer.Color}:\n{CurrentPlayer.Name}\nPrevious Dice Rolls: {CurrentPlayer.PrintPreviousDiceRollsList()}";
            

            //Loop player (move + check destination)
            int currentPlayerPosition;
            int nextPlayerPosition;
            do
            {
                Console.WriteLine($"\nremainingMovements: {remainingMovements}");
                Console.WriteLine($"Player To be Moved! Player: {CurrentPlayer}");

                //Move Player Pin
                PictureBox selectedPlayerPin = (PictureBox)MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"playerPin{CurrentPlayer.Number}", false)[0];
                int cellSize = MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell1", false)[0].Size.Width;
                int boardSize = MyFormGame.Controls.Find("boardPanel", false)[0].Size.Width;

                if (CurrentPlayer.CellNumber == 0)
                {
                    nextPlayerPosition = 1;
                    CurrentPlayer.X = 0;
                    CurrentPlayer.Y = 0;
                    selectedPlayerPin.Location = new Point(CurrentPlayer.X * cellSize + CurrentPlayer.PinDisplayOffsetX, (boardSize - CurrentPlayer.Y * cellSize) - selectedPlayerPin.Size.Height);
                    selectedPlayerPin.BringToFront();
                    await Task.Delay(100);

                }
                else
                {
                    currentPlayerPosition = CurrentPlayer.CellNumber;

                    nextPlayerPosition = currentPlayerPosition + 1;

                    CurrentPlayer.X += GameBoard.CellList[CurrentPlayer.CellNumber - 1].NextCellDeltaX;
                    CurrentPlayer.Y += GameBoard.CellList[CurrentPlayer.CellNumber - 1].NextCellDeltaY;
                }
                CurrentPlayer.CellNumber = nextPlayerPosition;
                
                
                await Task.Delay(200);

                selectedPlayerPin.Location = new Point(CurrentPlayer.X*cellSize + CurrentPlayer.PinDisplayOffsetX, (boardSize-CurrentPlayer.Y*cellSize)-selectedPlayerPin.Size.Height);
                selectedPlayerPin.BringToFront();

                Console.WriteLine($"Player new stats! Player: {CurrentPlayer}");

                remainingMovements--;

                //Check if gameover
                if (CheckGameOver())
                {
                    return;
                }
            }
            while (remainingMovements > 0);

            //////WORKING HERE //////
            // Check MysteryBox
            if (GameBoard.CellList[CurrentPlayer.CellNumber-1].MyMysteryBox != null)
            {
                Console.WriteLine($"MysteryBox Triggered! Current Player: {CurrentPlayer}");
                var mysteryBoxDestination = GameBoard.CellList[CurrentPlayer.CellNumber - 1].MyMysteryBox.Destination;
                string message = $"Surprise! Move to Cell number {mysteryBoxDestination}";
                string title = "Surprise!";
                MessageBox.Show(message, title);
                await JumpToPosition(mysteryBoxDestination);
            }

            // Check Ladder
            if (GameBoard.CellList[CurrentPlayer.CellNumber-1].MyLadder != null)
            {
                Console.WriteLine($"Ladder Triggered! Current Player: {CurrentPlayer}");
                var ladderDestination = GameBoard.CellList[CurrentPlayer.CellNumber - 1].MyLadder.Top;
                string message = $"Congratulations! Take a Shortcut to Cell number {ladderDestination}";
                string title = "Your Lucky Day!";
                MessageBox.Show(message, title);
                await JumpToPosition(ladderDestination);
            }

            // Check Snake
            if (GameBoard.CellList[CurrentPlayer.CellNumber - 1].MySnake != null) {
                Console.WriteLine($"Snake Triggered! Current Player: {CurrentPlayer}");
                var snakeDestination = GameBoard.CellList[CurrentPlayer.CellNumber - 1].MySnake.Tail;
                string message = $"Oh no! Go back to Cell number {snakeDestination}";
                string title = "Unlucky!";
                MessageBox.Show(message, title);
                await JumpToPosition(snakeDestination);
            }

            //if not gameover next player's turn
            CurrentPlayer = GameBoard.PlayerList[CurrentPlayer.Number % GameBoard.PlayerList.Count];

            //Update Current Player Label (Above Button)
            Label selectedLabelPlayerTurn = (Label)MyFormGame.Controls.Find("dicePanel", false)[0].Controls.Find("labelPlayerTurn", false)[0];
            selectedLabelPlayerTurn.Text = $"{CurrentPlayer.Color}'s turn\n({CurrentPlayer.Name})";
        }

        public async Task JumpToPosition(int cellNumberDestination)
        {
            
            //Move Player Pin
            PictureBox selectedPlayerPin = (PictureBox)MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"playerPin{CurrentPlayer.Number}", false)[0];
            int cellSize = MyFormGame.Controls.Find("boardPanel", false)[0].Controls.Find($"cell1", false)[0].Size.Width;
            int boardSize = MyFormGame.Controls.Find("boardPanel", false)[0].Size.Width;

            if(cellNumberDestination <= 0)
            {
                CurrentPlayer.CellNumber = 1;
            } else if (cellNumberDestination > boardSize) {
                CurrentPlayer.CellNumber = boardSize;
            } else
            {
                CurrentPlayer.CellNumber = cellNumberDestination;
            }

            int sumX = 0;
            int sumY = 0;

            for (int i = 0; i < CurrentPlayer.CellNumber - 1; i++)
            {
                sumX += GameBoard.CellList[i].NextCellDeltaX;
                sumY += GameBoard.CellList[i].NextCellDeltaY;
            }
            CurrentPlayer.X = sumX;
            CurrentPlayer.Y = sumY;

            await Task.Delay(100);

            selectedPlayerPin.Location = new Point(CurrentPlayer.X * cellSize + CurrentPlayer.PinDisplayOffsetX, (boardSize - CurrentPlayer.Y * cellSize) - selectedPlayerPin.Size.Height);
            selectedPlayerPin.BringToFront();

        }

        public Boolean CheckGameOver()
        {
            if (CurrentPlayer.CellNumber == GameBoard.Size * GameBoard.Size)
            {
                
                if(MessageBox.Show($"{CurrentPlayer.Name} won the game!\nRestart Game?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FormSelectLevel newFormSelectLevel = new FormSelectLevel();
                    newFormSelectLevel.Show();
                } 
                MyFormGame.Close();
                return true;
            }
            return false;
        }
    }
}
