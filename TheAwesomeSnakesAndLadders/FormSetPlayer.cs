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
    public partial class FormSetPlayer : Form
    {
        int playerQuantity;
        string gameDificulty;
        List<Player> playerList;
        private void submitButtonClicked(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    Player p = new Player(c.Text);
                    playerList.Add(p);
                }
                
            }
            var nextForm = new FormGame(playerQuantity, gameDificulty, playerList);
            nextForm.Show();
            nextForm.Size = new Size(1500, 1000);
            this.Hide();

        }

        public FormSetPlayer(int playerQuantityInput, string gameDificultyInput)
        {
            playerQuantity = playerQuantityInput;
            gameDificulty = gameDificultyInput;
            playerList = new List<Player>();

            InitializeComponent();
            for (int i = 0; i < playerQuantity; i++)
            {
                var playerLabel = new System.Windows.Forms.Label()
                {
                    Text = $"Player {i + 1} name: ",
                    Location = new Point(180, 120 + 30 * i),
                    Size = new System.Drawing.Size(80, 15),
                    Name = $"label{i + 1}"
                };
                this.Controls.Add(playerLabel);
                var playerTextbox = new System.Windows.Forms.TextBox()
                {
                    Text = $"Player {i+1} name",
                    Location = new Point(270, 115+30*i),
                    Size = new System.Drawing.Size(140, 15),
                    Name = $"textBox{i+1}"
                };
                this.Controls.Add(playerTextbox);
            }
            var submitButton = new System.Windows.Forms.Button()
            {
                Text = "Submit",
                Location = new Point(270, 115 + 30 * playerQuantity),
                Size = new System.Drawing.Size(100, 20),
                Name = "submitButton",
            };
            submitButton.Click += new EventHandler(this.submitButtonClicked);
            this.Controls.Add(submitButton);



        }

        private void FormSetPlayer_Load(object sender, EventArgs e)
        {

        }
    }
}
