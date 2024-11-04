using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheAwesomeSnakesAndLadders.GameLogic;

namespace TheAwesomeSnakesAndLadders
{
    public partial class Form1 : Form
    {
        private Board gameBoard;
        private Player player;
        public Form1()
        {
            InitializeComponent();
            boardSizeComboBox.SelectedIndex = 0; 
        }

        private int GetBoardSizeFromComboBox()
        {
            string selectedSize = boardSizeComboBox.SelectedItem.ToString();
            return int.Parse(selectedSize.Split('x')[0]);
        }

        private void CreateBoardGrid(int size)
        {
            boardPanel.Controls.Clear();
            int cellSize = boardPanel.Width / size;
            int totalCells = size * size;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int cellNumber;

                    if (row % 2 == 0)
                    {
                        // For even rows, fill left-to-right
                        cellNumber = totalCells - (row * size + col);
                    }
                    //only if we need to use it in the future
                    else
                    {
                        // For odd rows, fill right-to-left
                        cellNumber = totalCells - (row * size + (size - col - 1));
                    }

                    // Create a new label with the number for each cell
                    Label cell = new Label
                    {
                        Text = cellNumber.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    // Set the location of the label within the panel
                    cell.Location = new Point(col * cellSize, row * cellSize);

                    // Add the cell to the boardPanel
                    boardPanel.Controls.Add(cell);
                }
            }
        }



        private void startGameButton_Click(object sender, EventArgs e)
        {
            int boardSize = GetBoardSizeFromComboBox();

            CreateBoardGrid(boardSize);

            // Initialize the game board and player objects
            gameBoard = new Board(boardSize);
            player = new Player();
        }
    }
}
