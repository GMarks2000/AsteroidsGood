using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public class Bullet
    {
        public int x, y, speed, size;
        public string dir;

        public Bullet (int _x, int _y, int _size, int _speed, string _dir)
        {
            x = _x;
            y = _y;
            size = _size;
            speed = _speed;
            dir = _dir;
        }

        //move method. Moves bullet dependant on direction
        public void Move()
        {
            switch (dir)
            {
                case "up":
                    y -= speed;
                    break;
                case "down":
                    y += speed;
                    break;
                case "left":
                    x -= speed;
                    break;
                case "right":
                    x += speed;
                    break;
                default:
                    break;
            }
        }

        public bool checkOffscreen(UserControl u)
        {
            if (x < -20 || y < -20 || x > u.Width + 20 || y > u.Height + 20) { return true; }
            else { return false; }
        }        
    }
}
