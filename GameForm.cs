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
    public partial class GameForm : Form
    {
        private int height;
        private int width;
        Random random;

        const int PANEL_SIZE = 40;

        Panel[,] grid;

        /// <summary>
        /// Constructor.
        /// Initializes the random component, as well as an array representing the game board
        /// Fills the game board with panels
        /// Calculates the total number of mines in the game and adds them to the game board
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public GameForm(int height, int width)
        {
            InitializeComponent();
            random = new Random();
            this.height = height;
            this.width = width;

            grid = new Panel[width, height];
            int totalMines = (width * height) / 6;

            this.ClientSize = new Size(width * 40, height * 40);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Panel panel = new Panel(x, y);
                    panel.MouseDown += PanelClicked;
                    this.Controls.Add(panel);
                    grid[x, y] = panel;
                }
            }
            AssignMines(totalMines);
        }

        /// <summary>
        /// When a panel is clicked, fire this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                AnalyzePanel(sender as Panel, GetAdjacentMines(sender as Panel));
                DisablePanel(sender as Panel);
            }

            if (e.Button == MouseButtons.Right)
                DesignateBomb(sender as Panel);

            foreach (Panel panel in grid)
            {
                if (panel.Enabled == true && panel.HoldsMine == false)
                    return;
            }

            MessageBox.Show("YOU WON A COMPUTER GAME! YOU MUST BE SOOOO COOL!");
        }

        /// <summary>
        /// Returns the number of mines adjacent to the current panel
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        private int GetAdjacentMines(Panel panel)
        {
            if (panel.HoldsMine)
                return 0;

            int adjacentMines = 0;
            for (int y = panel.Y - 1; y <= panel.Y + 1; y++)
            {
                //Check if the y index is out of bounds
                if (y < 0 || y >= height)
                {
                    continue;
                }
                for (int x = panel.X - 1; x <= panel.X + 1; x++)
                {
                    //Check if the x index is out of bounds
                    if (x < 0 || x >= width)
                        continue;
                    
                    //Check if the current panel is the one being check
                    if (x == panel.X && y == panel.Y)
                        continue;
                    
                    //Check the panel at x and y
                    if (grid[x, y].HoldsMine)
                        adjacentMines++;
                }
            }

            return adjacentMines;
        }

        /// <summary>
        /// Analyze whether the click designates a win or a number
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="adjMines"></param>
        private void AnalyzePanel(Panel panel, int adjMines)
        {
            if (panel.BackColor == Color.Yellow)
                return;
            if (panel.HoldsMine)
            {
                panel.BackColor = Color.Red;
                foreach (Panel myPanel in grid)
                {
                    if (myPanel.HoldsMine)
                        myPanel.Text = "M";
                }
                GameOver();
            }
            else
            {
                panel.BackColor = Color.White;
                panel.Text = adjMines.ToString();
            }
        }

        /// <summary>
        /// Once the nature of a panel is shown, disable it
        /// </summary>
        /// <param name="panel"></param>
        private void DisablePanel(Panel panel)
        {
            if (panel.BackColor == Color.Yellow)
                return;
            panel.Enabled = false;
        }

        /// <summary>
        /// If a mine is clicked, show the Game Over message box and close the game board
        /// </summary>
        private void GameOver()
        {
            MessageBox.Show("HAHA YOU LOSE");
            this.Close();
        }

        /// <summary>
        /// Inserts mines amount of mines into the game board.
        /// Prevents overlapping
        /// </summary>
        /// <param name="mines"></param>
        private void AssignMines(int mines)
        {
            Random random = new Random();
            for (int i = 0; i < mines; i++)
            {
                int xNum = random.Next(width);
                int yNum = random.Next(height);

                if (grid[xNum, yNum].HoldsMine == false)
                {
                    grid[xNum, yNum].HoldsMine = true;
                }
                else
                {
                    i--;
                }
            }
        }

        /// <summary>
        /// On right click, toggles the color of an active panel to designate if the player thinks it is a bomb
        /// </summary>
        /// <param name="panel"></param>
        private void DesignateBomb(Panel panel)
        {
            if (panel.BackColor == Color.LightSteelBlue)
                panel.BackColor = Color.Yellow;
            else
                panel.BackColor = Color.LightSteelBlue;
        }
    }
}
