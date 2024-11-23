using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheAwesomeSnakesAndLadders
{
    public partial class FormSetPlayer : Form
    {
        public FormSetPlayer(int playerQuantity, string gameDificulty)
        {

            InitializeComponent();
            for (int i = 0; i < playerQuantity; i++)
            {
                var playerLabel = new System.Windows.Forms.Label()
                {
                    Text = $"Player {i + 1} name: ",
                    Location = new Point(180, 120 + 30 * i),
                    Size = new System.Drawing.Size(99, 99),
                    Name = $"label{i + 1}"
                };
                this.Controls.Add(playerLabel);
                var playerTextbox = new System.Windows.Forms.TextBox()
                {
                    Text = $"Player {i+1} name",
                    Location = new Point(240, 120+30*i),
                    Size = new System.Drawing.Size(99, 99),
                    Name = $"textBox{i+1}"
                };
                this.Controls.Add(playerTextbox);
            }
            

        }


    }
}
