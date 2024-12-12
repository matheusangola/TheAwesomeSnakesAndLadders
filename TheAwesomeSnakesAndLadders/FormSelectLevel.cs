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
    public partial class FormSelectLevel : Form
    {
        public FormSelectLevel()
        {
            InitializeComponent();
        }

        

        private void nextBtn_Click(object sender, EventArgs e)
        {
            int playerQuantity = Int32.Parse(numberOfPlayers.SelectedItem.ToString());
            string gameDificulty = gameLevel.SelectedItem.ToString();
            var nextForm = new FormSetPlayer(playerQuantity, gameDificulty);
            nextForm.Show();
            this.Hide();
        }

        private void FormSelectLevel_Load(object sender, EventArgs e)
        {

        }
    }
}
