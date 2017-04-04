using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
    public class Asteroid
    {
        public int x, y, size, xSpeed, ySpeed;

        //constructor method for asteroids
        public Asteroid(int _x, int _y, int _size, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            size = _size;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        //moves asteroids
        public void Move()
        {
            x += xSpeed;
            y += ySpeed;
        }

        //checks if asteroid is off the screen
        public bool checkOffscreen(UserControl u, int astSize)
        {
            if (x < -astSize - 20 || y < -astSize - 20 || x > u.Width + 20|| y > u.Height + 20) { return true; }
            else { return false; }
        }

        //checks for a collision with a bullet
        public bool checkCollision(Bullet b)
        {   
            //declares rectangles for asteroid and bullet and checks for their intersection
            Rectangle aRect = new Rectangle(x, y, size, size);
            Rectangle bRect = new Rectangle(b.x, b.y, b.size, b.size);
            return aRect.IntersectsWith(bRect);
        }

        //checks for a collision with player
        public bool checkCollision(int pX, int pY, int width, int height, string dir)
        {
            //switches the width and height if appropriate (based on player orientation)
            int w, h;
            if (dir == "up" || dir == "down")
            {
                w = width;
                h = height;
            }
            else
            {
                w = height;
                h = width;
            }
            
            //declares rectangles for the player and asteroids and checks for an intersection
            Rectangle aRect = new Rectangle(x, y, size, size);
            Rectangle bRect = new Rectangle(pX, pY, w, h);
            return aRect.IntersectsWith(bRect);
        }
    }
}
