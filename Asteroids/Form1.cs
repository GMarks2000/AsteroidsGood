using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Made by Gareth Marks
/// Due 3 April 2017
/// A simple asteroids-style game
/// </summary>

namespace Asteroids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //opens game screen on startup
            GameScreen gs = new GameScreen();
            this.Controls.Add(gs);
        }

        public static int score;
    }
}
