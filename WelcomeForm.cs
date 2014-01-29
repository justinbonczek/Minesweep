using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweep
{
    public partial class WelcomeForm : Form
    {
        GameForm gameForm;

        /// <summary>
        /// Opening form
        /// Prompts user for width and height of game
        /// </summary>
        public WelcomeForm()
        {
            InitializeComponent();

            lblHeightVal.Text = trackBar1.Value.ToString();
            lblWidthVal.Text = trackBar2.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblHeightVal.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            lblWidthVal.Text = trackBar2.Value.ToString();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            gameForm = new GameForm(trackBar1.Value, trackBar2.Value);
            gameForm.ShowDialog();

            this.Show();
        }
    }
}
