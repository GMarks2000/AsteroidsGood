using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class Explosion
    {
        public int x, y, size;

        public Explosion(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        //expands explosion and checks to see whether it has become large enough to remove
        public bool expandAndCheckSize(int threshold)
        {
            size += 2;
            x--;
            y--;
            if (size >= threshold) { return true; }
            else { return false; }
        }
    }
}
