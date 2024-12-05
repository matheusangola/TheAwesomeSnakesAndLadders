using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAwesomeSnakesAndLadders.GameLogic
{
    internal class Controller
    {
        string gameDificulty;
        Board gameBoard;
        Player currentPlayer;
        List<Dice> listDice;
        List<Player> listPlayers;


        public Controller(string gameDificultyInput, List<Player> listPlayerInput) 
        {
            gameDificulty = gameDificultyInput;
            listPlayers = listPlayerInput;
            listDice = new List<Dice>();
            currentPlayer = listPlayers[0];
            gameBoard = new Board(gameDificulty);
            
        }

        public void setBoard()
        {

        }

        public void rollDice() 
        {

        }

        public void movePlayer() 
        {

        }

        public void checkGameOver()
        {

        }
    }
}
