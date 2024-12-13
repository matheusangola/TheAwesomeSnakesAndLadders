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
        int PlayerQuantity;
        string GameDificulty;
        List<Player> PlayerList;
        List<String> ColorList;
        private void submitButtonClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < PlayerQuantity; i++)
            {
                TextBox selectedTextbox = (TextBox)this.Controls.Find($"textbox{i + 1}", false)[0];                {
                    Player p = new Player(selectedTextbox.Text, ColorList[i], i+1);
                    PlayerList.Add(p);
                    Console.WriteLine($"player {p}");
                }
                
            }
            var nextForm = new FormGame(PlayerQuantity, GameDificulty, PlayerList);
            nextForm.Show();
            nextForm.Size = new Size(1700, 1000);
            this.Hide();

        }

        public FormSetPlayer(int playerQuantityInput, string gameDificultyInput)
        {
            PlayerQuantity = playerQuantityInput;
            GameDificulty = gameDificultyInput;
            PlayerList = new List<Player>();
            ColorList = new List<string>() { "Blue", "Red", "Green", "Yellow" };

            InitializeComponent();
            for (int i = 0; i < PlayerQuantity; i++)
            {
                var playerLabel = new System.Windows.Forms.Label()
                {
                    Text = $"Player {i + 1} name {ColorList[i]}: ",
                    Location = new Point(180, 120 + 30 * i),
                    Size = new System.Drawing.Size(80, 15),
                    Name = $"label{i + 1}"
                };
                this.Controls.Add(playerLabel);
                var playerTextbox = new System.Windows.Forms.TextBox()
                {
                    Text = $"Player {i+1}",
                    Location = new Point(270, 115+30*i),
                    Size = new System.Drawing.Size(140, 15),
                    Name = $"textBox{i+1}"
                };
                this.Controls.Add(playerTextbox);
            }
            var submitButton = new System.Windows.Forms.Button()
            {
                Text = "Submit",
                Location = new Point(270, 115 + 30 * PlayerQuantity),
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
