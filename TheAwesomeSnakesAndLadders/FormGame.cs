﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TheAwesomeSnakesAndLadders.GameLogic;


namespace TheAwesomeSnakesAndLadders
{
    public partial class FormGame : Form
    {
        private Board gameBoard;
        private Player player;
        private Controller myController;
        public FormGame(int playerQuantity, string gameDificulty, List<Player> playerList)
        {
            InitializeComponent();
            myController = new Controller(gameDificulty, playerList, this);
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }
    }
}
