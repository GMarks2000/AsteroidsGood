using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public class Star
    {
        public int x, y, size;

        //constructor method for stars
        public Star (int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        //moves star and pops it onto other side of screen if necessary
        public void Move(UserControl u)
        {
            y--;
            if (y + size <= 0) { y = u.Height; }
        }
    }
}
