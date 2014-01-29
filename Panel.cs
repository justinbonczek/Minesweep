using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweep
{
    /// <summary>
    /// Class used to represent each individual panel in the Minesweeper game
    /// </summary>
    public class Panel : Button
    {
        private bool holdsMine;
        private int xPosition;
        private int yPosition;

        public bool HoldsMine
        {
            get { return holdsMine; }
            set { holdsMine = value; }
        }
        
        public int Y
        {
            get { return yPosition; }
        }

        public int X
        {
            get { return xPosition; }
        }

        public Panel(int x, int y)
            : base()
        {
            this.Location = new Point(40 * x, 40 * y);
            this.Size = new Size(40, 40);
            this.Text = "";
            this.Margin = Padding.Empty;
            this.Padding = Padding.Empty;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = Color.LightSteelBlue;
            xPosition = x;
            yPosition = y;
        }
    }
}
